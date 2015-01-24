using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Vector2 motion = new Vector2(0, 0);

    float speed = 3f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            motion = new Vector2(motion.x, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motion = new Vector2(motion.x, -1);
        }
        else
        {
            motion = new Vector2(motion.x, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            motion = new Vector2(-1, motion.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            motion = new Vector2(1, motion.y);
        }
        else
        {
            motion = new Vector2(0, motion.y);
        }

        motion.Normalize();
        transform.Translate((motion * speed) * Time.deltaTime);
	}

    // FixedUpdate is called once per fixed framerate frame
    void FixedUpdate()
    {

    }
}
