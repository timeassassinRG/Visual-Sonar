using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour
{
    public RectTransform graphContainer;
    [SerializeField]public Sprite circleSprite;
    //[SerializeField]private LineRenderer lineRenderer;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    //public RectTransform dashTemplateX;
    //public RectTransform dashTemplateY;

    [SerializeField] float yMaximum = 100f;
    [SerializeField] float xSize = 4f;
    [SerializeField] int separatorCount = 1;
    [SerializeField] int resolution = 1;
    void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        /*labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();*/
        //List<int> valueList = new List<int>() { 1,2,3,4,5,6,7,8,9,10 };
        //ShowGraph(valueList);   
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i = i+resolution)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            //lineRenderer.positionCount ++;
            //lineRenderer.SetPosition(i, new Vector3(xPosition, yPosition, 0));

            if (lastCircleGameObject != null)
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            
            lastCircleGameObject = circleGameObject;
        
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -3f);
            labelX.GetComponent<Text>().text = (i+1).ToString();

            //RectTransform dashX = Instantiate(dashTemplateX);
            //dashX.SetParent(graphContainer, false);
            //dashX.gameObject.SetActive(true);
            //dashX.anchoredPosition = new Vector2(xPosition, -3f);
        }

        //int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-3f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

            //RectTransform dashY = Instantiate(dashTemplateY);
            //dashY.SetParent(graphContainer, false);
            //dashY.gameObject.SetActive(true);
            //dashY.anchoredPosition = new Vector2(-3f, normalizedValue * graphHeight);
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPoisitionB){
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPoisitionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPoisitionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 0.5f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}