using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ref_Point_Select2 : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public Button Button_del;
    public Dot_render2 dot_Render2;
    void Start(){
        Current_value = 1;
    }
    public void up()
    {
        Current_value += 1;
        if(Current_value >= 9)
        {
            Current_value = 1;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    public void down()
    {
        Current_value -=1;
        if(Current_value <= 0)
        {
            Current_value = 8;
        }
        Text.text = Current_value.ToString();
        value_change();
    }
    private void value_change()
    {
        dot_Render2.color_change();
    }
}
