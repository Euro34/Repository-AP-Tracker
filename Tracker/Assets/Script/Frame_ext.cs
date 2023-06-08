using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public class Frame_ext : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture renderTexture;
    public Vid_Ended vid_Ended;
    private int i;
    private short button;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vid_Ended.isVideoPlaying && vid_Ended.Check <= 2)
        {
            if (i != 0)
            {
                Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

                // Read the pixel data from the Render Texture into the Texture2D
                RenderTexture.active = renderTexture;
                texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                texture.Apply();

                // Convert the Texture2D to a byte array
                byte[] bytes = texture.EncodeToPNG();

                // Save the image to a file (optional)
                string path = Path.Combine(Application.dataPath, @"Frames\Img_" + button + "_" + i.ToString() + ".png");
                File.WriteAllBytes(path, bytes);
                Debug.Log("Image saved to: " + path);

                // Don't forget to clean up
                RenderTexture.active = null;
                Destroy(texture);
                Debug.Log("Done");
            }
            i++;
        }
        else
        {
            i = 0;
        }
    }
}
