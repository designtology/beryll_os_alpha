using UnityEngine;

using UnityEngine.UI;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Transform Target;
	public float Zoom = 0.02f;

	public Vector2 TransformPosition(Vector3 position){
		Vector3 offset = position - Target.position;
		Vector2 newPosition = new Vector2 (offset.x, offset.z);

		newPosition *= Zoom;
		return newPosition;
	}
}
