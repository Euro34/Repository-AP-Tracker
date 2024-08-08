using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ref_Select : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public Button Button_del;
    private Image Del_Img;
    void Start(){ //Set the start point to 1
        Current_value = 0;
        Del_Img = Button_del.GetComponent<Image>();
        Color();
    }
    public void up()
    {
        Current_value++;
        if(Current_value >= 8)
        {
            Current_value = 0;
        }
        Text.text = (Current_value + 1).ToString();
        Color();
    }
    public void down()
    {
        Current_value--;
        if(Current_value < 0)
        {
            Current_value = 7;
        }
        Text.text = (Current_value + 1).ToString();
        Color();
    }
    void Color() //Deal with color
    {
        int Color_val = Current_value;
        byte R = (byte)((Color_val & 1) * 100 + 150);
        byte G = (byte)(((Color_val >> 1) & 1) * 100 + 150);
        byte B = (byte)(((Color_val >> 2) & 1) * 100 + 150);
        Del_Img.color = new Color32(R,G,B,150);
    }
}
