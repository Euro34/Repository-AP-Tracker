using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    bool up = false;
    bool down = false;
    public float move_speed = 500f;
    public Rigidbody Body;
    public Camera camera;
    public PlayerInput playerinput;
    void Update()
    {
        Quaternion quaternion = Quaternion.Euler(0,camera.transform.rotation.eulerAngles.y, 0);
        Vector2 input = playerinput.actions["Move"].ReadValue<Vector2>();
        Vector3 pos_holder = new Vector3(input.x, 0, input.y);
        pos_holder = quaternion * pos_holder;
        if (up)
        {
            pos_holder = pos_holder + new Vector3(0, move_speed * 0.7f, 0);
        }
        if (down)
        {
            pos_holder = pos_holder + new Vector3(0, -move_speed * 0.7f, 0);
        }
        Body.AddForce(pos_holder.normalized * 10f,ForceMode.Force);
        camera.transform.position = Body.position;
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