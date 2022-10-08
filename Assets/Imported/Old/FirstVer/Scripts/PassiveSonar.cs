using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSonar : MonoBehaviour
{
    private Transform[] pulseTransform;
    private float range;
    private float rangeMax;
    float rangeSpeed = 3f;
    private SpriteRenderer[] pulseRenderer;
    private Color radarColorTransparent = new Color(1, 0, 0, 0);
    private Color radarColor = new Color(1, 0, 0, 1);

    
    private float timer;
    private float timerMax = 5f;

    private void Awake()
    {
        timer = timerMax;
        pulseTransform = new Transform[3];
        pulseRenderer = new SpriteRenderer[3];

        pulseTransform[0] = transform.Find("Pulse");
        pulseRenderer[0] = pulseTransform[0].GetComponent<SpriteRenderer>();
        pulseRenderer[0].color = radarColorTransparent;

        pulseTransform[1] = transform.Find("Pulse1");
        pulseRenderer[1] = pulseTransform[1].GetComponent<SpriteRenderer>();
        pulseRenderer[1].color = radarColorTransparent;

        pulseTransform[2] = transform.Find("Pulse2");
        pulseRenderer[2] = pulseTransform[2].GetComponent<SpriteRenderer>();
        pulseRenderer[2].color = radarColorTransparent;

        rangeMax = 40f;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (timer > timerMax)
            {
                timer = 0f;
                ResetRange();
                StartCoroutine(Pulse());
                StartCoroutine(Pulse2());
                StartCoroutine(Pulse3());
            }
        }
        timer += Time.deltaTime;
    }

    public IEnumerator Pulse()
    {
        pulseRenderer[0].color = radarColor;
        while (range < rangeMax)
        {
            range += rangeSpeed * Time.deltaTime;
            pulseTransform[0].localScale = new Vector3(range, range);
            yield return null;
        }
    }

    public IEnumerator Pulse2()
    {
        pulseRenderer[1].color = radarColor;
        while (range < rangeMax)
        {
            range += rangeSpeed * Time.deltaTime;
            pulseTransform[1].localScale = new Vector3(Mathf.Clamp(range - 3, 0, range), Mathf.Clamp(range - 3, 0, range));
            yield return null;
        }
    }

    public IEnumerator Pulse3()
    {
        pulseRenderer[2].color = radarColor;
        while (range < rangeMax)
        {
            range += rangeSpeed * Time.deltaTime;
            pulseTransform[2].localScale = new Vector3(Mathf.Clamp(range - 6, 0, range), Mathf.Clamp(range - 6, 0, range));
            yield return null;
        }
    }

    public void ResetRange()
    {
        range = 0f;
        foreach (SpriteRenderer renderer in pulseRenderer)
            renderer.color = radarColorTransparent;
    }
}
