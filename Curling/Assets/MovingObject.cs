﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float speed;

    public Rigidbody rb;
    private bool pressed;
    private bool isMoving;
    private Vector3 previousPosition;
    private bool speedDown;
    private Vector3 startPosition;
    public bool finishedShot;
    public bool isAtStartPosition;
    private bool locked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        previousPosition = rb.position;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        speedDown = false;
        startPosition = rb.transform.position;
        finishedShot = false;
        isAtStartPosition = true;
        locked = false;
        StartCoroutine(increaseSpeed());
        StartCoroutine(controlLeftandRight());
    }

    void Update()
    {
        if (locked)
        {
            return;
        }
        float moveVertical = 2.5f;

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        if (rb.velocity.magnitude < .001f && rb.angularVelocity.magnitude < .001 && pressed && isMoving)
        {
            Debug.Log("object stopped moving");
            isMoving = false;
        }
        // Add force so that the rigidbody "glides"
        if (Input.GetKeyUp("space"))
        {
            if (!pressed)
            {
                rb.AddForce(movement * speed);
                speed = 0f;
                isMoving = true;
                pressed = true;
                isAtStartPosition = false;
            }
        }
        if (isMoving == false && pressed)
        {
            resetPosition();
        }
        if (isMoving)
        {
            rb.AddRelativeForce(new Vector3(0f, -0.5f, 0f));
            //Debug.Log("log");
            //rb.transform.Rotate(new Vector3(0,15,0) * Time.deltaTime);
        }
        previousPosition = rb.position;
    }

    public void resetPosition()
    {
        locked = true;
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        finishedShot = true;
        yield return new WaitForSeconds(5);
        this.transform.position = startPosition;
        pressed = false;
        isMoving = false;
        finishedShot = false;
        isAtStartPosition = true;
        locked = false;
    }

    private IEnumerator increaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f); // wait half a second
            if (Input.GetKey("space"))
            {
                if (!pressed && !isMoving)
                {
                    if (speed >= 100)
                    {
                        speedDown = true;
                    }
                    if (speed <= 0)
                    {
                        speedDown = false;
                    }
                    if (speedDown)
                    {
                        speed = speed - 1.5f;
                    }
                    else
                    {
                        speed = speed + 1.5f;
                    }
                }
            }
        }
    }

    private IEnumerator controlLeftandRight()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f); // wait half a second
            if (Input.GetKey("left"))
            {
                rb.AddForce(new Vector3(-2.5f,0,0));
            }
            if (Input.GetKey("right"))
            {
                rb.AddForce(new Vector3(2.5f, 0, 0));
            }
        }
    }
}
