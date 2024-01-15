using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Frame_ext : MonoBehaviour
{
    public RenderTexture renderTexture1;
    public RenderTexture renderTexture2;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public RawImage rawImage1;
    public RawImage rawImage2;
    public static List<List<byte[]>> byteslist = new List<List<byte[]>> { new List<byte[]>(), new List<byte[]>()};
    public static Texture2D[] textures = new Texture2D[2];
    private int[] n = new int[2] {1,1};
    private float skip;
    public int fps;
    private double[] vid_lenght = new double[2];
    public static int[] width = new int[2];
    public static int[] height = new int[2];

    void Start()
    {
        skip = (float)(1000 / fps);
        if(textures[0] != null)
        {
            rawImage1.texture = textures[0];
        }
        if (textures[1] != null)
        {
            rawImage2.texture = textures[1];
        }
    }

    public void button1()
    {
        byteslist[0].Clear();
        StartCoroutine(ExtractFramesCoroutine(videoPlayer1, renderTexture1, byteslist[0], rawImage1,0));
    }

    public void button2()
    {
        byteslist[1].Clear();
        StartCoroutine(ExtractFramesCoroutine(videoPlayer2, renderTexture2, byteslist[1], rawImage2,1));
    }
    
    private void VideoPreparationComplete(int vid_no, VideoPlayer videoPlayer, RenderTexture renderTexture)
    {
        vid_lenght[vid_no] = videoPlayer.length;
        n[vid_no] = (int)(vid_lenght[vid_no] * fps);
        width[vid_no] = (int)videoPlayer.texture.width;
        height[vid_no] = (int)videoPlayer.texture.height;
        renderTexture.Release();
        renderTexture.width = width[vid_no];
        renderTexture.height = height[vid_no];
    }

    private IEnumerator ExtractFramesCoroutine(VideoPlayer videoPlayer, RenderTexture renderTexture, List<byte[]> bytesList, RawImage rawImage, int vid_no)
    {
        videoPlayer.Pause();
        bool started = false;
        videoPlayer.prepareCompleted += (source) =>
        {
            started = true;
            VideoPreparationComplete(vid_no, videoPlayer, renderTexture);
            // You can also set other properties here if needed
        };
        float time = 0f;
        int i = 0;
        while (i <= n[vid_no])
        {
            if (started)
            {
                i++;
            }
            if (started && i > 1)
            {
                videoPlayer.time = time;
                time += skip / 1000;
                yield return new WaitForSeconds(0.1f);

                Texture2D textureIn = new Texture2D(width[vid_no], height[vid_no], TextureFormat.RGB24, false);
                RenderTexture.active = renderTexture;
                textureIn.ReadPixels(new Rect(0, 0, width[vid_no], height[vid_no]), 0, 0);
                textureIn.Apply();

                Color[] pixels = textureIn.GetPixels();
                for (int j = 0; j < pixels.Length; j++)
                {
                    pixels[j] = pixels[j].gamma; // Convert to gamma space
                }
                textureIn.SetPixels(pixels);
                textureIn.Apply();

                byte[] bytes = textureIn.EncodeToJPG();
                bytesList.Add(bytes);

                if (i == 2)
                {
                    Texture2D texture = new Texture2D(width[vid_no], height[vid_no] / 2, TextureFormat.RGB24, false);
                    texture.ReadPixels(new Rect(0, height[vid_no] / 2 - width[vid_no] / 4, width[vid_no], height[vid_no] / 2), 0, 0);
                    texture.Apply();
                    texture.SetPixels(pixels);
                    texture.Apply();
                    rawImage.texture = texture;
                    textures[vid_no] = texture;
                }
                RenderTexture.active = null;
                Destroy(textureIn);
            }

            yield return null;
        }

        i = 0;
    }
}
/*        for (int i = 1; i < byteslist1.Count; i++)
        {
            byte[] bytes = byteslist1[i];
            string path = Path.Combine(Application.dataPath + "/My Asset/Frames/1/Img1_" + i.ToString() + ".png");
            File.WriteAllBytes(path, bytes);
            Debug.Log("Image saved to: " + path + "_1");
        }*/