using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof())]

public class CameraFollowObject : MonoBehaviour
{
    private Vector3 offset;
    private CinemachineVirtualCamera vcam;
    private CinemachineFramingTransposer transposer;
    [SerializeField] private Transform playerTransform;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        transposer.m_TrackedObjectOffset = offset;
    }
}
