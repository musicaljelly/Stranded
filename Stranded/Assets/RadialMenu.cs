using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

	float BUTTON_RADIUS = 1.25f;

	bool isActive = false;
	bool inStageTwo = false;
	bool showingResult = false;

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
		// Middle mouse button
		if (Input.GetMouseButtonDown (2)) {
			isActive = !isActive;
			if (!isActive) {
				inStageTwo = false;
				showingResult = false;
				foreach (GameObject item in playerMenuItems) {
					item.renderer.enabled = false;
				}
				foreach (GameObject item in commandMenuItems) {
					item.renderer.enabled = false;
				}
			}
		}

		if (isActive) {
			if (showingResult) {
				// blabla
			} else if (!inStageTwo) {
				for (int i = 0; i < playerMenuItems.Length; i++) {
					GameObject item = playerMenuItems[i];

					float angleDifference = (360.0f / (float)playerMenuItems.Length) * i;
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
					if (IsMouseOverObject(item)) {
						color.a = 1;
					}
					spriteRenderer.color = color;
				}

				if (Input.GetMouseButtonUp(0)) {
					foreach (GameObject menuItem in playerMenuItems) {
						if (IsMouseOverObject(menuItem)) {
							menuItem.renderer.enabled = true;
							Vector3 newPos = Camera.main.transform.position;
							newPos.z = menuItem.transform.position.z;
							menuItem.transform.position = newPos;
							inStageTwo = true;
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

					float angleDifference = (360.0f / (float)commandMenuItems.Length) * i;
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
					if (IsMouseOverObject(item)) {
						color.a = 1;
					}
					spriteRenderer.color = color;
				}

				if (Input.GetMouseButtonUp(0)) {
					foreach (GameObject menuItem in commandMenuItems) {
						if (IsMouseOverObject(menuItem)) {
							menuItem.renderer.enabled = true;
							Vector3 newPos = Camera.main.transform.position;
							newPos.z = menuItem.transform.position.z;
							menuItem.transform.position = newPos;
							showingResult = true;
						} else {
							menuItem.renderer.enabled = false;
						}
					}
					if (!showingResult) {
						foreach (GameObject menuItem in playerMenuItems) {
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
}
