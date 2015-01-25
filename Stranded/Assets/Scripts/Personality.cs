using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using StrandedConstants;

public class Personality : MonoBehaviour {

    float idleTime = 10f; // time in seconds
    float idleCounter = 0f;
    int hunger = 0;
    int stress = 0;
    int mood;

    int random;

    NonPlayer nonPlayer;

    Task currentTask;
    Task randomTask;

    List<Task> ValidTasks = new List<Task>();

	// Use this for initialization
	void Start () {
        nonPlayer = this.gameObject.GetComponent<NonPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        currentTask = nonPlayer.GetCurrentTask();
        if (currentTask == Task.IDLE && idleCounter < idleTime)
        {
            idleCounter += 1 * Time.deltaTime;
        }
        else if (idleCounter >= idleTime)
        {
            idleCounter = 0;
            DoRandomTask();
        }
            
	}

    int checkMood()
    {
        mood = -hunger + -stress;
        return mood;
    }

    void BuildValidTasks()
    {
        ValidTasks.Clear();

        if (Camp.campfireLv == 0)
        {
            ValidTasks.Add(Task.START_FIRE);
        }

        if (Camp.campfireLv > 0 && Camp.campfireLv < 3)
        {
            ValidTasks.Add(Task.STOKE_FIRE);
        }

        if (Camp.campfireLv > 0)
        {
            ValidTasks.Add(Task.RELAX_SIT);
        }

        if (Camp.palmStock > 0)
        {
            ValidTasks.Add(Task.RELAX_PALMFAN);
        }

        if (Camp.foodStock > 0)
        {
            ValidTasks.Add(Task.EAT_FOOD);
        }

        ValidTasks.Add(Task.SCAVENGE_FOOD);
        ValidTasks.Add(Task.SCAVENGE_WOOD);
        ValidTasks.Add(Task.SCAVENGE_PALMS);
    }

    Task PickValidTask()
    {
        random = Random.Range(0, ValidTasks.Count);
        return ValidTasks[random];
    }

    void DoRandomTask()
    {
        BuildValidTasks();
        randomTask = PickValidTask();
        nonPlayer.pathfinder.updateTask(randomTask);
    }
}
