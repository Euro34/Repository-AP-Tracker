using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

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
        player.prepareCompleted += (source => //Wait until there is a path to call PreparationComplete() method
        {
            PreparationComplete(player, rawImage);
        });
    }
    void PreparationComplete(VideoPlayer player, RawImage rawImage)
    {
        rawImage.color = Color.white; //Remove the tint
        StartCoroutine(SetVideoTime(player));
    }
    IEnumerator SetVideoTime(VideoPlayer player)
    {
        yield return new WaitForSeconds(0.1f);
        player.time = player.length / 2; //Set thumbnail to the middle of the video
        player.Pause();
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
    void duration_compare() //Set the variable "duration" = the duration of the shortest between them
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