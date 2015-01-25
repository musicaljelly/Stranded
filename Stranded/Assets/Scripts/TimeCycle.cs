﻿using UnityEngine;
using System.Collections;

public class TimeCycle : MonoBehaviour
{
    int morningLength = 240;
    int nightLength = 60;
    int dayLength;

    int dayCount = 0;
    int rescueDay = 5;

    //float endDayCounter = 0;
    //int endDayGoal = 12;

    // EndDay vars
    bool dayNight = false; // false = DAY | true = NIGHT.
    bool pauseTime = false; // used to pause time passage for day transitioning and game pausing
    float fadeSpeed = 1.5f;

    float currentTime = 298;

	// Use this for initialization
	void Start () {
        dayLength = morningLength + nightLength;

        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
        if (pauseTime == false)
        {
            currentTime += 1 * Time.deltaTime;


            if (currentTime > morningLength)
            {
                dayNight = true;
            }
            else
            {
                dayNight = false;
            }

            if (currentTime >= dayLength)
            {
                currentTime = 0;
                EndDay();
            }
        }

	}

	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {
	
	}

    void EndDay()
    {
        pauseTime = true;

        FadeToBlack();
        NewDay();
        FadeToClear();

        pauseTime = false;
    }
    
    void FadeToBlack ()
    {
    }

    void FadeToClear ()
    {
    }

    void NewDay ()
    {
    }
}