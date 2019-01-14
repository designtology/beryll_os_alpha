using UnityEngine;
using System.Collections;

public class move_course : MonoBehaviour {

	public float speed;
	public Vector3 SectioningCoordinates = new Vector3 ();
	public bool newSectioningCoordinates = true;
	public InstantiateLevelCells newCellObjects;

	public AnimationClip idleAni;
	public AnimationClip walkAni;
	public AnimationClip runAni;
	public AnimationClip attackAni;
	public move_player points;
	Vector3 targetRotation = new Vector3();
	bool walk;

	public GameObject SceneCollider;
	public GameObject collision;





	// Use this for initialization
	void Start () {
		SectioningCoordinates.z = -35f;
	}

	// Update is called once per frame
	void Update () {
		if (points.winPoints > points.countPoints && points.started) {
			if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") > 0.3 && transform.position.z <= 155) {
				moving_course (0, speed, 0, -0.1f, true, false);
			} else if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") < -0.3 && transform.position.z >= -255) {
				moving_course (0, -speed, 0, 0.1f, false, false);
			} 


			if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0.3 && transform.position.x <= 205) {
				moving_course (speed, 0, -0.1f, 0, true, true);
			} else if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") < -0.3 && transform.position.x >= -205) {
				moving_course (-speed, 0, 0.1f, 0, false, true);
			} 
		}
	}


	void moving_course(float x_if,float z_if, float x_else, float z_else,bool positive_first,bool x_axis){
	//	GameObject Block = GameObject.FindGameObjectWithTag ("Block");


		if (newSectioningCoordinates) {
			
			SectioningCoordinates.x = newCellObjects.objectCells [newCellObjects.actualX, newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_X;
			SectioningCoordinates.y = newCellObjects.objectCells [newCellObjects.actualX, newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_Y;
			/*
			GameObject tempBlock = GameObject.FindGameObjectWithTag("Block");
			tempBlock.GetComponent<SphereSectionExample> ().CS_X = SectioningCoordinates.x;
			tempBlock.GetComponent<SphereSectionExample> ().CS_Y = SectioningCoordinates.y;
*/
			newCellObjects.objectCells [newCellObjects.actualX, newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_X = SectioningCoordinates.x;
			newCellObjects.objectCells [newCellObjects.actualX, newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_Y = SectioningCoordinates.y;
			newCellObjects.objectCells [newCellObjects.actualX, newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_Z = SectioningCoordinates.z;

			//Block = new newCellObjects;
			newSectioningCoordinates = false;
		}


		if (!collision.GetComponent<move_player>().collide)
		{			
			transform.Translate (x_if, 0, z_if);

			if(x_axis){
				if (positive_first) {
					newCellObjects.objectCells[newCellObjects.actualX,newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_X += speed;
				} else {
					newCellObjects.objectCells[newCellObjects.actualX,newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_X -= speed;
				}
			}else{
				if (positive_first) {
					newCellObjects.objectCells[newCellObjects.actualX,newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_Y += speed;
				} else {
					newCellObjects.objectCells[newCellObjects.actualX,newCellObjects.actualY].GetComponent<SphereSectionExample> ().CS_Y -= speed;
				}
			}
		//	SceneCollider.GetComponent<Transform> ().Translate (x_if, 0, z_if);
		}
	/*	else
		{
			transform.Translate (x_else, 0, z_else);

			if(x_axis){
				if (positive_first) {
					Block.GetComponent<SphereSectionExample> ().CS_X += 0.01f;
				} else {
					Block.GetComponent<SphereSectionExample> ().CS_X -= 0.01f;
				}
				SectioningCoordinates.x = Block.GetComponent<SphereSectionExample> ().CS_X;
			}else{
				if (positive_first) {
					Block.GetComponent<SphereSectionExample> ().CS_Y += 0.01f;
				} else {
					Block.GetComponent<SphereSectionExample> ().CS_Y -= 0.01f;
				}
				SectioningCoordinates.y = Block.GetComponent<SphereSectionExample> ().CS_Y;
			}

			SceneCollider.GetComponent<Transform> ().Translate (x_else, 0, z_else);
		}*/
	}

}
