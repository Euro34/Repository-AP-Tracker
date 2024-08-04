using OpenCvSharp;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;
using Accord.Math;

public class Tri_ang : MonoBehaviour
{
    public static List<double[]> tri_List = new List<double[]>();
    private string path;
    public void Assign() //Prepare the data before calculation and 
    {
        tri_List = new List<double[]>(); //Define tri_List as an empty list of list of double
        if(Cam_cali.proj1 != null && Cam_cali.proj2 != null || true) //Only progress if both projection matrix is calculated
        {
            if (Dot_render2.dot_list[0] == null || Dot_render2.dot_list[1] == null){Debug.Log("Not enough info");return;}
            Point2f null_point = new Point2f(); //Use to compare to the value in the dot_list to know if it have been capture
            int primary_vid = Frame_ext.fps[0] <= Frame_ext.fps[1] ? 0 : 1;
            for (int i = 0; i < Frame_ext.framecount[primary_vid]; i++) //Loop thru every frame
            {
                if (Dot_render2.dot_list[primary_vid][i] != null_point)
                {
                    double time = i / Frame_ext.fps[primary_vid];
                    double[] imgPoint1 = new double[2]{ Dot_render2.dot_list[primary_vid][i].X, Dot_render2.dot_list[primary_vid][i].Y };
                    Point2f Point2 = Estimate_imgPoint(Frame_ext.fps[0] <= Frame_ext.fps[1], time);
                    if (Point2 != null_point)
                    {
                        double[] imgPoint2 = new double[2]{ Point2.X, Point2.Y };
                        double[] result = tri_cal(imgPoint1, imgPoint2);
                        tri_List.Add(new double[] {time, result[0], result[1], result[2]});
                    }
                }
            }
        }
    }
    private Point2f Estimate_imgPoint(bool pri_vid, double time) 
    {
        int sec_vid = pri_vid ? 1 : 0;
        Point2f[] list = Dot_render2.dot_list[sec_vid];
        Point2f coord = new Point2f();
        int frame_before = (int)Math.Floor(time * Frame_ext.fps[sec_vid]);
        int frame_after = (int)Math.Ceiling(time * Frame_ext.fps[sec_vid]);
        if (frame_before == frame_after) {return list[frame_before];}
        if (list[frame_before] != null && list[frame_after] != null)
        {
            double dt = 1 / Frame_ext.fps[sec_vid];
            double r1 = (time - (frame_before / Frame_ext.fps[sec_vid]))/dt;
            double r2 = 1 - r1;
            coord = (list[frame_before] * r2) + (list[frame_after] * r1);
        }
        return coord;
    }
    private double[] tri_cal(double[] imgPoint1, double[] imgPoint2)
    {
        //triangulation
        Mat tri = new Mat();
        Cv2.TriangulatePoints(Cam_cali.proj1, Cam_cali.proj2, InputArray.Create(imgPoint1), InputArray.Create(imgPoint2), tri);
        tri = tri / tri.Get<double>(3, 0);
        //Calculate and return as list of double that include x, y, z
        return new double[] {tri.Get<double>(0, 0), tri.Get<double>(1, 0), tri.Get<double>(2, 0)};
    }
    public void Write_csv() //Export to csv file
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