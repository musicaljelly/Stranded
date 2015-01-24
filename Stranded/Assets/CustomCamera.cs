using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour {

	GameObject player = null;
	GameObject background = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("player");
		background = GameObject.Find ("beach");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPos = transform.position;
		Vector3 newPos = player.transform.position;
		newPos.z = transform.position.z;
		transform.position = newPos;

		Bounds backgroundBounds = background.collider2D.bounds;
		Bounds cameraBounds = collider2D.bounds;

		if (cameraBounds.min.x < backgroundBounds.min.x || cameraBounds.max.x > backgroundBounds.max.x) {
			newPos.x = oldPos.x;
		}

		if (cameraBounds.min.y < backgroundBounds.min.y || cameraBounds.max.y > backgroundBounds.max.y) {
			newPos.y = oldPos.y;
		}

		transform.position = newPos;
	}
}
