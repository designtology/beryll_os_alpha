using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getWidget : MonoBehaviour {
	public GameObject WidgetButton = GameObject.FindGameObjectWithTag ("WidgetBTN");

	// Use this for initialization
	void Start () {
		WidgetButton.GetComponent<Button> ().onClick.AddListener (showWidget);

	}
	
	// Update is called once per frame
	public void showWidget() {
		GameObject.FindGameObjectWithTag ("Widget").SetActive (true);

	}
}
