using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSonar : MonoBehaviour
{
    [SerializeField] private Transform pfRadarPing;
    private Transform pulseTransofrm;
    private float range;
    private float rangeMax;
    float rangeSpeed = 15f;
    private SpriteRenderer pulseRenderer;
    private List<Collider2D> alreadyPingedColliderList;
    private Color radarColorTransparent = new Color(0, 255, 48, 0);
    private Color radarColor = new Color(0, 255, 48, 1);

    private void Awake()
    {
        pulseTransofrm = transform.Find("Pulse");
        pulseRenderer = pulseTransofrm.GetComponent<SpriteRenderer>();
        pulseRenderer.color = radarColorTransparent;
        rangeMax = 40f;
        alreadyPingedColliderList = new List<Collider2D>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            ResetRange();
            StartCoroutine(Pulse());
        }
    }

    public IEnumerator Pulse()
    {
        while (range < rangeMax)
        {
            range += rangeSpeed * Time.deltaTime;
            pulseTransofrm.localScale = new Vector3(range, range);
            RaycastHit2D[] raycastHit2DArray = Physics2D.CircleCastAll(transform.position, range / 2f, Vector2.zero);
            foreach(RaycastHit2D raycastHit2D in raycastHit2DArray)
            if (raycastHit2D.collider != null)
            {
                if (!alreadyPingedColliderList.Contains(raycastHit2D.collider))
                {
                    alreadyPingedColliderList.Add(raycastHit2D.collider);
                    Debug.Log("Ping with " + raycastHit2D.collider.name + " at " + raycastHit2D.point);
                    Instantiate(pfRadarPing, raycastHit2D.point, Quaternion.identity);
                }
            }
            yield return null;
        }
    }

    public void ResetRange()
    {
        range = 0f;
        alreadyPingedColliderList.Clear();
        if(pulseRenderer.color == radarColorTransparent)
            pulseRenderer.color = radarColor;
    }
}