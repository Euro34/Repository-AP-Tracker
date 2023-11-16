using System.Collections.Generic;
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
    public Slider slider; 
    public Ref_Point_Select ref_Point;
    public Vid_select_Switch vid_Select_Switch;
    float Zoom, Zoom2;
    IDictionary<string, Name_Pos2d> refpoint1 = new Dictionary<string, Name_Pos2d>();
    IDictionary<string, Name_Pos2d> refpoint2 = new Dictionary<string, Name_Pos2d>();

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
    }
    public void Pos_Capture()
    {
        Vector3 position = (transform.position - startPos) / (Zoom2);
        Name_Pos2d obj = new Name_Pos2d();
        obj.SetPos(position);
        if (vid_Select_Switch.Select_Vid == 1) {
            Debug.Log("1");
            try
            {
                refpoint1.Add(ref_Point.Current_value.ToString(), obj);
            }
            catch
            {
                refpoint1[ref_Point.Current_value.ToString()] = obj;
            }
            for (int i = 1 ; i <= 8 ; i++){
                try{
                    Debug.Log(i +  " = " + refpoint1[i.ToString()]);
                }
                catch{
                    Debug.Log(i + " = nan");
                }
            }
        }
        else {
            Debug.Log("2");
            try
            {
                refpoint2.Add(ref_Point.Current_value.ToString(), obj);
            }
            catch
            {
                refpoint2[ref_Point.Current_value.ToString()] = obj;
            }
            for (int i = 1 ; i <= 8 ; i++){
                try{
                    Debug.Log(i +  " = " + refpoint2[i.ToString()]);
                }
                catch{
                    Debug.Log(i + " = nan");
                }
            }
        }
    }
}
public class Name_Pos2d
{
    public double pos_x { get; set; }
    public double pos_y { get; set; }
    public void SetPos(Vector3 vector)
    {
        pos_x = vector.x;
        pos_y = vector.y;
    }
    public override string ToString()
    {
        return $"x : {pos_x} ,y : {pos_y}";
    }
}

