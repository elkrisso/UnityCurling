using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight1 : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.005f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
    }
}
