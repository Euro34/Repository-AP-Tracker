using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class Frame_select : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public VideoPlayer videoPlayer1;
    public Button Button_del;
    public Dot_render2 dot_Render2;
    public Vid_select_Switch2 vid_Select2;
    public static float fps;
    void Start()
    {
        Current_value = 1; //Start at frame 1
        fps = videoPlayer1.frameRate; //Set the prefer framerate to the vid1 framerate
    }
    public void up()
    {
        Current_value++;
        if (Current_value > (Frame_ext.duration * fps))
        {
            Current_value--;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    public void down()
    {
        Current_value--;
        if (Current_value <= 0)
        {
            Current_value++;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    private void value_change()
    {
        dot_Render2.color_change(); //Set other frames opacity
        vid_Select2.AssignPic(Current_value - 1); //Change the frame to the right one
    }
}