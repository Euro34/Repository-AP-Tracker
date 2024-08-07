using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenCvSharp;
using System.IO;

public class New_Ref_Obj : MonoBehaviour
{
    public Button originalButton;
    public Transform buttonParent;
    public RectTransform panel_add;
    public RectTransform panel_edit;
    public static List<Ref_dst> Ref_List = new List<Ref_dst>();
    private float space = 262.5f; //Distant between each button
    private string path;
    private string fileContent;
    public TMP_InputField textField_name;
    public TMP_InputField textField_width;
    public TMP_InputField textField_height;
    public TMP_InputField textField_length;
    public TMP_InputField edit_textField_name;
    public TMP_InputField edit_textField_width;
    public TMP_InputField edit_textField_height;
    public TMP_InputField edit_textField_length;
    public static int Sel_Ref_i = -1;
    public int latest_button_index;

    void Start()
    {
        Load();
        if(originalButton != null) //Happen in Reference_Object scene but not in Main scene
        {
            originalButton.gameObject.SetActive(false); //Hide the original button
            panel_add.gameObject.SetActive(false); //Hide the add window
            panel_edit.gameObject.SetActive(false); //Hide the edit window
            Re_renderButton();
        }
    }
    public void write() //Save the reference object data to json file
    {
        Ref_dst_Array ref_Dst_Array = new Ref_dst_Array { Ref_dst_list = Ref_List}; //Change fromm list of ref to array of ref
        string json = JsonUtility.ToJson(ref_Dst_Array);
        File.WriteAllText(path, json); //Save them to a json file
    }
    public void Load() //Read the json file. If it empty, create a rubik object.
    {
        path = Application.persistentDataPath + "/Obj.json"; //Locate the json file
        Ref_List = new List<Ref_dst>(); //Clear Ref_List
        if (File.Exists(path)) //If the path exist
        {
            //Read file and save it to Ref_List
            StreamReader reader = new StreamReader(path);
            fileContent = reader.ReadToEnd();
            reader.Close();
            Ref_dst_Array ref_Dst_Array = JsonUtility.FromJson<Ref_dst_Array>(fileContent);
            Ref_List = ref_Dst_Array.Ref_dst_list;
        }
        else
        {
            //Set Rubik
            Ref_dst obj = new Ref_dst();
            obj.SetPos(5.5, 5.5, 5.5, "Rubik");
            Ref_List.Add(obj);
        }
    }

    public void CreateRef() //Show the add window
    {
        //Clear text field
        textField_name.text = "";
        textField_width.text = "";
        textField_height.text = "";
        textField_length.text = "";

        panel_add.gameObject.SetActive(true); //Show the panel
    }

    public void Set_Ref() //Save the reference object to the Ref_list and set it as the selected
    {
        //Read from the text field
        Tuple<float, float, float, string> windowSetValues = Window_Set(
            textField_name.text, textField_width.text, textField_length.text, textField_height.text
            ); //Change the type to the correct one
        float width = windowSetValues.Item1; 
        float height = windowSetValues.Item2;
        float length = windowSetValues.Item3;
        string name = windowSetValues.Item4;

        Ref_dst obj = new Ref_dst();
        obj.SetPos(width, height, length, name); //Convert the value to Ref_dst(class)
        Ref_List.Add(obj); //Save the reference object in a list
        int Sel_Ref_i = Ref_List.IndexOf(obj);
        RenderButton(Sel_Ref_i);
        Debug.Log("Reference object : " + Ref_List[Sel_Ref_i].Name);
        panel_add.gameObject.SetActive(false); //Hide the window
    }

    public void EditRef(Button clickedButton) //Show the edit window
    {
        latest_button_index = int.Parse(clickedButton.name); //Save which button is clicked
        panel_edit.gameObject.SetActive(true); //Show the edit window
        edit_textField_name.text = Ref_List[latest_button_index].Name; //Show the old name
        edit_textField_width.text = Ref_List[latest_button_index].Width.ToString(); //Show the old width
        edit_textField_height.text = Ref_List[latest_button_index].Height.ToString(); //Show the old height
        edit_textField_length.text = Ref_List[latest_button_index].Length.ToString(); //Show the old length
    }

    public void EditRef_Set() //Save the edit value and set it as the selected
    {
        //Read from the text field
        Tuple<float, float, float, string> windowSetValues = Window_Set(
            edit_textField_name.text, edit_textField_width.text, edit_textField_length.text, edit_textField_height.text
            ); //Change the type to the correct one
        float width = windowSetValues.Item1;
        float height = windowSetValues.Item2;
        float length = windowSetValues.Item3;
        string name = windowSetValues.Item4;

        Ref_dst obj = new Ref_dst();
        obj.SetPos(width, height, length, name); //Convert the value to Ref_dst(class)
        Ref_List[latest_button_index] = obj; //Save the new value
        Re_renderButton();
        Sel_Ref_i = latest_button_index; //Change the selected reference object
        Debug.Log("Reference object : " + Ref_List[Sel_Ref_i].Name);
        panel_edit.gameObject.SetActive(false); //Hide the window
    }

