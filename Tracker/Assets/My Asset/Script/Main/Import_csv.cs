using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Import_csv : MonoBehaviour
{
    public New_Ref_Obj new_ref_obj;
    public static double spf;
    public static List<double> frame_times = new List<double>();
    public void On_press()
    {
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((source) =>
        {
            if (source != null)
            {
                StreamReader reader = new StreamReader(source);
                int line_no = 0;
                int line_ref = 0;
                bool end = false;
                string reference = "";
                double min_dif = Double.MaxValue;
                double last_time = 0;
                //Read the csv file
                while (!reader.EndOfStream)
                {
                    string text = reader.ReadLine();
                    if (text == "" || text.Split(',')[0] == "")
                    {
                        end = true;
                        line_ref = line_no + 1;
                    }
                    if (line_no > 0 && !end)
                    {
                        double[] Values = Array.ConvertAll(text.Split(','), double.Parse);
                        Tri_ang.tri_List.Add(Values);
                        if (last_time != 0)
                        {
                            if (min_dif > (Values[0] - last_time))
                            {
                                min_dif = Values[0] - last_time;
                            }
                        }
                        last_time = Values[0];
                        frame_times.Add(Values[0]);
                    }
                    if (end && line_ref <= line_no)
                    {
                        reference += text.Split(',')[1] + ',';
                    }
                    line_no++;
                }
                spf = min_dif;
                reference = reference.Remove(reference.Length - 1);
                reader.Close();
                new_ref_obj.Load();
                int current_obj;
                bool in_list = false;
                for (current_obj = 0;current_obj < New_Ref_Obj.Ref_List.Count;current_obj++)
                {
                    if (New_Ref_Obj.Ref_List[current_obj].Check() == reference) {
                        in_list = true;
                        break;
                    }
                }
                if(!in_list)
                {
                    Ref_dst obj = new Ref_dst();
                    string[] value = reference.Split(",");
                    obj.Name = value[0];
                    obj.Width = double.Parse(value[1]);
                    obj.Height = double.Parse(value[2]);
                    obj.Length = double.Parse(value[3]);
                    New_Ref_Obj.Ref_List.Add(obj);
                    new_ref_obj.write();
                }
                New_Ref_Obj.Sel_Ref_i = current_obj;
            }
        }, new string[]{ NativeFilePicker.ConvertExtensionToFileType("csv")}
        );
    }
}
