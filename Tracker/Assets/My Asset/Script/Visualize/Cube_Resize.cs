using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cube_Resize : MonoBehaviour
{
    public GameObject Cube;
    public Vector3 cube_size;
    private List<double[]> pos_list;
    public float scale = 0;
    private float scale_ref = 1.0f;
    private float scale_trace = 1.0f;
    void Start()
    {
        pos_list = Tri_ang.tri_List;
        cube_size = New_Ref_Obj.Ref_List[New_Ref_Obj.Sel_Ref_i].ToVector3();
        Scale_ref_cal();
        scale_trace = Scale_trace_cal() / 230;
        scale = Mathf.Max(scale_ref, scale_trace);
        Set_Cube();
    }
    void Scale_ref_cal()
    {
        scale_ref = (cube_size.x + cube_size.y + cube_size.z) / (3 * 5);
    }

    float Scale_trace_cal()
    {
        double max = 0;
        double min = 0;
        foreach (double[] pos in pos_list)
        {
            if (pos.Skip(1).Max() > max)
            {
                max = pos.Skip(1).Max();
            }
            if (pos.Skip(1).Min() < min)
            {
                min = pos.Skip(1).Min();
            }
        }
        return Mathf.Max((float)max, -(float)min);
    }

    void Set_Cube()
    {
        cube_size = cube_size / scale;
        Cube.transform.localScale = cube_size;
        Cube.transform.position = cube_size / 2;
    }
}