    public void Edit_Del() //Delete the object
    {
        Ref_List.RemoveAt(latest_button_index); //Delete the ref data

        //Change the select reference object index
        if (latest_button_index < Sel_Ref_i){Sel_Ref_i--;}
        if (latest_button_index == Sel_Ref_i){Sel_Ref_i = -1;}

        Re_renderButton();
        panel_edit.gameObject.SetActive(false); //Hide the panel
    }

    public void RenderButton(int index) //Create a button for an object and set it in the right position
    {
        Button newButton = Instantiate(originalButton, buttonParent);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        buttonRect.localPosition = buttonRect.localPosition + new Vector3((((index + 1) % 3) - 1) * space, (index + 1) / 3 * (-space), 0f); //Calculate the position
        newButton.name = index.ToString();
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = Ref_List[index].ToString();
        newButton.gameObject.SetActive(true);
    }

    public void Re_renderButton() //Call RenderButton() method for each reference object in the Ref_list
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            // Check if the button's name matches the specified name
            int hold;
            if (int.TryParse(button.name, out hold))
            {
                Destroy(button.gameObject);
            }
        }
        for (int index = 0; index < Ref_List.Count; index++)
        {
            RenderButton(index);
        }
    }

    public Tuple<float, float, float, string> Window_Set(string N, string w, string h, string l) //Set the value in the window to the according object in Ref_list
    {
        try
        {
            string name = N;
            float width = float.Parse(w);
            float height = float.Parse(h);
            float length = float.Parse(l);
            return new Tuple<float, float, float, string>(width, height, length, name);
        }
        catch //Catch error from entering nonnumerical number in width height or length field
        {
            return new Tuple<float, float, float, string>(0, 0, 0, "Error");
        }
    }

    public void Close() //Close the window //Trigger by the button behind both panel
    {
        panel_add.gameObject.SetActive(false);
        panel_edit.gameObject.SetActive(false);
    }
}
public class Name_Pos3d //Collect pos in x y z and have methods to return different type
{
    public double pos_x { get; set; }
    public double pos_y { get; set; }
    public double pos_z { get; set; }
    public void SetCoord(double x, double y, double z)
    {
        pos_x = x;
        pos_y = y;
        pos_z = z;
    }
    public override string ToString()
    {
        return $"x : {pos_x} ,y : {pos_y},z: {pos_z}";
    }
    public List<double> ToList()
    {
        return new List<double> {pos_x, pos_y, pos_z};
    }
    public Point3d ToPoint3d()
    {
        return new Point3d(pos_x, pos_y, pos_z);
    }
}
[System.Serializable]
public class Ref_dst //set each of the 8 point of a reference object
{
    [SerializeField] private double width;
    [SerializeField] private double height;
    [SerializeField] private double length;
    [SerializeField] private string name;
    public double Width { get => width; set => width = value; }
    public double Height { get => height; set => height = value; }
    public double Length { get => length; set => length = value; }
    public Name_Pos3d[] List_Ref_Pos = new Name_Pos3d[8];
    public string Name { get => name; set => name = value; }

    public void SetPos(double width, double height, double length, string name)
    {
        Width = width;
        Height = height;
        Length = length;
        Name = name;
        SetPoint();
    }

    public void SetPoint() //Translate from Width Height Length of the box to x y z coordinate of each point
    {
        for (short i = 0; i < 8; i++)
        {
            double x = (int)(i & 1) * Width;
            double y = (int)((i >> 1) & 1) * Height;
            double z = (int)((i >> 2) & 1) * Length;
            Name_Pos3d obj = new Name_Pos3d();
            obj.SetCoord(x, y, z);
            List_Ref_Pos[i] = obj;
        }
    }
    public override string ToString()
    {
        return Name + "\n X = " + Width.ToString("F2") + "\n Y = " + Height.ToString("F2") + "\n Z = " + Length.ToString("F2");
    }
    public string Check() //To log
    {
        return Name + "," + Width + "," + Height + "," + Length;
    }
    public Point3d[] ToPoint3dList()
    {
        Point3d[] OutList = new Point3d[8];
        SetPoint();
        for (int i = 0; i < 8; i++)
        {
            OutList[i] = List_Ref_Pos[i].ToPoint3d();
        }
        return OutList;
    }
    public Vector3 ToVector3()
    {
        return new Vector3((float)width,(float)height,(float)length);
    }
}
[System.Serializable]
class Ref_dst_Array //Class for list of ref_dst
{
    public List<Ref_dst> Ref_dst_list;
}