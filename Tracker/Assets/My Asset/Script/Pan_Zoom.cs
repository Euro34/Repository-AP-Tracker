using System.Collections.Generic;
using UnityEngine;
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
    public Dot_render dot_render;
    float Zoom, Zoom2;
    IDictionary<string, Name_Pos2d> refpoint1 = new Dictionary<string, Name_Pos2d>();
    IDictionary<string, Name_Pos2d> refpoint2 = new Dictionary<string, Name_Pos2d>();
    List <IDictionary<string, Name_Pos2d>> refpoint = new List <IDictionary<string, Name_Pos2d>>();
    private void Awake()
    {
        initialScale = transform.localScale;
        startPos = transform.position; 
        Zoom2 = 1;
        refpoint.Add(new Dictionary<string, Name_Pos2d>());
        refpoint.Add(new Dictionary<string, Name_Pos2d>());
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
    public void Pos_Capture()
    {
        Name_Pos2d obj = new Name_Pos2d();
        RectTransform rawImageRect = RawImage_Img.GetComponent<RectTransform>();
        Vector2 position = rawImageRect.anchoredPosition;
        position[0] = (-1.2078f - position[0]) / Zoom2;
        position[1] = (-position[1])/Zoom2;
        obj.SetPos(position);
        dot_render.CreateDotCopy(position[0], position[1], vid_Select_Switch.Select_Vid + "-" +ref_Point.Current_value.ToString());
        try
        {
            refpoint[vid_Select_Switch.Select_Vid-1].Add(ref_Point.Current_value.ToString(), obj);
        }
        catch
        {
            refpoint[vid_Select_Switch.Select_Vid-1][ref_Point.Current_value.ToString()] = obj;
        }
        //Debug.Log thing from here
        /*for (int a = 0 ; a<=1 ; a++){
            for (int i = 1 ; i <= 8 ; i++){
                try{
                    Debug.Log((a+1).ToString()+ "_" + i +  " = " + refpoint[a][i.ToString()]);
                }
                catch{
                    Debug.Log((a+1).ToString()+ "_" + i + " = nan");
                }
            }
        }*/
        //To here
    }
}
public class Name_Pos2d
{
    public float pos_x { get; set; }
    public float pos_y { get; set; }
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

