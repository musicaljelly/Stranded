using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    Vector2 motion = new Vector2(0, 0);

    float speed = 3f;

	public List<GameObject> obstacles = new List<GameObject>();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            motion = new Vector2(motion.x, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motion = new Vector2(motion.x, -1);
        }
        else
        {
            motion = new Vector2(motion.x, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            motion = new Vector2(-1, motion.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            motion = new Vector2(1, motion.y);
        }
        else
        {
            motion = new Vector2(0, motion.y);
        }

        motion.Normalize();
		Vector3 oldPos = transform.position;
        transform.Translate((motion * speed) * Time.deltaTime);

		foreach (GameObject obstacle in obstacles) {
			Bounds obstacleBounds = obstacle.collider2D.bounds;
			obstacleBounds.center = new Vector3(obstacleBounds.center.x, obstacleBounds.center.y, 0);
			Bounds playerBounds = collider2D.bounds;
			playerBounds.center = new Vector3(playerBounds.center.x, playerBounds.center.y, 0);
			if (playerBounds.Intersects (obstacleBounds)) {
				transform.position = oldPos;
				break;
			}
		}
	}

    // FixedUpdate is called once per fixed framerate frame
    void FixedUpdate()
    {

    }
}
