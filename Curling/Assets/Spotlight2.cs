using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight2 : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.005f;
    public Vector3 offset;
    public MovingObject movingObject;
    float startAngle;

    void Start()
    {
        movingObject = FindObjectOfType<MovingObject>();
        startAngle = this.transform.eulerAngles.x;
        transform.position = new Vector3(2.29f, 15.15f, -23.84f);
    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
        this.transform.rotation = Quaternion.Euler(startAngle + (25f * (1f - movingObject.distCenterCupeQuotient)), this.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }

    public void SetColor(Color color)
    {
        transform.GetComponent<Light>().color = color;
    }
}

