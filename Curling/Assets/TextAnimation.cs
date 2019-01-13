using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {
    public MovingObject movingObject;
    private bool animate;
    public  Text text;
    public GameObject centerObject; 

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        animate = false;
        text.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (movingObject.finishedShot) {
            float distance = Mathf.Abs(Vector3.Distance(movingObject.endPosition, centerObject.transform.position));
            
            if (distance < 0.577)
            {
                text.text = "Very good shot!";
            } else if (distance < 1.157)
            {
                text.text = "Good shot!";
            } else if (distance < 1.775)
            {
                text.text = "Not bad!";
            } else
            {
                text.text = "Try again!";
            }
            text.GetComponent<CanvasRenderer>().SetAlpha(1f);
            if (!animate) {
                StartCoroutine(Wait());
            }
        }
	}

    IEnumerator Wait()
    {
        animate = true;
        yield return new WaitForSeconds(5);
        text.GetComponent<CanvasRenderer>().SetAlpha(0f);
        animate = false;
    }

    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(3);
    }
}
