using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeVirtCameraSetting : MonoBehaviour
{
    [SerializeField] private float x;

    private CinemachineFramingTransposer body;

    private CinemachineVirtualCamera cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<CinemachineVirtualCamera>();
        body= cm.GetCinemachineComponent<CinemachineFramingTransposer>();
        body.m_TrackedObjectOffset = Vector3.left;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.m_TrackedObjectOffset = new Vector3(x, body.m_TrackedObjectOffset.y, body.m_TrackedObjectOffset.z);
    }
}
