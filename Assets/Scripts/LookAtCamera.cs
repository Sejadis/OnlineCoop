using UnityEngine;
using UnityEngine.Serialization;

public class LookAtCamera : MonoBehaviour
{
    [FormerlySerializedAs("camera")] [SerializeField] private Camera targetCamera;
 // Update is called once per frame
    void Update()
    {
        var cam = targetCamera ??= Camera.main;
        if (cam != null)
        {
            transform.LookAt(cam.transform);
        }
    }
}
