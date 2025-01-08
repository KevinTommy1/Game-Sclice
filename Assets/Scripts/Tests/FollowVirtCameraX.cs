using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVirtCameraX : MonoBehaviour
{
    [SerializeField] GameObject virtCamera;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = virtCamera.transform.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z; 
        transform.position = pos;
    }
}
