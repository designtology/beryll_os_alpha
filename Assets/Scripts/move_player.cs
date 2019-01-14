using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;


public class move_player : MonoBehaviour {
	
	public float speed;

	public int countPoints = 0;
	public int winPoints = 4;
	public Text countPointsText;
	public countdown startStopTimer;

	public Text ScoreText;
	public Text FinalScoreText;
	int Score = 0;

	public AudioClip[] playAudio;	
	public bool started = false;

	public GameObject ScoreUI;



	Vector3 targetRotation = new Vector3();
	bool walk;

	public GameObject player;
	public bool collide = false;
	private Quaternion last_direction;
	private Quaternion actual_rotation;
	public GameObject playerCam;

	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;


	// Use this for initialization
	void Start () {
		//UnityEngine.SceneManagement.Scene targetObj = UnityEngine.SceneManagement.SceneManager.GetSceneAt(1);
		countPoints = 0;
		Score = 0;
//		controller = GetComponent<CharacterController> ();

	}

	// Update is called once per frame
	void Update () {
	//	Debug.Log (started);
		if (winPoints > countPoints && started) {
	//		countPointsText.text = countPoints.ToString ();
	//		ScoreText.text = Score.ToString();

			if (playerCam.activeSelf) {
			//	started = false;

			} else// if(transform.GetComponent<Rigidbody>().velocity != Vector3.zero){
			{
			//	Debug.Log (transform.GetComponent<Rigidbody> ().velocity);
				//if (gameObject.GetComponent<Rigidbody> ().velocity != Vector3.zero)
				//actual_rotation = Quaternion.LookRotation (new Vector3 (Input.GetAxisRaw ("Oculus_GearVR_LThumbstickX"), 0, Input.GetAxisRaw ("Oculus_GearVR_LThumbstickY")));
			}
			/*	
			if (this.gameObject.GetComponent<Rigidbody> ().velocity != Vector3.zero)
				actual_rotation = Quaternion.LookRotation (new Vector3 (Input.GetAxisRaw ("Oculus_GearVR_LThumbstickX"), 0, Input.GetAxisRaw ("Oculus_GearVR_LThumbstickY")));
			else
				actual_rotation = Quaternion.Euler (Vector3.zero);
*/
			Vector3 run = transform.forward * speed;

			/*
			if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") > 0.3 && transform.position.z <= 237) {				// HOCH
				actual_moving (run, actual_rotation, 0, -0.01f);
			} else if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") < -0.3 && transform.position.z >= -160) {		// RUNTER	
				actual_moving (run, actual_rotation, 0, 0.01f);
			}

			if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0.3 && transform.position.x <= 205) {				// RECHTS
				actual_moving (run, actual_rotation, -0.01f, 0); 
			} else if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") < -0.3 && transform.position.x >= -205) {		// LINKS
				actual_moving (run, actual_rotation, 0.01f, 0); 
			} 
*/

			if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") > 0.3 && transform.position.z <= 237 && Input.GetAxis ("Oculus_GearVR_LThumbstickX") < -0.3 && transform.position.x >= -205 ||
			   Input.GetAxis ("Oculus_GearVR_LThumbstickY") > 0.3 && transform.position.z <= 237 && Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0.3 && transform.position.x <= 205 ||
			   Input.GetAxis ("Oculus_GearVR_LThumbstickY") < -0.3 && transform.position.z >= -160 && Input.GetAxis ("Oculus_GearVR_LThumbstickX") < -0.3 && transform.position.x >= -205 ||
			   Input.GetAxis ("Oculus_GearVR_LThumbstickY") < -0.3 && transform.position.z >= -160 && Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0.3 && transform.position.x <= 205)
				speed = speed * 0.66f;
			else
				speed = speed;
		}
	}


	public void StartGame(){
		if (!started)
			started = true;
		else
			started = false;
	}

	void actual_moving(Vector3 run, Quaternion actual_rotation, float x,float z){
		if (!collide) {
			//player.GetComponent<Transform>().position += run;
		//	if(transform.GetComponent<Rigidbody>().velocity != Vector3.zero)
				transform.rotation = actual_rotation;
			//GetComponent<control_script> ().Run ();
		}/* else {
			transform.Translate (x, 0, z);
			transform.rotation = last_direction;
			GetComponent<control_script> ().OtherIdle ();			
		}
*/
	}

	void OnCollisionEnter (Collision col){

		if (col.gameObject.tag != "Terrain") {
			collide = true;
			//Debug.Log ("Player Collision ENTER: " + col.gameObject.name);

		}
	}

	void OnCollisionStay (Collision col){
		if (col.gameObject.tag != "Terrain") {

			StartCoroutine(waiting());

		}
		//else
			//collide = false;
	}

	public void playSound(int audioClipSource){
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().clip = playAudio [audioClipSource];
			GetComponent<AudioSource> ().Play ();
		}
		if (audioClipSource == 1) {
			
		//	FinalScoreText.text = "Score: " + Score.ToString ();
			StartGame ();
			ScoreUI.SetActive(true);
		}
	}

	void TimeToScore(float time){
		int temp = Convert.ToInt32(Mathf.RoundToInt(time));
		temp *= 10;
		Score += temp;
		Debug.Log (Score);
	}

	IEnumerator waiting(){
		yield return new WaitForSeconds (1);

		transform.rotation = Quaternion.LookRotation (new Vector3 (45, 0, 45));
		collide = false;
	}

	void OnCollisionExit (Collision col){
		if (col.gameObject.tag != "Terrain") {
			collide = false;

		//	Debug.Log ("Player Collision EXIT");
		}
	}

	void OnTriggerStay (Collider col){
		if ((col.gameObject.tag == "collectable" || col.gameObject.tag == "bonuspoints") && Input.GetButton("AButton")) {
			if (countPoints < winPoints) {
				Destroy (col.gameObject);

				if (col.gameObject.tag == "collectable") {
					playSound (0);
					Score += 100;
				} else {
					playSound (5);
					Score += 50;
				}
			}
				countPoints = countPoints + 1;

			if (countPoints == winPoints) {
				Debug.Log ("WON");
				startStopTimer.stop = true;
				TimeToScore (startStopTimer.timeLeft);
			}
		}
	}
}
