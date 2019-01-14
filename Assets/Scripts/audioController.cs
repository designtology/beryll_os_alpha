using UnityEngine;
using System.Collections;

public class audioController : MonoBehaviour {

	public AudioClip[] playAudio;	

	GameObject Globals;

	public void playSound(int audioClipSource){

		Globals = GameObject.FindGameObjectWithTag ("GlobalVariables");
		if (Globals.GetComponent<GlobalVariables> ().StoreSourceName == "Places") {

			Debug.Log (audioClipSource);
			if (GetComponent<AudioSource> ().isPlaying && playAudio[audioClipSource].length!=0) {
				//GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().clip = playAudio [audioClipSource];
				GetComponent<AudioSource> ().Play ();
			}
		}	
	}

}
