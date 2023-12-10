using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Ref_Point_Select : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public Button Button_del;
    private Image Del_Img;
    //public button color
    void Start(){
        Current_value = 1;
        Del_Img = Button_del.GetComponent<Image>();
        Color();
    }
    public void up()
    {
        Current_value += 1;
        if(Current_value >= 9)
        {
            Current_value = 1;
        }
        Text.text = Current_value.ToString();
        Color();
    }
    public void down()
    {
        Current_value -=1;
        if(Current_value <= 0)
        {
            Current_value = 8;
        }
        Text.text = Current_value.ToString();
        Color();
    }
    void Color()
    {
        int Color_val = Current_value - 1;
        byte R = (byte)((Color_val & 1) * 100 + 150);
        byte G = (byte)(((Color_val >> 1) & 1) * 100 + 150);
        byte B = (byte)(((Color_val >> 2) & 1) * 100 + 150);
        Del_Img.color = new Color32(R,G,B,150);
    }
}
