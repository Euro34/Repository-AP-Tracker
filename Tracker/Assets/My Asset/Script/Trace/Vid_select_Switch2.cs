using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class Vid_select_Switch2 : MonoBehaviour
{
    public int Select_Vid = 1;
    public TextMeshProUGUI Output;
    public RawImage RawImage_Img;
    public Dot_render2 dot_render;

    void Start()
    {
        Output.text = "Vid_1";
        AssignPic();
    }


    public void Switch()
    {
        if (Output.text == "Vid_1")
        {
            Output.text = "Vid_2";
            Select_Vid = 2;
            AssignPic();
            dot_render.Reset_(Select_Vid,1);
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = 1;
            AssignPic();
            dot_render.Reset_(Select_Vid,2);
        }
    }

    private void AssignPic()
    {
        if (Select_Vid == 1)
        {
            int width1 = (Frame_ext.width[0] * 4032) / Frame_ext.height[0];
            Texture2D texture1 = new Texture2D(Frame_ext.width[0], Frame_ext.height[0]);
            texture1.LoadImage(Frame_ext.byteslist[0][0]);
            texture1.Apply();

            // Set the new width while preserving the original height
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width1, 4032);
            RawImage_Img.texture = texture1;
        }
        else {
            int width2 = (Frame_ext.width[1] * 4032) / Frame_ext.height[1];
            Texture2D texture2 = new Texture2D(Frame_ext.width[1], Frame_ext.height[1]);
            texture2.LoadImage(Frame_ext.byteslist[1][0]);
            texture2.Apply();

            // Set the new width while preserving the original height
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width2, 4032);
            RawImage_Img.texture = texture2;
        }
    }
}
