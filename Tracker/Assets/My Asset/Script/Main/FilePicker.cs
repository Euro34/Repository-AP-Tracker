using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class FilePicker : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public TextMeshProUGUI Vid1_holder;
    public TextMeshProUGUI Vid2_holder;
    public static string Vid1_path;
    public static string Vid2_path;
    public static float rotation1;
    public static float rotation2;
    public Frame_ext frame_ext;
    // Start is called before the first frame update

    public void Import1()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            if (path != null)
            {
                rotation1 = -NativeGallery.GetVideoProperties(path).rotation;
                Vid1_path = path;
                videoPlayer1.url = Vid1_path;
                Vid1_holder.text = " ";
                frame_ext.button1();
            }
            else if (videoPlayer1.url == null)
            {
                Vid1_holder.text = "+";
            }
        });
    }
    public void Import2()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            if (path != null)
            {
                rotation2 = -NativeGallery.GetVideoProperties(path).rotation;
                Vid2_path = path;
                videoPlayer2.url = Vid2_path;
                Vid2_holder.text = " ";
                frame_ext.button2();
            }else if (videoPlayer2.url == null)
            {
                Vid2_holder.text = "+";
            }
        });
    }
    void Start()
    {
        if (Vid1_path != null)
        {
            videoPlayer1.url = Vid1_path;
            Vid1_holder.text = " ";
            frame_ext.button1();
        }
        else
        {
            Vid1_holder.text = "+";
        }
        if (Vid2_path != null)
        {
            videoPlayer2.url = Vid2_path;
            Vid2_holder.text = " ";
            frame_ext.button2();
        }
        else
        {
            Vid2_holder.text = "+";
        }
    }
}