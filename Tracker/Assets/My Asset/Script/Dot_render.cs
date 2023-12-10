using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot_render : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    private Image dotCopy_Border;
    public RectTransform canvasRectTransform; // Reference to the Canvas RectTransform
    private List<string> NameList = new List<string>();
    public Slider slider;
    private Color color = new Color32(255, 0, 0, 255);
    private byte Name_Color = 0;

    public void CreateDotCopy(float pos_x, float pos_y, string name)
    {
        Name_Color = (byte)(name[2]-'1');
        // Create a copy of the dot
        if (NameList.Contains(name))
        {
            GameObject dotcopy_obj = GameObject.Find("dot:" + name);
            dotCopy = dotcopy_obj.GetComponent<Image>();
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
            GameObject dotcopy_Border_obj = GameObject.Find("dot:" + name + "_Border");
            dotCopy_Border = dotcopy_Border_obj.GetComponent<Image>();
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            dotCopy_Border = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy_Border.gameObject.name = "dot:" + name + "_Border";

            // Adjust the position of the copy (for example, move it to the right)
            dotCopy_Border.color = new Color32(56, 56, 56, 255);
            dotCopy_Border.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);

            dotCopy_Border.rectTransform.sizeDelta = new Vector2(25f, 25f);
            dotCopy = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy.gameObject.name = "dot:" + name;

            // Adjust the position of the copy (for example, move it to the right)
            color_change();
            dotCopy.rectTransform.anchoredPosition = new Vector2(pos_x, pos_y);

            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f);
            NameList.Add(name);
        }
    }
    private void color_change()
    {
        //Color32[] colors = new Color32[] {new Color32(150, 250, 150, 150), new Color32(250, 250, 150, 150), new Color32(250, 150, 150, 150), new Color32(150, 150, 150, 150), new Color32(250, 250, 250, 150), new Color32(150, 250, 250, 150), new Color32(150, 150, 250, 150), new Color32(250, 150, 250, 150)};
        //color = colors[Name_Color];
        
        byte R = (byte)((Name_Color & 1) * 100 + 150);
        byte G = (byte)(((Name_Color >> 1) & 1) * 100 + 150);
        byte B = (byte)(((Name_Color >> 2) & 1) * 100 + 150);
        
        color = new Color32(R,G,B,150);
        if(dotCopy!= null)
        {
            dotCopy.color = color;
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
