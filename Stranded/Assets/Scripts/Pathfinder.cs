using UnityEngine;
using System;
using System.Collections.Generic;
using StrandedConstants;


public class Pathfinder {

	Vector3 NO_TASK_AVAILABLE = new Vector3(999, 999, 999);

	public static List<GameObject> collisionObjects = new List<GameObject>();
	public static bool collisionObjectsInitialized = false;

	public Task currentTask;
	public float currentSpeed;
	Vector3 currentCoordinates;

	private GameObject characterObject;
	private Vector3 currentTaskCoordinates;

	private GameObject obstacle;


	public Pathfinder(Task startingTask, float startingSpeed, 
	                  GameObject gameObj, Vector3 coordinates)
	{
		currentTask = startingTask;
		currentSpeed = startingSpeed;
		characterObject = gameObj;
		currentCoordinates = coordinates;

		
		if (!collisionObjectsInitialized)
		{
			initializeCollisionObjectList();
		}
	}
	public Vector3 findNextTranslation()
	{
		Animator animator = characterObject.GetComponentInChildren<Animator>();
		if (animator != null) {
			animator.SetBool("walk", false);
		}

		if (currentTask == Task.IDLE)
		{
			return new Vector3(0,0,0);
		}
		if (currentCoordinates == NO_TASK_AVAILABLE)
		{
			currentTask = Task.IDLE;
			return new Vector3(0,0,0);
		}

		else if (isGoalReached())
		{
			currentTask = Task.IDLE;
			return new Vector3(0,0,0);
		}
		else
		{
			if (animator != null) {
				animator.SetBool("walk", true);
			}

			float delta_x = currentTaskCoordinates.x - characterObject.transform.position.x; 
			float delta_y = currentTaskCoordinates.y - characterObject.transform.position.y;

			Vector3 delta_vector = new Vector3(delta_x, delta_y, 0f);
			delta_vector.Normalize();

			// Later we can change Z if we want to walk "behind" an obstacle
			// Much like what we do for player
			Vector3 movement = new Vector3(delta_vector.x * currentSpeed,
			                               delta_vector.y * currentSpeed, 0f);

			if (willHitSomething (movement))
			{
				if (Math.Abs(movement.y) > Math.Abs(movement.x))
				{
					takeDetourX ();
					movement = new Vector3(0f, 0f, 0f);
				}
				else
				{
					takeDetourY();
					movement = new Vector3(0f, 0f, 0f);
				}
			}
			return movement;
		}
	}

	public void updateCoordinates(Vector3 coordinates)
	{
		this.currentCoordinates = coordinates;
	}

	public void updateTask(Task task)
	{
		currentTask = task;

		if ((this.currentTask != Task.IDLE)
		    && (this.currentTask != Task.RELAX_PALMFAN)
		    && (this.currentTask != Task.RELAX_SIT)
		    && (this.currentTask != Task.EAT_FOOD))
		{
			currentTaskCoordinates = findClosestCurrentTask();
		}
	}

	private Vector3 findClosestCurrentTask()
	{
		GameObject[] listOfCurrentTasks = GameObject.FindGameObjectsWithTag(mapTaskToTag(this.currentTask));
		Vector3 coordinates = NO_TASK_AVAILABLE;

		float smallestDistance = 10000f;
		foreach (GameObject taskObject in listOfCurrentTasks)
		{
			float distance = this.find2DDistance(this.currentCoordinates, taskObject.transform.position);
			if (distance < smallestDistance)
			{
				smallestDistance = distance;
				coordinates = taskObject.transform.position;
			}
		}

		return coordinates;
	}

	bool isGoalReached()
	{
		float delta_x = currentTaskCoordinates.x - currentCoordinates.x;
		float delta_y = currentTaskCoordinates.y - currentCoordinates.y;
		if ((((delta_x <= 0) && (delta_x*-1 <= characterObject.renderer.bounds.size.x))
		     || ((delta_x >= 0) && (delta_x <= characterObject.renderer.bounds.size.x)))
		    && (((delta_y <= 0) && (delta_y*-1 <= characterObject.renderer.bounds.size.y))
		    || ((delta_y >= 0) && (delta_y <= characterObject.renderer.bounds.size.y))))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void setCurrentTaskCoordinates(Vector3 coor)
	{
		currentTaskCoordinates = coor;
	}

	private bool willHitSomething(Vector3 coordinates)
	{
		// Just loop through all GameObjects that have colliders
		// and make sure we're not walking into one.
		bool willHit = false;
		foreach(GameObject collider in Pathfinder.collisionObjects)
		{
			if (collider != characterObject && collider.renderer.bounds.Intersects(characterObject.renderer.bounds))
			{
				willHit = true;
				obstacle = collider;
			}
		
		}
		return willHit;
	}

	private void initializeCollisionObjectList()
	{
		GameObject[] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach(GameObject obj in allGameObjects)
		{
			if ((hasCollider (obj)) && (obj.name != "Main Camera") && (obj.name != "beach"))
			{
				Pathfinder.collisionObjects.Add(obj);
			}
		}
		Pathfinder.collisionObjectsInitialized = true;
	}

	private bool hasCollider(GameObject obj)
	{
		if (obj.collider2D != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void takeDetourX()
	{
		float obstacle_x = obstacle.gameObject.transform.position.x;

		// We should go left
		if (currentCoordinates.x < obstacle_x)
		{
			characterObject.gameObject.transform.Translate(new Vector3(-0.5f * currentSpeed, 0f, 0f));
		}
		// We should go right
		else if (currentCoordinates.x > obstacle_x)
		{
			characterObject.gameObject.transform.Translate(new Vector3(0.5f * currentSpeed, 0f, 0f));
		}
		
	}

	private void takeDetourY()
	{
		float obstacle_y = obstacle.gameObject.transform.position.y;
		
		// We should go down
		if (currentCoordinates.y < obstacle_y)
		{
			characterObject.gameObject.transform.Translate(new Vector3(-0.5f * currentSpeed, 0f, 0f));
		}
		// We should go up
		else if (currentCoordinates.y > obstacle_y)
		{
			characterObject.gameObject.transform.Translate(new Vector3(0.5f * currentSpeed, 0f, 0f));
		}
		
	}

	private string mapTaskToTag(Task task)
	{
		switch(task)
		{
			case (Task.SCAVENGE_FOOD):
				return "food";
			case (Task.SCAVENGE_WOOD):
				return "wood";
			case (Task.SCAVENGE_PALMS):
				return "palm";
			case (Task.START_FIRE):
				return "fire";
			case (Task.STOKE_FIRE):
				return "fire";
			case (Task.UPGRADE_SHELTER):
				return "shelter";
		}
		return "";
	}

	private float find2DDistance(Vector3 pointA, Vector3 pointB)
	{
		float A_x = pointA.x;
		float B_x = pointB.x;
		float A_y = pointA.y;
		float B_y = pointB.y;

		return (float) Math.Sqrt(Math.Pow((A_x-B_x), 2) + Math.Pow((A_y-B_y), 2));
	}
}
