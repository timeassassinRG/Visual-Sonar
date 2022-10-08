using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SemiRealisticPropagation : MonoBehaviour
{
    [SerializeField] Text speed;
    [SerializeField] Text Depth;
    float speedValue;
    float depthValue;

    void Start()
    {
        depthValue = 0;
        speedValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = speedValue.ToString();
        Depth.text = depthValue.ToString();
        //se il mouse sinistro è premuto fai partire la coroutine
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            StartCoroutine(Fall());
        }
    }

    public IEnumerator Fall(){
        speedValue = 0;
        while(Mathf.Abs(depthValue) < 2000000){
            depthValue += speedValue;
            transform.position = new Vector3(transform.position.x, depthValue, transform.position.z);
            speedValue += 3.6f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}