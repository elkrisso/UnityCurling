using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {
    [SerializeField]  public Image image;
    public MovingObject powerObject;
    private Mask mask;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.type = Image.Type.Filled;
        image.fillAmount = 50;
	}
	
	// Update is called once per frame
	void Update () {
        image.fillAmount = powerObject.speed / 100;
	}
}
