using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Frame_ext : MonoBehaviour
{
    public RenderTexture renderTexture1;
    public RenderTexture renderTexture2;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public GameObject myGameObject1;
    public GameObject myGameObject2;
    public static List<byte[]> byteslist1 = new List<byte[]>();
    public static List<byte[]> byteslist2 = new List<byte[]>();
    private bool started1;
    private bool started2;
    private int i1 = 0;
    private int i2 = 0;
    private int n1 = 1;
    private int n2 = 1;
    private float skip1;
    private float skip2;
    public int fps;
    private double vid_lenght1;
    private double vid_lenght2;
    public static int width1;
    public static int width2;
    public static int height1;
    public static int height2;

    public void button1()
    {
        i1 = 0;
        byteslist1.Clear();
        StartCoroutine(ExtractFramesCoroutine1());
    }

    public void button2()
    {
        i2 = 0;
        byteslist2.Clear();
        StartCoroutine(ExtractFramesCoroutine2());
    }

    private void VideoPreparationComplete1(VideoPlayer source)
    {
        started1 = true;
        vid_lenght1 = videoPlayer1.length;
        n1 = (int)(vid_lenght1 * fps);
        skip1 = (float)(1000 / fps);
        width1 = (int)videoPlayer1.width;
        height1 = (int)videoPlayer1.height;
        renderTexture1.Release();
        renderTexture1.width = width1;
        renderTexture1.height = height1;
    }

    private void VideoPreparationComplete2(VideoPlayer source)
    {
        started2 = true;
        vid_lenght2 = videoPlayer2.length;
        n2 = (int)(vid_lenght2 * fps);
        skip2 = (float)(1000 / fps);
        width2 = (int)videoPlayer2.width;
        height2 = (int)videoPlayer2.height;
        renderTexture2.Release();
        renderTexture2.width = width2;
        renderTexture2.height = height2;
    }

    private IEnumerator ExtractFramesCoroutine1()
    {
        videoPlayer1.Pause();
        started1 = false;
        videoPlayer1.prepareCompleted += VideoPreparationComplete1;
        float time1 = 0.1f;
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

                Texture2D texturein1 = new Texture2D(width1, height1, TextureFormat.RGB24, false);
                RenderTexture.active = renderTexture1;
                texturein1.ReadPixels(new Rect(0, 0, width1, height1), 0, 0);
                texturein1.Apply();

                Color[] pixels = texturein1.GetPixels();
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = pixels[i].gamma; // Convert to gamma space
                }
                texturein1.SetPixels(pixels);
                texturein1.Apply();

                byte[] bytes = texturein1.EncodeToJPG();
                byteslist1.Add(bytes);


                if (i1 == 2)
                {
                    RawImage rawImage1 = myGameObject1.GetComponent<RawImage>();
                    Texture2D texture1 = new Texture2D(width1, width1 / 2, TextureFormat.RGB24, false);
                    texture1.ReadPixels(new Rect(0, height1 / 2 - width1 / 4, width1, width1 / 2), 0, 0);
                    texture1.Apply();
                    texture1.SetPixels(pixels);
                    texture1.Apply();

                    rawImage1.texture = texture1;
                }
                RenderTexture.active = null;
                Destroy(texturein1);
            }

            yield return null;
        }
/*        for (int i = 1; i < byteslist1.Count; i++)
        {
            byte[] bytes = byteslist1[i];
            string path = Path.Combine(Application.dataPath + "/My Asset/Frames/1/Img1_" + i.ToString() + ".png");
            File.WriteAllBytes(path, bytes);
            Debug.Log("Image saved to: " + path + "_1");
        }*/
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

                Texture2D texturein2 = new Texture2D(width2, height2, TextureFormat.RGB24, false);
                RenderTexture.active = renderTexture2;
                texturein2.ReadPixels(new Rect(0, 0, width2, height2), 0, 0);
                texturein2.Apply();

                Color[] pixels = texturein2.GetPixels();
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = pixels[i].gamma; // Convert to gamma space
                }
                texturein2.SetPixels(pixels);
                texturein2.Apply();

                byte[] bytes = texturein2.EncodeToJPG();
                byteslist2.Add(bytes);


                if (i2 == 2)
                {
                    RawImage rawImage2 = myGameObject2.GetComponent<RawImage>();
                    Texture2D texture2 = new Texture2D(width2, width2 / 2);
                    texture2.ReadPixels(new Rect(0, height2 / 2 - width2 / 4, width2, width2 / 2), 0, 0);
                    texture2.Apply();
                    texture2.SetPixels(pixels);
                    texture2.Apply();
                    rawImage2.texture = texture2;
                }
                RenderTexture.active = null;
                Destroy(texturein2);
            }

            yield return null;
        }
        //for (int i = 0; i < byteslist2.Count; i++)
        //{
        //    byte[] bytes = byteslist2[i];
        //    string path = Path.Combine(Application.dataPath + "/My Asset/Frames/2/Img2_" + i.ToString() + ".png");
        //    File.WriteAllBytes(path, bytes);
        //    Debug.Log("Image saved to: " + path + "_2");
        //}
        i2 = 0;
    }
}