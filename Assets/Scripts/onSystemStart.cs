using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class onSystemStart : MonoBehaviour {
	public GameObject LoadGames;

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadSystem ());


	}
	
	// Update is called once per frame
	IEnumerator LoadSystem () {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
}