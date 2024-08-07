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
        initialScale = transform.localScale; //Get the start scale
        startPos = transform.position; //Get the start position
        Zoom2 = 1;
    }

    public void SetScale() //Deal with zoom
    {
        initialPos = transform.position;
        Zoom = (zoom_slider.Zoom) / 100; //Calculate the zoom
        transform.position = startPos + ((initialPos - startPos) * (Zoom / Zoom2)); //Calculate the position after zoom
        Zoom2 = Zoom; //Set the 
        transform.localScale = initialScale * Zoom; //Set the new scale
    }

    public void Reset_()
    {
        slider.value = Mathf.Sqrt(200); //Set the value on the slider
        transform.localScale = initialScale; //Set the scale back to original
        transform.position = startPos; //Set the position back to original
        Zoom2 = 1;
    }
}