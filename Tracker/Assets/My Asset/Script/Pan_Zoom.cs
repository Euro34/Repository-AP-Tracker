using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pan_Zoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDragging;
    private bool Slider_Drag;
    private float _currentScale;
    public float minScale, maxScale;
    public Slider _slider;
    private float _temp;
    private float _scalingRate = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        _currentScale = transform.localScale.x;
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        while (true)
        {
            if (_isDragging)
            {
                transform.localScale = new Vector2(_currentScale, _currentScale);
                float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (_temp > distance)
                {
                    _currentScale -=  (_temp - distance) * _scalingRate/ (Time.deltaTime);
                }

                else if (_temp < distance)
                {
                    _currentScale += (distance - _temp) * _scalingRate / (Time.deltaTime);
                }
                if (_currentScale < minScale)
                {
                    _currentScale = minScale;
                }
                if (_currentScale >= maxScale)
                {
                    _currentScale = maxScale;
                }
                _temp = distance;
                _slider.value = _currentScale;
            }
            else
            {
                _temp = 0;
            }
            if(Slider_Drag)
            {
                _currentScale = _slider.value;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.touchCount == 2)
        {
            _isDragging = true;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }
    public void SliderPointerDown()
    {
        Slider_Drag = true;
    }
    public void SliderPointerUp()
    {
        Slider_Drag = false;
    }
}
