using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vid_select_Switch : MonoBehaviour
{
    public TextMeshProUGUI Output;
    public int Select_Vid = 1;

    void Start()
    {
        Output.text = "Vid_1";
    }


    public void Switch()
    {
        if (Output.text == "Vid_1")
        {
            Output.text = "Vid_2";
            Select_Vid = 2;
        }
        else
        {
            Output.text = "Vid_1";
            Select_Vid = 1;
        }
        Debug.Log(Select_Vid);
    }
}
