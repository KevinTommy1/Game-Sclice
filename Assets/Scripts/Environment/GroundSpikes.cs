using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikes : MonoBehaviour
{
    private GameObject player;
    private GameObject spawnPoint;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPoint = GameObject.FindWithTag("SpawnPoint");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject whatHit = col.gameObject;
        if (whatHit.CompareTag("Player"))
        {
            player.GetComponent<Health>().GroundSpikeDamage(1);
            Debug.Log("Player took damage");
            player.transform.position = spawnPoint.transform.position;
        }
    }
}