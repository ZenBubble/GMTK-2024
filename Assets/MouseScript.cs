using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

// handles the scaling tool
public class MouseScript : MonoBehaviour
{
    private GameObject selected;
    private Vector3 offset;
    private LineRenderer lineRenderer;
    private HandleScript dragging;
    [SerializeField] private Transform handleTransform;
    [SerializeField] private LayerMask selectableLayers;
    [SerializeField] private LayerMask handleLayer;
    [SerializeField] private HandleScript[] handles;
    private Vector3 size;
    private float widthOffset;
    private float heightOffset;

    // Start is called before the first frame update
    void Start()
    {
        // set the line renderer and hide it on start when nothing is selected
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if left click has been pressed
        if (Input.GetMouseButtonDown(0))
        {
            // raycast to check if handle has been pressed
            RaycastHit2D handleHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                float.PositiveInfinity, handleLayer);

            // raycast to check if a selectable object has been pressed
            RaycastHit2D objectHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                float.PositiveInfinity, selectableLayers);

            if (handleHit)
            {
                // set dragging to the handle that was pressed on and record the offset of the mouse from center of the handle
                dragging = handleHit.collider.gameObject.GetComponent<HandleScript>();
                offset = dragging.gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (objectHit)
            {
                // select the object and create the outline and handles
                selected = objectHit.collider.gameObject;
                offset = selected.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = objectHit.transform.position;
                lineRenderer.enabled = true;

                updateHandles();
            }
            else
            {
                // if nothing has been clicked, remove the selectioin
                selected = null;
                lineRenderer.enabled = false;
                disableHandles();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // detect if the handle has been released
            dragging = null;
        }

        if (dragging != null)
        {
            // while the handle is being dragged, update the handle position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging.drag(mousePos, offset);
        }

        if (selected != null)
        {
            // while an object is selected, place the box and handles in the right position
            updateBox();
            updateHandles();
        }
    }

    private void updateHandles()
    {
        foreach (HandleScript hs in handles)
        {
            hs.place(selected);
        }
    }

    private void disableHandles()
    {
        foreach (HandleScript hs in handles)
        {
            hs.disableHandle();
        }
    }

    private void updateBox()
    {
        // draw a box around selected object with a linerenderer
        size = selected.GetComponent<SpriteRenderer>().bounds.size;
        widthOffset = size.x / 2f;
        heightOffset = size.y / 2f;

        lineRenderer.SetPosition(0, selected.transform.position + new Vector3(-widthOffset, heightOffset, 0));
        lineRenderer.SetPosition(1, selected.transform.position + new Vector3(widthOffset, heightOffset, 0));
        lineRenderer.SetPosition(2, selected.transform.position + new Vector3(widthOffset, -heightOffset, 0));
        lineRenderer.SetPosition(3, selected.transform.position + new Vector3(-widthOffset, -heightOffset, 0));
    }
}
