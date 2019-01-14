using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public int LoadLevel;

	// Use this for initialization
	void Start () {

		GameObject MainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		//GameObject Widget = GameObject.FindGameObjectWithTag ("Widgets");
		GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");

		MainCam.transform.parent = MainRoom.transform;
		//Widget.transform.parent = MainRoom.transform;


		SceneManager.LoadSceneAsync (LoadLevel,LoadSceneMode.Additive);

		StartCoroutine(CreateComponents());
	}	
		
	// Update is called once per frame
	IEnumerator CreateComponents () {

	//	float fadeTime = GameObject.FindGameObjectWithTag ("LoadScene").GetComponent<Fade> ().BeginFade (1);
		yield return new WaitForSeconds (1.0f);

		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");
		SceneManager.SetActiveScene (SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [LoadLevel]));

		GameObject SceneCollider;

		SceneCollider = new GameObject ("SceneCollider");
		SceneCollider.tag = "SceneCollider";

		SceneCollider.AddComponent<Rigidbody> ().useGravity = false;
		SceneCollider.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeAll;
		//Debug.Log ("COLLIDER CREATED");
		SceneCollider.AddComponent<BoxCollider> ().size = new Vector3 (55,10,55);
		SceneCollider.GetComponent<BoxCollider> ().transform.position  = new Vector3(1.4f,-58f,77);
		SceneCollider.GetComponent<BoxCollider> ().isTrigger = true;

		SceneCollider.AddComponent<InstantiateLevelCells> ();
		SceneCollider.AddComponent<DestroyLevelCells> ();


		GameObject SphereSection = GameObject.FindGameObjectWithTag ("Block");
		SphereSection.GetComponent<SphereSectionExample> ().enabled = true;




	}
}
