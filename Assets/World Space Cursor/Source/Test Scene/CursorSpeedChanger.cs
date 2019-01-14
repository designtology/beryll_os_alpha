using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorSpeedChanger : MonoBehaviour {

    public WorldCursor cursor;

    public Text currentSensitivityText;

    public float sensitivityStep = 1f;

	// Use this for initialization
	void Start () 
    {
        currentSensitivityText = GetComponent<Text>();
        currentSensitivityText.text = cursor.sensitivityFactor.ToString("F1");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey("+") || Input.GetKey("="))
            cursor.sensitivityFactor += sensitivityStep;
        else if (Input.GetKey("-"))
            cursor.sensitivityFactor -= sensitivityStep;

        currentSensitivityText.text = "Current speed: " + cursor.sensitivityFactor.ToString("F1");

	}
}
