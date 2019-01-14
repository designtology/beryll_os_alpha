using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class getHomeMenuData : MonoBehaviour {

	public GameObject[] btnActions;
	GameObject Globals;

	// Use this for initialization
	void Start () {
		btnActions = GameObject.FindGameObjectsWithTag ("Menu");
		int btnLength = btnActions.Length;

		for (int i = 0; i < btnLength; i++) {
			btnActions [i].GetComponent<Button> ().onClick.AddListener (delegate{ChangeGlobals(i);});
		}
	}

	public void ChangeGlobals(int i){
		Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");
		Globals.GetComponent<GlobalVariables> ().SetStoreSource (EventSystem.current.currentSelectedGameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
