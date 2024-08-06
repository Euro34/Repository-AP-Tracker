using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VisualScripting;

public class Vid_select_Switch2 : MonoBehaviour
{
    public static bool Select_Vid;
    public static double time;
    public TextMeshProUGUI Output;
    public TextMeshProUGUI time_txt;
    public RawImage RawImage_Img;
    public VideoPlayer Videoplayer;
    public Dot_render2 dot_render;
    public Frame_select frame_select;
    public Auto_track auto_track;

    void Start() //Set the start video to vid1
    {
        Videoplayer.sendFrameReadyEvents = true; //To enable Videoplayer.frameReady method
        Videoplayer.frameReady += (source, Lframe) => //Trigger everytime when frame is ready
        {
            time = Videoplayer.time;
            time_txt.text = time.ToString("F3") + " s";
            if (auto_track.Auto_Trace_Toggel)
            {
                auto_track.Tracking();
            }
        };
        Output.text = "Vid_1";
        Select_Vid = false;
        Videoplayer.url = FilePicker.Vid1_path;
        Videoplayer.prepareCompleted += source => AssignPic(0);
        RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1); //Rotate according to the metadata
    }


    public void Switch() //To switch between vid1 and vid2
    {
        if (!Select_Vid) //Set the correspoding video and dot
        {
            Output.text = "Vid_2";
            Select_Vid = true; //To vid 2
            Videoplayer.url = FilePicker.Vid2_path;
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation2); //Rotate according to the metadata
            auto_track.current_vid = 1;
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = false; //To vid 1
            Videoplayer.url = FilePicker.Vid1_path;
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1); //Rotate according to the metadata
            auto_track.current_vid = 0;
        }
        Videoplayer.prepareCompleted += source => //Wait until the video is ready
        {
            frame_select.Current_value = 0; 
            frame_select.value_change();//Set the frame
        };
        dot_render.Re_render_dot(Select_Vid); //Re-render the dot
    }

    public void AssignPic(int frame) //Set the video to the frame
    {
        Videoplayer.Pause();
        Videoplayer.frame = frame; //Set the video to the right frame
        RawImage_Img.color = Color.white; //Remove tint
        int width = (int)(Videoplayer.width * 4032 / Videoplayer.height); //Scale the video for different resolution
        RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
    }

    void OnDisable() 
    {
        if (Videoplayer != null)
        {
            Videoplayer.sendFrameReadyEvents = false; //To disable Videoplayer.frameReady method (It could use a lot of gpu)
        }
    }
}
