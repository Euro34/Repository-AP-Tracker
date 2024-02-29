using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Accord.Math;

public class Video_Slider : MonoBehaviour
{
    public Button toggle_button;
    public RectTransform control;
    public RectTransform rotate;
    public Slider slider;
    private bool istoggle;
    public double current_time;
    public List<int> frames = new List<int>();
    
    void Start()
    {
        istoggle = false;
        for(int i = 0; i < Tri_ang.tri_List.Count; i++)
        {
            frames.Add(Mathf.RoundToInt((float)((Tri_ang.tri_List[i][0] - Tri_ang.tri_List[0][0]) / Import_csv.spf)));
        }
        slider.maxValue = frames[frames.Count - 1];
    }
    public void Toggle()
    {
        if (istoggle)
        {
            istoggle = false;
            control.gameObject.SetActive(false);
            rotate.gameObject.SetActive(false);
            slider.gameObject.SetActive(true);
            toggle_button.image.color = new Color32(80, 80, 80, 255);
        }
        else
        {
            istoggle = true;
            control.gameObject.SetActive(true);
            rotate.gameObject.SetActive(true);
            slider.gameObject.SetActive(false);
            toggle_button.image.color = new Color32(80, 80, 80, 100);
        }
    }
}
