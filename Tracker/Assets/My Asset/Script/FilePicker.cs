using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class FilePicker : MonoBehaviour
{
    private string Finalpath;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public TextMeshProUGUI Vid1_holder;
    public TextMeshProUGUI Vid2_holder;
    private string holder;
    public string Vid1_path;
    public string Vid2_path;
    // Start is called before the first frame update
    public void Import()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("video/*");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                Finalpath = path;
                int index1 = Finalpath.LastIndexOf('/');
                string Temp = Finalpath.Substring(index1 + 1).ToString();
                holder = " ";
            }
        }, new string[] { FileType });
    }

    public void Import1()
    {
        Import();
        Vid1_path = Finalpath;
        videoPlayer1.url = Vid1_path;
        Vid1_holder.text = holder;
    }
    public void Import2()
    {
        Import();
        Vid2_path = Finalpath;
        videoPlayer2.url = Vid2_path;
        Vid2_holder.text = holder;
    }
    void Start()
    {
        Vid1_holder.text = "+";
        Vid2_holder.text = "+";
    }

    // Update is called once per frame
    void Update()
    {

    }
}