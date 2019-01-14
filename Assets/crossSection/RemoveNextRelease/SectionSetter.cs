using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SectionSetter : MonoBehaviour {
	private GameObject toBeSectioned;
	public Shader[] crossSectionShaders;
	public GUISkin skin1;
	private Section section = new Section();

    public GameObject settingPanel;

    public GameObject infoPanel;

    public GameObject sliderPanel;
    public Text title;
    public Slider rotZslider;
    public Slider rotXslider;
    public Slider rotYslider;
    public Slider offset_slider;

    public delegate void ChangeSettings(Vector3 s, float o);
    public static event ChangeSettings OnSettingsChange;

    public Vector3 sectionAngles;
    public Vector3 sectionNormal;

    private GameObject go;


	void Start () {

        if (rotZslider)
        {
            
            rotZslider.maxValue = 360f;
            rotZslider.value = 0f;
            rotZslider.minValue = 0f;
            //rotZslider.onValueChanged.AddListener(SectionChange);
        }
        if (rotYslider)
        {

            rotYslider.maxValue = 360f;
            rotYslider.value = 0f;
            rotYslider.minValue = 0f;
            //rotYslider.onValueChanged.AddListener(SectionChange);
        }
        if (rotXslider) { 
            
            rotXslider.maxValue = 360f;
            rotXslider.value = 180f;
            rotXslider.minValue = 0f;
            //rotXslider.onValueChanged.AddListener(SectionChange);
        }
        //if (offset_slider) offset_slider.onValueChanged.AddListener(SectionChange);
        //if (sliderPanel) sliderPanel.SetActive(false);
        settingPanel.SetActive(false);
        infoPanel.SetActive(true);
        enabled = false;
	}
    void SectionChange(float f)
    {
        //return;
        if (!toBeSectioned) return;
        sectionAngles.x = rotXslider.value;
        sectionAngles.z = rotZslider.value;
        sectionAngles.y = rotYslider.value;

        sectionNormal = Quaternion.Euler(sectionAngles) * Vector3.up;
        OnSettingsChange(sectionNormal, offset_slider.value);
    }


	public void newSettings (GameObject go, Section sect) {
        enabled = true;
		toBeSectioned = go;
		section = sect;
        sectionNormal = sect.norm;
        sectionAngles = Quaternion.FromToRotation(Vector3.up, sect.norm).eulerAngles;
        rotZslider.value = sectionAngles.z;
        rotXslider.value = sectionAngles.x;
        rotYslider.value = sectionAngles.y;
        offset_slider.maxValue = section.offsetRange;
        offset_slider.value = section.offset;
        offset_slider.minValue = -section.offsetRange;
        title.text = "SECTION PLANE SETTINGS on " + toBeSectioned.name;
        rotZslider.onValueChanged.AddListener(SectionChange);
        rotXslider.onValueChanged.AddListener(SectionChange);
        rotYslider.onValueChanged.AddListener(SectionChange);
        offset_slider.onValueChanged.AddListener(SectionChange);

	}

    public void newSettings(Vector3 n, float o)
    {
        rotZslider.onValueChanged.RemoveListener(SectionChange);
        rotXslider.onValueChanged.RemoveListener(SectionChange);
        rotYslider.onValueChanged.RemoveListener(SectionChange);
        offset_slider.onValueChanged.RemoveListener(SectionChange);
        section.offset = o;
        offset_slider.value = o;
        sectionNormal = n;
        sectionAngles = Quaternion.FromToRotation(Vector3.up,n).eulerAngles;
        rotZslider.value = sectionAngles.z;
        rotXslider.value = sectionAngles.x;
        rotYslider.value = sectionAngles.y;
        rotZslider.onValueChanged.AddListener(SectionChange);
        rotXslider.onValueChanged.AddListener(SectionChange);
        rotYslider.onValueChanged.AddListener(SectionChange);
        offset_slider.onValueChanged.AddListener(SectionChange);
    }
	
    void OnEnable()
    {
        settingPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    void OnDisable()
    {
        settingPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

	
}
