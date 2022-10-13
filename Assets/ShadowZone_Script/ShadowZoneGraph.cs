using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShadowZoneGraph
{
    [SerializeField] public static float[] speedAtDepths;
    [SerializeField] public static float[] angleVariationAtDepths;
    // Start is called before the first frame update
    
    static ShadowZoneGraph()
    {
        
        speedAtDepths = new float[3000];
        angleVariationAtDepths = new float[3000];

        float tmp = 10f;
        float tmp2 = 0f;
        for (int i = 0; i < 3000; i++)
        {
                speedAtDepths[i] = tmp;
                //tmp = tmp + 0.005f;
                
                if(i<800)
                    tmp2 = (i*45)/800;
                if(i>800){
                    tmp2 = 45 - ((i-800)*45)/800;
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
