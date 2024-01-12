using UnityEngine;
using OpenCvSharp;
using System.Collections.Generic;

public class Homography : MonoBehaviour
{
    /*
    List<short> Cal_Face;
    Point2f[][] srcPoint_List = new Point2f[6][];
    List<Point2f[]> dstPoint_List = new List<Point2f[]>();
    Mat[] Homolist;
    int a, b, c, d;

    public void Homo_Assign(){
        Homolist = new Mat[6];
        Cal_Face = new List<short>() { 1, 2, 3, 4, 5, 6 };
        for (byte i = 0; i < 8; i++)
        {
            try
            {
                string dump = Pan_Zoom.refpoint[0][(i+1).ToString()].ToString();
            }
            catch
            {
                //NaN_List.Add(Convert.ToString(i-1, 2).PadLeft(3, '0'));
                try
                {
                    Cal_Face[i >> 2] = 0;
                    Cal_Face[((i >> 1) & 1) + 2] = 0;
                    Cal_Face[(i & 1) + 4] = 0;
                }
                catch { }
            }
        }
        for (byte i = 0;i <= 5; i++)
        {
            if (Cal_Face[i] != 0)
            {
                Point2f[] srcPoint = new Point2f[4];
                a = (4 + (i >> 2) - 2 * (i >> 1)) * (i % 2) + 1;
                b = a + 1 + (i >> 2);
                c = b + 1 + (i >> 1) * 2 - ((i >> 2) * 3);
                d = c + 1 + (i >> 2);
                srcPoint[0] = new Point2f(Pan_Zoom.refpoint[0][a.ToString()].pos_x, Pan_Zoom.refpoint[0][a.ToString()].pos_y);
                srcPoint[1] = new Point2f(Pan_Zoom.refpoint[0][b.ToString()].pos_x, Pan_Zoom.refpoint[0][b.ToString()].pos_y);
                srcPoint[2] = new Point2f(Pan_Zoom.refpoint[0][c.ToString()].pos_x, Pan_Zoom.refpoint[0][c.ToString()].pos_y);
                srcPoint[3] = new Point2f(Pan_Zoom.refpoint[0][d.ToString()].pos_x, Pan_Zoom.refpoint[0][d.ToString()].pos_y);
                Debug.Log(Pan_Zoom.refpoint[0][a.ToString()].pos_x + ", " + Pan_Zoom.refpoint[0][a.ToString()].pos_y+ "," + Pan_Zoom.refpoint[0][b.ToString()].pos_x + "," + Pan_Zoom.refpoint[0][b.ToString()].pos_y + "," + Pan_Zoom.refpoint[0][c.ToString()].pos_x + "" + Pan_Zoom.refpoint[0][c.ToString()].pos_y + "," + Pan_Zoom.refpoint[0][d.ToString()].pos_x + "," + Pan_Zoom.refpoint[0][d.ToString()].pos_y);
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
                Debug.Log("Homography Matrix " + (i + 1) + ":\n" + Homolist[i].Dump());
            }
        }
    }
    private Mat Homo_Cal(byte Cal_Face_i)
    {
        try
        {
            Point2f[] dstPoints = new Point2f[4];
            Ref_dst obj = New_Ref_Obj.Ref_List[New_Ref_Obj.Selected_Ref_Index];
            int pos_1 = Cal_Face_i >> 2;
            int pos_2 = (((Cal_Face_i >> 1) & 1) | pos_1) + 1;
            dstPoints[0] = new Point2f(obj.List_Ref_Pos[a - 1].ToList()[pos_1], obj.List_Ref_Pos[a - 1].ToList()[pos_2]);
            dstPoints[1] = new Point2f(obj.List_Ref_Pos[b - 1].ToList()[pos_1], obj.List_Ref_Pos[b - 1].ToList()[pos_2]);
            dstPoints[2] = new Point2f(obj.List_Ref_Pos[c - 1].ToList()[pos_1], obj.List_Ref_Pos[c - 1].ToList()[pos_2]);
            dstPoints[3] = new Point2f(obj.List_Ref_Pos[d - 1].ToList()[pos_1], obj.List_Ref_Pos[d - 1].ToList()[pos_2]);
            return Cv2.GetPerspectiveTransform(srcPoint_List[Cal_Face_i], dstPoints);
        }
        catch
        {
            Debug.Log("No");
        }
        return null;
    }
    */
}