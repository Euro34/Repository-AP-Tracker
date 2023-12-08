using UnityEngine;
using OpenCvSharp;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

public class Homography : MonoBehaviour
{
    public Pan_Zoom pan_zoom;
    List<short> Cal_Face;
    Point2f[][] srcPoint_List = new Point2f[6][];
    List<Point2f[]> dstPoint_List = new List<Point2f[]>();
    Mat[] Homolist = new Mat[6];

    public void Homo_Assign(){
        Cal_Face = new List<short>() { 1, 2, 3, 4, 5, 6 };
        for (int i = 1; i <= 8; i++)
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
                    Cal_Face[(int)(NaN_str[0] - '0')] = 0;
                    Cal_Face[(int)(NaN_str[1] - '0') + 2] = 0;
                    Cal_Face[(int)(NaN_str[2] - '0') + 4] = 0;
                }
                catch { }
            }
        }
        for(int i = 0;i <= 5; i++)
        {
            if (Cal_Face[i] != 0)
            {
                string Face_bi = Convert.ToString(i , 2).PadLeft(3, '0');
                Point2f[] srcPoint = new Point2f[4];
                for (int a = 0; a <= 1; a++)
                {
                    for (int b = 0; b <= 1; b++)
                    {
                        short hold = 0;
                        if (i <= 2)
                        {
                            hold = (short)((int)(Face_bi[2] - '0') * 4 + a * 2 + b + 1);
                        }
                        else if (i<=4){
                            hold = (short)((int)(Face_bi[1] - '0') * 2 + a * 4 + b + 1);
                        }
                        else
                        {
                            hold = (short)((int)(Face_bi[0] - '0') + a * 4 + b * 2 + 1);
                        }
                        srcPoint[a * 2 + b] = new Point2f(pan_zoom.refpoint[0][hold.ToString()].pos_x, pan_zoom.refpoint[0][hold.ToString()].pos_y);
                    }
                }
                srcPoint_List[i] = srcPoint;
                Homolist[i] = Homo_Cal((short)i);
            }
        }
        srcPoint_List = new Point2f[8][];
        dstPoint_List = new List<Point2f[]>();
        for (int i = 0; i < Homolist.Length; i++)
        {
            if (Homolist[i] != null)
            {
                Debug.Log("Homography Matrix " + i + ":\n" + Homolist[i].Dump());
            }
        }
    }
    private Mat Homo_Cal(short Cal_Face_i)
    {
        Mat homography = new Mat();
        if (srcPoint_List[Cal_Face_i] != null)
        {
            Point2f[] dstPoints = new Point2f[4];
            dstPoints[1] = new Point2f(0, 0);
            dstPoints[3] = new Point2f(400, 400);
            dstPoints[2] = new Point2f(400, 400);
            dstPoints[0] = new Point2f(0, 400);
            homography = Cv2.GetPerspectiveTransform(srcPoint_List[Cal_Face_i], dstPoints);
            return homography;
        }
        else
        {
            return null;
        }
    }
}
