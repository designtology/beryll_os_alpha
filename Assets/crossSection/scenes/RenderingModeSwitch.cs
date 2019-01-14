using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RenderingModeSwitch : MonoBehaviour {

    public RenderingPath[] renderingOptions;
    private Dropdown RenderingMode;
    public int m = 0;

	// Use this for initialization
	void Start () {
        RenderingMode = gameObject.GetComponent<Dropdown>();
        RenderingMode.ClearOptions();
        List<string> options = new List<string>();
        foreach (RenderingPath rp in renderingOptions) options.Add(rp.ToString());
        RenderingMode.value = m;
        RenderingMode.AddOptions(options);

        RenderingMode.onValueChanged.AddListener(delegate { SetPath(RenderingMode.value); });

	}

    void OnLevelWasLoaded(int level)
    {
        Camera.main.renderingPath = renderingOptions[m];

    }
	
	// Update is called once per frame
	void SetPath (int i) {
        m = i;
        Camera.main.renderingPath = renderingOptions[m];
	}
}
