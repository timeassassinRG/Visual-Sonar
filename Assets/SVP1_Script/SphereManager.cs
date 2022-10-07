using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    GameObject[] spheres;
    int shperesCounter = 0;
    [SerializeField] GameObject Origin;

    void Start()
    {
        spheres = new GameObject[100];
        Origin = GameObject.Find("Origin");
        for (int i = -5; i >= -85; i = i - 1)
        {
            spheres[shperesCounter] = GameObject.Instantiate(Origin, Origin.transform.position, Quaternion.identity);
            AssignCorrectComponent(i);            
            spheres[shperesCounter].transform.name = "Sphere " + shperesCounter + " degree " + i + "°";
            shperesCounter++;
        }
        Origin.SetActive(false);
    }

    void AssignCorrectComponent(int i){
        if (spheres[shperesCounter].GetComponent<ConstantGradientSVP>() != null)
            {
                spheres[shperesCounter].GetComponent<ConstantGradientSVP>().enabled = true;
                spheres[shperesCounter].GetComponent<ConstantGradientSVP>().theta = i;
            }
        if (spheres[shperesCounter].GetComponent<ShadowZoneMovement>() != null)
            {
                spheres[shperesCounter].GetComponent<ShadowZoneMovement>().enabled = true;
                spheres[shperesCounter].GetComponent<ShadowZoneMovement>().theta = i;
            }
    }
}