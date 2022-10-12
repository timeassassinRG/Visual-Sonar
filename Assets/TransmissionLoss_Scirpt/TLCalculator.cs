using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLCalculator : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float sourceFrequencyLevel; //che è in KHz
    [SerializeField] float sourceLevel; //che è in dB
    // Start is called before the first frame update
    void Start()
    {
        if(sourceFrequencyLevel == 0)
            sourceFrequencyLevel = 10;
        if(sourceLevel == 0)
            sourceLevel = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSourceFrequencyLevel(){
        return sourceFrequencyLevel;
    }
}
