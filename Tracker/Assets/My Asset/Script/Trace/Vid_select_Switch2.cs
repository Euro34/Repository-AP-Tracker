using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class Vid_select_Switch2 : MonoBehaviour
{
    public bool Select_Vid;
    public TextMeshProUGUI Output;
    public RawImage RawImage_Img;
    public VideoPlayer Videoplayer;
    public Dot_render2 dot_render;

    void Start()
    {
        Select_Vid = false;
        Output.text = "Vid_1";
        Videoplayer.url = FilePicker.Vid1_path;
        Videoplayer.prepareCompleted += (source =>
        {
            Videoplayer.Pause();
            RawImage_Img.color = Color.white;
            int width = (int)((Videoplayer.width * 4032) / Videoplayer.height);
            RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
            AssignPic(0);
        });
        RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
    }


    public void Switch()
    {
        if (Output.text == "Vid_1")
        {
            Output.text = "Vid_2";
            Select_Vid = true;
            Videoplayer.url = FilePicker.Vid2_path;
            Videoplayer.prepareCompleted += (source =>
            {
                Videoplayer.Pause();
                RawImage_Img.color = Color.white;
                int width = (int)((Videoplayer.width * 4032) / Videoplayer.height);
                RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
                AssignPic(0);
                RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation2);
            });
            dot_render.Reset_(true);
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = false;
            Videoplayer.url = FilePicker.Vid1_path;
            Videoplayer.prepareCompleted += (source =>
            {
                Videoplayer.Pause();
                RawImage_Img.color = Color.white;
                int width = (int)((Videoplayer.width * 4032) / Videoplayer.height);
                RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
                AssignPic(0);
                RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
            });
            dot_render.Reset_(false);
        }
    }

    public void AssignPic(int frame)
    {
        Videoplayer.time = (double)frame/ (double)Ref_Point_Select2.fps;
    }
}
