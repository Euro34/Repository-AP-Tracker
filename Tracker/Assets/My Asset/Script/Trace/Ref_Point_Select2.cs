using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ref_Point_Select2 : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public Button Button_del;
    public Dot_render2 dot_Render2;
    public Vid_select_Switch2 vid_Select2;
    void Start()
    {
        Current_value = 1;
    }
    public void up()
    {
        Current_value++;
        if (Current_value > (Frame_ext.byteslist[vid_Select2.Select_Vid - 1].Count))
        {
            Current_value--;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    public void down()
    {
        Current_value--;
        if (Current_value <= 0)
        {
            Current_value++;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    private void value_change()
    {
        dot_Render2.color_change();
        vid_Select2.AssignPic(Current_value - 1);
    }
}