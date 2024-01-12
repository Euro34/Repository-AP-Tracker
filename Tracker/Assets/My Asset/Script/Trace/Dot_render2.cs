using UnityEngine;
using UnityEngine.UI;

public class Dot_render2 : MonoBehaviour
{
    public Image dotImage; // Drag your Image UI element here in the Inspector
    private Image dotCopy;
    private Image dotCopy_Border;
    public static Dot_value[,] dot_list = new Dot_value[8,2];
    public Vid_select_Switch2 vid_Select_Switch;
    public Ref_Point_Select2 ref_Point;
    public Pan_Zoom pan_Zoom;
    public RectTransform canvasRectTransform; // Reference to the Canvas RectTransform


    private void Start()
    {
        Reset_(1, 2);
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
            Color color = new Color32(255, 0, 0, 255);
            dotCopy = Instantiate(dotImage, canvasRectTransform); // Instantiate the dot in the canvas

            // Change properties of the copy if needed
            dotCopy.gameObject.name = "dot:" + vid.ToString() + '_' + dot_no.ToString();

            // Adjust the position of the copy (for example, move it to the right)

            dotCopy.rectTransform.sizeDelta = new Vector2(20f, 20f);
            color = new Color32(255, 255, 255, 150);
            dotCopy.color = color;
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
    public void color_change()
    {
        //Color32[] colors = new Color32[] {new Color32(150, 250, 150, 150), new Color32(250, 250, 150, 150), new Color32(250, 150, 150, 150), new Color32(150, 150, 150, 150), new Color32(250, 250, 250, 150), new Color32(150, 250, 250, 150), new Color32(150, 150, 250, 150), new Color32(250, 150, 250, 150)};
        //color = colors[Name_Color];
        int dot_selected = ref_Point.Current_value;
        int Vid = vid_Select_Switch.Select_Vid;
        for (int i = 3; i >= 0; i--)
        {
            int img_no = dot_selected - 3 + i;
            byte Transparency = (byte)(150 * ((float)i / 3));
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
            img_no = dot_selected + 3 - i;
            Transparency = (byte)(150 * ((float)i / 3));
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
    public void Dot_del()
    {
        int dot_selected = ref_Point.Current_value;
        int Vid = vid_Select_Switch.Select_Vid;
        GameObject dotcopy_Border_obj = GameObject.Find("dot:" + Vid.ToString() + '_' + dot_selected.ToString() + "_Border");
        Destroy(dotcopy_Border_obj);
        dot_list[dot_selected - 1, Vid - 1] = null;
    }
    public void Pos_Capture()
    {
        Vector2 position = canvasRectTransform.anchoredPosition;
        position[0] = (-1.2078f - position[0]) / pan_Zoom.Zoom2;
        position[1] = (-position[1]) / pan_Zoom.Zoom2;
        CreateDotCopy(position[0], position[1], vid_Select_Switch.Select_Vid, ref_Point.Current_value);
    }
}