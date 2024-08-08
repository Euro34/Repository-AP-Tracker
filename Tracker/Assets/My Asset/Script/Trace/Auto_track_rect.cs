using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Auto_track_rect : MonoBehaviour
{
    public Pan_Zoom pan_Zoom;
    public Auto_track auto_track;
    public RectTransform Box_Panel;
    public RectTransform canvasRectTransform;
    public TextMeshProUGUI Guide_Text;
    private Vector2 curr_pos;
    private Vector2 initial_pos;
    private float diff_pos_x;
    private float diff_pos_y;
    private bool click_count = false;
    

    private void Start()
    {
        Box_Panel.gameObject.SetActive(false); //Hide the bounding box
    }
    public void Auto_trace_rect() //Draw a box //Auto track function start from here
    {
        click_count = !click_count; //Toggle the bool
        if (click_count)
        {
            Guide_Text.color = new Color32(255, 100, 100, 255); //Set the color of the guide text to red

            //Middle of the object
            Box_Panel.gameObject.SetActive(true);

            //Deal with scale and transform
            initial_pos = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get the coordinate from unity

            //Get the start position
            initial_pos[0] = (-initial_pos[0]) / pan_Zoom.Zoom2;
            initial_pos[1] = (-initial_pos[1]) / pan_Zoom.Zoom2;

            Box_Panel.anchoredPosition = initial_pos; //Center the box at the first point
            StartCoroutine(start_resize()); //Continuously resize the box
        }
        else
        {
            Start_auto_trace();
            Guide_Text.color = new Color32(127, 255, 127, 255); //Set the color of the guide text to green
        }
    }
    private IEnumerator start_resize()
    {
        while(click_count)
        {
            curr_pos = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get position from unity

            //Calculate X
            curr_pos[0] = (-curr_pos[0]) / pan_Zoom.Zoom2; 
            diff_pos_x = Mathf.Abs(curr_pos[0] - initial_pos[0]);

            //Calculate Y
            curr_pos[1] = (-curr_pos[1]) / pan_Zoom.Zoom2; 
            diff_pos_y = Mathf.Abs(curr_pos[1] - initial_pos[1]); 

            if(diff_pos_x > 5 && diff_pos_y > 5) //Minimum size
            {
                Box_Panel.sizeDelta = new Vector2(diff_pos_x*2, diff_pos_y*2); //Set the size of the box
            }
            else
            {
                Box_Panel.sizeDelta = new Vector2(10,10);
            }
            yield return null;
        }
        yield return null;
    }
    public void Start_auto_trace()
    {
        auto_track.initialPosition = auto_track.Visualpos_to_Realpos(new Vector2(initial_pos[0] - diff_pos_x, initial_pos[1] + diff_pos_y)); //Set the initial bounding box to track
        auto_track.size = new Vector2(diff_pos_x, diff_pos_y); //Set the size of the bounding box
        StartCoroutine(auto_track.Start_track()); //Start to auto track
    }
}
