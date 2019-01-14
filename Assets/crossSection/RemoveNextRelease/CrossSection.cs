using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[System.Serializable]
public class Section
{
    public Vector3 norm = Vector3.up; public float offset = 0f; public float offsetRange = 0f;
}
public class CrossSection : MonoBehaviour {

    private List<Material> matList;
    private List<Material> clipMatList;
    private Renderer[] renderers;
    private Dictionary<Renderer, int[]> matDict;
	private SectionSetter setter;
	private Vector3 boundsCentre;
	public Section sectVars;
    public Color sectionColor = Color.red;
    private SectionRotator sr;


	void Awake () {	
		renderers = GetComponentsInChildren<Renderer>();
		setter = FindObjectOfType(typeof(SectionSetter)) as SectionSetter;
		calculateBounds();
		makeSectionMaterials();
		enabled = false;
        if (!GetComponent<Collider>())
        {
            Rigidbody r = gameObject.AddComponent<Rigidbody>();
            r.useGravity = false;
        }
	}

	void Start () {
        
	}

     void Update ()
  {
      if (Input.GetMouseButtonDown(0))
      {
          if (EventSystem.current.IsPointerOverGameObject()) return;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;
          Collider coll = gameObject.GetComponent<Collider>();
          if (coll.Raycast(ray, out hit, 10000f)) return;
          if (sr) {
            if (sr.gizmo) { 
              Destroy(sr.gizmo); return; 
              }
              Destroy(sr);
         
          }
          Cut(false);
          setter.enabled = false;
          enabled = false;
      }
  }




    void makeSectionMaterials()
    {
        matList = new List<Material>();
        clipMatList = new List<Material>();
        matDict = new Dictionary<Renderer, int[]>();
        foreach (Renderer rend in renderers)
        {
            Material[] mats = rend.sharedMaterials;
            int[] idx = new int[mats.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                int i = matList.IndexOf(mats[j]);
                if (i == -1)
                {
                    matList.Add(mats[j]);
                    i = matList.Count - 1;
                }
                idx[j] = i;
            }
            matDict.Add(rend, idx);
        }
        Debug.Log("matList " + matList.Count.ToString());
        foreach (Material mat in matList)
        {
            Material substitute = new Material(mat);

            string shaderName = mat.shader.name;
            shaderName = shaderName.Replace("Legacy Shaders/", "").Replace("(", "").Replace(")", "");
            Shader replacementShader = null;
#if UNITY_WEBGL||UNITY_ANDROID
            #if !UNITY_EDITOR
            //if (shaderName == "Standard") replacementShader = Shader.Find("CrossSection/Reflective/Specular");
            #endif
#endif
            if (replacementShader == null) replacementShader = Shader.Find("CrossSection/" + shaderName);
            if (replacementShader == null)
            {
                if (shaderName.Contains("Transparent/VertexLit"))
                {
                    replacementShader = Shader.Find("CrossSection/Transparent/Specular");
                }
                else if (shaderName.Contains("Transparent"))
                {
                    replacementShader = Shader.Find("CrossSection/Transparent/Diffuse");
                }
                else
                {
                    replacementShader = Shader.Find("CrossSection/Diffuse");
                }
            }
            substitute.shader = replacementShader;
            substitute.EnableKeyword("CLIP_PLANE");
            substitute.SetVector("_SectionPoint", new Vector4(boundsCentre.x, boundsCentre.y, boundsCentre.z, 1));
            substitute.SetVector("_SectionPlane", sectVars.norm);
            substitute.SetFloat("_ClipOffset", sectVars.offset);
            substitute.SetFloat("_SectionOffset", sectVars.offset);
            substitute.SetColor("_SectionColor", sectionColor);

            clipMatList.Add(substitute);
        }
    }


	void OnMouseDown() {
        if (!enabled)
        {
            enabled = true;
            setter.newSettings(gameObject,sectVars);
            Cut(enabled);
            return;
        }
        if (!gameObject.GetComponent<SectionRotator>())
        {
            sr = gameObject.AddComponent<SectionRotator>();
            sr.center = boundsCentre;
        }
	}


	public void Cut (bool val) {
		enabled = val;
        foreach (Renderer rend in renderers)
        {
            int[] idx = matDict[rend];
            Material[] mats = new Material[idx.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                mats[i] = val ? clipMatList[idx[i]] : matList[idx[i]];
            }
            rend.materials = mats;
        }
	}

    void OnEnable()
    {
        SectionSetter.OnSettingsChange += applySettings;
    }


    void OnDisable()
    {
        SectionSetter.OnSettingsChange -= applySettings;
    }

	public void applySettings (Section val) {
        sectVars = val;
        foreach (Material mat in clipMatList)
        {
            mat.SetVector("_SectionPlane", sectVars.norm);
            mat.SetFloat("_SectionOffset", sectVars.offset);
        }
	}

    public void applySettings(Vector3 planeNorm)
    {
        foreach (Material mat in clipMatList)
        {
            mat.SetVector("_SectionPlane", planeNorm);
        }
        sectVars.norm = planeNorm;
    }

    public void applySettings(Vector3 planeNorm, float f)
    {
        foreach (Material mat in clipMatList)
        {
            mat.SetVector("_SectionPlane", planeNorm);
            mat.SetFloat("_SectionOffset", f);
        }
        sectVars.offset = f;
        sectVars.norm = planeNorm;
        if (sr)
        {
            if (sr.gizmo) sr.gizmo.transform.rotation = Quaternion.FromToRotation(Vector3.up, planeNorm);
            if (sr.dragging) sr.gizmo.transform.position = sr.center + f * planeNorm;
        }
    }

    void OnMouseUp()
    {
        setter.newSettings(sectVars.norm, sectVars.offset);
    }
    void OnMouseExit()
    {
        setter.newSettings(sectVars.norm, sectVars.offset);
    }

	void calculateBounds() {
		Bounds bound = renderers[0].bounds;
		for(int i = 1; i < renderers.Length; i++) {
			bound.Encapsulate(renderers[i].bounds);
		}
		boundsCentre = bound.center;
		sectVars.offsetRange = bound.extents.magnitude;
	}
}
