using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Cursor input module for selecting items in a world-space ui. It uses an image on the canvas as the cursor to select objects on that canvas.
/// </summary>
[RequireComponent(typeof(Image))]
public class WorldCursorInputModule : BaseInputModule 
{
    /// <summary>
    /// The button used to select items. Take this from the Unity Input settings. Note that it doesn't have to be a mouse button (it can be anything!)
    /// </summary>
    private static string SelectButton = "Fire1";

    /// <summary>
    /// The image that will represent the cursor
    /// </summary>
    public static Image CursorImage;

    // list of objects to invoke UI commands on
    private static GameObject targetObject;
    private GameObject prevTargetObject;

    // there should only be one of these input modules in existance at any time
    private static WorldCursorInputModule _singleton;

    // a reference to the currently active world cursor
    private static WorldCursor activeCursor;
    public static void SetActiveCursor(WorldCursor worldCursor, string selectButtonName)
    {
        activeCursor = worldCursor;
        SelectButton = selectButtonName;
        CursorImage = activeCursor.thisImage;
    }

    /// <summary>
    /// Is the cursor in the middle of a drag action?
    /// </summary>
    private bool isDragging = false;

    protected override void Awake()
    {
        if (_singleton == null)
            _singleton = this;
        else
            Debug.LogError("WORLD SPACE CURSOR: There are multiple WorldCursorInputModule components in the scene! Please ensure there is only one.");
    }

    // called each tick on active input module
    public override void Process()
    {
        // Commented this out because it's appaently intended behavior for the button to stay highlighted after being pressed, 
        // so we need to be able to select nothing to visually and reset the selection if necessary
        //if (targetObject == null)
          //  return;

        // This sends keyboard updates to the selected UI object
        SendUpdateEventToSelectedObject();

        if (Input.GetButtonDown(SelectButton))
        {
            //Debug.Log("cursor down");
            // We tapped the select button so grab the event data
            PointerEventData data = new PointerEventData(_singleton.eventSystem);
            data.position = activeCursor.transform.position;
            data.selectedObject = targetObject;

            //Debug.Log("pointer down");
            ExecuteEvents.Execute(targetObject, data, ExecuteEvents.pointerDownHandler);
            isDragging = false;
        }

        if(Input.GetButton(SelectButton))
        {  
            //Debug.Log("pointer still down...");
            // We tapped the select button so grab the event data
            PointerEventData data = new PointerEventData(_singleton.eventSystem);
            data.position = activeCursor.transform.position;
            data.selectedObject = targetObject;

            Scrollbar scrollbar = targetObject != null ? targetObject.GetComponent<Scrollbar>() : null;
            Slider slider = targetObject != null ? targetObject.GetComponent<Slider>() : null;
            // if it is a scrollbar/slider, send drag events
            if (scrollbar != null || slider != null)
            {
                if (activeCursor.IsMoving == true)
                {
                    if (isDragging == false)
                    {
                        //Debug.Log("Scrollbar drag start");
                        isDragging = true;
                        ExecuteEvents.Execute(targetObject, data, ExecuteEvents.beginDragHandler);
                    }
                    else if (isDragging == true)
                    {
                        //Debug.Log("Scrollbar drag continue");
                        ExecuteEvents.Execute(targetObject, data, ExecuteEvents.dragHandler);
                    }
                }
                else if(isDragging == true)
                {
                    isDragging = false;
                    ExecuteEvents.Execute(targetObject, data, ExecuteEvents.endDragHandler);
                    //Debug.Log("Scrolling drag end");
                }
            }
        }

        //  poll input manager to see if the select button has been pressed
        if (Input.GetButtonUp(SelectButton))
        {
            PointerEventData data = new PointerEventData(_singleton.eventSystem);
            data.position = activeCursor.transform.position;
            data.selectedObject = targetObject;
            // if not a scrollbar or slider, send a click event
            Scrollbar scrollbar = targetObject != null ? targetObject.GetComponent<Scrollbar>() : null;
            Slider slider = targetObject != null ? targetObject.GetComponent<Slider>() : null;
            if (scrollbar == null && slider == null)
            {
                //Debug.Log("pointer click");
                ExecuteEvents.Execute(targetObject, data, ExecuteEvents.pointerClickHandler);
            }
            // otherwise (if it is a scrollbar/slider), send drag events
            else
            {
                //Debug.Log("Scrolling pointer up");
                ExecuteEvents.Execute(targetObject, data, ExecuteEvents.pointerUpHandler);
                if (isDragging == true)
                {
                    isDragging = false;
                    ExecuteEvents.Execute(targetObject, data, ExecuteEvents.endDragHandler);
                    //Debug.Log("Scrolling drag end");
                }
            }
        }
        
    }

    /// <summary>
    /// Sends keyboard events to the selected UI Object.
    /// </summary>
    private void SendUpdateEventToSelectedObject()
    {
        if (eventSystem.currentSelectedGameObject == null)
            return;

        var data = GetBaseEventData();
        ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
    }


    /// <summary>
    /// Should be called whenever the cursor starts hovering over an object. Passes on the event when an object is being hovered over.
    /// </summary>
    /// <param name="obj">The object that this cursor is now hovering over.</param>
    public static void SetTargetObject(GameObject obj)
    {
        // if nothing is being hovered over or hovering over a new (but non-null) object, unhover the current one (targetObject)
        if ((obj == null && targetObject != null) || (targetObject != null) && (targetObject != obj))
        {
            UnhoverObject(targetObject);
        }

        // if an object is being hovered over...
        if (obj != null)
        {
            // Check if this is the same object that was hovered last time. If so, do nothing.
            if (obj == targetObject)
                return;

            // we've entered a new GUI object, so excute that event to highlight it
            HoverObject(obj);
        }

        targetObject = obj;
    }

    
    private static void HoverObject(GameObject obj)
    {
        PointerEventData pEnterEvent = new PointerEventData(_singleton.eventSystem);
        pEnterEvent.pointerEnter = obj;
        // Deprecated in Unity 5
        //pEnterEvent.worldPosition = CursorImage.transform.position;

        ExecuteEvents.Execute(obj, pEnterEvent, ExecuteEvents.pointerEnterHandler);

        //Debug.Log("Hovering object");
    }

    private static void UnhoverObject(GameObject obj)
    {
        PointerEventData pExitEvent = new PointerEventData(_singleton.eventSystem);
        // Deprecated in Unity 5
        //pExitEvent.worldPosition = CursorImage.transform.position;
        ExecuteEvents.Execute(obj, pExitEvent, ExecuteEvents.pointerExitHandler);
        //Debug.Log("Unhovering object");
    }
}
