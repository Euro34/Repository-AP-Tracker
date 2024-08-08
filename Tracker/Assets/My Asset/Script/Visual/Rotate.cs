using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform cam_transform;
    bool IsDraging;
    public float sens = 8;
    Vector2 current_pos;
    Vector2 pos_holder;

    private IEnumerator Rotation()
    {
        bool first = true;
        current_pos = new Vector2();
        pos_holder = new Vector2();
        while (IsDraging)
        {
            current_pos = Input.GetTouch(0).position;
            if (!first)
            {
                float distanceX = (current_pos.x - pos_holder.x) * Time.deltaTime * sens;
                distanceX += cam_transform.rotation.eulerAngles.y;
                float distanceY = (pos_holder.y - current_pos.y) * Time.deltaTime * sens;
                distanceY += cam_transform.rotation.eulerAngles.x;
                cam_transform.rotation = Quaternion.Euler(distanceY, distanceX, 0);
            }
            pos_holder = current_pos;
            first = false;
            yield return null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            IsDraging = true;
            StartCoroutine(Rotation());
        }
        else
        {
            IsDraging = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsDraging=false;
    }
}
