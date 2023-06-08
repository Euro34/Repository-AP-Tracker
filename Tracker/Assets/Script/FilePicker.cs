using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class FilePicker : MonoBehaviour
{
    private string Finalpath;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI Vid1_name;
    public TextMeshProUGUI Vid2_name;
    public string Vid1_path;
    public string Vid2_path;
    private string Output;
    // Start is called before the first frame update
    public void Import(){
        string FileType = NativeFilePicker.ConvertExtensionToFileType("video/*");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null){
                Output = "+";
            }
            else
            {
                Finalpath = path;
                int index1 = Finalpath.LastIndexOf('/');
                string Temp = Finalpath.Substring(index1+1).ToString();
                Output = Temp.Substring(0,Temp.Length-4).ToString();
            }
        }, new string[] {FileType});
    }

    public void Import1()
    {
        Import();
        Vid1_path = Finalpath;
        Vid1_name.text = Output.ToString();
        videoPlayer.url = Vid1_path;
    }
    public void Import2()
    {
        Import();
        Vid2_path = Finalpath;
        Vid2_name.text = Output.ToString();
        videoPlayer.url = Vid2_path;
    }
    void Start(){
        Vid1_name.text = "+";
        Vid2_name.text = "+";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
