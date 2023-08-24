using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class Vid_select_Switch : MonoBehaviour
{
    public TextMeshProUGUI Output;
    private int Select_Vid = 1;
    public GameObject RawImage_Img;

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

    private void AssignPic()
    {
        RawImage rawImage = RawImage_Img.GetComponent<RawImage>();
        Texture2D texture = new Texture2D(3024,4032);
        texture.ReadPixels(new Rect(0, 0, 3024, 4032), 0, 0);
        texture.Apply();
        rawImage.texture = texture;
    }
}
