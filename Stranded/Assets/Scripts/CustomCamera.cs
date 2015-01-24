using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour {

	GameObject player = null;
	GameObject background = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("player");

	}
	
	// Update is called once per frame
	void Update () {

	}
}
