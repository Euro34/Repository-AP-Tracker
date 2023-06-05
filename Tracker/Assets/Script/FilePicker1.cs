using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePicker1 : MonoBehaviour
{
    private string Finalpath;
    // Start is called before the first frame update
    void Start()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp4");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
                Debug.Log("Operation cancelled");
            else
            {
                Finalpath = path;
                Debug.Log("Picked file: " + Finalpath);
            }
        }, new string[] {FileType});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
