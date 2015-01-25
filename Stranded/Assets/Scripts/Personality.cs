using UnityEngine;
using System.Collections;

public class Personality : MonoBehaviour {

    int hunger;
    int mood;
    int stress;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {
	
	}

    int checkMood()
    {
        mood = -hunger;
        return mood;
    }
}
