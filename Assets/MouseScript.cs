using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    private Transform selected = null;
    private Vector3 offset;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                selected = hit.transform;
                offset = selected.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = hit.transform.position;
                
            }
        }

        if (selected != null)
        {
            lineRenderer.SetPosition(0, selected.position);
            lineRenderer.SetPosition(1, selected.position + new Vector3(1, 0, 0));
            lineRenderer.SetPosition(2, selected.position + new Vector3(1, 1, 0));
            lineRenderer.SetPosition(3, selected.position + new Vector3(0, 1, 0));
        }
    }
}
