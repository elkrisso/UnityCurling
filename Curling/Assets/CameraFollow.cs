using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public MovingObject movingObject;
    public Transform target;
    public float smoothSpeed = 0.005f;
    public Vector3 offset;
    private bool started = true;

    void Start()
    {
     
    }

    void FixedUpdate() {

        if (movingObject.isAtStartPosition)
        {
            transform.position = new Vector3(0, 1.6f, -28);
            transform.rotation = Quaternion.Euler(6, 0, 0);
        } else { 
            if (started)
            {
                transform.position = new Vector3(1.21f, 1.83f, -27.81f);
                transform.rotation = Quaternion.Euler(24.408f, -40.033f, -19.144f);
                started = false;
            }
            Vector3 velocity = new Vector3(0, 0, 0);
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
        }
    }
}
