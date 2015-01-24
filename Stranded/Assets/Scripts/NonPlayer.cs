using UnityEngine;
using System.Collections;

/* This script represents the state for any non-player character */
/* Any changes to coordinates, mood, task, etc should update this script */

/* Let's implement the survival basics before we
   get into weapon-making and animal hunting */
enum Task
{
	IDLE,
	RELAX,
	SCAVENGE_FOOD,
	SCAVENGE_WOOD,
	CHOP_TREES,
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
			case (Task.CHOP_TREES):
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
	// within the camera screen to begin with
	Vector3 initializeCoordinates()
	{
		Vector3 cameraCoordinates = Camera.mainCamera.gameObject.transform.position;
		float cameraHeight = 2f * Camera.mainCamera.orthographicSize;
		float cameraWidth = cameraHeight * camera.aspect;

		float x_coor = Random.value * cameraWidth;
		float y_coor = Random.value * cameraHeight;
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





