﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Player : MonoBehaviour {

    Vector2 motion = new Vector2(0, 0);
    float speed = 3f;
	public List<GameObject> obstacles = new List<GameObject>();

	GameObject background = null;
	GameObject cam = null;


	// Use this for initialization
	void Start () {
		background = GameObject.Find ("beach");
		cam = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("TriggersR_0") < 0.5) {

			if (Input.GetKey (KeyCode.W) || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One) == new Vector2(0,1))
			{
				motion = new Vector2(motion.x, 1);
			}
			else if (Input.GetKey (KeyCode.S) || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One) == new Vector2(0, -1))
			{
				motion = new Vector2(motion.x, -1);
			}
			else
			{
				motion = new Vector2(motion.x, 0);
			}
			
			if (Input.GetKey (KeyCode.A) || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One) == new Vector2(-1, 0))
			{
				motion = new Vector2(-1, motion.y);
			}
			else if (Input.GetKey (KeyCode.D) || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One) == new Vector2(1, 0))
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
			
			// Update the camera. This needs to be here to eliminate stutter.
			Vector3 oldCamPos = cam.transform.position;
			Vector3 newPos = transform.position;
			newPos.z = cam.transform.position.z;
			cam.transform.position = newPos;
			
			Bounds backgroundBounds = background.collider2D.bounds;
			Bounds cameraBounds = cam.collider2D.bounds;
			
			if (cameraBounds.min.x < backgroundBounds.min.x || cameraBounds.max.x > backgroundBounds.max.x) {
				newPos.x = oldCamPos.x;
			}
			
			if (cameraBounds.min.y < backgroundBounds.min.y || cameraBounds.max.y > backgroundBounds.max.y) {
				newPos.y = oldCamPos.y;
			}
			
			cam.transform.position = newPos;
		}
	}

    // FixedUpdate is called once per fixed framerate frame
    void FixedUpdate()
    {

    }
}
