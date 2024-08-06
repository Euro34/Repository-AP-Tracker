using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System;

public class Frame_ext : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public RawImage rawImage1;
    public RawImage rawImage2;
    public static double[] fps = new double[2];
    public static double[] duration = new double[2];
    public static int[] framecount = new int[2];
    void Thumbnail(VideoPlayer player, RawImage rawImage, int vid)
    {
        player.prepareCompleted += (source => //Wait until there is a path to call PreparationComplete() method
        {
            PreparationComplete(player, rawImage, vid);
        });
    }
    void PreparationComplete(VideoPlayer player, RawImage rawImage, int vid)
    {
        rawImage.color = Color.white; //Remove the tint
        StartCoroutine(SetVideoTime(player, vid));
    }
    IEnumerator SetVideoTime(VideoPlayer player, int vid)
    {
        yield return new WaitForSeconds(0.001f);
        player.frame = 1; //Set the time on the vid to be the first frame of the video
        fps[vid] = player.frameRate; //Collect the fps of each video
        duration[vid] = player.length; //Collect duration of each video
        framecount[vid] = (int)player.frameCount; //Collect frame count of each video
        player.Pause();
    }
    public void button1()
    {
        Thumbnail(videoPlayer1, rawImage1, 0);
    }
    public void button2()
    {
        Thumbnail(videoPlayer2, rawImage2, 1);
    }
}