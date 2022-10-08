using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager3D : MonoBehaviour
{
    public LineRenderer linerenderer;
    public GameObject origin;
    [SerializeField] public GameObject[][] targets;

    private float range = 0;
    private float rangeMax = 1000;
    [SerializeField] private float speed;

    [SerializeField] private int numeroIntervalli; //standard 11
    [SerializeField] private float sfasamento;      //standard 15

    void Awake()
    {
        targets = new GameObject[numeroIntervalli][];
        for(int c=0; c<numeroIntervalli; c++)
            targets[c] = new GameObject[numeroIntervalli];

        linerenderer = GetComponent<LineRenderer>();
        origin = GameObject.Find("Origin");
        Debug.Log("origin: " + origin);
        for(int i=0; i<numeroIntervalli; i++)
        {
            for(int j=0; j<numeroIntervalli; j++)
            {
                targets[i][j] = Instantiate(origin, new Vector3(0, 0, 5), Quaternion.identity);
                targets[i][j].AddComponent<LineRenderer>();
                targets[i][j].gameObject.name = "Target " + i + "." + j;
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            ResetRange();
            for(int i=0; i<numeroIntervalli; i++)
            {
                for(int j=0; j<numeroIntervalli; j++)
                {
                    StartCoroutine(Run(i, j, ((i + 1)*sfasamento), ((j + 1)*sfasamento)));
                }
            }
        }
    }

    public IEnumerator Run(int i, int j,float gradiX, float gradiZ)
    {
        //Debug.Log("target: "+ (i+1) + "." + (j+1) + " | gradiX: " + gradiX +  " gradiZ: " + gradiZ + " | radiantiX: " + (Mathf.Deg2Rad*gradiX)+ " | radiantiZ: " + (Mathf.Deg2Rad*gradiZ));
        while(range < rangeMax)
        {
            range += speed;
            targets[i][j].transform.position = new Vector3((range * Mathf.Cos(Mathf.Deg2Rad * gradiX)), (-1)*(range * Mathf.Sin(Mathf.Deg2Rad * gradiZ)), (range * Mathf.Cos(Mathf.Deg2Rad * gradiZ)));
            targets[i][j].GetComponent<LineRenderer>().startColor = Color.green;
            targets[i][j].GetComponent<LineRenderer>().SetPosition(0, origin.transform.position);
            targets[i][j].GetComponent<LineRenderer>().SetPosition(1, targets[i][j].transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        
        yield return null;
    }

    void ResetRange()
    {
        range = 0;
    }
}
