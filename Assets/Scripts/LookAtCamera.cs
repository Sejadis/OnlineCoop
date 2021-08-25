using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
 // Update is called once per frame
    void Update()
    {
        var cam = camera ??= Camera.main;
        if (cam != null)
        {
            transform.LookAt(cam.transform);
        }
    }
}
