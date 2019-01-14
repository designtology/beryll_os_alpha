using UnityEngine;
using System.Collections;

public class allButtons : MonoBehaviour{

	public string ID;
	public GameObject MenuButton;

	public allButtons(string id, GameObject button){
		ID = id;
		MenuButton = button;
	}

}