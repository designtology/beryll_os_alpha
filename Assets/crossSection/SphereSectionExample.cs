using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.EventSystems;

public class SphereSectionExample : MonoBehaviour {

	//public float angle = 60f;
	public float CS_X = 0;
	public float CS_Y = 65;
	public float CS_Z = -35;

	float radius_1 = 42f; // 27 = 60% of 45 Pixel² of one tile of a game-level | aktuell 22,5

	void Start () {
	//	Renderer[] allrenderers = gameObject.GetComponentsInChildren<Renderer>();

      /*  Shader.DisableKeyword("CLIP_PLANE");
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        Shader.DisableKeyword("CLIP_SPHERE");
        //we have declared: "material.EnableKeyword("CLIP_PLANE");" on all the crossSectionStandard derived materials - in the CrossSectionStdShaderGUI editor script - so we have to switch it off
        foreach (Renderer r in allrenderers)
        {
            Material[] mats = r.sharedMaterials;
            foreach (Material m in mats) if (m.shader.name.Substring(0, 13) == "CrossSection/") m.DisableKeyword("CLIP_PLANE");
		}
		*/
	/*	Shader.EnableKeyword("CLIP_SPHERE");
		Shader.SetGlobalVector("_SectionPoint", new Vector3(CS_X,CS_Y,CS_Z));
		Shader.SetGlobalFloat("_Radius", 30); 
    */}
	
	void Update () {        
		Shader.EnableKeyword("CLIP_SPHERE");
		Shader.SetGlobalVector("_SectionPoint", new Vector3(CS_X,CS_Z,CS_Y));
		Shader.SetGlobalFloat("_Radius", radius_1); 		
/*        if (Input.GetMouseButtonDown(0))
        {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				Debug.Log("hit");
				return;
			}
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
			{                    
				
                if (hit.transform.IsChildOf(transform))
                {
					Debug.Log(hit.point);
                    Shader.EnableKeyword("CLIP_SPHERE");
                    Shader.SetGlobalVector("_SectionPoint", hit.point);
                    Shader.SetGlobalFloat("_Radius", 1.1f);
                    StartCoroutine(drag());
                }
            }
        } */
	}

    void OnEnable()
    {
        Shader.EnableKeyword("CLIP_SPHERE");
        //Shader.EnableKeyword("CLIP_PLANE");
    }

    void OnDisable()
    {
        Shader.DisableKeyword("CLIP_SPHERE");
        //Shader.DisableKeyword("CLIP_PLANE");
    }

    void OnApplicationQuit()
    {
        //disable clipping so we could see the materials and objects in editor properly
        Shader.DisableKeyword("CLIP_SPHERE");

    }

/*
    IEnumerator drag()
    {
        float cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 translation = Vector3.zero;
        Camera.main.GetComponent<maxCamera>().enabled = false;
        while (Input.GetMouseButton(0))
        {
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            float m = translation.magnitude;
            if(m>0.1f) Shader.SetGlobalFloat("_Radius", m);
            yield return null;
        }
        Camera.main.GetComponent<maxCamera>().enabled = true;
        
    }*/ 
}
