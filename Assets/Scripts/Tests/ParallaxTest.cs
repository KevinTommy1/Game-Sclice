using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [Range(-20.0f,20.0f)] public float x;
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
