using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float speed;
    public Rigidbody rb;
    private bool pressed;
    private bool isMoving;
    private bool speedDown;
    private Vector3 startPosition;
    public Vector3 endPosition;
    public bool finishedShot;
    public bool isAtStartPosition;
    private bool locked;
    public int currentPlayer;
    private Texture[] textures;
    public Spotlight1 spotlight1;
    public Spotlight2 spotlight2;
    private Color[] playerLightColors;
    float moveVertical = 2.5f;
    public CenterCube centerCube;
    public float startdistCenterCube, distCenterCube, distCenterCupeQuotient;
    Vector3 movement;
    bool detect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        speedDown = false;
        startPosition = rb.transform.position;
        finishedShot = false;
        isAtStartPosition = true;
        locked = false;
        currentPlayer = 0;
        movement = new Vector3(0.0f, 0.0f, moveVertical);
        detect = false;
        StartCoroutine(increaseSpeed());
        StartCoroutine(controlLeftandRight());
        StartCoroutine(addRelativeForce());
        
        // Load textures from Assets\Ressources.
        textures = new Texture[] { (Texture2D)Resources.Load("Curling_Stone_Texture1"), (Texture2D)Resources.Load("Curling_Stone_Texture2") };
        playerLightColors = new Color[] { new Color(0f, 0f, 255f), new Color(255f, 0f, 0f) };
        this.GetComponentInChildren<Renderer>().material.mainTexture = textures[currentPlayer];

        centerCube = FindObjectOfType<CenterCube>();
        startdistCenterCube = Mathf.Abs(Vector3.Distance(this.transform.position, centerCube.transform.position));
        distCenterCube = startdistCenterCube;
        distCenterCupeQuotient = distCenterCube / startdistCenterCube;

        // Set the Light for the current Player
        spotlight1 = FindObjectOfType<Spotlight1>();
        spotlight2 = FindObjectOfType<Spotlight2>();
        spotlight1.SetColor(playerLightColors[currentPlayer]);
        spotlight2.SetColor(playerLightColors[currentPlayer]);
    }

    void FixedUpdate()
    {
        distCenterCube = Mathf.Abs(Vector3.Distance(this.transform.position, centerCube.transform.position));
        distCenterCupeQuotient = distCenterCube / startdistCenterCube;
    }

    void Update() {
        if (locked)
        {
            return;
        }

        // if (rb.velocity.magnitude  < .0001f && rb.angularVelocity.magnitude < .0001 && pressed && isMoving)
        if (rb.velocity.magnitude < 0.001 && rb.angularVelocity.magnitude < 0.001f && detect)
        {
            Debug.Log("------------------------------ object stopped moving");
            isMoving = false;
            resetPosition();
        }

        // Add force so that the rigidbody "glides"
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("------------------------------------- object moving");
            if (!pressed)
            {
                rb.AddForce(movement * speed);
                speed = 0f;
                isMoving = true;
                pressed = true;
                isAtStartPosition = false;
                StartCoroutine(detectIfObjectStopped());
            }
        }

    }

    public void resetPosition()
    {
        locked = true;
        detect = false;
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        finishedShot = true;
        endPosition = this.transform.position;
        yield return new WaitForSeconds(5);
        this.transform.position = startPosition;
        pressed = false;
        isMoving = false;
        finishedShot = false;
        isAtStartPosition = true;
        locked = false;
        Debug.Log("startposition : s " + startPosition);

        // Set current player for curling stone and change texture.
        currentPlayer = currentPlayer == 0 ? 1 : 0;
        this.GetComponentInChildren<Renderer>().material.mainTexture = textures[currentPlayer];

        // Set the Light for the current player
        spotlight1.SetColor(playerLightColors[currentPlayer]);
        spotlight2.SetColor(playerLightColors[currentPlayer]);
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

    private IEnumerator addRelativeForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f); // wait half a second
            if (isMoving)
            {
                rb.AddRelativeForce(new Vector3(0f, 0.5f, 0f));
                //Debug.Log("log");
                //rb.transform.Rotate(new Vector3(0,15,0) * Time.deltaTime);
            }
        }
    }

    private IEnumerator detectIfObjectStopped() {
        yield return new WaitForSeconds(0.2f); // wait half a second
        detect = true;
    }
}
