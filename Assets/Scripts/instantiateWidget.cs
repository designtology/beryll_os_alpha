using UnityEngine;
using System.Collections;

public class instantiateWidget : MonoBehaviour {

	public Transform widget;
	GameObject[] prefab;
	GameObject button;

	// Use this for initialization
	void Start(){
	//	prefab.
	//	prefab = Resources.Load ("W_Weather") as GameObject;

	}

	public void onInstantiate () {
		
	//	Instantiate (prefab);

		// gewähltes widget als prefab suchen (gültigkeit prüfen) und links/rechts anhängen
		//in GlobalVars beide widgets und richtung (zwei parameter?!) definieren
		// In jede Szene nach Widgets abfragen und entsprechend Prefabs laden
	}

	public void deleteObject(){
	
	//	Destroy (prefab);

	}

	public void getObject(){
	//	button = this;
	//	button.GetComponent<Button> ().onClick.AddListener (valueUpdateFOV);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
