using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calcolatore : MonoBehaviour
{
    //condizioni attuali
    [SerializeField] InputField seaState;
    [SerializeField] InputField depth;
    //dati target
    [SerializeField] InputField radiatedNoiseSourceLevel;
    [SerializeField] InputField radiatedNoiseFrequency;
    [SerializeField] InputField targetStrength;
    [SerializeField] InputField targetDetectionRange;

    //dati sonar attivo e passivo
    [SerializeField] InputField sourceLevel;
    [SerializeField] InputField frequency;
    [SerializeField] InputField selfNoiseActive;
    [SerializeField] InputField selfNoisePassive;
    [SerializeField] InputField DI_active;
    [SerializeField] InputField DI_passive;
    [SerializeField] InputField DT_active;
    [SerializeField] InputField DT_passive;

    //riferimento al risultato
    [SerializeField] Text result;

    float seaStateValue;
    float depthValue;
    float radiatedNoiseSourceLevelValue;
    float radiatedNoiseFrequencyValue;
    float targetStrengthValue;
    float targetDetectionRangeValue;
    float sourceLevelValue;
    float frequencyValue;
    float selfNoiseActiveValue;
    float selfNoisePassiveValue;
    float DI_activeValue;
    float DI_passiveValue;
    float DT_activeValue;
    float DT_passiveValue;
    string resultValue;

    public void Awake(){
        setDefaultValues();
    }

    public void main(){
        getValues();
        //per determinare che sonar usare è necessario calcolare il FOM e la perdita di trasimissione per ogni modalità
        /*calcoliamo la transimission loss per ogni modalità:
        * controlliamo il target detection range e la profondità dell'acqua
        * nel caso in cui il target detection range è maggiore della profondità dell'acqua, bisognerà usare
        * l'equazione del cylindrical spreading
        */
        double absorptionCoefficentActive = ((0.036*Mathf.Pow(frequencyValue,2))/(Mathf.Pow(frequencyValue,2)+3600))+(3.2*Mathf.Pow(10,-7)*Mathf.Pow(frequencyValue,2));
        double absorptionCoefficentPassive = ((0.036*Mathf.Pow(radiatedNoiseFrequencyValue/1000,2))/(Mathf.Pow(radiatedNoiseFrequencyValue/1000,2)+3600))+(3.2*Mathf.Pow(10,-7)*Mathf.Pow(radiatedNoiseFrequencyValue/1000,2));
        Debug.Log("frequencyValue: "+frequencyValue);
        Debug.Log("radiatedNoiseFrequencyValue: "+radiatedNoiseFrequencyValue);
        Debug.Log("absorptionCoefficentActive: "+absorptionCoefficentActive);
        Debug.Log("absorptionCoefficentPassive: "+absorptionCoefficentPassive);
        double TrassmissionLossActive = 10 * Mathf.Log10(targetDetectionRangeValue) + (absorptionCoefficentActive * targetDetectionRangeValue);
        double TrassmissionLossPassive = 10 * Mathf.Log10(targetDetectionRangeValue) + (absorptionCoefficentPassive * targetDetectionRangeValue); 
        TrassmissionLossActive *= 2; //andata è ritorno
        Debug.Log("TL attivo: "+TrassmissionLossActive);
        Debug.Log("TL passivo: "+TrassmissionLossPassive);

        //nota il TL dipende solo dal detection range e la frequenza del segnale
        //Calcoliamo il FOM per la modalità attiva e il FOM per la modalità passiva
        //rimane da calcolare il NL per la modalità attiva e il NL per la modalità passiva
        //per questo dobbiamo sfruttare le curve di wenz ed il nomogram (non so come farlo)
        //il NL si calcola NL = AN  SN. 
        double NL_ACTIVE = 58.65;
        double NL_PASSIVE = 62.75;
        Debug.Log("NL Attivo: "+NL_ACTIVE);
        Debug.Log("NL Passivo: "+NL_PASSIVE);
        //calcolo FOM
        double FOM_ACTIVE = sourceLevelValue + targetStrengthValue - NL_ACTIVE + DI_activeValue - DT_activeValue;
        double FOM_PASSIVE = sourceLevelValue - NL_PASSIVE + DI_passiveValue - DT_passiveValue;
        Debug.Log("FOM Attivo: "+FOM_ACTIVE);
        Debug.Log("FOM Passivo: "+FOM_PASSIVE);
    
        resultValue = "FOM Attivo: "+FOM_ACTIVE+" TL Attivo: "+TrassmissionLossActive+ "\nFOM Passivo: "+FOM_PASSIVE+" TL Passivo: "+TrassmissionLossPassive;
        result.text = resultValue;
    }

    public void getValues(){
        seaStateValue = float.Parse(seaState.text);
        depthValue = float.Parse(depth.text);
        radiatedNoiseSourceLevelValue = float.Parse(radiatedNoiseSourceLevel.text);
        radiatedNoiseFrequencyValue = float.Parse(radiatedNoiseFrequency.text);
        targetStrengthValue = float.Parse(targetStrength.text);
        targetDetectionRangeValue = float.Parse(targetDetectionRange.text);
        sourceLevelValue = float.Parse(sourceLevel.text);
        frequencyValue = float.Parse(frequency.text);
        selfNoiseActiveValue = float.Parse(selfNoiseActive.text);
        selfNoisePassiveValue = float.Parse(selfNoisePassive.text);
        DI_activeValue = float.Parse(DI_active.text);
        DI_passiveValue = float.Parse(DI_passive.text);
        DT_activeValue = float.Parse(DT_active.text);
        DT_passiveValue = float.Parse(DT_passive.text);
    }

    public void setDefaultValues(){
        seaState.text = "2";
        depth.text = "200";
        radiatedNoiseSourceLevel.text = "100";
        radiatedNoiseFrequency.text = "500";
        targetStrength.text = "15";
        targetDetectionRange.text = "10000";
        sourceLevel.text = "110";
        frequency.text = "1,5";
        selfNoiseActive.text = "50";
        selfNoisePassive.text = "50";
        DI_active.text = "10";
        DI_passive.text = "10";
        DT_active.text = "-2";
        DT_passive.text = "-3";
    }

    public void printAllValues(){
        Debug.Log("seaStateValue: " + seaStateValue);
        Debug.Log("depthValue: " + depthValue);
        Debug.Log("radiatedNoiseSourceLevelValue: " + radiatedNoiseSourceLevelValue);
        Debug.Log("radiatedNoiseFrequencyValue: " + radiatedNoiseFrequencyValue);
        Debug.Log("targetStrengthValue: " + targetStrengthValue);
        Debug.Log("targetDetectionRangeValue: " + targetDetectionRangeValue);
        Debug.Log("sourceLevelValue: " + sourceLevelValue);
        Debug.Log("frequencyValue: " + frequencyValue);
        Debug.Log("selfNoiseActiveValue: " + selfNoiseActiveValue);
        Debug.Log("selfNoisePassiveValue: " + selfNoisePassiveValue);
        Debug.Log("DI_activeValue: " + DI_activeValue);
        Debug.Log("DI_passiveValue: " + DI_passiveValue);
        Debug.Log("DT_activeValue: " + DT_activeValue);
        Debug.Log("DT_passiveValue: " + DT_passiveValue);
    }
}