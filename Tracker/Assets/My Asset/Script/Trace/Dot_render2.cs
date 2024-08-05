using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System;
using System.Collections.Generic;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage;
    private Image dotCopy;
    private Image dotCopy_Border;
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
        Re_render_dot(false); //Start at vid1
    }
    public void Save_Pos(float pos_x, float pos_y, int vid, int frame) //Render the dot
    {
        // Create a copy of the dot
        if (GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString() + "_Border") != null) //Check if the dot for that frame and that video exist
        {
            //Move the dot
            GameObject dotcopy_obj = GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString());
            dotCopy = dotcopy_obj.GetComponent<Image>();
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            //Copy dot from original dot and namt it dot:<vid>_<frame> with border name similarly but with _border follow it and set it to the position
            dotCopy = Instantiate(dotImage, canvasRectTransform); //Copy a dot
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString(); //Rename the dot
            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f); //Set dot size
            Color color = new Color32(255, 255, 255, 225);
            dotCopy.color = color; //Set dot color
            dotCopy_Border = Instantiate(dotImage, canvasRectTransform); //Copy a border
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString() + "_Border"; //Rename a border
            dotCopy_Border.color = new Color32(56, 56, 56, 225);
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f);
            dotCopy_Border.transform.SetParent(dotCopy.transform);
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        dot_list[vid][frame] = new Point2f(pos_x, pos_y); //Save the coordinate to dot_list
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
                if (GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString() + "_Border") != null)
                {
                    GameObject dotCopy = GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString());
                    Image dotCopy_Img = dotCopy.GetComponent<Image>();
                    GameObject dotCopy_border = GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString() + "_Border");
                    Image dotCopy_Img_border = dotCopy_border.GetComponent<Image>();
                    Color color = new Color32(255, 255, 255, Transparency);
                    Color color_border = new Color32(56, 56, 56, Transparency);
                    if (dotCopy_Img != null)
                    {
                        dotCopy_Img_border.color = color_border;
                        dotCopy_Img.color = color;
                    }
                }
            }
        }
    }
    public void Re_render_dot(bool Selected_Vid) //Deal with opacity when switch between video
    {
        int Vid = Selected_Vid ? 1 : 0;
        for (int i = 0; i < Frame_ext.framecount[Vid]; i++)
        {
            if (dot_list[Vid][i] != new Point2f())
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Convert.ToByte(Selected_Vid).ToString() + '_' + i.ToString());
                dotcopy_Border_obj.SetActive(true);
            }
        }
        for (int i = 0; i < Frame_ext.framecount[Vid ^ 1] ; i++) //Vid ^ 1 is opposite of vid (^ is XOR)
        {
            if (dot_list[Vid ^ 1][i] != new Point2f())
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Convert.ToByte(!Selected_Vid).ToString() + '_' + i.ToString());
                dotcopy_Border_obj.SetActive(false);
            }
        }
    }
    public void Dot_del() //Delete the dot from that frame
    {
        int dot_selected = frame_select.Current_value;
        byte Vid = Convert.ToByte(Vid_select_Switch2.Select_Vid);
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border");
        Destroy(dotcopy_Border_obj); //Delete the render
        dot_list[Vid][dot_selected] = new Point2f(); //Delete the data
        Debug.Log("Delete : vid" + (Vid_select_Switch2.Select_Vid ? 2 : 1) + "_" + dot_selected);
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
            Debug.Log("vid" + (Vid_select_Switch2.Select_Vid ? 2 : 1) + "_" + frame_select.Current_value + " : x = " + position[0] + ", y = " + position[1]);
            frame_select.up(); //Advance the frame by 1
        }
        else
        {
            auto_track_rect.Auto_trace_rect(); //Auto track function start from here
        }
    }
}