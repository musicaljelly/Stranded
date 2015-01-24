using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Update the character's Z to be the bottom edge of its bounding box
		Vector3 newPos = transform.position;
		newPos.z = collider2D.bounds.center.y - collider2D.bounds.extents.y;
		transform.position = newPos;
	}
}
