using UnityEngine;
using System.Collections;

public class SectionRotator : MonoBehaviour
{
    [HideInInspector]
    public Vector3 center;
    public GameObject gizmo;
    public bool dragging = false;

    private float _sensitivity = 0.4f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;
    private Vector3 eangles;
    private GameObject cage;

    

    Quaternion startRot;
    Quaternion startCageRot;
    Vector3 startCagefwd;

    Quaternion localRot;

    CrossSection cs;

    void Start()
    {
        cs = GetComponent<CrossSection>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform == transform)
                {
                    if (gizmo) 
                    {
                        gizmo.transform.position = center + cs.sectVars.norm * cs.sectVars.offset;
                   
                    }
                    else
                    {
                        gizmo = Resources.Load("Gizmo") as GameObject;
                        cs = GetComponent<CrossSection>();

                        gizmo = Instantiate(gizmo, center + cs.sectVars.norm * cs.sectVars.offset, Quaternion.FromToRotation(Vector3.up, cs.sectVars.norm)) as GameObject;
                    }
                Texture offsetGizmoTexture = Resources.Load("offsetGizmo") as Texture;
                gizmo.GetComponent<Renderer>().material.mainTexture = offsetGizmoTexture;
                StartCoroutine(dragGizmo());
                }
            }
        }
        
        
        
        
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);
            // apply rotation
            _rotation.y = -(_mouseOffset.x) * _sensitivity;
            _rotation.x = -(_mouseOffset.y) * _sensitivity;

            gizmo.transform.rotation = Quaternion.Euler(eangles + new Vector3(_rotation.x, _rotation.y, 0)) * localRot;

            if (_mouseOffset == Vector3.zero) return;
            
            if (cs)
            {
                cs.applySettings(gizmo.transform.up);
            }
        }
    }

    IEnumerator dragGizmo() 
    {
        float cameraDistance = Vector3.Distance(center, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cameraDistance));
        Vector3 startPos = gizmo.transform.position;
        Vector3 translation = Vector3.zero;
        float startOffset = cs.sectVars.offset;
        float maxOffset = cs.sectVars.offsetRange;
        float minOffset = -cs.sectVars.offsetRange;
        Camera.main.GetComponent<maxCamera>().enabled = false;
        dragging = true;
        while (Input.GetMouseButton(1))
        {
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)&&(hit.transform == transform)) 
            {
                translation = hit.point - startPoint;
                translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cameraDistance)) - startPoint;
            }
            else
            {
                yield break;
            }*/
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            Vector3 projectedTranslation = Vector3.Project(translation, gizmo.transform.up);
            float newOffset = startOffset + (projectedTranslation.x + projectedTranslation.y + projectedTranslation.z) / (cs.sectVars.norm.x + cs.sectVars.norm.y + cs.sectVars.norm.z);
            if (newOffset > maxOffset)
            {
                gizmo.transform.position = center + cs.sectVars.norm * maxOffset;
                cs.applySettings(cs.sectVars.norm, maxOffset);
            }
            else if (newOffset < minOffset)
            {
                gizmo.transform.position = center + cs.sectVars.norm * minOffset;
                cs.applySettings(cs.sectVars.norm, minOffset);
            }
            else
            {
                gizmo.transform.position = startPos + Vector3.Project(translation, gizmo.transform.up);
                cs.applySettings(cs.sectVars.norm, newOffset);
            }
            yield return null;
        }
        Camera.main.GetComponent<maxCamera>().enabled = true;
    }


    void OnMouseDown()
    {

        //disable camera interaction
        Camera.main.GetComponent<maxCamera>().enabled = false;
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
        cage = new GameObject("rotationCage");
        cage.transform.position = transform.position;
        cage.transform.LookAt(Camera.main.transform);
        dragging = false;
        if (!gizmo) 
        {
            gizmo = Resources.Load("Gizmo") as GameObject;
            cs = GetComponent<CrossSection>();

            gizmo = Instantiate(gizmo, center, Quaternion.FromToRotation(Vector3.up, cs.sectVars.norm)) as GameObject;
        }
        else
        {
            gizmo.transform.position = center;
        }
        Texture offsetGizmoTexture = Resources.Load("rotGizmo") as Texture;
        gizmo.GetComponent<Renderer>().material.mainTexture = offsetGizmoTexture;
        localRot = Quaternion.Inverse(cage.transform.rotation) * Quaternion.FromToRotation(Vector3.up, cs.sectVars.norm);
        eangles = cage.transform.eulerAngles;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
        if (cage) Destroy(cage);
        Camera.main.GetComponent<maxCamera>().enabled = true;
    }



}