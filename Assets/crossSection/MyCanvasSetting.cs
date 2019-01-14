using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyCanvasSetting : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
#if UNITY_ANDROID      
        mySetting();
#endif
    }
	
    void OnLevelWasLoaded(int level)
    {
#if UNITY_ANDROID      
        mySetting();
#endif
    }

    void mySetting()
    {
        CanvasScaler[] canv = FindObjectsOfType(typeof(CanvasScaler)) as CanvasScaler[];
        foreach (CanvasScaler cns in canv) cns.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

}
