using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {


	/* Collision detection not working atm */
	void onCollisionEnter2D (Collision2D collision)
	{
		Debug.Log("On trigger enter");
		if (collision.gameObject.tag == "NPC")
		{
			DestroyObject(this.gameObject);
		}
	}
}
