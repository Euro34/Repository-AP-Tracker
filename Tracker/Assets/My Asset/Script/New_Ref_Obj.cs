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
    public static List<Ref_dst> Ref_List = new List<Ref_dst>();
    public float space = 262.5f;

    void Start()
    {
        originalButton.gameObject.SetActive(false);

        //Set Rubik
        Ref_dst obj = new Ref_dst();
        obj.SetPos(5.5f, 5.5f, 5.5f,"Rubik");
        Ref_List.Add(obj);
        RenderButton(Ref_List.IndexOf(obj));
    }

    public void CreateRef()
    {
        float width = 1, height = 2, lenght = 3;
        string name = "Nw_Button";

        Ref_dst obj = new Ref_dst();
        obj.SetPos(width, height, lenght, name);
        Ref_List.Add(obj);
        RenderButton(Ref_List.IndexOf(obj));
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

    public void Re_RenderButton()
    {
        // int ref_count = Ref_List.Count;
        // for (int i = 0; i < ref_count; i++)
        // {
        //     Button newButton = Instantiate(originalButton, buttonParent);
            
        //     //newButton.transform.position = new Vector3(0f, 0f, 0f);
        //     newButton.name = i.ToString();
        //     newButton.gameObject.SetActive(true);
        // }
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
    float Lenght { get; set; }
    public Name_Pos3d[] List_Ref_Pos = new Name_Pos3d[8];
    public string Name { get; set; }

    public void SetPos(float width, float hight, float lenght, string name)
    {
        Width = width;
        Height = hight;
        Lenght = lenght;
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
            float z = (int)(binary_i[0] - '0') * Lenght;
            Name_Pos3d obj = new Name_Pos3d();
            obj.SetPos(x, y, z);
            List_Ref_Pos[i] = obj;
        }
    }
    public override string ToString()
    {
        //string Out = "";
        return Name + "\n" + Width + "*" + Height + "*" + Lenght;
    }
}