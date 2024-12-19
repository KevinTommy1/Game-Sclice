using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int damage = 5;
    [SerializeField] private RaycastHit2D hit;

    void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("No RigidBody2D on " + gameObject.name);
        }
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!Input.GetKeyDown("fire1")) return;
        hit = Physics2D.Raycast(transform.position, transform.right, 1f, LayerMask.GetMask("Enemy", "Geo"));
        if (!hit) return;
        Debug.Log(hit.transform.name);
        //hit.transform.GetComponent<Health>().TakeDamage(damage);
    }
}