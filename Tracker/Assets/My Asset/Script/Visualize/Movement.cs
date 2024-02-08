using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool front = false;
    bool back = false;
    bool left = false;
    bool right = false;
    bool up = false;
    bool down = false;
    public float move_speed = 500f;
    public Rigidbody Body;
    public Camera camera;
    // Update is called once per frame
    void Update()
    {
        Quaternion quaternion = Quaternion.Euler(0,camera.transform.rotation.eulerAngles.y, 0);
        Vector3 pos_holder = new Vector3(0,0,0);
        if (front)
        {
            pos_holder = new Vector3(0, 0, move_speed);
            pos_holder = quaternion * pos_holder;
            
        }
        if (back)
        {
            pos_holder = new Vector3(0, 0, -move_speed);
            pos_holder = quaternion * pos_holder;
        }
        if (left)
        {
            pos_holder = new Vector3(-move_speed, 0, 0);
            pos_holder = quaternion * pos_holder;
        }
        if (right)
        {
            pos_holder = new Vector3(move_speed, 0, 0);
            pos_holder = quaternion * pos_holder;
        }
        if (up)
        {
            pos_holder = new Vector3(0, move_speed * 0.7f, 0);
        }
        if (down)
        {
            pos_holder = new Vector3(0, -move_speed * 0.7f, 0);
        }
        Body.AddForce(pos_holder.normalized * 10f,ForceMode.Force);
        camera.transform.position = Body.position;
    }
    public void Front_Down()
    {
        front = true;
    }
    public void Front_Up()
    {
        front = false;
    }
    public void Back_Down()
    {
        back = true;
    }
    public void Back_Up()
    {
        back = false;
    }
    public void Left_Down()
    {
        left = true;
    }
    public void Left_Up()
    {
        left = false;
    }
    public void Right_Down()
    {
        right = true;
    }
    public void Right_Up()
    {
        right = false;
    }
    public void Down_Down()
    {
        down = true;
    }
    public void Down_Up()
    {
        down = false;
    }
    public void Up_Down()
    {
        up = true;
    }
    public void Up_Up()
    {
        up = false;
    }
}
