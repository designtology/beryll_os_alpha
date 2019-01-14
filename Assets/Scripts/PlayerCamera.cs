using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject playerCamera;
	public GameObject playerObject;
	public move_player getStarted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("BButton")) {
			if (MainCamera.activeSelf) {
				DeactivateMain ();
			}
			else {
				ActivateMain ();

			}
		}
	}

	void DeactivateMain(){
		Debug.Log ("Deactivate Main");

		MainCamera.SetActive (false);
		//playerObject.SetActive (false);
		playerCamera.SetActive(true);
	}

	void ActivateMain(){
		Debug.Log ("Activate Main");

		MainCamera.SetActive (true);
		//playerObject.SetActive (true);
		playerCamera.SetActive (false);
	//	getStarted.started = true;
	}
}
