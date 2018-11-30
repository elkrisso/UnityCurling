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
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX;
    }

    void Update()
    {
        float moveVertical = 2.0f;

        if (Input.GetKey("space"))
        {
            if (speed >= 100)
            {
                //wait for key being released
                speed = 0;
            }
            else {
                speed = speed + 2f;
            }
        }
        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        if (rb.position.z < previousPosition.z+0.005 && isMoving)
        {
            Debug.Log("object stopped moving");
            isMoving = false;
        }
        else
        {
            // Add force so that the rigidbody "glides"
            if (isMoving)
            {
               rb.AddRelativeForce(new Vector3(0f,-0.5f,0f));
               //Debug.Log("log");
               //rb.transform.Rotate(new Vector3(0,15,0) * Time.deltaTime);
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
