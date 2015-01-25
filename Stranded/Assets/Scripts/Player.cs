using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Player : MonoBehaviour {

    Vector2 motion = new Vector2(0, 0);
    float speed = 3f;
	public List<GameObject> obstacles = new List<GameObject>();

	GameObject background = null;
	GameObject cam = null;
	Animator animator = null;


	// Use this for initialization
	void Start () {
		background = GameObject.Find ("beach");
		cam = GameObject.Find ("Main Camera");
		animator = this.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		animator.SetBool ("walk", true);

		if (Input.GetAxis ("TriggersR_0") < 0.5) {

			Vector2 gamepadMotion = GamePad.GetAxis (GamePad.Axis.LeftStick, GamePad.Index.One);
			Vector2 motion = new Vector2();

			if (Mathf.Abs(gamepadMotion.x) < 0.01 && Mathf.Abs (gamepadMotion.y) < 0.01) {

				if (Input.GetKey (KeyCode.W))
				{
					motion = new Vector2(motion.x, 1);
				}
				else if (Input.GetKey (KeyCode.S))
				{
					motion = new Vector2(motion.x, -1);
				}
				else
				{
					motion = new Vector2(motion.x, 0);
				}
				
				if (Input.GetKey (KeyCode.A))
				{
					motion = new Vector2(-1, motion.y);
				}
				else if (Input.GetKey (KeyCode.D))
				{
					motion = new Vector2(1, motion.y);
				}
				else
				{
					motion = new Vector2(0, motion.y);
				}

				if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
					animator.SetBool ("walk", false);
				}

			} else {
				motion = gamepadMotion;
			}
			
			motion.Normalize();
			Vector3 oldPos = transform.position;
			transform.Translate((motion * speed) * Time.deltaTime);

			foreach (GameObject obstacle in obstacles) {
				Bounds obstacleBounds = obstacle.collider2D.bounds;
				obstacleBounds.center = new Vector3(obstacleBounds.center.x, obstacleBounds.center.y, 0);
				Bounds playerBounds = collider2D.bounds;
				playerBounds.center = new Vector3(playerBounds.center.x, playerBounds.center.y, 0);

				Bounds backgroundBounds = background.collider2D.bounds;
				if (transform.position.x < backgroundBounds.min.x || transform.position.x > backgroundBounds.max.x || 
				    transform.position.y < backgroundBounds.min.y || transform.position.y > backgroundBounds.max.y) {
					transform.position = oldPos;
					break;
				}

				if (playerBounds.Intersects (obstacleBounds)) {
					Vector3 adjustedPos = transform.position;
					adjustedPos.y = oldPos.y;
					transform.position = adjustedPos;

					obstacleBounds = obstacle.collider2D.bounds;
					obstacleBounds.center = new Vector3(obstacleBounds.center.x, obstacleBounds.center.y, 0);
					playerBounds = collider2D.bounds;
					playerBounds.center = new Vector3(playerBounds.center.x, playerBounds.center.y, 0);

					if (playerBounds.Intersects (obstacleBounds)) {
						adjustedPos.x = oldPos.x;
					}
					transform.position = adjustedPos;
					break;
				}
			}
			
			// Update the camera. This needs to be here to eliminate stutter.
			Vector3 oldCamPos = cam.transform.position;
			Vector3 newPos = transform.position;
			newPos.z = cam.transform.position.z;
			cam.transform.position = newPos;
			
			Bounds bgBounds = background.collider2D.bounds;
			Bounds cameraBounds = cam.collider2D.bounds;
			
			if (cameraBounds.min.x < bgBounds.min.x || cameraBounds.max.x > bgBounds.max.x) {
				newPos.x = oldCamPos.x;
			}
			
			if (cameraBounds.min.y < bgBounds.min.y || cameraBounds.max.y > bgBounds.max.y) {
				newPos.y = oldCamPos.y;
			}
			
			cam.transform.position = newPos;

		} else {
			animator.SetBool ("walk", false);
		}
	}

    // FixedUpdate is called once per fixed framerate frame
    void FixedUpdate()
    {

    }
}
