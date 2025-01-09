using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class CoinFling : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float MinimumFlingForce;
    [SerializeField] private float MaximumFlingForce;
   
    void Start()
    {
        Vector2 FlingDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float FlingForce = UnityEngine.Random.Range(MinimumFlingForce, MaximumFlingForce);
        rb.AddForce(FlingDirection * FlingForce);
    }
}
