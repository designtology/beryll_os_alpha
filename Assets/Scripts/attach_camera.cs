using UnityEngine;
using System.Collections;

public class attach_camera : MonoBehaviour {
	// Use this for initialization
	public void Start () {
		Camera left_cam = Camera.allCameras[1];
		//Debug.Log (Camera.allCameras[1]);
		GetComponent<Canvas> ().worldCamera = left_cam;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
