using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// Manage behaviour of the draggable handles that allow scaling of objects
/// </summary>
public class HandleScript : MonoBehaviour
{
    // currently selected object
    private GameObject selected;
    private Vector3 selectedSize;
    // location of the handle. (-1, -1) is bottom left, (1, 1) is top right.
    [SerializeField] private int handlePosX;
    [SerializeField] private int handlePosY;
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    // Start is called before the first frame update
    void Start()
    {
        // disable the handles at the start where nothing is selected
        disableHandle();
    }

    /// <summary>
    /// places the handle in corresponding location given a selected object
    /// </summary>
    /// <param name="s"></param>
    public void place(GameObject s)
    {
        // enable the handle first
        enableHandle();
        // set the selected object
        selected = s;
        selectedSize = selected.GetComponent<SpriteRenderer>().bounds.size;
        float widthOffset = selectedSize.x / 2f;
        float heightOffset = selectedSize.y / 2f;

        // place handle in correct location
        transform.position = selected.transform.position + 
            new Vector3(handlePosX * widthOffset, handlePosY * heightOffset, 0);

        // get the bounds that the platform can move in
        EditablePlatformScript platformScript = selected.GetComponent<EditablePlatformScript>();
        maxX = platformScript.getMaxX();
        minX = platformScript.getMinX();
        maxY = platformScript.getMaxY();
        minY = platformScript.getMinY();
    }

    /// <summary>
    /// update handle position given mouse location
    /// </summary>
    /// <param name="mousePos"></param>
    /// <param name="offset"></param>
    public void drag(Vector3 mousePos, Vector3 offset)
    {
        // update the stored size of the selected object
        selectedSize = selected.GetComponent<SpriteRenderer>().bounds.size;

        // calculate how much the handle needs to be dragged
        float xPixelShift = mousePos.x - transform.position.x + offset.x;
        float yPixelShift = mousePos.y - transform.position.y + offset.y;

        // check if dragging the handle would bring the object below the minimum size. If so, set the image to the minimum size
        if (selectedSize.x + handlePosX * xPixelShift < 0.2)
        {
            xPixelShift = -1 * handlePosX * (selectedSize.x - 0.2f);
        } 
        else if (transform.position.x + xPixelShift > maxX && isBoundingBoxEnabled())
        {
            xPixelShift = maxX - transform.position.x;
        }
        else if (transform.position.x + xPixelShift < minX && isBoundingBoxEnabled())
        {
            xPixelShift = -1 * (transform.position.x - minX);
        }

        if (selectedSize.y + handlePosY * yPixelShift < 0.2)
        {
            yPixelShift = -1 * handlePosY * (selectedSize.y - 0.2f);
        }
        else if (transform.position.y + yPixelShift > maxY && isBoundingBoxEnabled())
        {
            yPixelShift = maxY - transform.position.y;
        }
        else if (transform.position.y + yPixelShift < minY && isBoundingBoxEnabled())
        {
            yPixelShift = -1 * (transform.position.y - minY);
        }

        // resize the object and move handle to the new location
        resizeSelected(xPixelShift, yPixelShift);
        moveHandle(xPixelShift, yPixelShift);
    }

    /// <summary>
    /// resize the selected object given the shift in position of the handle
    /// </summary>
    /// <param name="xPixelShift"></param>
    /// <param name="yPixelShift"></param>
    private void resizeSelected(float xPixelShift, float yPixelShift)
    {
        // original dimensions of the object. used to calculate how much the scale needs to be adjusted
        float originalWidth = selectedSize.x / selected.transform.localScale.x;
        float originalHeight = selectedSize.y / selected.transform.localScale.y;

        // new scale of the object
        float newScaleX = selected.transform.localScale.x + handlePosX * xPixelShift / originalWidth;
        float newScaleY = selected.transform.localScale.y + handlePosY * yPixelShift / originalHeight;

        // new location of the object. keeps the opposite corner of the image from moving. the center of the object moves
        // exactly half as much as the corner that we are dragging
        float newPosX = selected.transform.position.x + xPixelShift / 2f;
        float newPosY = selected.transform.position.y + yPixelShift / 2f;

        // apply the new scale and position to the object
        selected.transform.localScale = new Vector3(newScaleX, newScaleY, selected.transform.localScale.z);
        selected.transform.position = new Vector3(newPosX, newPosY, selected.transform.position.z);
    }

    /// <summary>
    /// move the handle based on the shift of the mouse
    /// </summary>
    /// <param name="xPixelShift"></param>
    /// <param name="yPixelShift"></param>
    private void moveHandle(float xPixelShift, float yPixelShift)
    {
        transform.position = new Vector3(transform.position.x + xPixelShift, transform.position.y + yPixelShift,
            transform.position.z);
    }

    /// <summary>
    /// enable handles
    /// </summary>
    private void enableHandle()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    /// <summary>
    /// disable handles
    /// </summary>
    public void disableHandle()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// checks if bounding boxes are enabled (toggled in inspector)
    /// </summary>
    /// <returns></returns>
    private Boolean isBoundingBoxEnabled()
    {
        return transform.parent.gameObject.GetComponent<MouseScript>().isBoundingBoxEnabled();
    }
}
