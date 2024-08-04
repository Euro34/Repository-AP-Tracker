using OpenCvSharp.Tracking;
using OpenCvSharp;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading;

public class Auto_track : MonoBehaviour
{
    public RectTransform Auto_Trace_Panel;
    public RenderTexture input;
    public RawImage output;
    public Mat current_frame = null;
    public VideoPlayer videoPlayer;
    public Frame_select ref_Point;
    public Dot_render2 dot_Render2;
    public int current_vid = 0;
    public bool Auto_Trace_Toggel = false;
    public Rect2d boundingBox;
    public Vector2 initialPosition = new Vector2();
    public Vector2 size = new Vector2();
    private Tracker tracker;
    private bool isInitialized = false;

    private void Start()
    {
        Auto_Trace_Panel.gameObject.SetActive(false);
    }

    public void Auto_trace_start()
    {
        Auto_Trace_Toggel = !Auto_Trace_Toggel;
        if (Auto_Trace_Toggel)
        {
            Auto_Trace_Panel.gameObject.SetActive(true);
        }
        else
        {
            Auto_Trace_Panel.gameObject.SetActive(false);
        }
    }
    public IEnumerator start_track()
    {
        yield return new WaitForSeconds((float)(1.0 / videoPlayer.frameRate));
        current_frame = OpenCvSharp.Unity.TextureToMat(rt_to_texture(input));
        tracker = Tracker.Create(TrackerTypes.KCF);
        double width = (double)((int)size.x);
        double height = (double)((int)size.y);
        boundingBox = new Rect2d(initialPosition.x, initialPosition.y, width, height);
        isInitialized = tracker.Init(current_frame, boundingBox);
        Tracking();
    }
    public void Tracking()
    {
        current_frame = OpenCvSharp.Unity.TextureToMat(rt_to_texture(input));
        if (isInitialized && tracker.Update(current_frame, ref boundingBox))
        {
            Point point1 = new Point(boundingBox.X, boundingBox.Y);
            Point point2 = new Point(boundingBox.X + boundingBox.Width, boundingBox.Y + boundingBox.Height);
            Cv2.Rectangle(current_frame, point1, point2, new Scalar(0, 0, 255), 2);
            Vector2 mid_pos = new Vector2();
            mid_pos.x = (float)(boundingBox.X + boundingBox.Width / 2);
            mid_pos.y = (float)(boundingBox.Y + boundingBox.Height / 2);
            mid_pos = realpos_to_visualpos(mid_pos);
            dot_Render2.Save_Pos(mid_pos.x, mid_pos.y, current_vid, ref_Point.Current_value);
        }
        output.texture = OpenCvSharp.Unity.MatToTexture(current_frame);
    }
    Texture2D rt_to_texture(RenderTexture rt)
    {
        Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = rt;

        texture.ReadPixels(new UnityEngine.Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();

        RenderTexture.active = currentActiveRT;

        return texture;
    }
    public Vector2 visualpos_to_realpos(Vector2 pos)
    {
        Vector2 realpos = new Vector2();
        Vector2 rawimg_size = output.rectTransform.sizeDelta;
        int width = output.texture.width;
        int height = output.texture.height;
        realpos.x = (pos.x*width/rawimg_size.x) + (width/2);
        realpos.y = (height/2) - (pos.y*height/rawimg_size.y);
        return realpos;
    }
    public Vector2 realpos_to_visualpos(Vector2 pos)
    {
        Vector2 visualpos = new Vector2();
        Vector2 rawimg_size = output.rectTransform.sizeDelta;
        int width = output.texture.width;
        int height = output.texture.height;
        visualpos.x = (pos.x - (width / 2))*rawimg_size.x/width;
        visualpos.y = ((height / 2) - pos.y)*rawimg_size.y/height;
        return visualpos;
    }

    public void update_track()
    {
        if (Auto_Trace_Toggel)
        {
            Thread.Sleep((int)(1000 / videoPlayer.frameRate));
            Tracking();
        }
    }
}
