using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System;

public class Dot_render1 : MonoBehaviour
{
    public Image dotImage; 
    public GameObject[] panel_dot = new GameObject[2];
    public static Point2f[,] dot_list = new Point2f[8, 2];
    public Vid_select_Switch1 vid_Select_Switch;
    public Ref_Select ref_Point;
    public Pan_Zoom pan_Zoom;
    public RectTransform canvasRectTransform;
    private Color color = new Color32(255, 0, 0, 255);

    private void Start()
    {
        Reset_(false); //Set the start to vid1
        for (int i = 0; i < 8; i++)
        {
            Point2f point1 = dot_list[i, 0];
            Point2f point2 = dot_list[i, 1];
            Point2f pointnull = new Point2f();
            if (point1 != pointnull) {CreateDotCopy(point1.X, point1.Y, 0, i);}
            if (point1 != pointnull) {CreateDotCopy(point2.X, point2.Y, 1, i);}
        }
    }
    public void CreateDotCopy(float pos_x, float pos_y, int vid, int dot_no)
    {
        Debug.Log("Calibrate : vid" + (vid_Select_Switch.Select_Vid ? 2 : 1) + "_" + ref_Point.Current_value + " : x = " + pos_x + ", y = " + pos_y);
        RectTransform panel = panel_dot[vid].GetComponent<RectTransform>();;
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border"); //Find the dot
        if (dotcopy_Border_obj != null) //Check if the dot for that frame and that video exist
        {
            Image dotCopy_Border = dotcopy_Border_obj.GetComponent<Image>(); //Get the image component
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y); //Move the dot
        }
        else
        {
            Image dotCopy_Border = Instantiate(dotImage, canvasRectTransform); // Copy the dot for the border
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border"; //Rename the border
            dotCopy_Border.color = new Color32(56, 56, 56, 255); //Change the border color
            dotCopy_Border.rectTransform.sizeDelta = new Vector2(18f, 18f); //Change border size
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y); //Change border position
            dotCopy_Border.transform.SetParent(panel); //Catagorize dot into vid1 and vid2

            Image dotCopy = Instantiate(dotImage, canvasRectTransform); // Copy the dot
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString(); //Rename the dot
            dotCopy.color = Color_change((byte)(dot_no)); //Change the dot color
            dotCopy.rectTransform.sizeDelta = new Vector2(15f, 15f); //Change the dot size
            dotCopy.transform.SetParent(dotCopy_Border.transform); //Set dot to be children of border
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0); //Change dot position
        }
        dot_list[dot_no, vid] = new Point2f(pos_x, pos_y); //Add position in point 2f to a 2d array of reference point number and vid number
    }
    private Color32 Color_change(byte Name_Color) //Deal with color of the dot
    {
        //Use binary to get color
        byte R = (byte)((Name_Color & 1) * 100 + 150);
        byte G = (byte)(((Name_Color >> 1) & 1) * 100 + 150);
        byte B = (byte)(((Name_Color >> 2) & 1) * 100 + 150);

        return new Color32(R, G, B, 150);
    }
    public void Reset_(bool Selected_Vid) //Change the dot set between each vid
    {
        panel_dot[0].SetActive(!Selected_Vid);
        panel_dot[1].SetActive(Selected_Vid);
    }
    public void Dot_del() //Delete the dot
    {
        int dot_selected = ref_Point.Current_value;
        byte Vid = Convert.ToByte(vid_Select_Switch.Select_Vid);
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border"); //Get dot border
        Destroy(dotcopy_Border_obj); //Delete the render
        dot_list[dot_selected, Vid] = new Point2f(); //Delete the data
        Debug.Log("Calibrate : Delete : vid" + (vid_Select_Switch.Select_Vid ? 2 : 1) + "_" + ref_Point.Current_value);
    }
    public void Pos_Capture()
    {
        Vector2 position;
        position = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get the position from unity from the center
        position[0] = (-position[0]) / pan_Zoom.Zoom2; //Calculate x
        position[1] = (-position[1]) / pan_Zoom.Zoom2; //Calculate y
        CreateDotCopy(position[0], position[1], Convert.ToByte(vid_Select_Switch.Select_Vid), ref_Point.Current_value - 1); //Render the dot
    }
}