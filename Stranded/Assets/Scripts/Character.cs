using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Update the character's Z to be the bottom edge of its bounding box
		/*
		foreach (Transform child in transform) {
			Vector3 newChildPos = child.transform.position;
			newChildPos.z = collider2D.bounds.center.y - collider2D.bounds.extents.y;
			child.transform.position = newChildPos;
		}
		*/
		Vector3 newPos = transform.position;
		newPos.z = collider2D.bounds.center.y - collider2D.bounds.extents.y;
		transform.position = newPos;
	}
}
