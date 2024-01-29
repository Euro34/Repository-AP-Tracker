using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Frame_ext : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public RawImage rawImage1;
    public RawImage rawImage2;
    public static double duration;
    private bool bool1 = false;
    private bool bool2 = false;
    void Vid_title(VideoPlayer player, RawImage rawImage)
    {
        player.prepareCompleted += (source =>
        {
            PreparationComplete(player, rawImage);
        });
    }
    void PreparationComplete(VideoPlayer player, RawImage rawImage)
    {
        player.Pause();
        rawImage.color = Color.white;
        player.time = player.length / 2;
        duration_compare();
    }
    public void button1()
    {
        Vid_title(videoPlayer1, rawImage1);
        bool1 = true;
    }
    public void button2()
    {
        Vid_title(videoPlayer2, rawImage2);
        bool2 = true;
    }
    void duration_compare()
    {
        if(bool1 && bool2)
        {
            if (videoPlayer1.length < videoPlayer2.length)
            {
                duration = videoPlayer1.length;
            }
            else
            {
                duration = videoPlayer2.length;
            }
        }
    }
}