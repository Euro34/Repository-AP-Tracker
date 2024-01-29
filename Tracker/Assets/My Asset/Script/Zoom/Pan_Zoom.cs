using UnityEngine;
using UnityEngine.UI;

public class Pan_Zoom : MonoBehaviour
{
    private Vector3 initialPos;
    private Vector3 initialScale;
    private Vector3 startPos;
    public Zoom_Slider zoom_slider;
    public Slider slider;
    public float Zoom, Zoom2;
    private void Awake()
    {
        initialScale = transform.localScale;
        startPos = transform.position;
        Zoom2 = 1;
    }

    public void SetScale()
    {
        initialPos = transform.position;
        Zoom = (zoom_slider.Zoom) / 100;
        transform.position = startPos + ((initialPos - startPos) * (Zoom / Zoom2));
        Zoom2 = Zoom;
        transform.localScale = initialScale * Zoom;
    }

    public void Reset_()
    {
        slider.value = Mathf.Sqrt(200);
        transform.localScale = initialScale;
        transform.position = startPos;
        Zoom2 = 1;
    }
}