using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the bounding box for editable platforms
/// </summary>
public class EditablePlatformScript : MonoBehaviour
{
    [SerializeField] private float maxXRel;
    [SerializeField] private float minXRel;
    [SerializeField] private float maxYRel;
    [SerializeField] private float minYRel;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LineRenderer lineRenderer;
    
    private float originalWidth;
    private float originalHeight;
    private float originalPosX;
    private float originalPosY;
    private Vector2 size;
    
    /// <summary>
    /// Draws boundiing box but hides it until the platform is selected
    /// </summary>
    private void Start()
    {
        initValues();
        lineRenderer.positionCount = 4;
        lineRenderer.SetPosition(0, new Vector3(getMinX(), getMinY(), 0));
        lineRenderer.SetPosition(1, new Vector3(getMinX(), getMaxY(), 0));
        lineRenderer.SetPosition(2, new Vector3(getMaxX(), getMaxY(), 0));
        lineRenderer.SetPosition(3, new Vector3(getMaxX(), getMinY(), 0));
        disableBoundingBox();
    }

    /// <summary>
    /// Converts from values relative to the edge of the platform to absolute global coordinates
    /// </summary>
    /// <returns></returns>
    public float getMaxX()
    {
        return originalPosX + size.x / 2f + maxXRel;
    }

    public float getMinX()
    {
        return originalPosX - size.x / 2f - minXRel;
    }

    public float getMaxY()
    {
        return originalPosY + size.y / 2f + maxYRel;
    }

    public float getMinY()
    {
        return originalPosY - size.y / 2f - minYRel;
    }

    /// <summary>
    /// Displays the bounding box in the editor
    /// </summary>
    private void OnDrawGizmos()
    {
        initValues();
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        Gizmos.DrawWireCube(new Vector3((getMinX() + getMaxX()) / 2f, (getMinY() + getMaxY()) / 2f, 0),
            new Vector3(getMaxX() - getMinX(), getMaxY() - getMinY(), 0.01f));
    }

    /// <summary>
    /// Sets initial references, needed for gizmos
    /// </summary>
    private void initValues()
    {
        size = boxCollider.bounds.size;
        originalPosX = transform.position.x;
        originalPosY = transform.position.y;
    }

    /// <summary>
    /// Enable the bounding box
    /// </summary>
    public void enableBoundingBox()
    {
        lineRenderer.enabled = true;
    }

    /// <summary>
    /// Disable the bounding box
    /// </summary>
    public void disableBoundingBox()
    {
        lineRenderer.enabled = false;
    }
}
