using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using Unity.VisualScripting;
using System;

public class Dot_render1 : MonoBehaviour
{
    public Image dotImage; 
    private Image dotCopy;
    private Image dotCopy_Border;
    public static Point2f[,] dot_list = new Point2f[8, 2];
    public Vid_select_Switch1 vid_Select_Switch;
    public Ref_Select ref_Point;
    public Pan_Zoom pan_Zoom;
    public RectTransform canvasRectTransform;
    private Color color = new Color32(255, 0, 0, 255);

    private void Start()
    {
        Reset_(false); //Set the start to vid1
    }
    public void CreateDotCopy(float pos_x, float pos_y, int vid, int dot_no)
    {
        // Create a copy of the dot
        if (GameObject.Find("dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border") != null)
        {
            GameObject dotcopy_Border_obj = GameObject.Find("dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border");
            dotCopy_Border = dotcopy_Border_obj.GetComponent<Image>();
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            dotCopy = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString();

            // Adjust the position of the copy (for example, move it to the right)
            color_change((byte)(dot_no - 1));

            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f);
            dotCopy_Border = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border";

            // Adjust the position of the copy (for example, move it to the right)
            dotCopy_Border.color = new Color32(56, 56, 56, 255);
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);

            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f);
            dotCopy.transform.SetParent(dotCopy_Border.transform);
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        dot_list[dot_no - 1, vid] = new Point2f(pos_x, pos_y); //Add position in point 2f to a 2d array of reference point number and vid number
    }
    private void color_change(byte Name_Color) //Deal with color of the dot
    {
        byte R = (byte)((Name_Color & 1) * 100 + 150);
        byte G = (byte)(((Name_Color >> 1) & 1) * 100 + 150);
        byte B = (byte)(((Name_Color >> 2) & 1) * 100 + 150);

        color = new Color32(R, G, B, 150);
        if (dotCopy != null)
        {
            dotCopy.color = color;
        }
    }
    public void Reset_(bool Selected_Vid) //Change the dot set between each vid
    {
        byte Vid = Convert.ToByte(Selected_Vid);
        for (int i = 1; i <= 8; i++)
        {
            if (dot_list[i - 1, Vid] != new Point2f())
            {
                CreateDotCopy(dot_list[i - 1, Convert.ToByte(Selected_Vid)].X, dot_list[i - 1, Vid].Y, Vid, i);
            }
            if (dot_list[i - 1, Convert.ToByte(!Selected_Vid)] != new Point2f())
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + (Convert.ToByte(!Selected_Vid)).ToString() + '_' + i.ToString() + "_Border");
                Destroy(dotcopy_Border_obj);
            }
        }
    }
    public void Dot_del() //Delete the dot
    {
        int dot_selected = ref_Point.Current_value;
        byte Vid = Convert.ToByte(vid_Select_Switch.Select_Vid);
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border");
        Destroy(dotcopy_Border_obj); //Delete the render
        dot_list[dot_selected - 1, Vid] = new Point2f(); //Delete the data
        Debug.Log("Delete : vid" + (vid_Select_Switch.Select_Vid ? 2 : 1) + "_" + ref_Point.Current_value);
    }
    public void Pos_Capture()
    {
        Vector2 position;
        position = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get the position from unity from the center
        position[0] = (-position[0]) / pan_Zoom.Zoom2; //Calculate x
        position[1] = (-position[1]) / pan_Zoom.Zoom2; //Calculate y
        CreateDotCopy(position[0], position[1], Convert.ToByte(vid_Select_Switch.Select_Vid), ref_Point.Current_value); //Render the dot
        Debug.Log("vid" + (vid_Select_Switch.Select_Vid ? 2 : 1) + "_" + ref_Point.Current_value + " : x = " + position[0] + ", y = " + position[1]);
    }
}