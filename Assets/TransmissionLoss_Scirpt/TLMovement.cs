using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sopprimi CS0162: Unreachable code detected
#pragma warning disable 162

public class TLMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float depth;
    [SerializeField] float xPos;
    [SerializeField] public float theta;

    [SerializeField] GameObject origin;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float yComponent;
    [SerializeField] float xComponent;

    public int vertexCount;
    bool started;

    void Start()
    {
        speed = 0.5f;
        depth = Mathf.Abs(transform.position.y);
        xPos = transform.position.x;
        started = false;
        
        /*vertexCount = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.red;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 4f;
        lineRenderer.endWidth = 4f;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(vertexCount, transform.position);
        vertexCount++;*/
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            started = true;
        if(started){
            xComponent = transform.position.x + speed * Mathf.Cos(theta * Mathf.Deg2Rad);
            yComponent = transform.position.y + speed * Mathf.Sin(theta * Mathf.Deg2Rad);
            yComponent = Mathf.Clamp(yComponent, -4000, 0);

            transform.position = new Vector3(xComponent, yComponent, 0);
            /*lineRenderer.positionCount++;
            lineRenderer.SetPosition(vertexCount, transform.position);
            vertexCount++;*/
        }

    }
}