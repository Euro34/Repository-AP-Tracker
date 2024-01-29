using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class Vid_select_Switch1 : MonoBehaviour
{
    public bool Select_Vid;
    public TextMeshProUGUI Output;
    public RawImage RawImage_Img;
    public VideoPlayer Videoplayer;
    public Dot_render1 dot_render;

    void Start()
    {
        Output.text = "Vid_1";
        Select_Vid = false;
        Videoplayer.url = FilePicker.Vid1_path;
        Videoplayer.prepareCompleted += (source => AssignPic());
        dot_render.Reset_(Select_Vid);
        RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
    }


    public void Switch()
    {
        if (Output.text == "Vid_1")
        {
            Output.text = "Vid_2";
            Select_Vid = true;
            Videoplayer.url = FilePicker.Vid2_path;
            Videoplayer.prepareCompleted += (source => AssignPic());
            dot_render.Reset_(Select_Vid);
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation2);
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = false;
            Videoplayer.url = FilePicker.Vid1_path;
            Videoplayer.prepareCompleted += (source => AssignPic());
            dot_render.Reset_(Select_Vid);
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1);
        }
    }

    private void AssignPic()
    {
        Videoplayer.Pause();
        Videoplayer.time = Videoplayer.length / 2;
        RawImage_Img.color = Color.white;
        int width = (int)((Videoplayer.width * 4032) / Videoplayer.height);
        RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
    }
}
