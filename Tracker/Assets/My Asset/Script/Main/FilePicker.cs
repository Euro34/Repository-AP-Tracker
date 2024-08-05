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

    void Start()
    {
        if (Vid1_path != null) //For Dev. Otherwise it just go straight to else
        {
            videoPlayer1.url = Vid1_path;
            Vid1_holder.text = " ";
            frame_ext.button1();
        }
        else
        {
            Vid1_holder.text = "+";
        }
        if (Vid2_path != null) //For Dev. Otherwise it just go straight to else
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
    public void Import1()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) => //Request permission then import
        {
            if (path != null)
            {
                rotation1 = -NativeGallery.GetVideoProperties(path).rotation; //Read the rotation matadata
                Vid1_path = path;
                videoPlayer1.url = Vid1_path;
                Vid1_holder.text = " "; //Set the button text to an empty string
                frame_ext.button1(); //Call frame_ext to show thumbnail
                Debug.Log("Vid1 path : " + Vid1_path);
            }
        });
    }
    public void Import2()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) => //Request permission then import
        {
            if (path != null)
            {
                rotation2 = -NativeGallery.GetVideoProperties(path).rotation; //Read the rotation matadata
                Vid2_path = path;
                videoPlayer2.url = Vid2_path;
                Vid2_holder.text = " "; //Set the button text to an empty string
                frame_ext.button2(); //Call frame_ext to show thumbnail
                Debug.Log("Vid2 path : " + Vid2_path);
            }
        });
    }
}