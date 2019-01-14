using UnityEngine;
using System.Collections;

public class attachStorePanels : MonoBehaviour {

	// Use this for initialization
	public void attachObject(){

		GameObject ShowHide = GameObject.FindGameObjectWithTag ("ShowHideDetails");
		ShowHide.GetComponent<ShowHideDetails> ().SwitchDetailsVideo ();
	}
}
