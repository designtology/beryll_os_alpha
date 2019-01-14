using UnityEngine;
using System.Collections;


public class PlayerJump : MonoBehaviour {
	public float jumpHeight = 5f;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("AButton")) {
			
			//GetComponent<Rigidbody> ().velocity = new Vector3(0,jumpHeight,0);
			//GetComponent<Animation> ().Play ("jump complete", PlayMode.StopAll);
			GetComponent<control_script> ().Strike() ;
		}
		
		
	}
}
