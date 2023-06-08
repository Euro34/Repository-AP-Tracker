using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public class Frame_ext : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture renderTexture;
    void Start()
    {
        Thread.Sleep(2000);
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        // Read the pixel data from the Render Texture into the Texture2D
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Convert the Texture2D to a byte array
        byte[] bytes = texture.EncodeToPNG();

        // Save the image to a file (optional)
        string path = Path.Combine(Application.persistentDataPath, @"C:\Users\User\Documents\GitHub\Repository-Tracker\Tracker\Assets\Frames\test1.png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("Image saved to: " + path);

        // Don't forget to clean up
        RenderTexture.active = null;
        Destroy(texture);
        Debug.Log("Done");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
