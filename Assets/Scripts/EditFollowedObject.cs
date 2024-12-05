using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFollowedObject : MonoBehaviour
{
    [Range(-20f, 20f)] private float y;
    [Range(-20f, 20f)] private float x;
    [SerializeField] GameObject followedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followedObject.transform.position = new Vector3(x, y, 0);
    }
}
