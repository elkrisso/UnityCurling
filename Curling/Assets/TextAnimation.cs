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
