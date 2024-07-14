using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using Unity.VisualScripting;

public class Vid_select_Switch2 : MonoBehaviour
{
    public bool Select_Vid;
    public TextMeshProUGUI Output;
    public RawImage RawImage_Img;
    public VideoPlayer[] Videoplayers = new VideoPlayer[2];
    public Dot_render2 dot_render;
    public RenderTexture[] renderTexture = new RenderTexture[2];
    private int[] width = new int[2];
    private double multiplier = 0;

    void Start()
    {
        Select_Vid = false;
        Output.text = "Vid_1";
        Videoplayers[0].url = FilePicker.Vid1_path;
        Videoplayers[1].url = FilePicker.Vid2_path;
        Videoplayers[0].prepareCompleted += (source =>
        {
            multiplier = Videoplayers[Convert.ToByte(0)].frameRate / Ref_Point_Select2.fps;
            RawImage_Img.color = Color.white;
            width[0] = (int)((Videoplayers[0].width * 4032) / Videoplayers[0].height);
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width[0], 4032);
            AssignPic(0);
        });
        Videoplayers[1].prepareCompleted += (source =>
        {
            width[1] = (int)((Videoplayers[1].width * 4032) / Videoplayers[1].height);
        });
        RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
    }


    public void Switch()
    {
        if (Output.text == "Vid_1")
        {
            multiplier = Videoplayers[Convert.ToByte(1)].frameRate / Ref_Point_Select2.fps;
            Output.text = "Vid_2";
            Select_Vid = true;
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width[1], 4032);
            AssignPic(0);
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation2);
            RawImage_Img.texture = renderTexture[1];
            dot_render.Reset_(true);
        }
        else
        {
            multiplier = Videoplayers[Convert.ToByte(0)].frameRate / Ref_Point_Select2.fps;
            Output.text = "Vid_1";
            Select_Vid = false;
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width[0], 4032);
            AssignPic(0);
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
            RawImage_Img.texture = renderTexture[0];
            dot_render.Reset_(false);
        }
    }

    public void AssignPic(int frame)
    {
        byte sel = Convert.ToByte(Select_Vid);
        Videoplayers[sel].frame = Mathf.RoundToInt((float)(frame * multiplier));
        Debug.Log(Videoplayers[sel].frame);
    }
}
