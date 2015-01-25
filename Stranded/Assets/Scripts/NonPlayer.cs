<<<<<<< HEAD
using UnityEngine;
using System.Collections;
using System;

using StrandedConstants;

/* This script represents the state for any non-player character */
/* Any changes to coordinates, mood, task, etc should update this script */

public class NonPlayer : MonoBehaviour {
	
	// Movement
	public float startingSpeed = 0.5f;

	// Attributes/Moods
    Personality personality;

	// Pathfinding
	public Pathfinder pathfinder;

	// Use this for initialization
	void Start () {
		Vector3 startingCoordinates = initializeCoordinates ();
		Vector3 coordinateDifference = this.transform.position - startingCoordinates;
		this.transform.Translate (coordinateDifference, Space.World);

		
		pathfinder = new Pathfinder (Task.IDLE, startingSpeed, this.gameObject,
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

		// For debugging
		/*
		if (Input.GetMouseButton (0)) 
		{
			pathfinder.updateTask(Task.START_FIRE);
		}
		else if (Input.GetMouseButton(1))
		{
			pathfinder.updateTask(Task.UPGRADE_SHELTER);
		}
		else if (Input.GetKey (KeyCode.Space))
		{
			pathfinder.updateTask(Task.SCAVENGE_FOOD);
		}
		*/

		transform.Translate (pathfinder.findNextTranslation());
		pathfinder.updateCoordinates(transform.position);
	}
	
	// Let's throw all the characters in a random place 
	// within the camera view to begin with
	Vector3 initializeCoordinates()
	{ 
		Camera mainCamera = GameObject.Find ("Main Camera").camera;
		Vector3 cameraCoordinates = mainCamera.gameObject.transform.position;
		float cameraHeight = mainCamera.collider2D.bounds.extents.y / 2;
		float cameraWidth = mainCamera.collider2D.bounds.extents.x / 2;

		UnityEngine.Random.seed = (int)DateTime.Now.ToFileTime();
		float xrand = UnityEngine.Random.value - 0.5f;
		float yrand = UnityEngine.Random.value - 0.5f;

		float xcoor = (xrand * cameraWidth) + cameraCoordinates.x;
		float ycoor = (yrand * cameraHeight) + cameraCoordinates.y;
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

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			Destroy(collision.gameObject);
		}
	}

    public Task GetCurrentTask()
    {
        return this.pathfinder.currentTask;
    }
=======
using UnityEngine;
using System.Collections;
using System;

using StrandedConstants;

/* This script represents the state for any non-player character */
/* Any changes to coordinates, mood, task, etc should update this script */

public class NonPlayer : MonoBehaviour {
	
	// Movement
	public float startingSpeed = 0.5f;

	// Attributes/Moods


	// Pathfinding
	public Pathfinder pathfinder;

	// Use this for initialization
	void Start () {
		pathfinder = new Pathfinder (Task.IDLE, startingSpeed, this.gameObject,
		                             transform.position);
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

		// For debugging
		/*
		if (Input.GetMouseButton (0)) 
		{
			pathfinder.updateTask(Task.START_FIRE);
		}
		else if (Input.GetMouseButton(1))
		{
			pathfinder.updateTask(Task.UPGRADE_SHELTER);
		}
		else if (Input.GetKey (KeyCode.Space))
		{
			pathfinder.updateTask(Task.SCAVENGE_FOOD);
		}
		*/

		transform.Translate (pathfinder.findNextTranslation());
		pathfinder.updateCoordinates(transform.position);
	}


	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			Destroy(collision.gameObject);
		}
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
>>>>>>> c6619f19bd297978f1016e1724a10873bbf7e736
}