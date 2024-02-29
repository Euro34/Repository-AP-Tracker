using OpenCvSharp;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tri_ang : MonoBehaviour
{
    public static List<double[]> tri_List = new List<double[]>();
    private string path;
    private void tri_cal(int frame)
    {
        //triangulation
        double[] imgPoint1 = new double[2] { Dot_render2.dot_list[frame, 0].X, Dot_render2.dot_list[frame, 0].Y };
        double[] imgPoint2 = new double[2] { Dot_render2.dot_list[frame, 1].X, Dot_render2.dot_list[frame, 1].Y };
        Mat tri = new Mat();
        Cv2.TriangulatePoints(Cam_cali.proj1, Cam_cali.proj2, InputArray.Create(imgPoint1), InputArray.Create(imgPoint2), tri);
        tri = tri / tri.Get<double>(3, 0);
        tri_List.Add(new double[] { ((double)frame/(double)Ref_Point_Select2.fps), tri.Get<double>(0, 0), tri.Get<double>(1, 0), tri.Get<double>(2, 0) });
    }
    public void Assign()
    {
        tri_List = new List<double[]>();
        if(Cam_cali.proj1 != null && Cam_cali.proj2 != null)
        {
            Point2f null_point = new Point2f();
            for (int i = 0; i < (int)(Frame_ext.duration*Ref_Point_Select2.fps); i++)
            {
                if (Dot_render2.dot_list[i, 0] != null_point && Dot_render2.dot_list[i, 1] != null_point)
                {
                    tri_cal(i);
                }
            }
        }
    }
    public void Write_csv()
    {
        path = Application.persistentDataPath + "/Untitle.csv";
        TextWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Time(s),Pos_X,Pos_Y,Pos_Z");
        writer.Close();
        writer = new StreamWriter(path, true);
        foreach(double[] Tri in Tri_ang.tri_List)
        {
            writer.WriteLine(Tri[0].ToString() + "," + Tri[1].ToString() + "," + Tri[2].ToString() + "," + Tri[3].ToString());
        }
        writer.Write("\n\n\n");
        writer.WriteLine("Reference_Object," + New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].Name);
        writer.WriteLine("Width," + New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].Width.ToString());
        writer.WriteLine("Height," + New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].Height.ToString());
        writer.Write("Length," + New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].Length.ToString());
        writer.Close();
        NativeFilePicker.ExportFile(path);
    }
}