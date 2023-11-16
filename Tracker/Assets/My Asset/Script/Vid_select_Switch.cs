using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class Vid_select_Switch : MonoBehaviour
{
    public int Select_Vid = 1;
    public TextMeshProUGUI Output;
    public RawImage RawImage_Img;
    public Pan_Zoom pan_zoom;

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
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = 1;
            AssignPic();
        }
    }

    private void AssignPic()
    {
        if (Select_Vid == 1)
        {
            int width1 = (Frame_ext.width1 * 4032) / Frame_ext.height1;
            Texture2D texture1 = new Texture2D(Frame_ext.width1, Frame_ext.height1);
            texture1.LoadImage(Frame_ext.byteslist1[1]);
            texture1.Apply();

            // Set the new width while preserving the original height
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width1, 4032);
            RawImage_Img.texture = texture1;
        }
        else {
            int width2 = (Frame_ext.width2 * 4032) / Frame_ext.height2;
            Texture2D texture2 = new Texture2D(Frame_ext.width2, Frame_ext.height2);
            texture2.LoadImage(Frame_ext.byteslist2[1]);
            texture2.Apply();

            // Set the new width while preserving the original height
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width2, 4032);
            RawImage_Img.texture = texture2;
        }
    }
}
