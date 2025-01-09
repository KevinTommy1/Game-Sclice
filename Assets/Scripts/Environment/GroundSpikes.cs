using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikes : MonoBehaviour
{
    private GameObject player;
    private Vector3 previousPosition;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            transform.position = previousPosition;
            player.GetComponent<Health>().GroundSpikeDamage(1);
        }
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            previousPosition = transform.position;
        }
    }
}