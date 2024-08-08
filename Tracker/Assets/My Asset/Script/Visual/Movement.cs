using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    bool up = false;
    bool down = false;
    public int move_speed = 50;
    public Rigidbody Body;
    public Camera cam;
    public PlayerInput playerinput;
    void Update()
    {
        Quaternion quaternion = Quaternion.Euler(0,cam.transform.rotation.eulerAngles.y, 0);
        Vector2 input = playerinput.actions["Move"].ReadValue<Vector2>();
        Vector3 pos_holder = new Vector3(input.x, 0, input.y);
        pos_holder = quaternion * pos_holder;
        if (up)
        {
            pos_holder = pos_holder + new Vector3(0,  0.7f, 0);
        }
        if (down)
        {
            pos_holder = pos_holder + new Vector3(0, -0.7f, 0);
        }
        Body.velocity = pos_holder * move_speed;
        cam.transform.position = Body.position;
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