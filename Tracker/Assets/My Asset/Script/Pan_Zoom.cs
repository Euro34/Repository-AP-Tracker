//using UnityEngine;

//public class Pan_Zoom : MonoBehaviour
//{
//    private UnityEngine.Vector3 initialScale;

//    [SerializeField]
//    private float presetScale = 1.0f;
//    [SerializeField]
//    private float maxZoom = 10f;

//    private void Awake()
//    {
//        initialScale = transform.localScale;
//    }

//    public void SetScale(float scale)
//    {
//        var desiredScale = initialScale * scale;
//        desiredScale = ClampDesiredScale(desiredScale);
//        transform.localScale = desiredScale;
//    }

//    private UnityEngine.Vector3 ClampDesiredScale(UnityEngine.Vector3 desiredScale)
//    {
//        desiredScale = UnityEngine.Vector3.Max(initialScale, desiredScale);
//        desiredScale = UnityEngine.Vector3.Min(initialScale * maxZoom, desiredScale);
//        return desiredScale;
//    }
//}