using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class PositiveGradient : MonoBehaviour
{
     public LineRenderer linerenderer;
    public GameObject origin;
    [SerializeField]public GameObject empty;
    [SerializeField] public List<GameObject> normalEnds;
    [SerializeField] public List<GameObject> targets;
    [SerializeField] public List<GameObject> BeziersManager;

    [SerializeField] float pGradient;
    private float range = 0;
    private float rangeMax = 1000;
    private float speed = 0.1f;

    void Awake()
    {

        linerenderer = GetComponent<LineRenderer>();
        origin = GameObject.Find("Origin");
        empty = GameObject.Find("Empty");
        for (int i = 0; i < 11; i++)
        {
            normalEnds.Add(Instantiate(empty, new Vector3(0, 0, 5), Quaternion.identity));
            targets.Add(Instantiate(origin, new Vector3(0, 0, 5), Quaternion.identity));
            BeziersManager.Add(Instantiate(empty, new Vector3(0, 0, 5), Quaternion.identity));
            
            targets[i].AddComponent<LineRenderer>();
            targets[i].gameObject.name = "PositiveSVPtarget " + i;

            BeziersManager[i].AddComponent<Bezier>();
            BeziersManager[i].gameObject.name = "SVP_BezierManager" + i;
            normalEnds[i].gameObject.name = "NE" + i;
            
            Transform[] points = new Transform[4];
            points[0] = origin.transform;
            points[1] = origin.transform;
            points[2] = normalEnds[i].transform;
            points[3] = targets[i].transform;
            BeziersManager[i].GetComponent<Bezier>().SetPoints(points);


        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            ResetRange();
            foreach (GameObject target in targets)
            {
                StartCoroutine(Run(targets.IndexOf(target), ((targets.IndexOf(target) + 1)*15)));
            }
        }
    }

    public IEnumerator Run(int i, float gradi)
    {
        //float positiveSpeed = 0;
        Debug.Log("PositiveSVPtarget : "+ (i+1) + " | gradi: " + gradi + " | radianti: " + (Mathf.Deg2Rad*gradi));
        while(range < rangeMax)
        {
            range += speed;
            //siccome è positive gradient
            //float pGradient = Mathf.Abs(targets[i].transform.position.y);
            //pGradient += speed;

            pGradient = Mathf.Abs(targets[i].transform.position.y);
            targets[i].transform.position = new Vector3((range * Mathf.Cos(Mathf.Deg2Rad * gradi)), ((-1)* (range * Mathf.Sin(Mathf.Deg2Rad * gradi)))+pGradient, 5);
            normalEnds[i].transform.position = new Vector3(((range * Mathf.Cos(Mathf.Deg2Rad * gradi))/2), ((-1)* (range * Mathf.Sin(Mathf.Deg2Rad * gradi)))/2, 5);
            BeziersManager[i].GetComponent<Bezier>().SetCP3(normalEnds[i].transform.position);
            BeziersManager[i].GetComponent<Bezier>().SetCP4(targets[i].transform.position);
            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }

    void ResetRange()
    {
        range = 0;
    }
}
