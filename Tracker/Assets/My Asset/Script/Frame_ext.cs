using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class Frame_ext : MonoBehaviour
{
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
    private float skip1;
    private float skip2;
    public int fps = 2;
    private double vid_length1;
    private double vid_length2;

    public void button1()
    {
        button = 1;
        i1 = 0;
        byteslist1.Clear();
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
        vid_length1 = videoPlayer1.length;
        n1 = (int)(vid_length1 * fps);
        skip1 = (float)(1000 / fps);
    }

    private void VideoPreparationComplete2(VideoPlayer source)
    {
        started2 = true;
        vid_length2 = videoPlayer2.length;
        n2 = (int)(vid_length2 * fps);
        skip2 = (float)(1000 / fps);
    }

    private IEnumerator ExtractFramesCoroutine1()
    {
        videoPlayer1.Pause();
        started1 = false;
        videoPlayer1.prepareCompleted += VideoPreparationComplete1;
        float time1 = 0f;
        while (i1 < n1)
        {
            if (started1)
            {
                i1++;
            }
            if (started1 && i1 > 1)
            {
                videoPlayer1.time = time1;
                time1 += skip1 / 1000;

                yield return new WaitForSeconds(0.1f);

                RenderTexture.active = renderTexture1;
                Texture2D texturein1 = new Texture2D(renderTexture1.width, renderTexture1.height, TextureFormat.RGB24, false);
                texturein1.ReadPixels(new Rect(0, 0, renderTexture1.width, renderTexture1.height), 0, 0);
                texturein1.Apply();

                Texture2D correctedTexture1 = ApplyGammaCorrection(texturein1);

                byte[] bytes = correctedTexture1.EncodeToJPG();
                byteslist1.Add(bytes);

                if (i1 == 2)
                {
                    RawImage rawImage1 = myGameObject1.GetComponent<RawImage>();
                    Texture2D texture1 = new Texture2D(300, 100);
                    texture1.ReadPixels(new Rect(90, 85, 300, 100), 0, 0);
                    texture1.Apply();
                    rawImage1.texture = texture1;
                }

                RenderTexture.active = null;
                Destroy(texturein1);
                Destroy(correctedTexture1);
            }

            yield return null;
        }

        i1 = 0;
    }

    private IEnumerator ExtractFramesCoroutine2()
    {
        videoPlayer2.Pause();
        started2 = false;
        videoPlayer2.prepareCompleted += VideoPreparationComplete2;
        float time2 = 0f;
        while (i2 < n2)
        {
            if (started2)
            {
                i2++;
            }
            if (started2 && i2 > 1)
            {
                videoPlayer2.time = time2;
                time2 += skip2 / 1000;

                yield return new WaitForSeconds(0.1f);

                RenderTexture.active = renderTexture2;
                Texture2D texturein2 = new Texture2D(renderTexture2.width, renderTexture2.height, TextureFormat.RGB24, false);
                texturein2.ReadPixels(new Rect(0, 0, renderTexture2.width, renderTexture2.height), 0, 0);
                texturein2.Apply();

                Texture2D correctedTexture2 = ApplyGammaCorrection(texturein2);

                byte[] bytes = correctedTexture2.EncodeToJPG();
                byteslist2.Add(bytes);

                if (i2 == 2)
                {
                    RawImage rawImage2 = myGameObject2.GetComponent<RawImage>();
                    Texture2D texture2 = new Texture2D(300, 100);
                    texture2.ReadPixels(new Rect(90, 85, 300, 100), 0, 0);
                    texture2.Apply();
                    rawImage2.texture = texture2;
                }

                RenderTexture.active = null;
                Destroy(texturein2);
                Destroy(correctedTexture2);
            }

            yield return null;
        }

        i2 = 0;
    }

    private Texture2D ApplyGammaCorrection(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = pixels[i].gamma;
        }

        Texture2D correctedTexture = new Texture2D(texture.width, texture.height);
        correctedTexture.SetPixels(pixels);
        correctedTexture.Apply();

        return correctedTexture;
    }
}
