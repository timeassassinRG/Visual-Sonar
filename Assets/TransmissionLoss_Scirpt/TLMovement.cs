using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        speed = 10f;
        depth = Mathf.Abs(transform.position.y);
        xPos = transform.position.x;
        started = false;
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
        }

    }
}