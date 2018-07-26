// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    //
    // VARIABLES
    //

    public Camera cam;

    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;       // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?
    private bool isZooming;     // Is the camera zooming?

    private bool toggleCamera;


    void Start()
    {
        toggleCamera = false;
    }

    //
    // UPDATE
    //

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggleCamera = !toggleCamera;
        }
        if (toggleCamera)
        {
            cam.enabled = true;


            // Get the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (!IsPointerOverUIObject ()) {
						// Get mouse origin
						mouseOrigin = Input.mousePosition;
						isRotating = true;
					}
				}
            }

            // Get the right mouse button
            if (Input.GetMouseButtonDown(1))
            {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (!IsPointerOverUIObject ()) {
						// Get mouse origin
						mouseOrigin = Input.mousePosition;
						isPanning = true;
					}
				}
            }

            // Get the middle mouse button
            if (Input.GetMouseButtonDown(2))
            {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (!IsPointerOverUIObject ()) {
						// Get mouse origin
						mouseOrigin = Input.mousePosition;
						isZooming = true;
					}
				}
            }

            // Disable movements on button release
            if (!Input.GetMouseButton(0)) isRotating = false;
            if (!Input.GetMouseButton(1)) isPanning = false;
            if (!Input.GetMouseButton(2)) isZooming = false;

            // Rotate camera along X and Y axis
            if (isRotating)
            {
                Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
                transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
            }

            // Move the camera on it's XY plane
            if (isPanning)
            {
                Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
                transform.Translate(move, Space.Self);
            }

            // Move the camera linearly along Z axis
            if (isZooming)
            {
                Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                Vector3 move = pos.y * zoomSpeed * transform.forward;
                transform.Translate(move, Space.World);
            }
        } else
        {
            cam.enabled = false;
        }
    }

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
		eventDataCurrentPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;   
	}
}