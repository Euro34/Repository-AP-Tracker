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
    private RenderTexture renderTexture;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    private VideoPlayer videoPlayer;
    public GameObject myGameObject;
    private List<byte[]> byteslist = new List<byte[]>();
    public List<byte[]> byteslist1 = new List<byte[]>();
    public List<byte[]> byteslist2 = new List<byte[]>();
    private bool started;
    private int i = 0;
    private short button;
    private int n = 10000;
    private int skip;
    public int fps = 20;
    private double vid_lenght;

    public void button1()
    {
        button = 1;
        i = 0;
        byteslist = new List<byte[]>();
        renderTexture = renderTexture1;
        videoPlayer = videoPlayer1;
        myGameObject = GameObject.Find("RawImage_vid1");
        StartCoroutine(ExtractFramesCoroutine());
        byteslist1 = byteslist;
    }
    
    public void button2()
    {
        button = 2;
        i = 0;
        byteslist = new List<byte[]>();
        renderTexture = renderTexture2;
        videoPlayer = videoPlayer2;
        myGameObject = GameObject.Find("RawImage_vid2");
        StartCoroutine(ExtractFramesCoroutine());
        byteslist2 = byteslist;
    }

    private void VideoPreparationComplete(VideoPlayer source)
    {
        started = true;
        vid_lenght = videoPlayer.length;
        n = (int)(vid_lenght * fps);
        skip = (int)(1000 / fps);
    }

    // Update is called once per frame
    private IEnumerator ExtractFramesCoroutine()
    {
        videoPlayer.Pause();
        started = false;
        videoPlayer.prepareCompleted += VideoPreparationComplete;
        while (i < n)
        {
            if (started)
            {
                i++;
            }
            if (started == true && i > 1)
            {
                videoPlayer.time += skip / 1000;
                Texture2D texturein = new Texture2D(480, 270, TextureFormat.RGB24, false);

                // Read the pixel data from the Render Texture into the Texture2D
                RenderTexture.active = renderTexture;
                texturein.ReadPixels(new Rect(0, 0, 480, 270), 0, 0);
                texturein.Apply();

                // Convert the Texture2D to a byte array
                byte[] bytes = texturein.EncodeToJPG();

                byteslist.Add(bytes);

                // Don't forget to clean up
                RenderTexture.active = null;
                Destroy(texturein);
                Debug.Log(byteslist.Count);

                if (i == 12)
                {
                    RawImage rawImage = myGameObject.GetComponent<RawImage>();
                    Texture2D texture = new Texture2D(300, 100);
                    texture.LoadImage(byteslist[10]);
                    rawImage.texture = texture;
                }
            }
            yield return null;
        }
        //if (i > 1)
        //{
        //    for (i = 0; i < byteslist.Count; i++)
        //    {
        //        //byte[] bytes = byteslist[i];
        //        // Save the image to a file (optional)
        //        //string path = Path.Combine(Application.dataPath, @"Frames/Img_" + button + "_" + (i - 1).ToString() + ".png");
        //        //File.WriteAllBytes(path, bytes);
        //        //Debug.Log("Image saved to: " + path);
        //    }
        //    i = 0;
        //}
    }

    void Update()
    {

    }
}
