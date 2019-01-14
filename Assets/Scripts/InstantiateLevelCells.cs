using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstantiateLevelCells : MonoBehaviour {

	public int sizeX = 5;
	public int sizeY = 5;

	public int startX = 2;
	public int startY = 2;

	public int actualX = 2;
	public int actualY = 2;

	public float squareDimensions = 96.0f;
	public Vector3 startPos = new Vector3(1.1078f,-55.1f,67.2f);
	public Vector3 actualPos = Vector3.zero;

	public string compass = "";

	public GameObject[,] objectCells = new GameObject[5,5];
	public GameObject[,] triggerCells = new GameObject[5,5];
	public GameObject[,] centerTrigger = new GameObject[5,5];

	public bool[,] activatedCells = new bool[5, 5];

	void Start(){
		actualPos = startPos;

		for(int i = 0;i<sizeX;i++){
			for (int j = 0; j < sizeY; j++) {
				activatedCells [i, j] = false;
			}

		}



		activatedCells [startX, startY] = true;
		objectCells [startX, startY] = Instantiate (Resources.Load ("Mummy/Levels/22") as GameObject);
		triggerCells[startX,startY] = Instantiate (Resources.Load ("Mummy/Trigger"),objectCells[startX,startY].transform.position,transform.rotation) as GameObject;
		triggerCells [startX, startY].transform.parent = objectCells [startX, startY].transform;
		triggerCells [startX, startY].transform.localScale = new Vector3 (1, 1, 1);


		GameObject MainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject MainRoom = GameObject.FindGameObjectWithTag ("MainRoom");

		MainCam.transform.parent = MainRoom.transform;

		StartCoroutine(CreateComponents());

	}


	void OnTriggerEnter (Collider direction)
	{
		if(direction.tag != "Trigger_center"){
			compass = direction.tag;
			}



		switch (direction.tag) {
		case "Trigger_center":
			if (direction.GetComponent<TriggerCellArray> ().y < actualY) {
				if (direction.GetComponent<TriggerCellArray> ().x == actualX) {
					setNewTrigger (actualX, actualY - 1);
					actualPos = new Vector3 (actualPos.x,actualPos.y,actualPos.z+squareDimensions);
					actualY--;
				} else if (direction.GetComponent<TriggerCellArray> ().x < actualX) {
					setNewTrigger (actualX - 1, actualY - 1);
					actualX--;
					actualY--;
					actualPos = new Vector3 (actualPos.x-squareDimensions,actualPos.y,actualPos.z+squareDimensions);
				} else if (direction.GetComponent<TriggerCellArray> ().x > actualX) {
					setNewTrigger (actualX + 1, actualY - 1);
					actualPos = new Vector3 (actualPos.x+squareDimensions,actualPos.y,actualPos.z+squareDimensions);
					actualY--;
					actualX++;
				}
			} else if (direction.GetComponent<TriggerCellArray> ().y > actualY) {
				if (direction.GetComponent<TriggerCellArray> ().x == actualX) {
					setNewTrigger (actualX, actualY + 1);
					actualPos = new Vector3 (actualPos.x,actualPos.y,actualPos.z-squareDimensions);
					actualY++;
				} else if (direction.GetComponent<TriggerCellArray> ().x < actualX) {
					setNewTrigger (actualX - 1, actualY + 1);
					actualPos = new Vector3 (actualPos.x-squareDimensions,actualPos.y,actualPos.z-squareDimensions);
					actualY++;
					actualX--;
				} else if (direction.GetComponent<TriggerCellArray> ().x > actualX) {
					setNewTrigger (actualX + 1, actualY + 1);
					actualPos = new Vector3 (actualPos.x+squareDimensions,actualPos.y,actualPos.z-squareDimensions);
					actualX++;
					actualY++;
				}
			
			} else if (direction.GetComponent<TriggerCellArray> ().y == actualY){
				if (direction.GetComponent<TriggerCellArray> ().x < actualX) {
					setNewTrigger (actualX - 1, actualY);
					actualPos = new Vector3 (actualPos.x - squareDimensions, actualPos.y, actualPos.z);
					actualX--;
				} else if (direction.GetComponent<TriggerCellArray> ().x > actualX) {
					setNewTrigger (actualX + 1, actualY);
					actualPos = new Vector3 (actualPos.x + squareDimensions, actualPos.y, actualPos.z);
					actualX++;
				}
			}

			Destroy (centerTrigger [actualX, actualY]);

		break;

		case "N":
			editArrays (actualX, actualY - 1, new Vector3(actualPos.x,actualPos.y,actualPos.z+squareDimensions));
			break;

		case "NO":
			editArrays (actualX+1, actualY - 1, new Vector3(actualPos.x+squareDimensions,actualPos.y,actualPos.z+squareDimensions));
			break;
			

		case "O":
			editArrays (actualX+1, actualY, new Vector3(actualPos.x+squareDimensions,actualPos.y,actualPos.z));
			break;
		

		case "SO":
			editArrays (actualX + 1, actualY + 1, new Vector3 (actualPos.x + squareDimensions, actualPos.y, actualPos.z - squareDimensions));
			break;
			

		case "S":
			editArrays (actualX, actualY + 1, new Vector3(actualPos.x,actualPos.y,actualPos.z-squareDimensions));
			break;
			

		case "SW":
			editArrays (actualX - 1, actualY + 1, new Vector3(actualPos.x-squareDimensions,actualPos.y,actualPos.z-squareDimensions));
			break;
			

		case "W":
			editArrays (actualX-1, actualY, new Vector3(actualPos.x-squareDimensions,actualPos.y,actualPos.z));
			break;
			

		case "NW":
			editArrays (actualX-1, actualY - 1, new Vector3(actualPos.x-squareDimensions,actualPos.y,actualPos.z+squareDimensions));
			break;
			
		}
	}


	void CreateTriggers(){
	
	}
		
	void editArrays(int X,int Y,Vector3 direction){
		if (activatedCells [X, Y] == false) {
			activatedCells [X, Y] = true;

		//	StartCoroutine(CreateComponents());


			//GameObject SphereSection = GameObject.FindGameObjectWithTag ("Block");

			try{
				objectCells [X, Y] = Instantiate (Resources.Load ("Mummy/Levels/" + (X).ToString () + (Y).ToString ()) as GameObject);
				addTriggerCenter (X,Y);
			}
			catch{
				string tempCompass = compass;
				//Vector3 tempRotation = Vector3.zero;

				switch (compass) {
				case "NW":
					if (actualX > 1)
						tempCompass = "N";
					if (actualY > 1)
						tempCompass = "W";
					break;

				case "NO":
					if (actualX < sizeX - 2)
						tempCompass = "N";
					if (actualY > 1)
						tempCompass = "O";
					break;

				case "SO":
					if (actualX < sizeX - 2)
						tempCompass = "S";
					if (actualY < sizeY - 2)
						tempCompass = "O";
					break;

				case "SW":
					if (actualX > 1)
						tempCompass = "S";
					if (actualY < sizeY -2)
						tempCompass = "W";
					break;


				}
					
				objectCells [X, Y] = Instantiate (Resources.Load("Mummy/Levels/" + tempCompass + "_eom"),direction,transform.rotation) as GameObject;
			//	Debug.Log("EOM: " + compass + " ACTUAL POS: " + actualX + " / " + actualY + " - DIR: " + direction);
			}

		//	if(objectCells[X,Y] != null) 
				

		}
	}

	void setNewTrigger(int X,int Y){
		Destroy (triggerCells [actualX, actualY]);
		objectCells[X,Y].GetComponent<SphereSectionExample> ().enabled = false;
		objectCells [X, Y].GetComponent<SphereSectionExample> ().CS_X = objectCells [actualX, actualY].GetComponent<SphereSectionExample> ().CS_X;
		objectCells [X, Y].GetComponent<SphereSectionExample> ().CS_Y = objectCells [actualX, actualY].GetComponent<SphereSectionExample> ().CS_Y;
		objectCells [X, Y].GetComponent<SphereSectionExample> ().CS_Z = objectCells [actualX, actualY].GetComponent<SphereSectionExample> ().CS_Z;
			
	//	if(X<actualX){
	//		if(Y<actualY){
	//	actualPos = new Vector3(actualPos.x,actualPos.y,actualPos.z+squareDimensions);
	//	Debug.Log("Actual Pos: " + actualPos.z + "SqDim: " +squareDimensions);
	//				}
	//			}
	//			else{}
		addTriggerCenter (actualX,actualY);

		try{
			triggerCells[X,Y] = Instantiate (Resources.Load ("Mummy/Trigger"),objectCells[X,Y].transform.position,transform.rotation) as GameObject;
			objectCells[X,Y].GetComponent<SphereSectionExample> ().enabled = true;

		}
		catch{
		}

		if (triggerCells [X, Y] != null) {
			triggerCells [X, Y].transform.parent = objectCells [X, Y].transform;
			triggerCells [X, Y].transform.localScale = new Vector3 (1, 1, 1);

			triggerCells [X, Y].AddComponent<TriggerCellArray> ();
			triggerCells [X, Y].GetComponent<TriggerCellArray> ().x = X;
			triggerCells [X, Y].GetComponent<TriggerCellArray> ().y = Y;
		}
	//	actualY -= 1;
	}

	void addTriggerCenter(int X,int Y){
		
		centerTrigger[X,Y] = new GameObject ("TriggerCenter");
		centerTrigger[X,Y].transform.parent = objectCells [X, Y].transform;
		centerTrigger[X,Y].transform.position = objectCells [X, Y].transform.position;
//		TriggerCenter.transform.localScale = objectCells [X, Y].transform.localScale;

		centerTrigger[X,Y].tag = "Trigger_center";
		centerTrigger[X,Y].AddComponent<BoxCollider> ();
		centerTrigger[X,Y].GetComponent<BoxCollider> ().size = new Vector3 (30, 10, 30);

		centerTrigger[X,Y].GetComponent<BoxCollider> ().isTrigger = true;
		centerTrigger[X,Y].AddComponent<TriggerCellArray> ();
		centerTrigger[X,Y].GetComponent<TriggerCellArray> ().x = X;
		centerTrigger[X,Y].GetComponent<TriggerCellArray> ().y = Y;

		Vector3 tempPos = centerTrigger[X,Y].GetComponent<BoxCollider> ().transform.position;
		tempPos.x = 0;
		tempPos.y = 0;
		tempPos.z = 0;

		Vector3 tempScale = centerTrigger[X,Y].GetComponent<BoxCollider> ().transform.localScale;
		tempScale.x = 0;
		tempScale.y = 0;
		tempScale.z = 0;
	}
		
	IEnumerator CreateComponents () {
		yield return new WaitForSeconds (1.0f);

		GameObject SphereSection = GameObject.FindGameObjectWithTag ("Block");
		SphereSection.GetComponent<SphereSectionExample> ().enabled = true;
	}

}
