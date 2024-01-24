using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using Unity.VisualScripting;
using System;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    private Image dotCopy_Border;
    public static Point2f[,] dot_list;
    public Vid_select_Switch2 vid_Select_Switch;
    public Ref_Point_Select2 ref_Point;
    public Pan_Zoom pan_Zoom;
    public RectTransform canvasRectTransform;

    private void Start()
    {
        Reset_(1, 2);
        dot_list = new Point2f[Math.Max(Frame_ext.byteslist[0].Count, Frame_ext.byteslist[1].Count), 2];
    }
    public void CreateDotCopy(float pos_x, float pos_y, int vid, int dot_no)
    {
        // Create a copy of the dot
        if (GameObject.Find("dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border") != null)
        {
            GameObject dotcopy_Border_obj = GameObject.Find("dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border");
            dotCopy_Border = dotcopy_Border_obj.GetComponent<Image>();
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
            color_change();
        }
        else
        {
            Color color = new Color32(255, 0, 0, 255);
            dotCopy = Instantiate(dotImage, canvasRectTransform);
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString();
            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f);
            color = new Color32(255, 255, 255, 150);
            dotCopy.color = color;
            dotCopy_Border = Instantiate(dotImage, canvasRectTransform);
            dotCopy_Border.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString() + "_Border";
            dotCopy_Border.color = new Color32(56, 56, 56, 255);
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f);
            dotCopy.transform.SetParent(dotCopy_Border.transform);
            dotCopy.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        dot_list[dot_no - 1, vid - 1] = new Point2f(pos_x, pos_y);
    }
    public void color_change()
    {
        int dot_selected = ref_Point.Current_value;
        int Vid = vid_Select_Switch.Select_Vid;
        for (int i = -2; i <= 2; i++)
        {
            int img_no = dot_selected + i;
            if (img_no >= 0)
            {
                byte Transparency = (byte)(50 * (3 - Math.Abs(i)));
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
    public void Reset_(int Selected_Vid, int Last_Vid)
    {
        for (int i = 1; i <= Frame_ext.byteslist[0].Count; i++)
        {
            if (dot_list[i - 1, Selected_Vid - 1] != new Point2f())
            {
                CreateDotCopy(dot_list[i - 1, Selected_Vid - 1].X, dot_list[i - 1, Selected_Vid - 1].Y, Selected_Vid, i);
            }
            if (dot_list[i - 1, Last_Vid - 1] != new Point2f())
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Last_Vid.ToString() + '_' + i.ToString() + "_Border");
                Destroy(dotcopy_Border_obj);
            }
        }
    }
    public void Dot_del()
    {
        int dot_selected = ref_Point.Current_value;
        int Vid = vid_Select_Switch.Select_Vid;
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border");
        Destroy(dotcopy_Border_obj);
        dot_list[dot_selected - 1, Vid - 1] = new Point2f();
    }
    public void Pos_Capture()
    {
        Vector2 position = canvasRectTransform.anchoredPosition;
        position[0] = (-1.2078f - position[0]) / pan_Zoom.Zoom2;
        position[1] = (-position[1]) / pan_Zoom.Zoom2;
        CreateDotCopy(position[0], position[1], vid_Select_Switch.Select_Vid, ref_Point.Current_value);
    }
}