using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class WorldCursor : MonoBehaviour 
{
    /// <summary>
    /// The name of the cursor's X axis in Unity's Input settings
    /// </summary>
    public string cursorXaxis = "Mouse X";

    /// <summary>
    /// The name of the cursor's Y axis in Unity's Input settings
    /// </summary>
    public string cursorYaxis = "Mouse Y";

    /// <summary>
    /// The name of the cursor's select button in Unity's Input settings
    /// </summary>
    public string selectButton = "Fire1";

    /// <summary>
    /// How quickly the cursor moves around the canvas.
    /// </summary>
    public float sensitivityFactor = 5f;

    /// <summary>
    /// If this is true, the cursor will not leave the bounds of the canvas it is attached to.
    /// This can be changed at runtime, and it will automatically snap the cursor to the correct edge if out of bounds.
    /// </summary>
    public bool enableBoundsChecking = true;
    
    /// <summary>
    /// The image representing this world cursor. If you want to set this in the inspector, just use the Image component on the 
    /// cursor object.
    /// </summary>
    [HideInInspector]
    public Image thisImage;

    /// <summary>
    /// If this is enabled, you can construct your UI at runtime, but it will have to check for every frame for new selectables
    /// on the canvas. If you have a lot of these selectables, it could slow down your game. It's best to leave this disabled unless
    /// you need to use it.
    /// </summary>
    public bool enableRuntimeUISupport = false;


    #region Internal Vars

    /// <summary>
    /// The canvas that ths cursor is interacting with - for internal use
    /// </summary>
    private Canvas uiCanvas;

    /// <summary>
    /// The cached rect of the uiCanvas - for internal use
    /// </summary>
    private Rect uiCanvasRect;

    /// <summary>
    /// A list of all the selectables on the uiCanvas - for internal use
    /// </summary>
    private List<Collider> selectablesOnCanvas = new List<Collider>();

    /// <summary>
    /// Is this world cursor moving right now? - for internal use
    /// </summary>
    private bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }

    /// <summary>
    /// The object that the cursor is selecting right now. null if nothing is selected. - for internal use
    /// </summary>

    private GameObject currentSelectedObject = null;

    #endregion

    void OnEnable()
    {
        // if there are other world cursors active in the scene, disable them
        WorldCursor[] otherCursors = FindObjectsOfType<WorldCursor>();
        if (otherCursors != null)
        {
            foreach (WorldCursor w in otherCursors)
                if(w != this)
                    w.gameObject.SetActive(false);
        }

        // enable this cursor
        WorldCursorInputModule.SetActiveCursor(this, selectButton);

        // lock the cursor before the first active frame
        LockAndHideCursor();
    }

    void Awake()
    {
        if (thisImage == null)
            thisImage = GetComponent<Image>();

        uiCanvas = thisImage.canvas;
        uiCanvasRect = uiCanvas.GetComponent<RectTransform>().rect;
    }

    void Start()
    {
        bool auto = CacheSelectablesOnCanvas();

        if(auto == true)
            Debug.Log("WORLD SPACE CURSOR: At least one of the selectable elements on canvas \"" + uiCanvas.name + 
                "\" has no collider2D. If the auto-setup doesn't work well, make sure to set up all the colliders yourself.");
    }

    void OnTriggerEnter(Collider selectable)
    {
        // make sure it's a selectable on the canvas before attempting to select it
        if (!selectablesOnCanvas.Contains(selectable))
            return;

        WorldCursorInputModule.SetTargetObject(selectable.gameObject);
        currentSelectedObject = selectable.gameObject;
        //Debug.Log("WORLD SPACE CURSOR: highlighting " + selectable.name);
    }

    void OnTriggerExit(Collider selectable)
    {
        if (selectable.gameObject == currentSelectedObject.gameObject)
        {
            //Debug.Log("WORLD SPACE CURSOR: un highlighting " + selectable.name);
            WorldCursorInputModule.SetTargetObject(null);
            currentSelectedObject = null;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        // run every frame to ensure it's always locked even if something else unlocks it
        LockAndHideCursor();

        UpdateCursorLocation();

        if(enableRuntimeUISupport)
        {
            bool auto = CacheSelectablesOnCanvas();
            if(auto)
                Debug.Log("WORLD SPACE CURSOR: At least one of the selectable elements on canvas \"" + uiCanvas.name +
                    "\" has no collider2D. If the auto-setup doesn't work well, make sure to set up all the colliders yourself.");
        }
	}

    /// <summary>
    /// Locks & hides the default system cursor
    /// </summary>
    private static void LockAndHideCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;

        if (Cursor.visible == true)
            Cursor.visible = false;
    }

    // Update location of cursor based on mouse location. Bounds checking is also done here.
    private void UpdateCursorLocation()
    {
        float mouseX = Input.GetAxisRaw(cursorXaxis);
        float mouseY = Input.GetAxisRaw(cursorYaxis);
        bool isCursorMoving = false;
        Vector3 localPositionAddend = Vector3.zero;

        // horizontal movement
        if (mouseX > 0 && (this.transform.localPosition.x <= uiCanvasRect.xMax || enableBoundsChecking == false))
        {
            //Debug.Log("move right");
            localPositionAddend += new Vector3(mouseX * sensitivityFactor, 0, 0);
            isCursorMoving = true;
        }
        else if (mouseX < 0 && (this.transform.localPosition.x >= uiCanvasRect.xMin || enableBoundsChecking == false))
        {
            //Debug.Log("move left");
            localPositionAddend += new Vector3(mouseX * sensitivityFactor, 0, 0);
            isCursorMoving = true;
        }

        // vertical movement
        if (mouseY > 0 && (this.transform.localPosition.y <= uiCanvasRect.yMax || enableBoundsChecking == false))
        {
            //Debug.Log("move up");
            localPositionAddend += new Vector3(0, mouseY * sensitivityFactor, 0);
            isCursorMoving = true;
        }
        else if (mouseY < 0 && (this.transform.localPosition.y >= uiCanvasRect.yMin || enableBoundsChecking == false))
        {
            //Debug.Log("move down");
            localPositionAddend += new Vector3(0, mouseY * sensitivityFactor, 0);
            isCursorMoving = true;
        }

        transform.localPosition += localPositionAddend;

        // reset cursor position if bounds checking is enabled and it is beyond the bounds of the canvas
        if (enableBoundsChecking)
        {
            if (this.transform.localPosition.x > uiCanvasRect.xMax)
                transform.localPosition = new Vector3(uiCanvasRect.xMax, transform.localPosition.y, transform.localPosition.z);
            else if (this.transform.localPosition.x < uiCanvasRect.xMin)
                transform.localPosition = new Vector3(uiCanvasRect.xMin, transform.localPosition.y, transform.localPosition.z);

            if (this.transform.localPosition.y > uiCanvasRect.yMax)
                transform.localPosition = new Vector3(transform.localPosition.x, uiCanvasRect.yMax, transform.localPosition.z);
            else if (this.transform.localPosition.y < uiCanvasRect.yMin)
                transform.localPosition = new Vector3(transform.localPosition.x, uiCanvasRect.yMin, transform.localPosition.z);
        }

        isMoving = isCursorMoving;
    }

    /// <summary>
    /// Caches all selectables on the canvas.
    /// </summary>
    /// <returns>Whether the auto set-up had to be used on all the ui elements or not. See manual for more details on the auto set-up.</returns>
    private bool CacheSelectablesOnCanvas()
    {
        bool auto = false;

        foreach (Selectable s in uiCanvas.GetComponentsInChildren<Selectable>())
        {
            Collider sCollider = s.GetComponent<Collider>();
            if (sCollider == null)
            {
                auto = true;
                BoxCollider boxCollider = s.gameObject.AddComponent<BoxCollider>();
                if (s.image != null)
                {
                    Rect r = s.gameObject.GetComponent<RectTransform>().rect;
                    // At least one collider in a 3D collision needs a non-zero z size.
                    // For 100% foolproofing, these auto-generated colliders are set with a non-zero z size in case
                    // the user messes with the World Space Cursor's collider.
                    // If you're reading this and you want 2D colliders for your selectable, you can change the 
                    // z size below as long as you make sure your cursor object's collider has a non-zero z size.
                    boxCollider.size = new Vector3(r.width, r.height, 0.1f); 
                    boxCollider.center = Vector3.zero;
                }

                sCollider = boxCollider;
            }
            selectablesOnCanvas.Add(sCollider);
        }
        return auto;
    }
}
