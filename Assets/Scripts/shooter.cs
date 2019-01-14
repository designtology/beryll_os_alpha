using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class shooter : MonoBehaviour {
	
	
	GameObject prefab;
	GameObject dir_line;
	public Text shots_taken1;


	public float timeBetweenShots = 0.3333F;  // Allow 3 shots per second
	public GameObject TrajectoryPointPrefeb;
	private int numOfTrajectoryPoints = 5;
	private List<GameObject> trajectoryPoints;
	private float timestamp;

	float dest_z = 0F;
	float dest_x = 150F;
	int shots_count = 0;

	void Start () {
		prefab = Resources.Load ("barrel") as GameObject;
		dir_line = Resources.Load ("dir_line") as GameObject;
		//shots_taken1 = gameObject.GetComponent<Text>();

		Instantiate (dir_line);
		dir_line.GetComponent<Renderer>().enabled = false;

		trajectoryPoints = new List<GameObject>();
		//   TrajectoryPoints are instatiated
		for(int i=0;i<numOfTrajectoryPoints;i++)
		{
			GameObject dot = (GameObject) Instantiate(TrajectoryPointPrefeb);
			dot.GetComponent<Renderer>().enabled = false;
			trajectoryPoints.Insert(i,dot);
		}

		//Startposition of Trajectory Points
		Vector3 startPos = GetComponentInParent<Transform>().position;
		Vector3 vel = new Vector3(dest_x,200,dest_z);

		setTrajectoryPoints(startPos, vel/prefab.GetComponent<Rigidbody>().mass);

		for (int i = 0; i < numOfTrajectoryPoints; i++) {
			trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0.3) {
			dest_x++;
		}
		if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") <- 0.3) {
			dest_x--;
		}
		if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") > 0.3) {
			dest_z--;
		}
		if (Input.GetAxis ("Oculus_GearVR_LThumbstickY") < -0.3) {
			dest_z++;
		}




		float h = Input.GetAxis("Oculus_GearVR_LThumbstickX");
		float v = Input.GetAxis ("Oculus_GearVR_LThumbstickY");


	

		//if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		if(h != 0 || v != 0)			
		{
			Vector3 vel1 = new Vector3(dest_x,1000,dest_z) - transform.position;
			setTrajectoryPoints(transform.position, vel1/prefab.GetComponent<Rigidbody>().mass);
		}
		else if(Input.GetButton("AButton"))
		{
			createBarrel();
			print (Input.GetAxis ("LVertical"));
		}
	}

	//---------------------------------------    
	// Following method creates new ball
	//---------------------------------------    
	private void createBarrel()
	{
		if(Time.time >= timestamp)
		{
			GameObject barrel = Instantiate(prefab) as GameObject;
			barrel.transform.position = transform.position;
			Rigidbody rb = barrel.GetComponent<Rigidbody>();
			rb.velocity = new Vector3(dest_x,100,dest_z) / 9;
			timestamp = Time.time + timeBetweenShots;
			shots_count++;
			shots_taken1.text = shots_count.ToString();
		}
	}

	//---------------------------------------    
	// Following method displays projectile trajectory path. It takes two arguments, start position of object(ball) and initial velocity of object(ball).
	//---------------------------------------    
	void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity)
	{
		float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x));
		float angle = Mathf.Rad2Deg*(Mathf.Atan2(10 , pVelocity.x)); // Winkel / Höhe des letzten TP	
		float fTime = 0;
		float ver_z = pVelocity.z / 50;
		float ver_z1 = ver_z;
		fTime += 0.01f;
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			ver_z1 += ver_z;
			float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime);
			Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy,ver_z1);
			trajectoryPoints[i].transform.position = pos;
			trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
			//trajectoryPoints[i].transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.x)*Mathf.Rad2Deg);
			fTime += 0.0185f;
			//print ("For-Versatz: " + ver_z1);
		}
		//dir_line.transform.position = new Vector3(pStartPosition.x + velocity * (fTime-0.2f) * Mathf.Cos(angle * Mathf.Deg2Rad) , 3.59f ,ver_z1);
		//dir_line.transform.position = GetComponentInParent<Transform>().position;
		//print (dir_line.transform.position);
		//dir_line.GetComponent<Renderer>().enabled = true;
	}
}