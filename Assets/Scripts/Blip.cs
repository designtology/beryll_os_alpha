using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blip : MonoBehaviour {

	public Transform Target;

	MiniMap map;
	RectTransform myRectTransform;

	void Start(){
		if(Target.tag == "collectable_blip") Debug.Log("BLIIIIIIIIIIII");

		map = GetComponentInParent<MiniMap> ();
		myRectTransform = GetComponent<RectTransform> ();
	}

	void LateUpdate(){
		Vector2 newPosition = map.TransformPosition (Target.position);
		myRectTransform.localPosition = newPosition;
	}
}
