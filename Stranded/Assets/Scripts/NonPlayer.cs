using UnityEngine;
using System.Collections;

/* This script represents the state for any non-player character */
/* Any changes to coordinates, mood, task, etc should update this script */

/* Let's implement the survival basics before we
   get into weapon-making and animal hunting */
public enum Task
{
	IDLE,
	RELAX,
	SCAVENGE_FOOD,
	SCAVENGE_WOOD,
	SCAVENGE_PALMS,
	START_FIRE,
	STOKE_FIRE,
	COOK_FOOD,
	EAT_FOOD
}

public class NonPlayer : MonoBehaviour {
	
	// Movement
	Vector2 motion = new Vector2(0, 0);
	float speed = 3f;

	// Attributes/Moods


	// Pathfinding
	Task task;
	Vector3 taskCoordinates;

	// Use this for initialization
	void Start () {
		task = Task.IDLE;

		Vector3 startingCoordinates = initializeCoordinates ();
		Vector3 coordinateDifference = this.transform.position - startingCoordinates;
		this.transform.Translate (coordinateDifference, Space.World);
	}
	
	// Update is called once per frame
	void Update () {

		// Update attributes/mood

		// Graphics and Movement/Pathfinding
		switch(task)
		{
			case (Task.IDLE):
				break;
			case (Task.RELAX):
				break;
			case (Task.SCAVENGE_FOOD):
				break;
			case (Task.SCAVENGE_WOOD):
				break;
			case (Task.SCAVENGE_PALMS):
				break;
			case (Task.START_FIRE):
				break;
			case (Task.STOKE_FIRE):
				break;
			case (Task.COOK_FOOD):
				break;
			case (Task.EAT_FOOD):
				break;
		}
	
	}


	// Let's throw all the characters in a random place 
	// within the camera view to begin with
	Vector3 initializeCoordinates()
	{
		Camera mainCamera = GameObject.Find ("Main Camera").camera;
		Vector3 cameraCoordinates = mainCamera.gameObject.transform.position;
		float cameraHeight = mainCamera.orthographicSize;
		float cameraWidth = cameraHeight * mainCamera.aspect;

		float xrand = Random.value;
		float yrand = Random.value;
		int xmirror = 1;
		int ymirror = 1;

		if (xrand > 0.5) {
			xmirror = -1;
		}
		if (yrand > 0.5) {
			ymirror = -1;
		}

		float x_coor = (Random.value * cameraWidth * xmirror) + cameraCoordinates.x;
		float y_coor = (Random.value * cameraHeight * ymirror) + cameraCoordinates.y;
		Vector3 coordinates = new Vector3 (x_coor, y_coor, 0);

		/* Implement this later if we have time; for the time
		 * being we can deal with the bug of initializing a character
		 * over an existing object */
		// bool coordinatesGood = isInitializeCoordinateSafe (coordinates);
		// while (!coordinatesGood) 
		// {
		//	coordinates = fixCoordinates(coordinates);
		//	coordinatesGood = isInitializeCoordinatesSafe(coordinates);
		// }
		return coordinates;
	}
}





