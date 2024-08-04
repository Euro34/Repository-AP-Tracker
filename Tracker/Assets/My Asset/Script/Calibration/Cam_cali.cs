using OpenCvSharp;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Cam_cali : MonoBehaviour
{
    public static Mat proj1;
    public static Mat proj2;
    private Point2f[] imgPoints;
    private Point3d[] worldPoints;
    private Point3d[] OutList = new Point3d[8];
    public Tri_ang tri_ang;
    private Mat Cal_Proj(byte img_no)
    {
        List_Check(img_no);
        if (imgPoints.Length != worldPoints.Length || imgPoints.Length < 6) //If there are less than 6 point the accuracy will drop dramatically
        {
            Debug.Log("There must be at least 6 point correspondences.");
            return null;
        }
        //Calculate projection matrix from 2d and real coordinate of every point using SVD
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
    public void Assign() //Prepare the data before triangulate all the point //This method is call when calculate button is pressed
    {
        New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].SetPoint();
        OutList = New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].ToPoint3dList(); //Call for an array of Point3D of a reference object
        proj1 = Cal_Proj(0); //Find Projection matrix of vid1
        proj2 = Cal_Proj(1); //Find Projection matrix of vid2
        tri_ang.Assign(); //Call to triangulate
    }
    public void List_Check(byte img_no) //Prepare the data before calculate the projection matrix
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
                worldPoints[j] = OutList[i];
                j++;
            }
        }
    }
}
