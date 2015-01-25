using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

	float BUTTON_RADIUS = 1.25f;

	bool isActive = false;

	float radius = 3f;
	int numButtons = 5;

	GameObject[] menuItems = null;

	// Use this for initialization
	void Start () {
		menuItems = GameObject.FindGameObjectsWithTag ("MenuItem");
	}
	
	// Update is called once per frame
	void Update () {
		// Middle mouse button
		if (Input.GetMouseButton (2)) {
			isActive = true;
		} else { 
			isActive = false;
		}

		if (isActive) {
			for (int i = 0; i < numButtons; i++) {
				float angleDifference = (360.0f / (float)numButtons) * i;
				Vector3 vect = Quaternion.AngleAxis(angleDifference, Vector3.forward) * Vector3.up;
				vect.Normalize();
				vect *= radius;

				GameObject item = menuItems[i];
				Vector3 newPos = transform.position + vect;
				newPos.z = item.transform.position.z;
				item.transform.position = newPos;
				item.renderer.enabled = true;

				SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
				Color color = spriteRenderer.color;
				color.a = 0.75f;
				Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if (item.renderer.enabled && Vector2.Distance(mousePosInWorld, new Vector2(item.transform.position.x, item.transform.position.y)) < BUTTON_RADIUS) {
					color.a = 1;
				}
				spriteRenderer.color = color;
			}
		} else {
			if (Input.GetMouseButtonUp(2)) {
				foreach (GameObject menuItem in menuItems) {
					Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					if (menuItem.renderer.enabled && Vector2.Distance(mousePosInWorld, new Vector2(menuItem.transform.position.x, menuItem.transform.position.y)) < BUTTON_RADIUS) {
						menuItem.renderer.enabled = true;
					} else {
						menuItem.renderer.enabled = false;
					}
				}
			}
		}

	}
}
