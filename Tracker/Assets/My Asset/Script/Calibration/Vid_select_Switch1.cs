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

    void Start() //Set the 
    {
        Output.text = "Vid_1";
        Select_Vid = false;
        Videoplayer.url = FilePicker.Vid1_path;
        Videoplayer.prepareCompleted += source => AssignPic();
        dot_render.Reset_(Select_Vid); //Set the video player
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
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = false; //To vid 1
            Videoplayer.url = FilePicker.Vid1_path;
            RawImage_Img.transform.rotation = Quaternion.Euler(0, 0, FilePicker.rotation1); //Rotate according to the metadata
        }
        Videoplayer.prepareCompleted += source => AssignPic();
        dot_render.Reset_(Select_Vid); //Re-render the dot
    }

    private void AssignPic()
    {
        Videoplayer.Pause();
        Videoplayer.frame = 1; //Set the time on the vid to be the first frame of the video
        RawImage_Img.color = Color.white; //Remove tint
        int width = (int)(Videoplayer.width * 4032 / Videoplayer.height); //Scale the video for different resolution
        RawImage_Img.rectTransform.sizeDelta = new Vector2(width, 4032);
    }
}
