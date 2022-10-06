using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManger : MonoBehaviour
{
    [SerializeField] public float [] speedAtDepths;
    [SerializeField] public float inputTemperature;
    [SerializeField] public float inputSalinity;
    [SerializeField] public float inputRangeMine;
    [SerializeField] public float inputRangeMax;
    [SerializeField] public GameObject WindowGraph;

    void Awake(){
        speedAtDepths = new float[2000];
        /*float tmp = 300;
        for(int i = 0; i < 500; i++){
            speedAtDepths[i] = tmp;
            tmp++;
        }
        for(int i = 501; i < 2000; i++){
            speedAtDepths[i] = tmp;
            tmp -= .5f;
        }*/
        WindowGraph = GameObject.Find("WindowGraph");
        Debug.Log(WindowGraph);
        computeSpeed();
        ComputeGraph();
    }

    public void ComputeGraph(){
        List<int> valueList = new List<int>();
        for(int i = 0; i < 2000; i++){
            valueList.Add((int)speedAtDepths[i]);
        }
        WindowGraph.GetComponent<WindowGraph>().ShowGraph(valueList);
    }

    public void computeSpeed(){
        for(int i=0 ; i<2000 ; i++){
            speedAtDepths[i] = 1450 + 4.61f * inputTemperature - 0.045f * inputTemperature * inputTemperature + 0.0182f * i + 1.3f * (inputSalinity - 35f);
        }
    }

}