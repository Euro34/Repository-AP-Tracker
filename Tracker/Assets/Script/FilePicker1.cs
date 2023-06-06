using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class FilePicker1 : MonoBehaviour
{
    public string Finalpath;
    public TextMeshProUGUI FinalpathUI;
    public string Output;
    // Start is called before the first frame update
    public void Import(){
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp4");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null){
                Output = "Cancelled";
            }
            else
            {
                Finalpath = path;
                Output = "Picked file: " + Finalpath;
            }
        }, new string[] {FileType});
        FinalpathUI.text = Finalpath.ToString();
    }
    void Start(){
        FinalpathUI.text = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
