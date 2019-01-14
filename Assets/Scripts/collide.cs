using UnityEngine;
using System.Collections;

public class collide : MonoBehaviour
{	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Terrain") {
		} else {

			if (col.gameObject) {
				Destroy (col.gameObject);
			}
		}
	}	
}