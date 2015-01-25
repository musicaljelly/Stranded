using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	GameObject player = null;

	// Use this for initialization
	void Start () {

		// This lets the player know that this obstacle exists for collision purposes
		player = GameObject.Find("Rose");
		Player playerComponent = player.GetComponent<Player> ();
		playerComponent.obstacles.Add (this.gameObject);

		// Assuming the obstacle doesn't move, set it's Z to the bottom edge of its bounding box
		Vector3 newPos = transform.position;
		newPos.z = collider2D.bounds.center.y - collider2D.bounds.extents.y;
		transform.position = newPos;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
