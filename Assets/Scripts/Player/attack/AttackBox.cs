using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hittable")
        {
            Debug.Log("swoosh " + other.gameObject.name + " has bin hit");
        }
    }
}
