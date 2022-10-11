using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RangeTracker : MonoBehaviour
{
    /* classe che tiene conto del raggio e calcola il transmission loss
     * si usa la prima "sfera" in quanto è parallela all'asse delle x. C'è da considerare quando il transmission loss
     * passa da perdita per propagazione sferica a perdita per propagazione ciilindrica
     */

    //reference to origin gameObj
    [SerializeField] public GameObject origin;
    [SerializeField] public float radius;

    [SerializeField] public Text[] text;


    [SerializeField] public float transmissionLoss;
    [SerializeField] public float sourceLevel;
    [SerializeField] public float sourceFrequencyLevel;
    void Start()
    {
        radius = 0;
        transmissionLoss = 0;
        sourceLevel = 0;
        sourceFrequencyLevel = 0;
        text = new Text[5];
        //in caso origin non è stato messo dall'inspector
        origin = GameObject.Find("Origin");

        text[0] = GameObject.Find("Raggio").GetComponent<Text>();
        text[1] = GameObject.Find("Propagazione").GetComponent<Text>();
        text[2] = GameObject.Find("TL").GetComponent<Text>();
        text[3] = GameObject.Find("SL").GetComponent<Text>();
        text[4] = GameObject.Find("SHz").GetComponent<Text>();
    }

    void Update()
    {
        radius = Vector3.Distance(origin.transform.position, transform.position);
        WriteData();
    }

    public void WriteData(){
        text[0].text = "Raggio: " + radius.ToString() + " m";

        if(radius >= 2000)
            text[1].text = "Propagazione: cilindrica";
        else
            text[1].text = "Propagazione: sferica";
        
        text[2].text = "TL: " + transmissionLoss.ToString() + " dB";
        text[3].text = "SL: " + sourceLevel.ToString() + " dB";
        text[4].text = "SHz: " + sourceFrequencyLevel.ToString() + " Hz";
        return;
    }


}