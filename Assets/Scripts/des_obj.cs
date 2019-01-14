using UnityEngine;
using System.Collections;

public class des_obj : MonoBehaviour
{	
	public GameObject destroy_obj0;
	public GameObject destroy_obj1;
	public GameObject destroy_obj2;
	public GameObject destroy_obj3;

	void OnCollisionEnter (Collision col)
	{	

		if (col.gameObject.name != "Terrain") {
			if (col.gameObject.name == "barrel(Clone)") {
				Destroy (gameObject);
				Instantiate(destroy_obj0,new Vector3(transform.position.x,transform.position.y,0.0f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj1,new Vector3(transform.position.x,transform.position.y,0.1f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj2,new Vector3(transform.position.x,transform.position.y,0.2f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
				Instantiate(destroy_obj3,new Vector3(transform.position.x,transform.position.y,0.3f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
			}
		}
	}	
}