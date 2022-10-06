using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    GameObject[] spheres;
    int shperesCounter=0;
    [SerializeField] GameObject Origin;

    void Start()
    {
        spheres = new GameObject[100];
        Origin = GameObject.Find("Origin");
        for(int i=-15; i>=-65; i=i-5){
            spheres[shperesCounter] = GameObject.Instantiate(Origin, Origin.transform.position, Quaternion.identity);
            spheres[shperesCounter].GetComponent<ConstantGradientSVP>().enabled = true;
            spheres[shperesCounter].transform.name = "Sphere "+shperesCounter+" degree "+i+"°";
            spheres[shperesCounter].GetComponent<ConstantGradientSVP>().theta = i;
            shperesCounter++;
        }
        Origin.SetActive(false);
    }
}
