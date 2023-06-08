using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

public class Vid_Ended : MonoBehaviour
{
    public short Check = 0;
    public GameObject videoPlayerObject;

    private VideoPlayer videoPlayer;
    public bool isVideoPlaying = false;

    private void Start()
    {
        // Get the reference to the VideoPlayer component
        videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();

        // Subscribe to the prepareCompleted event
        videoPlayer.prepareCompleted += OnVideoPrepareCompleted;
    }

    private void OnVideoPrepareCompleted(VideoPlayer source)
    {
        // Video has started playing
        isVideoPlaying = true;
        Debug.Log("Video Started");
    }

    private void Update()
    {
        if (isVideoPlaying && !videoPlayer.isPlaying)
        {
            // If Check = 2 Video has reached the ended
            Check += 1;
        }
        if(Check > 2)
        {
            Check = 0;
            isVideoPlaying=false;
            videoPlayer.prepareCompleted += OnVideoPrepareCompleted;
        }
    }
}
