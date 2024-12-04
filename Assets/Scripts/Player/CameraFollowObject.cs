using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]

public class CameraFollowObject : MonoBehaviour
{
    private Vector3 offset;
    private CinemachineVirtualCamera vcam;
    private CinemachineFramingTransposer transposer;
    private PlayerMovement playerMovement;
    private bool lastFacingDirection = false;
    [SerializeField] private GameObject player;
    void Start()
    {
        if (!TryGetComponent(out vcam))
        {
            Debug.LogError("No VirtualCamera on " + gameObject.name);
        }
        else
        {
        }
            transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (!player.TryGetComponent(out playerMovement))
        {
            Debug.LogError($"No PlayerMovement on {player.name}, or given gameobject isn't the player");
        }
        else{lastFacingDirection = playerMovement.isFacingRight;}
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transposer.m_TrackedObjectOffset = offset;
        
        // check if facing direction has changed, if so, update cameraOffset
        if (lastFacingDirection!= playerMovement.isFacingRight)
        {
            lastFacingDirection = playerMovement.isFacingRight;
            ChangeCameraOffset();
        }
    }

    private void ChangeCameraOffset()
    {
        throw new System.NotImplementedException();
    }
}
