using UnityEngine;
using System.Collections;
//using UnityEngine.EventSystems;

public class TooltipObject : MonoBehaviour {

	void OnMouseEnter () {
        //if (!EventSystem.current.IsPointerOverGameObject()) 
        //{
        ToolTipManager.SetCurrent(gameObject);
        Debug.Log(name);
        //}
	}
    void OnMouseExit()
    {
        ToolTipManager.SetCurrent(null);
    }
}
