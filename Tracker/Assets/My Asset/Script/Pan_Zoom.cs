using UnityEngine;
using UnityEngine.EventSystems;

public class Pan_Zoom : MonoBehaviour
{
    private UnityEngine.Vector3 initialScale;
    private UnityEngine.Vector3 initialPos;
    private UnityEngine.Vector3 startPos;
    public Zoom_Slider zoom_slider;
    float Zoom, Zoom2;

    private void Awake()
    {
        initialScale = transform.localScale;
        startPos = transform.position - new Vector3(0f,19.5f,0f);
        Zoom2 = 1;
    }

    public void SetScale()
    {
        initialPos = transform.position;
        Zoom = (zoom_slider.Zoom)/100;
        var desiredScale = initialScale * Zoom;
        Debug.Log(Zoom.ToString() + Zoom2.ToString() + (Zoom / Zoom2).ToString());
        transform.position = startPos + ((initialPos - startPos) * (Zoom/Zoom2));
        Zoom2 = Zoom;
        transform.localScale = desiredScale;
    }
}