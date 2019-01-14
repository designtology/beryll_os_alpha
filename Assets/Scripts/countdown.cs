using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countdown : MonoBehaviour {

	public float timeLeft = 300.0f;
	public bool stop = false;
	public bool stopSound = false;


	private float minutes;
	private float seconds;

	public move_player playSound;

	public Text text;

	public void startTimer(float from){
		stop = false;
		timeLeft = from;
		Update();
//		StartCoroutine(updateCoroutine());
	}


	void Update() {
		if (stop) {
			if (!stopSound) {
				playSound.playSound (1);
				stopSound = true;
			}
		return;
		}
		timeLeft -= Time.deltaTime;

		minutes = Mathf.Floor(timeLeft / 60);
		seconds = timeLeft % 60;
		if(seconds > 59) seconds = 59;
		if(minutes < 0) {
			stop = true;
			minutes = 0;
			seconds = 0;
		}
		if (timeLeft <= 15f && timeLeft >=1f)
			playSound.playSound (2);
	//	if (timeLeft <= 5f)
	//		playSound.playSound (2, 2f);
		//        fraction = (timeLeft * 100) % 100;
	}

	private IEnumerator updateCoroutine(){			
		
		while(!stop){
			text.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds(0.2f);
		}
	}
}
