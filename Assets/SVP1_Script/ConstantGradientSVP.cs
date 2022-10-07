using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantGradientSVP : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float SpeedGradient;
    [SerializeField] float depth;
    [SerializeField] float xPos;
    [SerializeField] public float firtsThetaValue;
    [SerializeField] public float theta;
    [SerializeField] public float refractionVariaton;
    [SerializeField] public float newTheta;
    [SerializeField] float xi;
    [SerializeField] float P;

    [SerializeField] bool started;

    [SerializeField] int bounce;

    public int test;
    [SerializeField] GameObject origin;
    [SerializeField] LineRenderer lineRenderer;

    bool needToGoUp;
    [SerializeField] float yComponent;
    [SerializeField] float xComponent;
    float goingUpAngle;

    void Start()
    {
        depth = Mathf.Abs(transform.position.y);
        xPos = transform.position.x;
        started = false;
        origin = GameObject.Find("Origin");
        bounce = 0;
        test = 0;
        firtsThetaValue = theta;
        refractionVariaton = SVPGraph.angleVariationAtDepths[(int)depth];
        newTheta = theta + refractionVariaton;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.red;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;


        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(test, transform.position);
        //lineRenderer.positionCount++;
        test++;

        xi = CalculateXi(theta, depth);
        needToGoUp = false;
        goingUpAngle = 0;
    }


    // Update is called once per frame
    void Update()
    {
        depth = Mathf.Abs(transform.position.y);
        xPos = transform.position.x;
        speed = CalculateSpeed(depth);
        SpeedGradient = CalculateSpeedGradient(depth);
        xi = NewXi(theta, depth);
        P = CalculateP(xi, depth);

        //on mouse click
        if (Input.GetMouseButtonDown(0))
            started = true;
        if (started)
        {
            refractionVariaton = SVPGraph.angleVariationAtDepths[(int)depth];
            newTheta = theta + refractionVariaton;
            newTheta = Mathf.Clamp(newTheta, -90, 90);
            xi = NewXi(newTheta, depth);
            P = CalculateP(xi, depth);
            speed = CalculateSpeed(depth);
            SpeedGradient = CalculateSpeedGradient(depth);
            
            if (Mathf.Abs(newTheta) <= 5f && !needToGoUp)
                needToGoUp = true;
            
            
            if (needToGoUp)
                theta = Mathf.Lerp(theta, 10f, 0.001f);
            
            yComponent = -depth + speed * Mathf.Sin(Mathf.Deg2Rad * (newTheta));
            xComponent = xPos + speed * Mathf.Cos(Mathf.Deg2Rad * (firtsThetaValue));
            transform.position = new Vector3(xComponent, yComponent, 0);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(test, transform.position);
            test++;
        }
        if (Mathf.Abs(transform.position.y) >= 1999 || transform.position.y >= 0)
        {
            bounce++;

            if (transform.position.y >= 0)
            {
                needToGoUp = false;
                if(firtsThetaValue < -45) 
                    theta = firtsThetaValue + 45;
                else 
                    theta = firtsThetaValue;              
            }
            if (Mathf.Abs(transform.position.y) >= 1999)
                theta = -firtsThetaValue;
            
        }
    }



    public float CalculateSpeed(float y)
    {
        return GetSpeed(y - 1) + (y - (y - 1)) * CalculateSpeedGradient(y - 1);
    }

    public float CalculateSpeedGradient(float y)
    {
        return (SVPGraph.speedAtDepths[(int)y + 1] - SVPGraph.speedAtDepths[(int)y]) / (y + 1 - y);
    }

    public float CalculateP(float xi, float y)
    {
        return (-1) * (1 / (xi * CalculateSpeed(y)));
    }

    public float CalculateXi(float theta, float y)
    {
        return Mathf.Cos(Mathf.Deg2Rad * theta) / CalculateSpeed(y);
    }

    public float GetSpeed(float y)
    {
        return SVPGraph.speedAtDepths[(int)y];
    }

    /*public float calculateRange(float y)
    {
        return -CalculateP(theta, y)*(Mathf.Sin(Mathf.Deg2Rad * theta) - Mathf.Sin(Mathf.Deg2Rad * theta ));
    }*/

    public float CalculateRadius(float y)
    {
        return -(1 / CalculateXi(theta, y) * CalculateSpeedGradient(y));
    }

    public float NewXi(float theta, float y)
    {
        return Mathf.Cos(Mathf.Deg2Rad * theta) / CalculateSpeed(y);
    }

}