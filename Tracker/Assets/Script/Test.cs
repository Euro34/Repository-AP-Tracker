using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Test : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int frameWidth = 640;
    public int frameHeight = 480;
    public string outputFolder = "C:\\Users\\User\\Documents\\GitHub\\Repository-Tracker\\Tracker\\Assets\\Frames";
    public FilePicker Script;

    private bool isExtractingFrames = false;
    // Start is called before the first frame update
    void Start()
    {
        //videoPlayer.prepareCompleted += VideoPlayerPrepareCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExtractFrames();
        }
    }
    public void ExtractFrames()
    {
        if (isExtractingFrames)
        {
            Debug.Log("Frame extraction is already in progress.");
            return;
        }

        isExtractingFrames = true;
        Directory.CreateDirectory(outputFolder);

        // Set video player to the current video clip
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, Script.Vid1_path);

        // Prepare the video player to get frame count
        videoPlayer.Prepare();
    }

    private void VideoPlayerPrepareCompleted(VideoPlayer vp)
    {
        // Get the total number of frames in the video clip
        long totalFrames = (long)vp.frameCount;

        // Extract each frame
        for (long frame = 0; frame < totalFrames; frame++)
        {
            // Set the current frame to extract
            vp.frame = frame;

            // Wait for the next frame to be ready
            while (!vp.isPrepared)
            {
                continue;
            }

            // Capture the current frame
            Texture2D texture = new Texture2D(frameWidth, frameHeight, TextureFormat.RGB24, false);
            RenderTexture.active = (UnityEngine.RenderTexture)vp.texture;
            texture.ReadPixels(new Rect(0, 0, frameWidth, frameHeight), 0, 0);
            texture.Apply();

            // Save the frame as an image file
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(outputFolder, $"frame_{frame}.png"), bytes);

            // Clean up the texture
            Destroy(texture);
            Debug.Log("Frame extraction Inprogress");
        }

        Debug.Log("Frame extraction complete.");
        isExtractingFrames = false;
    }
}
