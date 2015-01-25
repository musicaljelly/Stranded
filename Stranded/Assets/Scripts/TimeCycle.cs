using UnityEngine;
using System.Collections;

public class TimeCycle : MonoBehaviour
{
	int morningFadeInLength = 30;
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

    float currentTime = 0;

	SpriteRenderer nightCoverRenderer = null;

	Color referenceColor = new Color(0, 0, 0, 0);

	// Use this for initialization
	void Start () {
        dayLength = morningLength + nightLength;
        //guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		
		nightCoverRenderer = GetComponent<SpriteRenderer> ();
		referenceColor = nightCoverRenderer.color;
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

		Color newColor = nightCoverRenderer.color;
		if (currentTime < morningFadeInLength) {
			if (newColor.a > 0.999) {
				newColor = referenceColor;
			}
			newColor.a = ((morningFadeInLength - currentTime) / (2 * morningFadeInLength));
		} else if (currentTime > morningLength && currentTime < morningLength + nightLength - 2) {
			newColor.a = ((currentTime - morningLength) / (nightLength * 1.5f));
		} else if (currentTime > morningLength + nightLength - 2 && currentTime < morningLength + nightLength - 1) {
			newColor.a += 0.01f;
			newColor.r -= 0.01f;
			newColor.g -= 0.01f;
			newColor.b -= 0.01f;
		} else if (currentTime > morningLength + nightLength - 1 && currentTime < morningLength + nightLength) {
			newColor.a = 1f;
			newColor.r = 0f;
			newColor.g = 0f;
			newColor.b = 0f;
		}
		nightCoverRenderer.color = newColor;

	}

	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {
	
	}

    void EndDay()
    {
        pauseTime = true;
		
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
