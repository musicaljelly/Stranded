using UnityEngine;
using System.Collections;

using StrandedConstants;

/* This script represents the state for any non-player character */
/* Any changes to coordinates, mood, task, etc should update this script */

public class NonPlayer : MonoBehaviour {
	
	// Movement
	public float startingSpeed = 0.03f;

	// Attributes/Moods


	// Pathfinding
	public Pathfinder pathfinder;

	// Use this for initialization
	void Start () {
		Vector3 startingCoordinates = initializeCoordinates ();
		Vector3 coordinateDifference = this.transform.position - startingCoordinates;
		this.transform.Translate (coordinateDifference, Space.World);

		
		pathfinder = new Pathfinder (Task.IDLE, startingSpeed, renderer.bounds.size,
		                             this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		// Update attributes/mood

		// Graphics and Movement/Pathfinding
		switch(pathfinder.currentTask)
		{
			case (Task.IDLE):
				break;
			case (Task.RELAX_SIT):
				break;
			case (Task.RELAX_PALMFAN):
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
			case (Task.UPGRADE_SHELTER):
				break;
			case (Task.EAT_FOOD):
				break;
		}

		if (Input.GetMouseButton (0) && !RadialMenu.isActive) 
		{
			//Debug.Log("Got some mouse action");
			pathfinder.currentTask = Task.EAT_FOOD;
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pathfinder.setCurrentTaskCoordinates(mousePosition);
		}
		this.transform.Translate (pathfinder.findNextTranslation());
		pathfinder.updateCoordinates(transform.position);
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

		float xcoor = (Random.value * cameraWidth * xmirror) + cameraCoordinates.x;
		float ycoor = (Random.value * cameraHeight * ymirror) + cameraCoordinates.y;
		Vector3 coordinates = new Vector3 (xcoor, ycoor, 0);

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

	void CheckValidTasks(bool freeWill = false)
	{
		//if campfire intensity > 0
		//Add RELAX_SIT to ValidTasks list
		
		//if palms > 0
		//Add RELAX_PALMFAN to ValidTasks list
		
		//Add SCAVENGE_FOOD, SCAVENGE_WOOD, & SCAVENGE_PALMS to ValidTasks list
		
		//if campfire intensity == 0
		//Add START_FIRE to list
		
		//if campfire intensity > 0 && < 3
		//Add STOKE_FIRE to list
		
		//if food > 0
		//Add EAT_FOOD to list
		
		
		//if 
	}
}
