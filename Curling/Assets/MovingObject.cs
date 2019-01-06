using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private bool pressed;
    private bool isMoving;
    private Vector3 previousPosition;
    private bool speedDown;
    private Vector3 startPosition;
    public bool finishedShot;
    private bool locked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        previousPosition = rb.position;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX;
        speedDown = false;
        startPosition = rb.transform.position;
        finishedShot = false;
        locked = false;
    }

    void Update()
    {
        if (locked) {
            return;
        }
        float moveVertical = 2.0f;

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
        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        //if (rb.position.z == previousPosition.z && isMoving)
        if (rb.velocity.sqrMagnitude < .01 && rb.angularVelocity.sqrMagnitude < .01 && pressed && isMoving)
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
            if (!pressed) {
                rb.AddForce(movement * speed);
                speed = 0f;
                isMoving = true;
                pressed = true;
            }
        }
        if (isMoving == false && pressed) {
            resetPosition();
        }
        previousPosition = rb.position;
    }

    public void resetPosition() {
        locked = true;
        StartCoroutine(wait());
    }
    IEnumerator wait(){
        finishedShot = true;
        yield return new WaitForSeconds(5);
        this.transform.position = startPosition;
        pressed = false;
        isMoving = false;
        finishedShot = false;
        locked = false;

    }
}
