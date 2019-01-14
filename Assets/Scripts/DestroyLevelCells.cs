using UnityEngine;
using System.Collections;

public class DestroyLevelCells : MonoBehaviour {

	public int actualX = 0;
	public int actualY = 0;

	InstantiateLevelCells instantiateLevelCells;

	void Start(){

	}

	void OnTriggerEnter (Collider direction){
		switch (direction.tag) {
		case "U_N":
			DestroyCells(0,1);
			break;

		case "U_NO":
			DestroyCells(-1,1);
			break;

		case "U_O":
			DestroyCells(-1,0);
			break;

		case "U_SO":
			DestroyCells(-1,-1);
			break;

		case "U_S":
			DestroyCells(0,-1);
			break;

		case "U_SW":
			DestroyCells(+1,-1);
			break;

		case "U_W":
			DestroyCells(1,0);
			break;

		case "U_NW":
			DestroyCells(1,1);
			break;
		}
	}

	void DestroyCells(int X,int Y){
		instantiateLevelCells = gameObject.GetComponent<InstantiateLevelCells> ();

		if (instantiateLevelCells.activatedCells [instantiateLevelCells.actualX + X, instantiateLevelCells.actualY + Y]) {
			GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");
			GameObject Block = GameObject.FindGameObjectWithTag ("Block");

			MainRoom.GetComponent<move_course> ().newSectioningCoordinates = true;

			Destroy (instantiateLevelCells.objectCells [instantiateLevelCells.actualX + X, instantiateLevelCells.actualY + Y]);
			instantiateLevelCells.activatedCells [instantiateLevelCells.actualX + X, instantiateLevelCells.actualY + Y] = false;
		}
	}
}
