using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SVPGraph
{
    [SerializeField] public static float[] speedAtDepths;
    [SerializeField] public static float[] angleVariationAtDepths;
    // Start is called before the first frame update
    
    static SVPGraph()
    {
        
        speedAtDepths = new float[3000];
        angleVariationAtDepths = new float[3000];

        float tmp = 2f;
        float tmp2 = 10f;
        for (int i = 0; i < 3000; i++)
        {
                speedAtDepths[i] = tmp;
                //tmp = tmp + 0.005f;
                tmp2 = (i*60)/1500;
                if(i > 1500){
                    tmp2 = 60;
                }
                angleVariationAtDepths[i] = tmp2;
                
                //tmp += 0.001f;
                /*if (tmp2<90)
                    tmp2 += 0.1f;*/
                
                //Debug.Log("speed at depth " + i + " is " + speedAtDepths[i]);
                //Debug.Log("angle variation at depth " + i + " is " + angleVariationAtDepths[i]);
        }               
    }
}