using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pan_Zoom : MonoBehaviour
{
    private UnityEngine.Vector3 initialPos;
    private UnityEngine.Vector3 initialScale;
    private UnityEngine.Vector3 startPos;
    public Zoom_Slider zoom_slider;
    public RawImage RawImage_Img;
    float Zoom, Zoom2;
    public Slider slider; 

    private void Awake()
    {
        initialScale = transform.localScale;
        startPos = transform.position - new Vector3(0f,19.5f,0f); 
        Zoom2 = 1;
    }

    public void SetScale()
    {
        initialPos = transform.position;
        Zoom = (zoom_slider.Zoom) / 100;
        transform.position = startPos + ((initialPos - startPos) * (Zoom / Zoom2));
        Debug.Log(transform.position);
        Zoom2 = Zoom;
        transform.localScale = initialScale * Zoom;
    }

    public void Reset()
    {
        slider.value = Mathf.Sqrt(200);
        transform.localScale = initialScale;
        transform.position = startPos;
        //Debug.Log(startPos);
    }
}