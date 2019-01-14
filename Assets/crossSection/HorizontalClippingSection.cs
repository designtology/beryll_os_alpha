using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HorizontalClippingSection : MonoBehaviour {

    private GameObject xyzSectionPanel;
    private Text topSliderLabel, middleSliderLabel, bottomSliderLabel;
    private Slider slider;
    private Toggle xtoggle, ytoggle, ztoggle, gizmotoggle;

    public Color sectionColor;

    private List<Material> matList;
    private List<Material> clipMatList;
    private Renderer[] renderers;
    private Dictionary<Renderer, int[]> matDict;

	private Vector3 boundsCentre;
    private Vector3 sectionplane = Vector3.up;
	
	public Transform ZeroAlignment;
    public bool accurateBounds = true;

    //public enum ConstrainedAxis { X, Y, Z };
    public Planar_xyzClippingSection.ConstrainedAxis selectedAxis = Planar_xyzClippingSection.ConstrainedAxis.Y;

	//private float sliderRange = 0f;

    public Bounds boundbox;

    private GameObject rectGizmo;

    private Vector3 zeroAlignmentVector = Vector3.zero;

    public bool gizmoOn = true;

    private Vector3 sliderRange = Vector3.zero;

    private float sectionX = 0;
    private float sectionY = 0;
    private float sectionZ = 0;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        xyzSectionPanel = GameObject.Find("xyzSectionPanel");
        if (xyzSectionPanel)
        {
            slider = xyzSectionPanel.GetComponentInChildren<Slider>();
            topSliderLabel = xyzSectionPanel.transform.Find("sliderPanel/MaxText").GetComponent<Text>();
            middleSliderLabel = xyzSectionPanel.transform.Find("sliderPanel/Slider").GetComponentInChildren<Text>();
            bottomSliderLabel = xyzSectionPanel.transform.Find("sliderPanel/MinText").GetComponent<Text>();
            if (xyzSectionPanel.transform.Find("axisOptions"))
            {
                xtoggle = xyzSectionPanel.transform.Find("axisOptions/Panel/X_Toggle").GetComponent<Toggle>();
                ytoggle = xyzSectionPanel.transform.Find("axisOptions/Panel/Y_Toggle").GetComponent<Toggle>();
                ztoggle = xyzSectionPanel.transform.Find("axisOptions/Panel/Z_Toggle").GetComponent<Toggle>();
                xtoggle.isOn = selectedAxis == Planar_xyzClippingSection.ConstrainedAxis.X;
                ytoggle.isOn = selectedAxis == Planar_xyzClippingSection.ConstrainedAxis.Y;
                ztoggle.isOn = selectedAxis == Planar_xyzClippingSection.ConstrainedAxis.Z;
            }
            if (xyzSectionPanel.transform.Find("gizmoToggle"))
            {
                gizmotoggle = xyzSectionPanel.transform.Find("gizmoToggle").GetComponent<Toggle>();
                gizmotoggle.isOn = gizmoOn;
            }
        }
        if (ZeroAlignment) zeroAlignmentVector = ZeroAlignment.position;
        calculateBounds();
        makeSectionMaterials();
        setupGizmo();
        //makeSectionMaterials();
        //selectedAxis = ConstrainedAxis.Y;
        setSection();
    }

	void Start () {	
        if (slider) slider.onValueChanged.AddListener(SliderListener);
        if (xyzSectionPanel) xyzSectionPanel.SetActive(enabled);
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        Shader.EnableKeyword("CLIP_PLANE");
        Shader.SetGlobalVector("_SectionPlane", Vector3.up);
        if (xtoggle) xtoggle.onValueChanged.AddListener(delegate { SetAxis(xtoggle.isOn, Planar_xyzClippingSection.ConstrainedAxis.X); });
        if (ytoggle) ytoggle.onValueChanged.AddListener(delegate { SetAxis(ytoggle.isOn, Planar_xyzClippingSection.ConstrainedAxis.Y); });
        if (ztoggle) ztoggle.onValueChanged.AddListener(delegate { SetAxis(ztoggle.isOn, Planar_xyzClippingSection.ConstrainedAxis.Z); });
        if (gizmotoggle) gizmotoggle.onValueChanged.AddListener(GizmoOn);
	}

    public void SliderListener(float value)
    {
        if (middleSliderLabel) middleSliderLabel.text = value.ToString("0.0");

        switch (selectedAxis)
        {
            case Planar_xyzClippingSection.ConstrainedAxis.X:
                sectionX = value + zeroAlignmentVector.x;
                if (rectGizmo) rectGizmo.transform.position = new Vector3(sectionX, boundbox.center.y, boundbox.center.z);
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Y:
                sectionY = value + zeroAlignmentVector.y;
                if (rectGizmo) rectGizmo.transform.position = new Vector3(boundbox.center.x, sectionY, boundbox.center.z);
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Z:
                sectionZ = value + zeroAlignmentVector.z;
                if (rectGizmo) rectGizmo.transform.position = new Vector3(boundbox.center.x, boundbox.center.y, sectionZ);
                break;
        }
        Shader.SetGlobalVector("_SectionPoint", new Vector3(sectionX,sectionY,sectionZ));
    }

    public void SetAxis(bool b, Planar_xyzClippingSection.ConstrainedAxis a)
    {
        if (b) 
        { 
            selectedAxis = a;
            Debug.Log(a);
            RectGizmo rg = rectGizmo.GetComponent<RectGizmo>();
            rg.transform.position = Vector3.zero;
            rg.SetSizedGizmo(boundbox.size, selectedAxis);
            setSection();
        }
    }

    void setSection()
    {
        float sliderMaxVal = 0f;
        float sliderVal = 0f;
        float sliderMinVal = 0f;
        Vector3 sectionpoint = new Vector3(sectionX,sectionY,sectionZ);
        switch (selectedAxis)
        {
            case Planar_xyzClippingSection.ConstrainedAxis.X:
                sectionplane = Vector3.right;
                rectGizmo.transform.position = new Vector3(sectionX, boundbox.center.y, boundbox.center.z);
                sliderMaxVal = boundbox.min.x + sliderRange.x - zeroAlignmentVector.x;
                sliderVal = sectionX - zeroAlignmentVector.x;
                sliderMinVal = boundbox.min.x - zeroAlignmentVector.x;
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Y:
                sectionplane = Vector3.up;
                rectGizmo.transform.position = new Vector3(boundbox.center.x, sectionY, boundbox.center.z);
                sliderMaxVal = boundbox.min.y + sliderRange.y - zeroAlignmentVector.y;
                sliderVal = sectionY - zeroAlignmentVector.y;
                sliderMinVal = boundbox.min.y - zeroAlignmentVector.y;
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Z:
                sectionplane = Vector3.forward;
                rectGizmo.transform.position = new Vector3(boundbox.center.x, boundbox.center.y, sectionZ);
                sliderMaxVal = boundbox.min.z + sliderRange.z - zeroAlignmentVector.z;
                sliderVal = sectionZ - zeroAlignmentVector.z;
                sliderMinVal = boundbox.min.z - zeroAlignmentVector.z;
                break;
        }

        Shader.SetGlobalVector("_SectionPoint", sectionpoint);
        Shader.SetGlobalVector("_SectionPlane", sectionplane);


        if (topSliderLabel) topSliderLabel.text = sliderMaxVal.ToString("0.0");
        if (bottomSliderLabel) bottomSliderLabel.text = sliderMinVal.ToString("0.0");

        if (slider)
        {
            slider.maxValue = sliderMaxVal;

            slider.value = sliderVal;
            middleSliderLabel.text = sliderVal.ToString("0.0");

            slider.minValue = sliderMinVal;
        }
    }


    void makeSectionMaterials()
    {
        matList = new List<Material>();
        clipMatList = new List<Material>();
        matDict = new Dictionary<Renderer, int []>();
        foreach (Renderer rend in renderers) {
            Material[] mats = rend.sharedMaterials;
            int[] idx = new int[mats.Length];
            for(int j = 0; j < mats.Length; j++) {
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
        foreach (Material mat in matList)
        {
            string shaderName = mat.shader.name;
            Debug.Log(shaderName);
            if (shaderName.Length > 13)
            {
                if (shaderName.Substring(0, 13) == "CrossSection/")
                {
                    clipMatList.Add(mat);
                    continue;
                }
            }
            Material substitute = new Material(mat);
            //substitute.name = "subst_" + substitute.name;
            shaderName = shaderName.Replace("Legacy Shaders/", "").Replace("(", "").Replace(")", "");
            Shader replacementShader = null;
#if UNITY_WEBGL||UNITY_ANDROID
            //if (shaderName == "Standard") replacementShader = Shader.Find("CrossSection/Reflective/Specular");
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
            substitute.SetColor("_SectionColor", sectionColor);

            clipMatList.Add(substitute);
        }
        foreach (Renderer rend in renderers)
        {
            int[] idx = matDict[rend];
            Material[] mats = new Material[idx.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                mats[i] = clipMatList[idx[i]];
            }
            rend.materials = mats;
        }
    }


	void calculateBounds() {
        if (accurateBounds)
        {
            boundbox = calculateMeshBounds();
        }
        else
        {
            boundbox = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                boundbox.Encapsulate(renderers[i].bounds);
                /*          This gives the accurate results only when the objects are not rotated or rotated by multiplication of 90 degrees.
                            A general way to get accurate results would be to iterate through all the mesh points, and find their positions range in the world space.
                            But this can take long in case of complex meshes*/
            }
        }
		boundsCentre = boundbox.center;

        sliderRange = new Vector3((float)SignificantDigits.CeilingToSignificantFigures((decimal)(1.08f * 2 * boundbox.extents.x), 2), 
            (float)SignificantDigits.CeilingToSignificantFigures((decimal)(1.08f * 2 * boundbox.extents.y), 2),  
            (float)SignificantDigits.CeilingToSignificantFigures((decimal)(1.08f * 2 * boundbox.extents.z), 2));
        sectionX = boundbox.min.x + sliderRange.x;
        sectionY = boundbox.min.y + sliderRange.y;
        sectionZ = boundbox.min.z + sliderRange.z;
        //sliderRange = 2 * boundbox.extents.y;
/*      This is an arbitrary clearance roundup.
        To ensure the maximum slider value to be above the object range
        Should work in case of architectural objects with units are given in meters or feet. But might need readjustment for other cases.
        */

        //sectVars.offset = sliderRange-sectVars.offsetRange;
	}

    Bounds calculateMeshBounds()
    {
        Bounds accurateBounds = new Bounds();
        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh ms = meshes[i].mesh;
            Vector3 tr = meshes[i].gameObject.transform.position;
            Vector3 ls = meshes[i].gameObject.transform.lossyScale;
            Quaternion lr = meshes[i].gameObject.transform.rotation;
            int vc = ms.vertexCount;
            for (int j = 0; j < vc; j++)
            {
                if (i == 0 && j == 0)
                {
                    accurateBounds = new Bounds(tr + lr * Vector3.Scale(ls, ms.vertices[j]), Vector3.zero);
                }
                else
                {
                    accurateBounds.Encapsulate(tr + lr * Vector3.Scale(ls, ms.vertices[j]));
                }
            }
        }
        return accurateBounds;
    }

    void setupGizmo()
    {
        rectGizmo = Resources.Load("rectGizmo") as GameObject;
        rectGizmo = Instantiate(rectGizmo, boundbox.center + (-boundbox.extents.y + (slider? slider.value:0) + zeroAlignmentVector.y) * transform.up, Quaternion.identity) as GameObject;

        RectGizmo rg = rectGizmo.GetComponent<RectGizmo>();

        rg.SetSizedGizmo(boundbox.size, selectedAxis);
        /* Set rectangular gizmo size here: inner width, inner height, border width.
         */
        rectGizmo.SetActive(false);

    }

    void OnEnable()
    {
        if (xyzSectionPanel) xyzSectionPanel.SetActive(true);
        if (slider)
        {
            Shader.SetGlobalVector("_SectionPoint", new Vector3(sectionX, sectionY, sectionZ));
        }
    }

    void OnDisable()
    {
        if (xyzSectionPanel) xyzSectionPanel.SetActive(false);
        Shader.DisableKeyword("CLIP_PLANE");
        //Shader.SetGlobalVector("_SectionPoint", boundbox.min + sliderRange);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Collider coll = gameObject.GetComponent<Collider>();
            if (coll.Raycast(ray, out hit, 10000f))
            {
                if(gizmoOn) rectGizmo.SetActive(true);
                StartCoroutine(dragGizmo());
            }
            else 
            {
                rectGizmo.SetActive(false);
            }
        }
    }

    IEnumerator dragGizmo()
    {
        float cameraDistance = Vector3.Distance(boundbox.center, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 startPos = rectGizmo.transform.position;
        Vector3 translation = Vector3.zero;
        Camera.main.GetComponent<maxCamera>().enabled = false;
        if (slider) slider.onValueChanged.RemoveListener(SliderListener);
        while (Input.GetMouseButton(0))
        {
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            Vector3 projectedTranslation = Vector3.Project(translation, sectionplane);
            Vector3 newPoint = startPos + projectedTranslation;
            switch (selectedAxis) {
                case Planar_xyzClippingSection.ConstrainedAxis.X:
                    if (newPoint.x > boundbox.max.x) sectionX = boundbox.max.x;
                    else if (newPoint.x < boundbox.min.x)  sectionX = boundbox.min.x; 
                    else sectionX = newPoint.x;
                    break;
                case Planar_xyzClippingSection.ConstrainedAxis.Y:
                    if (newPoint.y > boundbox.max.y) sectionY = boundbox.max.y;
                    else if (newPoint.y < boundbox.min.y) sectionY = boundbox.min.y;
                    else sectionY = newPoint.y;
                    break;
                case Planar_xyzClippingSection.ConstrainedAxis.Z:
                    if (newPoint.z > boundbox.max.z) sectionZ = boundbox.max.z;
                    else if (newPoint.z < boundbox.min.z) sectionZ = boundbox.min.z;
                    else sectionZ = newPoint.z;
                    break;
            }
            setSection();
            yield return null;
        }
        Camera.main.GetComponent<maxCamera>().enabled = true;
        if (slider) slider.onValueChanged.AddListener(SliderListener);
    }

    public void GizmoOn(bool val) 
    {
        gizmoOn = val;
        if (rectGizmo) rectGizmo.SetActive(val);
    }

}
