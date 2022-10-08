using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer linerenderer;
    public GameObject origin;
    [SerializeField] public List<GameObject> targets;

    private float range = 0;
    private float rangeMax = 1000;
    private float speed = 0.1f;

    void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
        origin = GameObject.Find("Origin");
        for (int i = 0; i < 11; i++)
        {
            targets.Add(Instantiate(origin, new Vector3(0, 0, 5), Quaternion.identity));
            targets[i].AddComponent<LineRenderer>();
            targets[i].gameObject.name = "Normal Target " + i;
            //targets[i].GetComponent<LineRenderer>().material = linerenderer.material;
            //targets[i].GetComponent<LineRenderer>().startColor = linerenderer.startColor;
            //targets[i].GetComponent<LineRenderer>().endColor = linerenderer.endColor;
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
        Debug.Log("target: "+ (i+1) + "| gradi: " + gradi + "| radianti: " + (Mathf.Deg2Rad*gradi));
        while(range < rangeMax)
        {
            range += speed;
            targets[i].transform.position = new Vector3((range * Mathf.Cos(Mathf.Deg2Rad * gradi)), ((-1)* (range * Mathf.Sin(Mathf.Deg2Rad * gradi))), 5);
            targets[i].GetComponent<LineRenderer>().SetPosition(0, origin.transform.position);
            targets[i].GetComponent<LineRenderer>().SetPosition(1, targets[i].transform.position);
            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }

    void ResetRange()
    {
        range = 0;
    }
}