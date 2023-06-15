using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using System;

public class Frame_ext : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture renderTexture1;
    public RenderTexture renderTexture2;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public GameObject myGameObject1;
    public GameObject myGameObject2;
    public List<byte[]> byteslist1 = new List<byte[]>();
    public List<byte[]> byteslist2 = new List<byte[]>();
    private bool started1;
    private bool started2;
    private int i1 = 0;
    private int i2 = 0;
    private short button;
    private int n1 = 1;
    private int n2 = 1;
    private double skip1;
    private double skip2;
    public int fps = 20;
    private double vid_lenght1;
    private double vid_lenght2;

    public void button1()
    {
        button = 1;
        i1 = 0;
        byteslist2.Clear();
        myGameObject1 = GameObject.Find("RawImage_vid1");
        StartCoroutine(ExtractFramesCoroutine1());
    }
    
    public void button2()
    {
        button = 2;
        i2 = 0;
        byteslist2.Clear();
        myGameObject2 = GameObject.Find("RawImage_vid2");
        StartCoroutine(ExtractFramesCoroutine2());
    }

    private void VideoPreparationComplete1(VideoPlayer source)
    {
        started1 = true;
        vid_lenght1 = videoPlayer1.length;
        n1 = (int)(vid_lenght1 * fps);
        skip1 = (1000 / fps);
    }
    private void VideoPreparationComplete2(VideoPlayer source)
    {
        started2 = true;
        vid_lenght2 = videoPlayer2.length;
        n2 = (int)(vid_lenght2 * fps);
        skip2 = (1000 / fps);
    }

    // Update is called once per frame
    private IEnumerator ExtractFramesCoroutine1()
    {
        videoPlayer1.Pause();
        started1 = false;
        videoPlayer1.prepareCompleted += VideoPreparationComplete1;
        while (i1 < n1)
        {
            if (started1)
            {
                i1++;
            }
            if (started1 == true && i1 > 1)
            {
                videoPlayer1.time += (double)skip1 / 1000;
                Texture2D texturein1 = new Texture2D(480, 270, TextureFormat.RGB24, false);

                // Read the pixel data from the Render Texture into the Texture2D
                RenderTexture.active = renderTexture1;
                texturein1.ReadPixels(new Rect(0, 0, 480, 270), 0, 0);
                texturein1.Apply();

                // Convert the Texture2D to a byte array
                byte[] bytes = texturein1.EncodeToJPG();

                byteslist1.Add(bytes);

                // Don't forget to clean up
                RenderTexture.active = null;
                Destroy(texturein1);
                Debug.Log(byteslist1.Count);

                if (i1 == 12)
                {
                    RawImage rawImage1 = myGameObject1.GetComponent<RawImage>();
                    Texture2D texture1 = new Texture2D(300, 100);
                    texture1.ReadPixels(new Rect(0, 0, 300, 100), 0, 0);
                    texture1.Apply();
                    rawImage1.texture = texture1;
                }
            }
            yield return null;
        }
    }
    private IEnumerator ExtractFramesCoroutine2()
    {
        videoPlayer2.Pause();
        started2 = false;
        videoPlayer2.prepareCompleted += VideoPreparationComplete2;
        while (i2 < n2)
        {
            if (started2)
            {
                i2++;
            }
            if (started2 == true && i2 > 1)
            {
                videoPlayer2.time += (double)skip2 / 1000;
                Texture2D texturein2 = new Texture2D(480, 270, TextureFormat.RGB24, false);

                // Read the pixel data from the Render Texture into the Texture2D
                RenderTexture.active = renderTexture2;
                texturein2.ReadPixels(new Rect(0, 0, 480, 270), 0, 0);
                texturein2.Apply();

                // Convert the Texture2D to a byte array
                byte[] bytes = texturein2.EncodeToJPG();

                byteslist2.Add(bytes);

                // Don't forget to clean up
                Destroy(texturein2);
                Debug.Log(byteslist2.Count);

                if (i2 == 12)
                {
                    RawImage rawImage2 = myGameObject2.GetComponent<RawImage>();
                    Texture2D texture2 = new Texture2D(300, 100);
                    texture2.ReadPixels(new Rect(0, 0, 300, 100), 0, 0);
                    texture2.Apply();
                    rawImage2.texture = texture2;
                }
                RenderTexture.active = null;
            }
            yield return null;
        }
    }
}
