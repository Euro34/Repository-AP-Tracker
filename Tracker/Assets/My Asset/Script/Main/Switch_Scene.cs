using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_Scene : MonoBehaviour //Scene need to be added in "Build Setting" > "Scenes In Build" first
{
    public void Back()
    {
        SceneManager.LoadScene(0);

    }
    public void to_edit_Rela_Point()
    {
        SceneManager.LoadScene(1);
    }
    public void to_edit_Rela_Object()
    {
        SceneManager.LoadScene(2);
    }
    public void to_Tracking_Object()
    {
        SceneManager.LoadScene(3);
    }
    public void to_Visualize()
    {
        SceneManager.LoadScene(4);
    }
}