using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class returntomain : MonoBehaviour {

	public void UnloadAllScenes(){

		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	
		int allActiveScenes = SceneManager.sceneCount;



		for(int i = 0; i<allActiveScenes; i++){
			SceneManager.UnloadScene (SceneNames.GetComponent<GlobalVariables> ().scenes [i]);
			//SceneManager.UnloadScene(SceneManager.GetSceneAt(i));
		}
	}
}
