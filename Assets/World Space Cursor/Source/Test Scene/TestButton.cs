using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void OnClick () 
    {
        Debug.Log("Test button pressed");
	}
}
