using UnityEngine;
using System.Collections;
using StrandedConstants;

public class Food : MonoBehaviour {

	public int framesToWaitToReEnable = 250;
	public bool isEnabled = true;

	private int frameEnableCountdown = 0;

	void Update()
	{
		if (!isEnabled)
		{
			if (frameEnableCountdown == 0)
			{
				isEnabled = true;
				frameEnableCountdown = framesToWaitToReEnable;
			}
			else
			{
				frameEnableCountdown = frameEnableCountdown - 1;
			}
		}
	}
	public void onCollisionEnter2D (Collision2D collision)
	{
		Debug.Log("Collision enter");
		if (collision.gameObject.tag == "NPC")
		{
			// Make sure this isn't an accidental collision
			Pathfinder path = this.gameObject.GetComponent<Pathfinder>();
			Task task = path.currentTask;
			if ((isEnabled) && (task == Task.SCAVENGE_FOOD))
			{
				updateFood();
				path.updateTask(Task.IDLE);
			}
		}
	}


	public void onTriggerEnter2D (Collider2D collider)
	{
		Debug.Log("trigger enter");
		if (collider.gameObject.tag == "NPC")
		{
			// Make sure this isn't an accidental collision
			Pathfinder path = this.gameObject.GetComponent<Pathfinder>();
			Task task = path.currentTask;
			if ((isEnabled) && (task == Task.SCAVENGE_FOOD))
			{
				updateFood();
				path.updateTask(Task.IDLE);
			}

		}
	}

	public void updateFood()
	{
		if (Random.value > 0.5)
		{
			Camp.AddToFoodStock((int) (Random.value * 100));
		}
		isEnabled = false;
	}
}
