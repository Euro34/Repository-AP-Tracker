using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_Scene_Relative_Point : MonoBehaviour
{
    public void to_edit_Rela_Point()
    {
        SceneManager.LoadScene("Calibration");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main");
    }
}
