using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri_ang : MonoBehaviour
{
    public void tri_cal(int frame)
    {
        //triangulation
        double[] imgPoint1 = new double[2] { Dot_render2.dot_list[frame, 0].X, Dot_render2.dot_list[frame, 0].Y };
        double[] imgPoint2 = new double[2] { Dot_render2.dot_list[frame, 1].X, Dot_render2.dot_list[frame, 1].Y };
        Mat tri = new Mat();
        Cv2.TriangulatePoints(Cam_cali.proj1, Cam_cali.proj2, InputArray.Create(imgPoint1), InputArray.Create(imgPoint2), tri);
        tri = tri / tri.Get<double>(3, 0);
        Debug.Log(tri.Dump());
    }
    public void test()
    {
        tri_cal(0);
    }
}
