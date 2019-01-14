using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetReferences : MonoBehaviour {
	public GameObject FOV_Slider ;
	public GameObject CAMPOS_Slider ;
	public GameObject CAMHEIGHT_Slider;
	public GameObject WebCamButton;
	public GameObject WindowPosition ;
	public GameObject OptionsMenu;


	// Use this for initialization
	void Start () {
		GameObject Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");

		//GameObject WebCamTexture = GameObject.FindGameObjectWithTag ("WebCam");
		CAMPOS_Slider.GetComponent<Slider> ().value = Globals.GetComponent<GlobalVariables> ().HeadPosition.z;
		CAMHEIGHT_Slider.GetComponent<Slider> ().value = Globals.GetComponent<GlobalVariables> ().HeadPosition.y;

		FOV_Slider.GetComponent<Slider> ().onValueChanged.AddListener (valueUpdateFOV);
		CAMPOS_Slider.GetComponent<Slider> ().onValueChanged.AddListener (valueUpdatePOS);
		CAMHEIGHT_Slider.GetComponent<Slider> ().onValueChanged.AddListener (valueUpdateHEIGHT);
		WebCamButton.GetComponent<Button> ().onClick.AddListener(WebCamAttach);
		Debug.Log ("THIS POS: " + OptionsMenu.GetComponent<Transform>().position);
		OptionsMenu.GetComponent<Transform>().position = WindowPosition.GetComponent<Transform> ().position;
	}

	public void SetCamPos(int value){

		GameObject Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");
		Vector3 newPos = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> ().position;

		switch (value){
			case 1:
				newPos = new Vector3 (0, -50, -20);
			break;

			case 2:
				newPos = new Vector3 (0, -35, -45);
			break;

			case 3:
				newPos = new Vector3 (0, -10, -45);
			break;
		}

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> ().position = newPos;
		Globals.GetComponent<GlobalVariables> ().HeadPosition = newPos;
	}


	public void valueUpdateFOV (float value){
		Camera.allCameras[0].fieldOfView = FOV_Slider.GetComponent<Slider>().value;
		Camera.allCameras[1].fieldOfView = FOV_Slider.GetComponent<Slider>().value;
	}

	public void valueUpdatePOS (float value){
		GameObject Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");

		Vector3 newPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position;
		newPos.z = CAMPOS_Slider.GetComponent<Slider> ().value;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> ().position = newPos;

		Globals.GetComponent<GlobalVariables> ().HeadPosition.z = newPos.z;
	}	

	public void valueUpdateHEIGHT (float value){
		GameObject Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");

		Vector3 newHeight = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position;
		newHeight.y = CAMHEIGHT_Slider.GetComponent<Slider> ().value;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> ().position = newHeight;

		Globals.GetComponent<GlobalVariables> ().HeadPosition.y = newHeight.y;
	}

	public void WebCamAttach(){
		if(GameObject.FindGameObjectWithTag ("WebCam").GetComponent<Renderer> ().enabled)
			GameObject.FindGameObjectWithTag ("WebCam").GetComponent<Renderer> ().enabled = false;
		else
			GameObject.FindGameObjectWithTag ("WebCam").GetComponent<Renderer> ().enabled = true;
	}
}