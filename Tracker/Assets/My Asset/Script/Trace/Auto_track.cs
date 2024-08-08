using OpenCvSharp.Tracking;
using OpenCvSharp;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class Auto_track : MonoBehaviour
{
    public GameObject loop_button;
    public RectTransform Auto_Trace_Panel;
    public RectTransform Bounding_Panel;
    public RenderTexture input;
    public RawImage output;
    public VideoPlayer videoPlayer;
    public Frame_select frame_Select;
    public Dot_render2 dot_Render2;
    public TextMeshProUGUI Guide_Text;
    private Tracker tracker;
    public Mat current_frame = null;
    public Rect2d boundingBox;
    public Vector2 initialPosition = new Vector2();
    public Vector2 size = new Vector2();
    public int current_vid = 0;
    public bool Auto_Trace_Toggel = false;
    public bool Auto_Loop = false;
    private int factor = 3;
    private bool isInitialized = false;

    private void Start()
    {
        Auto_Trace_Panel.gameObject.SetActive(false);
        Bounding_Panel.gameObject.SetActive(false);
    }

    public void Auto_trace_start()
    {
        //Toggle the auto track function
        Auto_Trace_Toggel = !Auto_Trace_Toggel;

        //Toggle the auto track panel
        if (Auto_Trace_Toggel)
        {
            Auto_Trace_Panel.gameObject.SetActive(true);
        }
        else
        {
            Auto_Trace_Panel.gameObject.SetActive(false);
            Stop_Track();
        }
    }
    public IEnumerator Start_track()
    {
        current_frame = OpenCvSharp.Unity.TextureToMat(rt_to_texture(input)); //Get the frame input
        tracker = Tracker.Create(TrackerTypes.KCF); //Set the model to use

        //Downscale the frame
        double width = (int)(size.x/factor);
        double height = (int)(size.y/factor); 

        boundingBox = new Rect2d(initialPosition.x, initialPosition.y, width, height);
        isInitialized = tracker.Init(current_frame, boundingBox); //Set isInitialized to true when it is ready
        Tracking(); //Start to track
        Bounding_Panel.gameObject.SetActive(true); //Show the bounding box
        yield return null;
    }
    public void Tracking()
    {
        current_frame = OpenCvSharp.Unity.TextureToMat(rt_to_texture(input)); //Get the frame input
        if (isInitialized && tracker.Update(current_frame, ref boundingBox)) //Trigger OpenCvSharp auto track function //It will move the boundingBox
        {
            //Calculate which position to save (The middle of the box)
            Vector2 mid_pos = new Vector2();
            mid_pos.x = (float)(boundingBox.X + boundingBox.Width / 2);
            mid_pos.y = (float)(boundingBox.Y + boundingBox.Height / 2);
            mid_pos = Realpos_to_Visualpos(mid_pos); //Translate the OpenCvSharp coordinate to our own
            Bounding_Panel.anchoredPosition = mid_pos;
            dot_Render2.Save_Pos(mid_pos.x, mid_pos.y, current_vid, frame_Select.Current_value); //Save the cord
            if (Auto_Loop)
            {
                frame_Select.up();
            }
        }
    }

    public void Stop_Track()
    {
        Bounding_Panel.gameObject.SetActive(false); //Hide the bounding box
        Guide_Text.color = new Color32(255, 100, 100, 255); //Set the guide color back to red
        isInitialized = false; //Reset the initialization
        if (Auto_Loop) //Stop auto loop
        {
            Track_Loop();
        }
    }
    public void Track_Loop()
    {
        Auto_Loop = !Auto_Loop;
        if (Auto_Loop)
        {
            loop_button.GetComponent<Image>().color = new Color32(65, 65, 65, 255); //Change the button's color
            frame_Select.up(); //Kick start the loop
        }
        else
        {
            loop_button.GetComponent<Image>().color = new Color32(77, 77, 77, 255); //Change the button's color
        }
    }

    Texture2D rt_to_texture(RenderTexture rt) //Resize and convert texture from RenderTexture to Texture2D
    {
        RenderTexture tempRT = RenderTexture.GetTemporary(rt.width / factor, rt.height / factor, 0, rt.format);

        // Copy and resize the original RenderTexture to the temporary one
        Graphics.Blit(rt, tempRT);

        Texture2D texture = new Texture2D(tempRT.width, tempRT.height, TextureFormat.RGB24, false);

        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = tempRT;

        texture.ReadPixels(new UnityEngine.Rect(0, 0, tempRT.width, tempRT.height), 0, 0);
        texture.Apply();

        RenderTexture.active = currentActiveRT;

        return texture;
    }
    //OpenCvSharp use top-left as (0,0) and scaledown by <factor>
    //We use center as (0,0)
    public Vector2 Visualpos_to_Realpos(Vector2 pos) //Convert from our cord system to OpenCvSharp system
    {
        Vector2 realpos = new Vector2();
        Vector2 rawimg_size = output.rectTransform.sizeDelta;
        int width = output.texture.width;
        int height = output.texture.height;
        realpos.x = (pos.x*width/rawimg_size.x) + (width/2);
        realpos.y = (height/2) - (pos.y*height/rawimg_size.y);
        return realpos / factor;
    }
    public Vector2 Realpos_to_Visualpos(Vector2 pos) //Convert from OpenCvSharp cord system to our
    {
        Vector2 visualpos = new Vector2();
        Vector2 rawimg_size = output.rectTransform.sizeDelta;
        pos *= factor;
        int width = output.texture.width;
        int height = output.texture.height;
        visualpos.x = (pos.x - (width / 2))*rawimg_size.x/width;
        visualpos.y = ((height / 2) - pos.y)*rawimg_size.y/height;
        return visualpos;
    }
    
}
