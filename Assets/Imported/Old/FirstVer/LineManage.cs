using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManage : MonoBehaviour
{
    void Start()
    {
        GameObject p1 = GameObject.Find("TShip1");
        //draw a line beetwen p1 e p2
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.SetPosition(0, new Vector3(0, 0.5f, 0));
        lineRenderer.SetPosition(1, new Vector3(0, -3, 0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
