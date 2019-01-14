using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadAdditive : MonoBehaviour {

	// Use this for initialization
	public void ToggleAdditiveScene(int SceneName){

		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	

		if (!SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [SceneName]).isLoaded )
			SceneManager.LoadSceneAsync (SceneName,LoadSceneMode.Additive);
			else
				SceneManager.UnloadScene(SceneName);


	}
}
