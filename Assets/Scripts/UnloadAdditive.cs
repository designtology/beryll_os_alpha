using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UnloadAdditive : MonoBehaviour {

	// Use this for initialization
	public void UnLoadAdditiveScene() {
		SceneManager.UnloadScene (4);
	}

}
