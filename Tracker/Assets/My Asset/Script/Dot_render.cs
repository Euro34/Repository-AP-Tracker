using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot_render : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    public RectTransform canvasRectTransform; // Reference to the Canvas RectTransform
    private List<string> NameList = new List<string>();
    public Slider slider;
    private byte R = 255;
    private byte G = 0;
    private byte B = 0;
    private Color color = new Color32(255, 0, 0, 255);

    public void CreateDotCopy(float pos_x, float pos_y, string name)
    {
        // Create a copy of the dot
        if (NameList.Contains(name))
        {
            GameObject dotcopy_obj = GameObject.Find("dot:" + name);
            dotCopy = dotcopy_obj.GetComponent<Image>();
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            dotCopy = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy.gameObject.name = "dot:" + name;
            dotCopy.color = color;

            // Adjust the position of the copy (for example, move it to the right)
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);

            dotCopy.rectTransform.sizeDelta = new Vector2(75f, 75f);
            NameList.Add(name);
        }
    }
    public void color_change()
    {
        int color_int = (int)slider.value;
        if (color_int <= 255)
        {
            R = 200;
            G = (byte)color_int;
            B = 0;
        }
        else if (color_int <= 510)
        {
            R = (byte)(510 - color_int);
            G = 200;
            B = 0;
        }
        else if (color_int <= 765)
        {
            R = 0;
            G = 200;
            B = (byte)(color_int - 510);
        }
        else if (color_int <= 1020)
        {
            R = 0;
            G = (byte)(1020 - color_int);
            B = 200;
        }
        else if (color_int <= 1275)
        {
            R = (byte)(color_int - 1020);
            G = 0;
            B = 200;
        }
        else
        {
            R = 200;
            G = 0;
            B = (byte)(1530 - color_int);
        }
        color = new Color32(R, G, B, 255);
        if(dotCopy!= null)
        {
            dotCopy.color = color;
        }
    }
    public void Set_dot(string name)
    {
        if (NameList.Contains(name))
        {
            GameObject dotcopy_obj = GameObject.Find("dot:" + name);
            dotCopy = dotcopy_obj.GetComponent<Image>();
        }
        else
        {
            dotCopy = null;
        }
    }
    public void Reset_(int vid1,int vid2)
    {
        for(int i = 1; i <= 8; i++)
        {
            if (NameList.Contains(vid1.ToString() + "-" + i))
            {
                GameObject dotcopy_obj = GameObject.Find("dot:" + vid1.ToString() + "-" + i);
                dotCopy = dotcopy_obj.GetComponent<Image>();
                dotCopy.enabled = true;
            }
        }
        for (int i = 1; i <= 8; i++)
        {
            if (NameList.Contains(vid2.ToString() + "-" + i))
            {
                GameObject dotcopy_obj = GameObject.Find("dot:" + vid2.ToString() + "-" + i);
                dotCopy = dotcopy_obj.GetComponent<Image>();
                dotCopy.enabled = false;
            }
        }
    }
}
