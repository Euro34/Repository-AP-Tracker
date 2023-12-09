using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class New_Ref_Obj : MonoBehaviour
{
    public Button originalButton;
    public Transform buttonParent;
    public RectTransform panel_add;
    public static List<Ref_dst> Ref_List = new List<Ref_dst>();
    public static int Selected_Ref_Index;
    private float space = 262.5f;
    public TMP_InputField textField_name;
    public TMP_InputField textField_width;
    public TMP_InputField textField_height;
    public TMP_InputField textField_length;

    void Start()
    {
        originalButton.gameObject.SetActive(false);
        panel_add.gameObject.SetActive(false);

        //Set Rubik
        if (Ref_List.Count == 0)
        {
            Ref_dst obj = new Ref_dst();
            obj.SetPos(5.5f, 5.5f, 5.5f,"Rubik");
            Ref_List.Add(obj);
            RenderButton(Ref_List.IndexOf(obj));
            Selected_Ref_Index = 0;
        }
        else
        {
            Re_renderButton();
        }
    }

    public void CreateRef()
    {
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

    public void RenderButton(int index)
    {
        Button newButton = Instantiate(originalButton, buttonParent);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        buttonRect.localPosition = buttonRect.localPosition + new Vector3((((index+1)%3)-1)*(space), (float)(int)((index+1)/3)*(-space), 0f);
        newButton.name = index.ToString();
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = Ref_List[index].ToString();
        newButton.gameObject.SetActive(true);
    }

    public void Re_renderButton()
    {
        for (int index = 0; index<Ref_List.Count; index++)
        {
            Button newButton = Instantiate(originalButton, buttonParent);
            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            buttonRect.localPosition = buttonRect.localPosition + new Vector3((((index+1)%3)-1)*(space), (float)(int)((index+1)/3)*(-space), 0f);
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
            return new Tuple<float, float, float, string>(0,0,0,"Error");
        }
    }
}

public class Name_Pos3d
{
    public float pos_x { get; set; }
    public float pos_y { get; set; }
    public float pos_z { get; set; }
    public void SetPos(float x, float y, float z)
    {
        pos_x = x;
        pos_y = y;
        pos_z = z;
    }
    public override string ToString()
    {
        return $"x : {pos_x} ,y : {pos_y},z: { pos_z}";
    }
}
public class Ref_dst
{
    float Width { get; set; }
    float Height { get; set; }
    float Length { get; set; }
    public Name_Pos3d[] List_Ref_Pos = new Name_Pos3d[8];
    public string Name { get; set; }

    public void SetPos(float width, float height, float length, string name)
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
            string binary_i = Convert.ToString(i, 2).PadLeft(3, '0');
            float x = (int)(binary_i[2] - '0') * Width;
            float y = (int)(binary_i[1] - '0') * Height;
            float z = (int)(binary_i[0] - '0') * Length;
            Name_Pos3d obj = new Name_Pos3d();
            obj.SetPos(x, y, z);
            List_Ref_Pos[i] = obj;
        }
    }
    public override string ToString()
    {
        return Name + "\n" + Width + "*" + Height + "*" + Length;
    }
}