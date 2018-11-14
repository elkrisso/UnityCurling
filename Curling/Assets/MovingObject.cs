using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private bool pressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // we don't need that right now
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = 0.0f;
        float moveVertical = 3.0f;

        if (Input.GetKey("space"))
        {
            Debug.Log("space pressed!");
            if (speed >= 400)
            {
                //wait for key being released
                speed = 0;
            }
            else {
                speed = speed + 2f;
            }
        }

        if (Input.GetKeyUp("space"))
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
            speed = 0f;
        }

    }
}
