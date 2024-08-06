using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Auto_track_rect : MonoBehaviour
{
    public Pan_Zoom pan_Zoom;
    public Auto_track auto_track;
    public Image rect_image;
    public RectTransform canvasRectTransform;
    public TextMeshProUGUI Guide_Text;
    private Vector2 curr_pos;
    private Vector2 initial_pos;
    private float diff_pos_x;
    private float diff_pos_y;
    private bool click_count = false;
    

    private void Start()
    {
        rect_image.gameObject.SetActive(false);
    }
    public void Auto_trace_rect() //Draw a box //Auto track function start from here
    {
        click_count = !click_count;
        if (click_count)
        {
            Guide_Text.color = new Color32(127, 255, 127, 255);
            //Middle of the object
            rect_image.gameObject.SetActive(true);
            //Deal with scale and transform
            initial_pos = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get the coordinate from unity
            initial_pos[0] = (-initial_pos[0]) / pan_Zoom.Zoom2; //Calculate X
            initial_pos[1] = (-initial_pos[1]) / pan_Zoom.Zoom2; //Calculate Y

            rect_image.rectTransform.anchoredPosition = initial_pos; //Center the box at the first point
            StartCoroutine(start_resize()); //Continuously resize the box
        }
        else
        {
            //Edge of the object
            Start_auto_trace();
            rect_image.gameObject.SetActive(false);
            Guide_Text.color = new Color32(255, 100, 100, 255);
        }
    }
    private IEnumerator start_resize()
    {
        while(click_count)
        {
            curr_pos = Quaternion.Euler(0f, 0f, 0f) * canvasRectTransform.anchoredPosition; //Get position from unity
            curr_pos[0] = (-curr_pos[0]) / pan_Zoom.Zoom2; //Calculate X
            curr_pos[1] = (-curr_pos[1]) / pan_Zoom.Zoom2; //Calculate Y
            diff_pos_x = Mathf.Abs(curr_pos[0] - initial_pos[0]);
            diff_pos_y = Mathf.Abs(curr_pos[1] - initial_pos[1]);
            if(diff_pos_x > 5 && diff_pos_y > 5)
            {
                rect_image.rectTransform.sizeDelta = new Vector2(diff_pos_x*2, diff_pos_y*2);
            }
            else
            {
                rect_image.rectTransform.sizeDelta = new Vector2(10,10);
            }
            yield return null;
        }
        yield return null;
    }
    public void Start_auto_trace()
    {
        auto_track.initialPosition = auto_track.Visualpos_to_Realpos(new Vector2(initial_pos[0] - diff_pos_x, initial_pos[1] + diff_pos_y));
        auto_track.size = new Vector2(diff_pos_x, diff_pos_y);
        StartCoroutine(auto_track.Start_track());
    }
}
