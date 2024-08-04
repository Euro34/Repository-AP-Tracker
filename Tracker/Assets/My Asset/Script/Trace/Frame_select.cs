using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Frame_select : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Dot_render2 dot_Render2;
    public Vid_select_Switch2 vid_Select2;
    public int Current_value;
    void Start()
    {
        Current_value = 0; //Start at frame 1 (frame index 0 is the first frame)
    }
    public void up()
    {
        Current_value++;
        if (Current_value >= Frame_ext.framecount[Vid_select_Switch2.Select_Vid ? 1 : 0]) //To stop from going to the frame that doesn't exist
        {
            Current_value--;
        }
        value_change();
    }
    public void down()
    {
        Current_value--;
        if (Current_value < 0) //To stop from going to the frame that doesn't exist
        {
            Current_value = 0;
        }
        value_change();
    }
    public void value_change()
    {
        Text.text = (Current_value + 1).ToString(); //To display frame number
        dot_Render2.color_change(); //Set other frames opacity
        vid_Select2.AssignPic(Current_value); //Change the frame to the right one
    }
}