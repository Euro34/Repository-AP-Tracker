using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Zoom_Slider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging;
    private bool SliderDrag;
    private float currentScale;
    public float minScale, maxScale;
    public Slider slider;
    private float temp;
    private float scalingRate = 0.01f;
    public float Zoom;
    public TextMeshProUGUI zoom_txt;
    // Start is called before the first frame update
    void Start()
    {
        currentScale = transform.localScale.x;
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        while (true)
        {
            if (isDragging)
            {
                transform.localScale = new Vector2(currentScale, currentScale);
                float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (temp > distance)
                {
                    currentScale -=  (temp - distance) * scalingRate/ (Time.deltaTime);
                }

                else if (temp < distance)
                {
                    currentScale += (distance - temp) * scalingRate / (Time.deltaTime);
                }
                if (currentScale < minScale)
                {
                    currentScale = minScale;
                }
                if (currentScale >= maxScale)
                {
                    currentScale = maxScale;
                }
                temp = distance;
                slider.value = currentScale;
            }
            else
            {
                temp = 0;
            }
            if(SliderDrag)
            {
                currentScale = slider.value;
            }
            Zoom = (Mathf.Pow(slider.value, 2)) / 2;
            zoom_txt.text = Zoom.ToString() + " %";
            yield return null;
        }
    }
    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.touchCount == 2)
        {
            isDragging = true;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
    public void SliderPointerDown()
    {
        SliderDrag = true;
    }
    public void SliderPointerUp()
    {
        SliderDrag = false;
    }
}
