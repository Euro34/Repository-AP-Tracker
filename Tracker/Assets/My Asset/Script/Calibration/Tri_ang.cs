using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri_ang : MonoBehaviour
{
    public void tri_cal(int frame)
    {
        //triangulation
        /*double[] imgPoint1 = new double[2] { imagePoints1[frame].X, imagePoints1[frame].Y };
        double[] imgPoint2 = new double[2] { imagePoints2[frame].X, imagePoints2[frame].Y };  :] 
        Mat tri = new Mat();
        Cv2.TriangulatePoints(Cam_cali.proj1, Cam_cali.proj2, InputArray.Create(imgPoint1), InputArray.Create(imgPoint2), tri);
        tri = tri / tri.Get<double>(3, 0);
        Debug.Log(tri.Dump());*/
    }
}
