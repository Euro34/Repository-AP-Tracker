using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System;
using System.Collections.Generic;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    private Image dotCopy_Border;
    public static Point2f[][] dot_list = new Point2f[2][];
    public Vid_select_Switch2 vid_Select_Switch;
    public Frame_select frame_select;
    public Pan_Zoom pan_Zoom;
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
        Reset_(false); //Start at vid1
    }
    public void Save_Pos(float pos_x, float pos_y, int vid, int frame) //Render the dot
    {
        // Create a copy of the dot
        if (GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString() + "_Border") != null)
        {
            GameObject dotcopy_Border_obj = GameObject.Find("dot:" + vid.ToString() + '_' + frame.ToString() + "_Border");
            dotCopy_Border = dotcopy_Border_obj.GetComponent<Image>();
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            dotCopy = Instantiate(dotImage, canvasRectTransform);
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString();
            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f);
            Color color = new Color32(255, 255, 255, 225);
            dotCopy.color = color;
            dotCopy_Border = Instantiate(dotImage, canvasRectTransform);
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + frame.ToString() + "_Border";
            dotCopy_Border.color = new Color32(56, 56, 56, 225);
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f);
            dotCopy.transform.SetParent(dotCopy_Border.transform);
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        dot_list[vid][frame] = new Point2f(pos_x, pos_y);
    }
    public void color_change() //Chage the opacity of the dot depend on how far it is from current frame. The further the more transparent
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
                if (GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString()) != null)
                {
                    GameObject dotCopy_border = GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString() + "_Border");
                    Image dotCopy_Img_border = dotCopy_border.GetComponent<Image>();
                    GameObject dotCopy = GameObject.Find("dot:" + Vid.ToString() + '_' + img_no.ToString());
                    Image dotCopy_Img = dotCopy.GetComponent<Image>();
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
    public void Reset_(bool Selected_Vid) //Switch 
    {
        int Vid = Selected_Vid ? 1 : 0;
        for (int i = 0; i < Frame_ext.framecount[Vid]; i++)
        {
            if (dot_list[Vid][i] != new Point2f())
            {
                Save_Pos(dot_list[Vid][i].X, dot_list[Vid][i].Y, Vid, i);
            }
        }
        for (int i = 0; i < Frame_ext.framecount[Vid ^ 1] ; i++)
        {
            if (dot_list[Vid ^ 1][i] != new Point2f())
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Convert.ToByte(!Selected_Vid).ToString() + '_' + i.ToString() + "_Border");
                dotcopy_Border_obj.SetActive(false);
            }
        }
    }
    public void Dot_del() //Delete that frame dot
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
        Save_Pos(position[0], position[1], Convert.ToByte(Vid_select_Switch2.Select_Vid), frame_select.Current_value); //Save and render the dot
        Debug.Log("vid" + (Vid_select_Switch2.Select_Vid ? 2 : 1) + "_" + frame_select.Current_value + " : x = " + position[0] + ", y = " + position[1]);
        frame_select.up(); //Advance the frame by 1
    }
}