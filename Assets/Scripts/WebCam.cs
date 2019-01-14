using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebCam : MonoBehaviour {

	public Material cam_mat;
	public bool switchCamera = false;
	public Text CamText;

	// Use this for initialization
	void Start () {
	
		WebCamDevice[] device = WebCamTexture.devices;
		WebCamTexture WebC_Texture = new WebCamTexture();

		if(device.Length > 0)
		{
			WebC_Texture.deviceName = device[0].name;
			WebC_Texture.requestedFPS = 30;

			WebC_Texture.Play();
		}
		cam_mat.mainTexture = WebC_Texture;
	}
	
	// Update is called once per frame
	public void SwitchCam () {
		if (switchCamera == false) {
			GetComponent<Renderer>().enabled = true;
			switchCamera = true;

			CamText.text = "Cam Off";
		} else {
			GetComponent<Renderer>().enabled = false;
			switchCamera = false;

			CamText.text = "Cam On";
		}
	}
}
