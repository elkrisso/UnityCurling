using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private bool pressed;
    private bool isMoving;
    private Vector3 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        previousPosition = rb.position;
    }

    void Update()
    {
        // we don't need that right now
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = 0.0f;
        float moveVertical = 2.0f;

        if (Input.GetKey("space"))
        {
            if (speed >= 100)
            {
                //wait for key being released
                speed = 0;
            }
            else {
                speed = speed + 1f;
            }
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (rb.position == previousPosition && isMoving)
        {
            Debug.Log("object stopped moving");
            isMoving = false;
        }
        else
        {
            // Add force so that the rigidbody "glides"
            if (isMoving)
            {
                rb.AddRelativeForce(movement * 0.5f);
            }
        }
        if (Input.GetKeyUp("space"))
        {
            rb.AddForce(movement * speed);
            speed = 0f;
            isMoving = true;
        }
        previousPosition = rb.position;

    }
}
