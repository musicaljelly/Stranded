﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;


public class CommandPair {
	public Task task;
	public GameObject character;
}


public class RadialMenu : MonoBehaviour {

	float BUTTON_RADIUS = 1.25f;

	bool isActive = false;
	bool inStageTwo = false;
	bool showingResult = false;
	bool usingGamepad = false;
	bool releasedAfterGamepadSelect = true;

	float RESULT_START_TIME = 1.0f;
	float resultTimeLeft = -1.0f;
	GameObject selectedCommandObject = null;
	GameObject selectedPlayerObject = null;

	float radius = 3f;

	GameObject[] playerMenuItems = null;
	GameObject[] commandMenuItems = null;

	// Use this for initialization
	void Start () {
		playerMenuItems = GameObject.FindGameObjectsWithTag ("PlayerMenuItem");
		commandMenuItems = GameObject.FindGameObjectsWithTag ("CommandMenuItem");
	}
	
	// Update is called once per frame
	void Update () {

		if (!releasedAfterGamepadSelect && Input.GetAxis ("TriggersR_0") < 0.5) {
			releasedAfterGamepadSelect = true;
		}

		// Middle mouse button
		if (Input.GetMouseButtonDown (2) && !(usingGamepad && isActive)) {
			isActive = !isActive;
			if (!isActive) {
				Reset();
			} else {
				usingGamepad = false;
			}
		}

		if (!showingResult && releasedAfterGamepadSelect) {
			if (Input.GetAxis ("TriggersR_0") < 0.5 && isActive && usingGamepad) {
				Reset ();
				usingGamepad = false;
			} else if (Input.GetAxis ("TriggersR_0") >= 0.5 && !(isActive && !usingGamepad)) {
				isActive = true;
				usingGamepad = true;
			}
		}


		if (isActive) {
			if (showingResult) {
				SpriteRenderer spriteRenderer = selectedCommandObject.GetComponent<SpriteRenderer>();
				Color newColor = spriteRenderer.color;
				resultTimeLeft -= Time.deltaTime;
				newColor.a = resultTimeLeft;

				selectedCommandObject.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

				if (resultTimeLeft < 0) {
					newColor.a = 1.0f;
					selectedCommandObject.renderer.enabled = false;
					Reset();
				}

				spriteRenderer.color = newColor;

			} else if (!inStageTwo) {
				float angleSize = 360.0f / (float)playerMenuItems.Length;

				for (int i = 0; i < playerMenuItems.Length; i++) {
					GameObject item = playerMenuItems[i];

					float angleDifference = angleSize * i;
					Vector3 vect = Quaternion.AngleAxis(angleDifference, Vector3.forward) * Vector3.up;
					vect.Normalize();
					vect *= radius;

					Vector3 newPos = transform.position + vect;
					newPos.z = item.transform.position.z;
					item.transform.position = newPos;
					item.renderer.enabled = true;

					SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
					Color color = spriteRenderer.color;
					color.a = 0.75f;
					if ((IsMouseOverObject(item) && !usingGamepad) || (IsControllerPointingAtObject(angleDifference, angleSize)) && usingGamepad) {
						color.a = 1;
					}
					spriteRenderer.color = color;
				}

				if ((Input.GetMouseButtonUp(0) && !usingGamepad) || (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One) && usingGamepad)) {
					foreach (GameObject menuItem in playerMenuItems) {
						SpriteRenderer spriteRenderer = menuItem.GetComponent<SpriteRenderer>();
						if (IsMouseOverObject(menuItem) || (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One) && spriteRenderer.color.a > 0.999)) {
							menuItem.renderer.enabled = true;
							Vector3 newPos = Camera.main.transform.position;
							newPos.z = menuItem.transform.position.z;
							menuItem.transform.position = newPos;
							inStageTwo = true;
							selectedPlayerObject = menuItem;
						} else {
							menuItem.renderer.enabled = false;
						}
					}
					if (!inStageTwo) {
						foreach (GameObject menuItem in playerMenuItems) {
							menuItem.renderer.enabled = true;
						}
					}
				}
			} else {
				for (int i = 0; i < commandMenuItems.Length; i++) {
					GameObject item = commandMenuItems[i];

					float angleSize = 360.0f / (float)commandMenuItems.Length;
					float angleDifference = angleSize * i;
					Vector3 vect = Quaternion.AngleAxis(angleDifference, Vector3.forward) * Vector3.up;
					vect.Normalize();
					vect *= radius;

					Vector3 newPos = transform.position + vect;
					newPos.z = item.transform.position.z;
					item.transform.position = newPos;
					item.renderer.enabled = true;

					SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
					Color color = spriteRenderer.color;
					color.a = 0.75f;
					if ((IsMouseOverObject(item) && !usingGamepad) || (IsControllerPointingAtObject(angleDifference, angleSize)) && usingGamepad) {
						color.a = 1;
					}
					spriteRenderer.color = color;

					selectedPlayerObject.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
				}

				if ((Input.GetMouseButtonUp(0) && !usingGamepad) || (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One) && usingGamepad)) {
					for (int i = 0; i < commandMenuItems.Length; i++) {
						GameObject menuItem = commandMenuItems[i];
						SpriteRenderer spriteRenderer = menuItem.GetComponent<SpriteRenderer>();
						if (IsMouseOverObject(menuItem) || (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One) && spriteRenderer.color.a > 0.999)) {
							menuItem.renderer.enabled = true;
							Vector3 newPos = Camera.main.transform.position;
							newPos.z = menuItem.transform.position.z;
							menuItem.transform.position = newPos;
							showingResult = true;
							selectedCommandObject = menuItem;
							resultTimeLeft = RESULT_START_TIME;

							foreach (GameObject obj in playerMenuItems) {
								obj.renderer.enabled = false;
							}
							foreach (GameObject obj in commandMenuItems) {
								obj.renderer.enabled = false;
							}
							selectedCommandObject.renderer.enabled = true;	

							if (usingGamepad) {
								releasedAfterGamepadSelect = false;
							}

							CommandPair command = new CommandPair();
							command.character = null;
							command.task = (Task)i;
							BroadcastMessage("IssueCommand", command);

						} else {
							menuItem.renderer.enabled = false;
						}
					}
					if (!showingResult) {
						foreach (GameObject menuItem in commandMenuItems) {
							menuItem.renderer.enabled = true;
						}
					}
				}


			}
		} else {
			foreach (GameObject item in playerMenuItems) {
				item.renderer.enabled = false;
			}
		}

	}

	bool IsMouseOverObject (GameObject obj) {
		Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return Vector2.Distance (mousePosInWorld, new Vector2 (obj.transform.position.x, obj.transform.position.y)) < BUTTON_RADIUS;
	}

	bool IsControllerPointingAtObject (float desiredAngle, float angleDiff) {

		// Negated to make them match Unity's coordinate system
		float controllerX = -Input.GetAxis ("L_XAxis_0");
		float controllerY = -Input.GetAxis ("L_YAxis_0");

		if (Mathf.Abs (controllerX) < 0.1 && Mathf.Abs (controllerY) < 0.1) {
			return false;
		}

		Vector2 direction = new Vector2(controllerX, controllerY);
		float angle = Vector2.Angle (Vector2.up, direction);
		if (controllerX < 0) {
			angle = 360 - angle;
		}
		return Mathf.Abs(Mathf.DeltaAngle (angle, desiredAngle)) < (angleDiff / 2);
	}

	void Reset () {
		isActive = false;
		inStageTwo = false;
		showingResult = false;
		foreach (GameObject item in playerMenuItems) {
			item.renderer.enabled = false;
		}
		foreach (GameObject item in commandMenuItems) {
			item.renderer.enabled = false;
		}
		selectedCommandObject = null;
		selectedPlayerObject = null;
	}

	// For debugging purposes
	void IssueCommand (CommandPair command) {
		print (command.task);
	}
}