using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float disapperTimer;
    private float disapperTimerMax;
    private Color color;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        disapperTimerMax = 1f;
        disapperTimer = 0f;
        color = new Color(1, 0, 0, 1);
    }

    private void Update(){
        disapperTimer += Time.deltaTime;
        color.a = Mathf.Lerp(disapperTimerMax, 0, disapperTimer / disapperTimerMax);
        spriteRenderer.color = color;
        if(disapperTimer >= disapperTimerMax){
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color){
        this.color = color;
    }
    public void SetDisapperTimerMax(float timer){
        disapperTimerMax = timer;
        disapperTimer = 0f;
    }
}
