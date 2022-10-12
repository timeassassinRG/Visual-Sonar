using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirlceManager : MonoBehaviour
{
    [SerializeField] GameObject[] spheres;
    [SerializeField] GameObject Origin;
    int shperesCounter = 0;

    int vertexCount = 0;

    [SerializeField] LineRenderer lineRenderer;

    bool ok;
    void Start()
    {
        spheres = new GameObject[360];
        Origin = GameObject.Find("Origin");
        for (int i = 0; i < 360; i = i + 2)
        {
            spheres[shperesCounter] = GameObject.Instantiate(Origin, Origin.transform.position, Quaternion.identity);
            spheres[shperesCounter].transform.name = "Sphere " + shperesCounter + " degree " + i + "°";
            if (spheres[shperesCounter].GetComponent<TLMovement>() != null)
            {
                spheres[shperesCounter].GetComponent<TLMovement>().enabled = true;
                spheres[shperesCounter].GetComponent<TLMovement>().theta = i;
                //se la sfera ha angolo ° aggiuni uno script per tenere conto del raggio e calcolare successivamente il transmission loss
                if (i == 0)
                {
                    spheres[shperesCounter].AddComponent<RangeTracker>();
                    spheres[shperesCounter].transform.name = "MainSphere";
                }
            }
            shperesCounter++;
        }
        Origin.GetComponent<TLMovement>().enabled = false;
        lineRenderer = GetComponent<LineRenderer>();
        vertexCount = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.red;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 20f;
        lineRenderer.endWidth = 20f;
        lineRenderer.positionCount = 0;
        ok = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ok)
        {
            ok = true;
        }
        if (ok)
        {
            lineRenderer.positionCount = shperesCounter + 1;
            for (int i = 0; i < shperesCounter; i++)
                lineRenderer.SetPosition(i, spheres[i].transform.position);
            lineRenderer.SetPosition(shperesCounter, spheres[0].transform.position);
        }
    }

}
