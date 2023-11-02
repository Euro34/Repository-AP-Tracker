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
    public Ref_Point_Select ref_Point;

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
        Zoom2 = Zoom;
        transform.localScale = initialScale * Zoom;
    }

    public void Reset_()
    {
        slider.value = Mathf.Sqrt(200);
        transform.localScale = initialScale;
        transform.position = startPos;
        //Debug.Log(startPos);
    }
    public void Pos_Capture()
    {
        Vector3 position = (transform.position - startPos) / (Zoom2);
        Debug.Log(position);
        Debug.Log(ref_Point.Current_value);
        Name_Pos2d obj = new Name_Pos2d();
        obj.SetPos(ref_Point.Current_value.ToString(), position);
        Debug.Log("Name: " + obj.Name);
        Debug.Log("Position X: " + obj.pos_x);
        Debug.Log("Position Y: " + obj.pos_y);
    }
}
public class Name_Pos2d
{
    public string Name { get; set; }
    public double pos_x { get; set; }
    public double pos_y { get; set; }

    // Constructor to initialize the object with values
    public void SetPos(string name, Vector3 vector)
    {
        Name = name;
        pos_x = vector.x;
        pos_y = vector.y;
    }
}
