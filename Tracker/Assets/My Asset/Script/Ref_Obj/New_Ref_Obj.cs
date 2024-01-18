using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenCvSharp;

public class New_Ref_Obj : MonoBehaviour
{
    public Button originalButton;
    public Transform buttonParent;
    public RectTransform panel_add;
    public RectTransform panel_edit;
    public static List<Ref_dst> Ref_List = new List<Ref_dst>();
    private float space = 262.5f;
    public TMP_InputField textField_name;
    public TMP_InputField textField_width;
    public TMP_InputField textField_height;
    public TMP_InputField textField_length;
    public TMP_InputField edit_textField_name;
    public TMP_InputField edit_textField_width;
    public TMP_InputField edit_textField_height;
    public TMP_InputField edit_textField_length;
    public static int Sel_Ref_i;
    public int latest_button_index;

    void Start()
    {
        originalButton.gameObject.SetActive(false);
        panel_add.gameObject.SetActive(false);
        panel_edit.gameObject.SetActive(false);

        //Set Rubik
        if (Ref_List.Count == 0)
        {
            Ref_dst obj = new Ref_dst();
            obj.SetPos(5.5f, 5.5f, 5.5f, "Rubik");
            Ref_List.Add(obj);
            RenderButton(Ref_List.IndexOf(obj));
            Sel_Ref_i = 0;
        }
        else
        {
            Re_renderButton();
        }
    }

    public void CreateRef()
    {
        textField_name.text = "";
        textField_width.text = "";
        textField_height.text = "";
        textField_length.text = "";

        panel_add.gameObject.SetActive(true);
    }

    public void Set_Ref()
    {
        Tuple<float, float, float, string> windowSetValues = Window_Set();
        float width = windowSetValues.Item1;
        float height = windowSetValues.Item2;
        float length = windowSetValues.Item3;
        string name = windowSetValues.Item4;

        Ref_dst obj = new Ref_dst();
        obj.SetPos(width, height, length, name);
        Ref_List.Add(obj);
        RenderButton(Ref_List.IndexOf(obj));

        panel_add.gameObject.SetActive(false);
    }

    public void EditRef(Button clickedButton)
    {
        latest_button_index = int.Parse(clickedButton.name);
        panel_edit.gameObject.SetActive(true);
        edit_textField_name.text = Ref_List[latest_button_index].Name;
        edit_textField_width.text = Ref_List[latest_button_index].Width.ToString();
        edit_textField_height.text = Ref_List[latest_button_index].Height.ToString();
        edit_textField_length.text = Ref_List[latest_button_index].Length.ToString();
    }

    public void EditRef_Set()
    {
        float width = float.Parse(edit_textField_width.text);
        float height = float.Parse(edit_textField_height.text);
        float length = float.Parse(edit_textField_length.text);
        string name = edit_textField_name.text;

        Ref_dst obj = new Ref_dst();
        obj.SetPos(width, height, length, name);
        Ref_List[latest_button_index] = obj;
        Re_renderButton();
        Sel_Ref_i = latest_button_index;
        panel_edit.gameObject.SetActive(false);
    }

    public void Edit_Del()
    {
        Ref_List.RemoveAt(latest_button_index);
        Re_renderButton();
        panel_edit.gameObject.SetActive(false);
    }

    public void RenderButton(int index)
    {
        Button newButton = Instantiate(originalButton, buttonParent);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        buttonRect.localPosition = buttonRect.localPosition + new Vector3((((index + 1) % 3) - 1) * (space), (float)(int)((index + 1) / 3) * (-space), 0f);
        newButton.name = index.ToString();
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = Ref_List[index].ToString();
        newButton.gameObject.SetActive(true);
    }

    public void Re_renderButton()
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
            Button newButton = Instantiate(originalButton, buttonParent);
            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            buttonRect.localPosition = buttonRect.localPosition + new Vector3((((index + 1) % 3) - 1) * (space), (float)(int)((index + 1) / 3) * (-space), 0f);
            newButton.name = index.ToString();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = Ref_List[index].ToString();
            newButton.gameObject.SetActive(true);
        }
    }

    public Tuple<float, float, float, string> Window_Set()
    {
        try
        {
            string name = textField_name.text;
            float width = float.Parse(textField_width.text);
            float height = float.Parse(textField_height.text);
            float length = float.Parse(textField_length.text);
            return new Tuple<float, float, float, string>(width, height, length, name);
        }
        catch
        {
            Debug.Log("You Little Piece of ...");
            return new Tuple<float, float, float, string>(0, 0, 0, "Error");
        }
    }

    public void Close()
    {
        panel_add.gameObject.SetActive(false);
        panel_edit.gameObject.SetActive(false);
    }
}

public class Name_Pos3d
{
    public double pos_x { get; set; }
    public double pos_y { get; set; }
    public double pos_z { get; set; }
    public void SetPos(double x, double y, double z)
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
        return new List<double> { pos_x, pos_y, pos_z };
    }
    public Point3d ToPoint3d()
    {
        return new Point3d(pos_x, pos_y, pos_z);
    }
}
public class Ref_dst
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public Name_Pos3d[] List_Ref_Pos = new Name_Pos3d[8];
    public string Name { get; set; }

    public void SetPos(double width, double height, double length, string name)
    {
        Width = width;
        Height = height;
        Length = length;
        Name = name;
        SetPoint();
    }

    private void SetPoint()
    {
        for (short i = 0; i < 8; i++)
        {
            double x = (int)(i & 1) * Width;
            double y = (int)((i >> 1) & 1) * Height;
            double z = (int)((i >> 2) & 1) * Length;
            Name_Pos3d obj = new Name_Pos3d();
            obj.SetPos(x, y, z);
            List_Ref_Pos[i] = obj;
        }
    }
    public override string ToString()
    {
        return Name + "\n" + Width + "*" + Height + "*" + Length;
    }
    public Point3d[] ToPoint3dList()
    {
        Point3d[] OutList = new Point3d[8];
        for (int i = 0; i < 8; i++)
        {
            OutList[i] = List_Ref_Pos[i].ToPoint3d();
        }
        return OutList;
    }
}