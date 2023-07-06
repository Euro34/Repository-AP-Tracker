using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pan_Zoom : MonoBehaviour
{
    private UnityEngine.Vector3 initialScale;
    public Zoom_Slider zoom_slider;
    float Zoom;


    [SerializeField]
    private int maxZoom = 50;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void SetScale()
    {
        Zoom = (zoom_slider.Zoom)/100;
        var desiredScale = initialScale * Zoom;
        desiredScale = ClampDesiredScale(desiredScale);
        transform.localScale = desiredScale;
    }

    private UnityEngine.Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initialScale/2, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoom, desiredScale);
        return desiredScale;
    }
}