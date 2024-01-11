using UnityEngine;
using UnityEngine.UI;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    private Image dotCopy_Border;
    public static Dot_value[,] dot_list = new Dot_value[8,2];
    public RectTransform canvasRectTransform; // Reference to the Canvas RectTransform
    private Color color = new Color32(255, 0, 0, 255);
    private byte Name_Color = 0;

    private void Start()
    {
        Reset_(1, 2);
    }
    public void CreateDotCopy(float pos_x, float pos_y, int vid, int dot_no)
    {
        Name_Color = (byte)(dot_no - 1);
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
            color_change();

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
        Dot_value obj = new Dot_value();
        obj.SetPos(pos_x, pos_y);
        dot_list[dot_no - 1, vid - 1] = obj;
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
    public void Reset_(int Selected_Vid, int Last_Vid)
    {
        for (int i = 1; i <= 8; i++) 
        {
            if (dot_list[i - 1, Selected_Vid - 1] != null)
            {
                CreateDotCopy(dot_list[i - 1, Selected_Vid - 1].pos_x, dot_list[i - 1, Selected_Vid - 1].pos_y, Selected_Vid, i);
            }
            if (dot_list[i - 1, Last_Vid - 1] != null)
            {
                GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Last_Vid.ToString() + '_' + i.ToString() + "_Border");
                Destroy(dotcopy_Border_obj);
            }
        }
    }
    public void Dot_del(int dot_selected, int Vid)
    {
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border");
        Destroy(dotcopy_Border_obj);
        dot_list[dot_selected -1, Vid -1] = null;
    }
}