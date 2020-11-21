using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
    // Line renderer component
    private LineRenderer lr;

    private void Awake()
    {
        // Get line renderer component
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points= new Vector3[2];

        // Set the points and position of the line
        points[1] = new Vector3(0,0,0);
        points[0] = endPoint * 5;
        lr.SetPositions(points);
        lr.startWidth = 3.0f;
        lr.endWidth = lr.startWidth * 1.5f;
        
    }

    public void EndRender()
    {
        lr.positionCount = 0;
    }
}
