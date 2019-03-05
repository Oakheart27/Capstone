using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Drags image under mouse
public class ItemDragHandler : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";

    private bool dragging = false;

    private Vector2 originalPosition;

    private Transform objectToDrag;

    private Image objectToDragImage;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    // NUITRACK STUFF
 /*   bool active = false;
    bool press = false;
    bool pressed = false;
    */
    void Start()
    {
        Application.runInBackground = true;
       // NuitrackManager.onHandsTrackerUpdate += NuitrackManager_onHandsTrackerUpdate;
    }
    

    void Update()
    {
        //nuitrack.HandTrackerData handTrackerData;

        // Begin to drag object
        if (Input.GetMouseButtonDown(0) && dragging == false)
        {
            //if(Input.GetMouseButtonUp(0))
            //{
                objectToDrag = GetDraggableTransformUnderMouse();

                if (objectToDrag != null)
                {
                    dragging = true;

                    objectToDrag.SetAsLastSibling();

                    originalPosition = objectToDrag.position;

                    objectToDragImage = objectToDrag.GetComponent<Image>();

                    objectToDragImage.raycastTarget = false;
                }
            //}
            
        }

        // Drag object
        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        // Drop object
        if (Input.GetMouseButtonUp(0) && dragging == true)
        {
            //if(Input.GetMouseButtonUp(0))
            //{
                objectToDrag.position = Input.mousePosition;

                objectToDragImage.raycastTarget = true;

                dragging = false;
            //}
            
        }
    } // end Update()

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;


    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }
    


} // End ItemDragHandler class


