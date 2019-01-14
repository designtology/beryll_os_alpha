using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour {

	public int UnLoadScene;

	void OnTriggerEnter (Collider other)
	{			


		if (other.gameObject.tag == "SceneCollider") {
			StartCoroutine( UnLoad ());
		} 
	}

	public void UnloadFromStore(){
		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	

		if (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [31]).isLoaded) {
			SceneManager.UnloadScene (31);
			Debug.Log ("Unload From Store12");

			StartCoroutine (ClearScene ());
		}
	}

	public void RestoreStoreUI(){
		GameObject ShowHide = GameObject.FindGameObjectWithTag ("ShowHideDetails");
		//ShowHide.GetComponent<ShowHideDetails> ().SwitchDetails (,"VideoLib");
	}

	public void closeOptions(){
		SceneManager.UnloadScene (4);
	}

	public void UnloadByID(int ID){
		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	

		if (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [ID]).isLoaded) {
			SceneManager.UnloadScene (ID);
			StartCoroutine (ClearScene ());
		}
	}

	IEnumerator UnLoad(){
		
		yield return new WaitForSeconds (1.0f);

		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	
		Debug.Log("Unloading..");

		if (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [UnLoadScene]).isLoaded) {
			Debug.Log ("Comp added");

			SceneManager.UnloadScene (UnLoadScene);	
			StartCoroutine (ClearScene ());
			//new block was here

			GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");
			GameObject Block = GameObject.FindGameObjectWithTag ("Block");


	
			Block.GetComponent<SphereSectionExample> ().enabled = true;
			Block.GetComponent<SphereSectionExample> ().CS_X = MainRoom.GetComponent<move_course> ().SectioningCoordinates.x;
			Block.GetComponent<SphereSectionExample> ().CS_Y = MainRoom.GetComponent<move_course> ().SectioningCoordinates.y;
			Block.GetComponent<SphereSectionExample> ().CS_Z = MainRoom.GetComponent<move_course> ().SectioningCoordinates.z;

			//Debug.Log(MainRoom.GetComponent<move_course> ().SectioningCoordinates.x);
			//Debug.Log(MainRoom.GetComponent<move_course> ().SectioningCoordinates.y);

		} else {
			Debug.Log("Could not unload..");
		}
	}

	IEnumerator ClearScene(){
		yield return new WaitForSeconds (1.0f);
		Resources.UnloadUnusedAssets ();
	}
}
