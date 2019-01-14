using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class adjustGlobals : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject[] SwitchRadials;	


	// Use this for initialization
	void Start () {
		GameObject Globals = GameObject.FindGameObjectWithTag("GlobalVariables");

		MainCamera.GetComponent<Transform> ().position = Globals.GetComponent<GlobalVariables> ().HeadPosition;

		string l_Widget = Globals.GetComponent<GlobalVariables> ().leftWidget;
		string r_Widget = Globals.GetComponent<GlobalVariables> ().rightWidget;
		Debug.Log (l_Widget);
		Debug.Log (r_Widget);

		if (l_Widget != string.Empty) {
			GameObject l_WidgetGO = Instantiate(Resources.Load (l_Widget) as GameObject, new Vector3(-120,10,110), Quaternion.Euler (0,-30,0)) as GameObject;
			Debug.Log (l_Widget);

			if (SceneManager.GetActiveScene().buildIndex == 2){
					GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");
					l_WidgetGO.transform.parent = MainRoom.transform;
				}
		}

		if (r_Widget != string.Empty) {
			GameObject r_WidgetGO = Instantiate (Resources.Load (r_Widget) as GameObject, new Vector3 (120, 10, 110), Quaternion.Euler (0, 30, 0)) as GameObject;

			if (SceneManager.GetActiveScene().buildIndex == 2){
				GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");
				r_WidgetGO.transform.parent = MainRoom.transform;
			}
		}

		if (SceneManager.GetSceneByName (Globals.GetComponent<GlobalVariables> ().scenes [7]).isLoaded) {
			switch (Globals.GetComponent<GlobalVariables> ().StoreSourceName) {
			case "GamesLib":
			case "GameFavorites":
			case "GamesStore":
				SwitchRadials [0].SetActive (true);
				break;
			case "MediaStore":
			
			case "Places":
				SwitchRadials [1].SetActive (true);
				break;

			case "VideoLib":
				SwitchRadials [2].SetActive (true);
				break;

			case "WidgetsLib":
				SwitchRadials [3].SetActive (true);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
