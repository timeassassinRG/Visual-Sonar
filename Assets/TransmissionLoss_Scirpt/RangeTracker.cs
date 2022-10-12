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


    [SerializeField] public float PropagationTransmissionLoss;
    [SerializeField] public float sourceLevel;
    [SerializeField] public float sourceFrequencyLevel;

    [SerializeField] float maxSphericTransmissionLoss;
    [SerializeField] float AbsorptionCoefficent;

    float TLtotal;
    void Start()
    {
        radius = 0;
        PropagationTransmissionLoss = 0;
        sourceLevel = 0;
        sourceFrequencyLevel = GameObject.Find("TransmissionLossCalculator").GetComponent<TLCalculator>().GetSourceFrequencyLevel();
        CalculateAbsorptionCoefficent();
        text = new Text[7];
        //in caso origin non è stato messo dall'inspector
        origin = GameObject.Find("Origin");

        text[0] = GameObject.Find("Raggio").GetComponent<Text>();
        text[1] = GameObject.Find("Propagazione").GetComponent<Text>();
        text[2] = GameObject.Find("TL").GetComponent<Text>();
        text[3] = GameObject.Find("SL").GetComponent<Text>();
        text[4] = GameObject.Find("SHz").GetComponent<Text>();
        text[5] = GameObject.Find("Alpha").GetComponent<Text>();
        text[6] = GameObject.Find("TLtot").GetComponent<Text>();
    }

    void Update()
    {
        radius = Vector3.Distance(origin.transform.position, transform.position);
        CalculateAtRunTime();
        WriteData();
    }

    public void WriteData(){
        radius = Mathf.Round(radius);
        text[0].text = "Raggio: " + radius.ToString() + " m";

        if(radius >= 2000)
            text[1].text = "Tipo di propagazione: cilindrica";
        else
            text[1].text = "Tipo di Propagazione: sferica";
        
        PropagationTransmissionLoss = Mathf.Clamp(PropagationTransmissionLoss, 0, Mathf.Infinity);
        text[2].text = "Perdita di trasmissione per propagazione: " + PropagationTransmissionLoss.ToString() + " dB";
        text[3].text = "SL: " + sourceLevel.ToString() + " dB";
        text[4].text = "Source Frequency Level: " + sourceFrequencyLevel.ToString() + " kHz";
        text[5].text = "Coefficente di assorbimento:: " + AbsorptionCoefficent.ToString() + " dB/m";
        TLtotal = Mathf.Clamp(TLtotal, 0, Mathf.Infinity);
        text[6].text = "Perdita Totale: " + TLtotal + " dB";
        return;
    }

    public void CalculateAtRunTime(){
        CalculateTL();
        CalculateTotalTL();
    }

    public void CalculateTL(){
        //calcola il transmission loss
        //perdita per propagazione sferica
        
        if(radius <= 2000){
            PropagationTransmissionLoss = 20 * Mathf.Log10(radius);
        }
        //perdita per propagazione cilindrica
        else{
            PropagationTransmissionLoss = 20 * Mathf.Log10(2000) + 10 * Mathf.Log10(radius-2000);
        }
        return;
    }

    public void CalculateAbsorptionCoefficent(){
        //calcola il coefficiente di assorbimento
        AbsorptionCoefficent = (((float)0.036*Mathf.Pow(sourceFrequencyLevel,2))/(Mathf.Pow(sourceFrequencyLevel,2)+3600))+((float)3.2*Mathf.Pow(10,-7)*Mathf.Pow(sourceFrequencyLevel,2));
        return;
    }

    public void CalculateTotalTL(){
        //calcola il transmission loss totale
        TLtotal = PropagationTransmissionLoss + AbsorptionCoefficent * radius;
        return;
    }
}