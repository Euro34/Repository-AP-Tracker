using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Auto_track_rect : MonoBehaviour
{
    public Pan_Zoom pan_Zoom;
    public Auto_track auto_track;
    public Image rect_image;
    public RectTransform canvasRectTransform;
    private Vector2 curr_pos;
    private Vector2 initial_pos;
    private float diff_pos_x;
    private float diff_pos_y;
    private bool click_count = false;

    private void Start()
    {
        rect_image.gameObject.SetActive(false);
    }
    public void Auto_trace_rect()
    {
        if(click_count)
        {
            click_count = false;
            Start_auto_trace();
            rect_image.gameObject.SetActive(false);
        }
        else
        {
            click_count = true;
            rect_image.gameObject.SetActive(true);
            initial_pos = Quaternion.Euler(0f, 0f, -FilePicker.rotation1) * canvasRectTransform.anchoredPosition;
            initial_pos[0] = (-1.2078f - initial_pos[0]) / pan_Zoom.Zoom2;
            initial_pos[1] = (-initial_pos[1]) / pan_Zoom.Zoom2;
            rect_image.rectTransform.anchoredPosition = initial_pos;
            StartCoroutine(start_resize());
        }
    }
    private IEnumerator start_resize()
    {
        while(click_count)
        {
            curr_pos = Quaternion.Euler(0f, 0f, -FilePicker.rotation1) * canvasRectTransform.anchoredPosition;
            curr_pos[0] = (-1.2078f - curr_pos[0]) / pan_Zoom.Zoom2;
            curr_pos[1] = (-curr_pos[1]) / pan_Zoom.Zoom2;
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
    private void Start_auto_trace()
    {
        auto_track.initialPosition = auto_track.visualpos_to_realpos(new Vector2(initial_pos[0] - diff_pos_x, initial_pos[1] + diff_pos_y));
        auto_track.size = new Vector2(diff_pos_x, diff_pos_y);
        StartCoroutine(auto_track.start_track());
    }
}
