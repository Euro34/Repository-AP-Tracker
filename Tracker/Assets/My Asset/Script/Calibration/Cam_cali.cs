using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cam_cali : MonoBehaviour
{
    public static Mat proj1;
    public static Mat proj2;
    private Mat Cal_Proj(Point2d[] imagePoints, Point3d[] worldPoints)
    {
        if (imagePoints.Length != worldPoints.Length || imagePoints.Length <= 6)
            throw new ArgumentException("There must be at least 6 point correspondences.");

        var A = new List<double>();
        for (int i = 0; i < imagePoints.Length; i++)
        {
            double x = imagePoints[i].X;
            double y = imagePoints[i].Y;
            double X = worldPoints[i].X;
            double Y = worldPoints[i].Y;
            double Z = worldPoints[i].Z;

            A.AddRange(new double[] { -X, -Y, -Z, -1, 0, 0, 0, 0, x * X, x * Y, x * Z, x });
            A.AddRange(new double[] { 0, 0, 0, 0, -X, -Y, -Z, -1, y * X, y * Y, y * Z, y });
        }

        using (Mat matA = new Mat(2 * imagePoints.Length, 12, MatType.CV_64FC1, A.ToArray()))
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
        var imagePoints1 = new Point2d[]
        {
            new Point2d (-20.21478 , 17.69323),
            new Point2d ( 257.1361 , 262.8997),
            new Point2d ( -27.93365 , 315.8745),
            new Point2d ( 320.1559 , 587.304),
            new Point2d ( -311.2058 , 249.6825),
            new Point2d ( -386.7028 , 582.863),
            new Point2d ( -34.06646 , 801.212)
        };

        var imagePoints2 = new Point2d[]
        {
            new Point2d ( 13.16618 , -243.1684),
            new Point2d ( 225.5721 , -41.238),
            new Point2d ( 18.67966 , 96.73628),
            new Point2d ( 255.6205 , 276.8885),
            new Point2d ( -285.1118 , -91.27262),
            new Point2d ( -330.8735 , 238.0186),
            new Point2d ( -67.46811 , 385.2279)
        };

        var worldPoints = new Point3d[]
        {
            new Point3d(0, 0, 0),
            new Point3d(1, 0, 0),
            new Point3d(0, 1, 0),
            new Point3d(1, 1, 0),
            new Point3d(0, 0, 1),
            new Point3d(0, 1, 1),
            //new Point3d(1, 1, 1)
        };

        proj1 = Cal_Proj(imagePoints1, worldPoints);
        proj2 = Cal_Proj(imagePoints2, worldPoints);
        Debug.Log(proj1.Dump());
        Debug.Log(proj2.Dump());
    }
    void Start()
    {
        Assign();
    }
}
