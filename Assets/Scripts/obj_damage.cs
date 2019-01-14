using UnityEngine;
using System.Collections;

public class obj_damage : MonoBehaviour {
	
	public GameObject destroy_obj0;
	public GameObject destroy_obj1;
	public GameObject destroy_obj2;
	public GameObject destroy_obj3;

	public int health = 30;
	int damage_barrel = 11;
	int damage_terrain = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Terrain") {
			if (health >= 0)
			{
				health -= damage_terrain;
				print ("Terrain -10");
			}
			else{
				print ("terrain");
				Destroy (gameObject);
				Instantiate(destroy_obj0,new Vector3(transform.position.x,transform.position.y,0.0f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj1,new Vector3(transform.position.x,transform.position.y,0.1f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj2,new Vector3(transform.position.x,transform.position.y,0.2f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj3,new Vector3(transform.position.x,transform.position.y,0.3f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
			}
		}

		if (col.gameObject.name == "barrel(Clone)") {
			if (health >= 0)
			{
				health -= damage_barrel;
				print ("Barrel -10");
			}
			else
			{
				print ("barrel");
				Destroy (gameObject);
				Instantiate(destroy_obj0,new Vector3(transform.position.x,transform.position.y,0.0f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj1,new Vector3(transform.position.x,transform.position.y,0.1f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj2,new Vector3(transform.position.x,transform.position.y,0.2f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj3,new Vector3(transform.position.x,transform.position.y,0.3f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
			}
		}
	}

}
