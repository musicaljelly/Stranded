using UnityEngine;
using System.Collections;

public class TimeCycle : MonoBehaviour
{

    int dayLength;
    int dayCount = 0;
    int rescueDay = 5;
    int morningLength = 240;
    int nightLength = 60;
    bool dayNight = false; // false = DAY | true = NIGHT.

    float currentTime = 0;

	// Use this for initialization
	void Start () {
        dayLength = morningLength + nightLength;
	}
	
	// Update is called once per frame
	void Update () {
	    currentTime += 1 * Time.deltaTime;


        if (currentTime > morningLength)
        {
            dayNight = true;
        }
        else
        {
            dayNight = false;
        }



        if (Input.GetKey(KeyCode.Space))
            Debug.Log("Current Time: " + currentTime + "  State: " + dayNight);
	}

	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {
	
	}
}
