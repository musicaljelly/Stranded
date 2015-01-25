using UnityEngine;
using System;
using StrandedConstants;


public class Pathfinder {

	// Need to add current goal size later if we
	// don't want the player walking into the goal

	public Task currentTask;
	public float currentSpeed;
	Vector3 currentCoordinates;

	private Vector3 characterImageSize;
	private Vector3 currentTaskCoordinates;

	public Pathfinder(Task startingTask, float startingSpeed, 
	                  Vector3 characterSize, Vector3 coordinates)
	{
		currentTask = startingTask;
		currentSpeed = startingSpeed;
		characterImageSize = characterSize;
		currentCoordinates = coordinates;
	}

	public Vector3 findNextTranslation()
	{
		if (currentTask == Task.IDLE)
		{
			return new Vector3(0,0,0);
		}
		else if (isGoalReached())
		{
			currentTask = Task.IDLE;
			return new Vector3(0,0,0);
		}
		else
		{
			float delta_x = currentTaskCoordinates.x - currentCoordinates.x;
			float delta_y = currentTaskCoordinates.y - currentCoordinates.y;

			Vector3 delta_vector = new Vector3(delta_x, delta_y, 0f);
			delta_vector.Normalize();

			// Later we can change Z if we want to walk "behind" an obstacle
			// Much like what we do for player
			Vector3 movement = new Vector3(delta_vector.x * currentSpeed,
			                               delta_vector.y * currentSpeed, 0);

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
		findClosestCurrentTaskCoordinates ();
	}

	void findClosestCurrentTaskCoordinates()
	{

	}

	bool isGoalReached()
	{
		float delta_x = currentCoordinates.x - currentTaskCoordinates.x;
		float delta_y = currentCoordinates.y - currentTaskCoordinates.y;
		if ((((delta_x <= 0) && (delta_x*-1 <= this.characterImageSize.x))
			|| ((delta_x >= 0) && (delta_x <= this.characterImageSize.x)))
			&& (((delta_y <= 0) && (delta_y*-1 <= this.characterImageSize.y))
			    || ((delta_y >= 0) && (delta_y <= this.characterImageSize.y))))
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
		Debug.Log ("setCurrentTaskCoordinates called, set to " + coor.x + ", " + coor.y);
		currentTaskCoordinates = coor;
	}

}
