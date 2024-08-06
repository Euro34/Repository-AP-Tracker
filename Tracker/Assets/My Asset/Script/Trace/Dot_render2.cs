using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage; //Circle picture
    public GameObject[] panel_dot = new GameObject[2];
    public static Point2f[][] dot_list = new Point2f[2][];
    public Vid_select_Switch2 vid_Select_Switch;
    public Frame_select frame_select;
    public Pan_Zoom pan_Zoom;
    public Auto_track auto_track;
    public Auto_track_rect auto_track_rect;
    public RectTransform canvasRectTransform;

    private void Start()
    {
        if (dot_list[0] == null || dot_list[0].Length == 0)
        {
            dot_list[0] = new Point2f[Frame_ext.framecount[0]];
        }
        if (dot_list[1] == null || dot_list[1].Length == 0)
        {
            dot_list[1] = new Point2f[Frame_ext.framecount[1]];
        }
        Switch_dot(false); //Start at vid1
        foreach (var dotlist in dot_list)
        {
            for (int i = 0; i < dotlist.Length; i++)
            {
                if (dotlist[i] != new Point2f()) { Save_Pos(dotlist[i].X, dotlist[i].Y, 0, i);}
            }
        }
    }
    public void Save_Pos(float pos_x, float pos_y, int vid, int frame) //Render the dot
    {
        Debug.Log("Trace : vid" + (Vid_select_Switch2.Select_Vid ? 2 : 1) + "_" + (frame_select.Current_value + 1) + " : x = " + pos_x + ", y = " + pos_y);
        RectTransform panel = panel_dot[vid].GetComponent<RectTransform>();
        // Create a copy of the dot
        if (GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString() + "_Border") != null) //Check if the dot for that frame and that video exist
        {
            GameObject dotcopy_obj = GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString() + "_Border"); //Find the dot
            Image dotCopy = dotcopy_obj.GetComponent<Image>(); //Get the image component
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y); //Move the dot
        }
        else
        {
            Image dotCopy_Border = Instantiate(dotImage, canvasRectTransform); //Copy a border
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString() + "_Border"; //Rename a border
            dotCopy_Border.color = new Color32(56, 56, 56, 80); //Set border color
            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f); //Set border size
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y); //Change border position
            dotCopy_Border.transform.SetParent(panel); //Catagorize dot into vid1 and vid2

            Image dotCopy = Instantiate(dotImage, canvasRectTransform); // Copy the dot
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString(); //Rename the dot
            dotCopy.color = new Color32(255, 255, 255, 30); //Change the dot color
            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f); //Change the dot size
            dotCopy.transform.SetParent(dotCopy_Border.transform); //Set dot to be children of border
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0); //Change dot position
        }
        dot_list[vid][frame] = new Point2f(pos_x, pos_y); //Save the coordinate to dot_list
        Color_change();
    }
    public void Color_change() //Chage the opacity of the dot depend on how far it is from current frame. The further the more transparent
    {
        int dot_selected = frame_select.Current_value; 
        byte Vid = Convert.ToByte(Vid_select_Switch2.Select_Vid);
        for (int i = -3; i <= 3; i++)
        {
            int img_no = dot_selected + i;
            if (img_no >= 0)
            {
                byte Transparency = (byte)(75 * (3 - Math.Abs(i)));
                if (Transparency == 0) {Transparency = 35;}
                GameObject dotCopy = GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString()); //Find the dot
                if (dotCopy != null) //Check if the dot exist
                {
                    Image dotCopy_Img = dotCopy.GetComponent<Image>(); //Get the image and assign it to dot_Copy_Img
                    dotCopy_Img.color = new Color32(255, 255, 255, Transparency); //Change the opacity
                }
            }
        }
    }
    public void Switch_dot(bool Selected_Vid) //Deal with switching the dot when switch between video
    {
        panel_dot[0].SetActive(!Selected_Vid);
        panel_dot[1].SetActive(Selected_Vid);
        Re_Color_dot();
    }
    public void Re_Color_dot()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < dot_list[i].Length; j++)
            {
                GameObject dotCopy = GameObject.Find("dot:" + i.ToString() + '_' + j.ToString()); //Find the dot
                if (dotCopy != null) //Check if the dot exist
                {
                    Image dotCopy_Img = dotCopy.GetComponent<Image>(); //Get the image and assign it to dot_Copy_Img
                    dotCopy_Img.color = new Color32(255, 255, 255, 35); //Change the opacity
                }
            }
        }
        Color_change();
    }
    public void Dot_del() //Delete the dot from that frame
    {
        int dot_selected = frame_select.Current_value;
        byte Vid = Convert.ToByte(Vid_select_Switch2.Select_Vid);
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border"); //Find the dot
        Destroy(dotcopy_Border_obj); //Delete the render
        dot_list[Vid][dot_selected] = new Point2f(); //Delete the data
        Debug.Log("Trace : Delete : vid" + (Vid_select_Switch2.Select_Vid ? 2 : 1) + "_" + (dot_selected + 1));
    }
    public void Pos_Capture()
    {
        Vector2 position;
        position = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get the position from unity from the center
        position[0] = (-position[0]) / pan_Zoom.Zoom2;  //Calculate x
        position[1] = (-position[1]) / pan_Zoom.Zoom2; //Calculate y
        
        if (!auto_track.Auto_Trace_Toggel)
        {
            Save_Pos(position[0], position[1], Convert.ToByte(Vid_select_Switch2.Select_Vid), frame_select.Current_value); //Save and render the dot
            frame_select.up(); //Advance the frame by 1
        }
        else
        {
            auto_track_rect.Auto_trace_rect(); //Auto track function start from here
        }
    }
}