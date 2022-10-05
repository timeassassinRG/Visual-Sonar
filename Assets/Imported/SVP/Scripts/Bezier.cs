using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(LineRenderer))]
//nascondi i warning CS0618
#pragma warning disable 0618
public class Bezier : MonoBehaviour
{
    public Transform[] controlPoints;
    public LineRenderer lineRenderer;
    
    private int curveCount = 0;    
    private int layerOrder = 0;
    private int SEGMENT_COUNT = 50;
    
        
    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
        curveCount = (int)controlPoints.Length / 3;
    }

    public void SetPoints(Transform[] cp){
        controlPoints = cp;
    }

    public void SetCP3(Vector3 pos){
        controlPoints[2].position = pos;
    }

    public void SetCP4(Vector3 pos){
        controlPoints[3].position = pos;
    }

    void Update()
    {
        DrawCurve();
    }
    
    void DrawCurve()
    {
        for (int j = 0; j <curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints [nodeIndex].position, controlPoints [nodeIndex + 1].position, controlPoints [nodeIndex + 2].position, controlPoints [nodeIndex + 3].position);
                lineRenderer.SetVertexCount(((j * SEGMENT_COUNT) + i));
                lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
            }
            
        }
    }
        
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
        
        return p;
    }
}