using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGames : MonoBehaviour {

	bool loadScene = false;
	public float fadeTime;
	public GUITexture overlay;

	void Update(){
	//	if(!loadScene)
	//	Debug.Log ("Loading ...");

	}


	void Awake(){

		//	overlay.pixelInset = new Rect (0,0,Screen.width,Screen.height);
		Debug.Log("Started Overlay PixelInset");
		//Fade to clear

		StartCoroutine (FadeToClear());
	}

	public void LoadGame(int LoadLevel){
		overlay.pixelInset = new Rect (0,0,Screen.width,Screen.height);

		StartCoroutine (FadeToBlack ());

		GameObject SceneNames = GameObject.FindGameObjectWithTag("GlobalVariables");	

		if (!SceneManager.GetSceneByName (SceneNames.GetComponent<GlobalVariables> ().scenes [LoadLevel]).isLoaded) {
			loadScene = true;
			//StartCoroutine (LoadLevelNow (LoadLevel));
			SceneManager.LoadSceneAsync (LoadLevel,LoadSceneMode.Single );

	//		StartCoroutine (CreateComponents (LoadLevel));

	//		StartCoroutine (ClearScene ());
		}

	}

	IEnumerator LoadLevelNow(int LoadLevel){
		AsyncOperation async = SceneManager.LoadSceneAsync (LoadLevel,LoadSceneMode.Single );
		/*
		GameObject SceneNames = GameObject.FindGameObjectWithTag("ReadSceneNames");	

		UnityEngine.SceneManagement.Scene SceneToMove = SceneManager.GetSceneByName (SceneNames.GetComponent<ReadSceneNames> ().scenes [LoadLevel]);
		SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag ("MainCamera") as GameObject, SceneToMove);

		SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("GameController"), SceneToMove);
	//	SceneManager.MoveGameObjectToScene (GameObject.FindGameObjectWithTag("Widgets"), SceneToMove);
*/


		while(!async.isDone)
			yield return null;
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene (1);
	}

	public void RestartGame(int Game){
		SceneManager.LoadScene (Game);
	}

	public void setPanorama(Material panoName){
		Camera left_cam = Camera.allCameras[0];
		Camera right_cam = Camera.allCameras[1];
		left_cam.GetComponent<Skybox> ().material = panoName;
		right_cam.GetComponent<Skybox> ().material = panoName;

	}

	private IEnumerator FadeToClear(){

		Debug.Log("Started Coroutine FadeToClear()");

		overlay.gameObject.SetActive (true);
		overlay.color = Color.black;

		float rate = 1.0f / fadeTime;

		float progress = 0.0f;

		while (progress < 1.0f) {

			overlay.color = Color.Lerp (Color.black,Color.clear,progress);

			progress += rate * Time.deltaTime;

			yield return null;
		}

		overlay.color = Color.clear;
		overlay.gameObject.SetActive (false);

	}

	private IEnumerator FadeToBlack(){

		Debug.Log("Started Coroutine FadeToBlack()");

		overlay.gameObject.SetActive (true);
		overlay.color = Color.clear;

		float rate = 1.0f / fadeTime;

		float progress = 0.0f;

		while (progress < 1.0f) {

			overlay.color = Color.Lerp (Color.clear,Color.black,progress);

			progress += rate * Time.deltaTime;
			Debug.Log (progress);

		}

		overlay.color = Color.black;
		yield return null;
		//	yield return new WaitForSeconds (2.0f);

	}

	// Update is called once per frame
	IEnumerator CreateComponents (int LoadLevel) {

//		float fadeTime = GameObject.FindGameObjectWithTag ("LoadScene").GetComponent<Fade> ().BeginFade (1);
		yield return new WaitForSeconds (1.0f);

		SceneManager.UnloadScene(33);
	}

	IEnumerator ClearScene(){
		yield return new WaitForSeconds (1.0f);
		AsyncOperation async = Resources.UnloadUnusedAssets ();

		while(!async.isDone)
			yield return null;
	}
}



