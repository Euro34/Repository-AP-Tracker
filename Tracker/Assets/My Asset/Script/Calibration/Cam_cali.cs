using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Cam_cali : MonoBehaviour
{
    public static Mat proj1;
    public static Mat proj2;
    private Point2f[] imgPoints;
    private Point3d[] worldPoints;
    private Mat Cal_Proj(byte img_no)
    {
        List_Check(img_no);
        if (imgPoints.Length != worldPoints.Length || imgPoints.Length < 6)
        {
            Debug.Log("There must be at least 6 point correspondences.");
            return null;
        }
        var A = new List<double>();
        for (int i = 0; i < imgPoints.Length; i++)
        {
            double x = imgPoints[i].X;
            double y = imgPoints[i].Y;
            double X = worldPoints[i].X;
            double Y = worldPoints[i].Y;
            double Z = worldPoints[i].Z;

            A.AddRange(new double[] { -X, -Y, -Z, -1, 0, 0, 0, 0, x * X, x * Y, x * Z, x });
            A.AddRange(new double[] { 0, 0, 0, 0, -X, -Y, -Z, -1, y * X, y * Y, y * Z, y });
        }

        using (Mat matA = new Mat(2 * imgPoints.Length, 12, MatType.CV_64FC1, A.ToArray()))
        using (Mat w = new Mat())
        using (Mat u = new Mat())
        using (Mat vt = new Mat())
        {
            Cv2.SVDecomp(matA, w, u, vt, SVD.Flags.ModifyA);
            Mat L = vt.Row[vt.Rows - 1];
            Mat M = L.Reshape(1, 3); // Reshaping to 3x4 matrix
            return M;
        }
    }
    public void Assign()
    {
        proj1 = Cal_Proj(0);
        proj2 = Cal_Proj(1);
        Debug.Log(proj1.Dump());
        Debug.Log(proj2.Dump());
    }
    public void List_Check(byte img_no)
    {
        int j = 0;
        imgPoints = new Point2f[0];
        worldPoints = new Point3d[0]; 
        for (int i = 0; i < 8;i++)
        {
            if (Dot_render1.dot_list[i,img_no] != new Point2f())
            {
                Array.Resize(ref imgPoints, j + 1);
                Array.Resize(ref worldPoints, j + 1);
                imgPoints[j] = Dot_render1.dot_list[i, img_no];
                worldPoints[j] = New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].List_Ref_Pos[i].ToPoint3d();
                j++;
            }
        }
    }
}
