using UnityEngine;
using System.Collections;

public class NoMaterial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.color = Color.gray;

	}
	
	// Update is called once per frame
	void Update () {
	}
}
