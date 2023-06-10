using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using System;

public class Frame_ext : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture renderTexture;
    public Vid_Ended vid_Ended;
    private int i = 0;
    private short button;
    public List<byte[]> byteslist = new List<byte[]>();

    public void button1()
    {
        button = 1;
    }
    public void button2()
    {
        button = 2;
    }
    void Start()
    {
        StartCoroutine(ExtractFramesCoroutine());
    }

    // Update is called once per frame
    private IEnumerator ExtractFramesCoroutine()
    {
        while (true)
        {
            if (vid_Ended.isVideoPlaying && vid_Ended.Check <= 2)
            {
                if (i > 1)
                {
                    Texture2D texturein = new Texture2D(480, 270, TextureFormat.RGB24, false);

                    // Read the pixel data from the Render Texture into the Texture2D
                    RenderTexture.active = renderTexture;
                    texturein.ReadPixels(new Rect(0, 0, 480, 270), 0, 0);
                    texturein.Apply();

                    // Convert the Texture2D to a byte array
                    byte[] bytes = texturein.EncodeToPNG();

                    byteslist.Add(bytes);

                    // Don't forget to clean up
                    RenderTexture.active = null;
                    Destroy(texturein);
                    Debug.Log(byteslist.Count);
                }
                i++;
            }
            else
            {
                if (i > 1)
                {
                    for(i = 0; i < byteslist.Count; i++)
                    {
                        //byte[] bytes = byteslist[i];
                        // Save the image to a file (optional)
                        //string path = Path.Combine(Application.dataPath, @"Frames/Img_" + button + "_" + (i - 1).ToString() + ".png");
                        //File.WriteAllBytes(path, bytes);
                        //Debug.Log("Image saved to: " + path);
                    }
                    //string path = Path.Combine(Application.dataPath, @"Frames/vid" + button + "_thumbnail.png");
                    //File.WriteAllBytes(path, byteslist[10]);
                    //Debug.Log("Image saved to: " + path);

                    // Find the game object with RawImage component
                    GameObject myGameObject = GameObject.Find("RawImage_vid1");

                    // Get the RawImage component
                    RawImage rawImage = myGameObject.GetComponent<RawImage>();

                    // Load the image into a Texture2D
                    Texture2D texture = new Texture2D(300, 100);
                    texture.LoadImage(byteslist[10]);

                    // Assign the Texture2D to the RawImage component
                    rawImage.texture = texture;
                    i = 0;
                }
            }
            Thread.Sleep(1);
            yield return null;
        }
    }
    void Update()
    {

    }
}
