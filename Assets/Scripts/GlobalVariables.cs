using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {
	private static GlobalVariables playerInstance;

	public string StoreSourceName = "EMPTY!";
	public string MovieName = string.Empty;

	public Vector3 HeadPosition = Vector3.zero;
	public bool CamStatus = false;
	public string panoMaterial;
	public bool Mute = false;

	public string leftWidget = string.Empty;
	public string rightWidget = string.Empty;

	public GameObject Widget_left;
	public GameObject Widget_right;

	public string[] scenes;


	public void SetStoreSource(string name){
		StoreSourceName = name;
		//Debug.Log (StoreSourceName);
	}

	void Awake(){
		DontDestroyOnLoad (this);

		if (playerInstance == null) {
			playerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}

	void Update(){
		if (Input.GetButtonDown ("AButton")) {
			MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp | MouseOperations.MouseEventFlags.LeftDown);
		}
	}

	public void setMovieName(string name){
		MovieName = name + "mp4";
	}
}
