using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ref_Point_Select2 : MonoBehaviour
{
    public int Current_value;
    public TextMeshProUGUI Text;
    public Button Button_del;
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
    }
    public void down()
    {
        Current_value -=1;
        if(Current_value <= 0)
        {
            Current_value = 8;
        }
        Text.text = Current_value.ToString();
    }
}
