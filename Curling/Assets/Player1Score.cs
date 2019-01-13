using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Score : MonoBehaviour {

    public MovingObject movingObject;
    public Text text;
    public int score;
    public GameObject centerObject;
    private bool updatedScore;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        score = 0;
        updatedScore = false;
        text.text = "Player 1: " + score;
    }
	
	// Update is called once per frame
	void Update () {
        if (updatedScore || movingObject.currentPlayer == 1)
        {
            return;
        }
        if (movingObject.finishedShot)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        float distance = Mathf.Abs(Vector3.Distance(movingObject.endPosition, centerObject.transform.position));

        if (distance < 0.577)
        {
            score += 3;
        }
        else if (distance < 1.157)
        {
            score += 2;
        }
        else if (distance < 1.775)
        {
            score += 1;
        }
        text.text = "Player 1: " + score;

        updatedScore = true;
        yield return new WaitForSeconds(5);
        updatedScore = false;
    }
}
