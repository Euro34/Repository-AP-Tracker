using UnityEngine;
using OpenCvSharp;
using System.Collections.Generic;
using System;

public class Homography : MonoBehaviour
{
    public Pan_Zoom pan_zoom;
    List<short> Cal_Face;
    Point2f[][] srcPoint_List = new Point2f[6][];
    List<Point2f[]> dstPoint_List = new List<Point2f[]>();
    Mat[] Homolist = new Mat[6];

    public void Homo_Assign(){
        Cal_Face = new List<short>() { 1, 2, 3, 4, 5, 6 };
        for (byte i = 1; i <= 8; i++)
        {
            try
            {
                string dump = pan_zoom.refpoint[0][i.ToString()].ToString();
            }
            catch
            {
                //NaN_List.Add(Convert.ToString(i-1, 2).PadLeft(3, '0'));
                string NaN_str = Convert.ToString(i - 1, 2).PadLeft(3, '0');
                try
                {
                    Cal_Face[(i-1) >> 2] = 0;
                    Cal_Face[(((i - 1) >> 1) - 2 * ((i - 1) >> 2)) + 2] = 0;
                    Cal_Face[((i - 1) - 2 * ((i - 1) >> 1)) + 4] = 0;
                }
                catch { }
            }
        }
        for(byte i = 0;i <= 5; i++)
        {
            if (Cal_Face[i] != 0)
            {
                Point2f[] srcPoint = new Point2f[4];
                int a = (4 + (i >> 2) - 2 * (i >> 1)) * (i % 2) + 1;
                int b = a + 1 + (i >> 2);
                int c = b + 1 + (i >> 1) * 2 - ((i >> 2) * 4) + (i >> 2);
                int d = c + 1 + (i >> 2);
                srcPoint[0] = new Point2f(pan_zoom.refpoint[0][a.ToString()].pos_x, pan_zoom.refpoint[0][a.ToString()].pos_y);
                srcPoint[1] = new Point2f(pan_zoom.refpoint[0][b.ToString()].pos_x, pan_zoom.refpoint[0][b.ToString()].pos_y);
                srcPoint[2] = new Point2f(pan_zoom.refpoint[0][c.ToString()].pos_x, pan_zoom.refpoint[0][c.ToString()].pos_y);
                srcPoint[3] = new Point2f(pan_zoom.refpoint[0][d.ToString()].pos_x, pan_zoom.refpoint[0][d.ToString()].pos_y);
                Debug.Log(srcPoint[0]);
                srcPoint_List[i] = srcPoint;
                Homolist[i] = Homo_Cal(i);
            }
        }
        srcPoint_List = new Point2f[8][];
        dstPoint_List = new List<Point2f[]>();
        for (int i = 0; i < Homolist.Length; i++)
        {
            if (Homolist[i] != null)
            {
                Debug.Log("Homography Matrix " + (i+1) + ":\n" + Homolist[i].Dump());
            }
        }
    }
    private Mat Homo_Cal(byte Cal_Face_i)
    {
        Mat homography = new Mat();
        if (srcPoint_List[Cal_Face_i] != null)
        {
            Point2f[] dstPoints = new Point2f[4];
            dstPoints[0] = new Point2f(0, 0);
            dstPoints[1] = new Point2f(400, 0);
            dstPoints[2] = new Point2f(0, 400);
            dstPoints[3] = new Point2f(400, 400);
            homography = Cv2.GetPerspectiveTransform(srcPoint_List[Cal_Face_i], dstPoints);
            return homography;
        }
        else
        {
            return null;
        }
    }
}
