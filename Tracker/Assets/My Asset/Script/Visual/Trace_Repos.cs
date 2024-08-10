using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Trace_Repos : MonoBehaviour
{
    public TextMeshProUGUI text_time;
    public Slider slider;
    public GameObject Object;
    public Transform group;
    public Cube_Resize cube_Resize;
    public Image[] images;
    public Video_Slider video_Slider;
    private GameObject copy;
    private int currentframe;
    private Color hide_color = new Color32(175,175,175,255);
    
    // Update is called once per frame
    void Start()
    {
        images[1].enabled = false;
        StartCoroutine(Wait());
        text_time.text = Tri_ang.tri_List[0][0].ToString("N2");
    }
    private IEnumerator Wait()
    {
        while(cube_Resize.scale == 0)
        {
            yield return null;
        }
        for (int i = 0; i < ((Tri_ang.tri_List[Tri_ang.tri_List.Count - 1][0] - Tri_ang.tri_List[0][0])/Import_csv.spf); i++)
        {
            if (video_Slider.frames.IndexOf(i) != -1)
            {
                copy = Instantiate(Object);
                copy.name = "Object_frame_" + i.ToString();
                copy.transform.SetParent(group);
                copy.transform.position = listtovector3(Tri_ang.tri_List[video_Slider.frames.IndexOf(i)]) * cube_Resize.scale;
                Debug.Log(listtovector3(Tri_ang.tri_List[video_Slider.frames.IndexOf(i)]));
            }
        }
        color_change();
    }
    private Vector3 listtovector3(double[] list)
    {
        Vector3 pos = new Vector3((float)list[1], (float)list[2], (float)list[3]);
        return pos;
    }
    public void OnChange()
    {
        currentframe = video_Slider.frames.IndexOf((int)slider.value);
        text_time.text = (slider.value * Import_csv.spf + Tri_ang.tri_List[0][0]).ToString("N2");
        if (currentframe == -1)
        {
            text_time.text += " (null)";
        }
        color_change();
    }
    private void color_change()
    {
        GameObject obj;
        Renderer renderer;
        Color color;
        Color_Reset();
        for (int i = -2; i <= 2; i++)
        {
            int img_no = (int)slider.value + i;
            if (img_no >= 0)
            {
                byte alpha = (byte)(75 * (3 - Math.Abs(i)));
                if (GameObject.Find("Object_frame_" + img_no.ToString()) != null)
                {
                    obj = GameObject.Find("Object_frame_" + img_no.ToString());
                    renderer = obj.GetComponent<Renderer>();
                    color = new Color32(255, 255, 255, alpha);
                    renderer.material.color = color;
                }
            }
            if (i == 0) 
            {
                if (GameObject.Find("Object_frame_" + img_no.ToString()) != null)
                {
                    obj = GameObject.Find("Object_frame_" + img_no.ToString());
                    renderer = obj.GetComponent<Renderer>();
                    color = new Color32(130,30,30,255);
                    renderer.material.color = color;
                }
            }
        }
    }
    private void Color_Reset() 
    {
        for(int i = 0;i <= slider.maxValue; i++)
        {
            if (GameObject.Find("Object_frame_" + i.ToString()) != null)
            {
                GameObject obj = GameObject.Find("Object_frame_" + i.ToString());
                Renderer renderer = obj.GetComponent<Renderer>();
                Color color = new Color32(255, 255, 255, 0);
                renderer.material.color = color;
            }
        }
    }

    public void play_pause()
    {
        if (images[0].enabled)
        {
            images[3].color = hide_color;
            images[0].enabled = false;
            images[1].enabled = true;
            StartCoroutine(Play());
        }
        else
        {
            images[3].color = Color.white;
            images[1].enabled = false;
            images[0].enabled = true;
        }
    }
    private IEnumerator Play()
    {
        if(slider.value == slider.maxValue)
        {
            slider.value = 0;
        }
        while (images[1].enabled)
        {
            Thread.Sleep((int)(Import_csv.spf * 1000));
            slider.value += 1;
            if (slider.value == slider.maxValue)
            {
                if (images[2].color == hide_color)
                {
                    slider.value = 0;
                }
                else
                {
                    images[3].color = Color.white;
                    images[1].enabled = false;
                    images[0].enabled = true;
                    break;
                }
            }
            yield return null;
        }
    }
    public void loop() {
        if (images[2].color == hide_color)
        {
            images[2].color = Color.white;
        }
        else
        {
            images[2].color = hide_color;
        }
    }
    public void skip()
    {
        if(slider.value != slider.maxValue)
        {
            slider.value++;
        }
        else
        {
            slider.value = 0;
        }
    }
    public void rewind()
    {
        if (slider.value != 0)
        {
            slider.value--;
        }
        else
        {
            slider.value = slider.maxValue;
        }
    }
}
