using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : UnityEngine.MonoBehaviour {

	public int Scene;

	void OnTriggerEnter (Collider other)
	{
		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	
		//GameObject BlockSectioning = GameObject.FindGameObjectWithTag("Block");

		//float block_xpos =  BlockSectioning.GetComponent<SphereSectionExample> ().CS_X;
		//float block_ypos =  BlockSectioning.GetComponent<SphereSectionExample> ().CS_Y;
		//float block_zpos =  BlockSectioning.GetComponent<SphereSectionExample> ().CS_Z;

		if (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]).buildIndex < 0) { // Wenn Szene existiert
			SceneManager.LoadScene (Scene, LoadSceneMode.Additive);								// Lade die Szene Additiv
		//	Debug.Log ("Could load scene " + SceneManager.GetSceneByName (SceneNames.GetComponent<ReadSceneNames> ().scenes [Scene]).name);

			//StartCoroutine (SetActive (block_xpos,block_ypos,block_zpos));
			StartCoroutine (SetActive ());
		} else {
			Debug.Log ("Could NOT load scene " + SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]).name);

		}

		/*

			
		 
		*/


	}

	IEnumerator SetActive(){
		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	
		//Debug.Log ("waiting.....");
	
		yield return new WaitForSeconds (0.5f);
		//Debug.Log (".... end waiting");

		if (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]).isLoaded) {
			//	SceneManager.SetActiveScene (SceneManager.GetSceneAt(Scene));
			SceneManager.SetActiveScene (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]));	
			//yield return new WaitForSeconds (0.5f);
			//Lädt Szene aus vorhandenem SceneManager Index
			MoveObjects ();

		//	Debug.Log (SceneManager.GetActiveScene ().name);
		//	Debug.Log (SceneManager.GetSceneByName (SceneNames.GetComponent<ReadSceneNames> ().scenes [Scene]).name);		
		} else {
			Debug.Log ("Scene not active " + SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]).name);
				
		}
	}

	private void MoveObjects(){
		//Debug.Log ("moving");


	/*	GameObject NewBlock = GameObject.FindGameObjectWithTag("Block");
		Debug.Log ("found BLOCK");
		NewBlock.AddComponent<SphereSectionExample>().CS_X = x;	// Funktioniert aktuell nur bei Scene 22
		NewBlock.GetComponent<SphereSectionExample>().CS_Y = y;
		NewBlock.GetComponent<SphereSectionExample>().CS_Z = z;
	*/
		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	

		UnityEngine.SceneManagement.Scene SceneToMove = SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [Scene]);

		SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("SceneCollider"), SceneToMove);
	//	SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("SceneLight"), SceneToMove);
//		SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("MainCamera"), SceneToMove);
	//	SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("Player"), SceneToMove);

	}
}

