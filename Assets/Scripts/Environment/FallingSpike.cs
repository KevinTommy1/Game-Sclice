using System;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private bool playerTrigger = false;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isNotHit;

    private float speed;
    private bool hasStartedFalling;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not tagged");
        }
    }

    private void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 - .1f);
        if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 5f) &&
            !playerTrigger)
        {
            if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 2f,
                    layerMaskPlayer))
            {
                playerTrigger = true;
                hasStartedFalling = true;
            }
        }

        if (hasStartedFalling)
        {
            isFalling = true;
            transform.position += new Vector3(0, -20, 0) * (speed * Time.deltaTime);
            speed += 0.004f;
            if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerMaskGround))
            {
                isFalling = false;
                hasStartedFalling = false;
            }
        }

        if (isFalling && Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMaskPlayer))
        {
            if (!isNotHit)
            {
                player.GetComponent<Health>().TakeDamage(1, Vector2.right, 5f, 1f);
                isNotHit = true;
            }
        }

        Debug.DrawRay(transform.position, Vector2.down * 10f, Color.red);
    }

    private void DrawDebugRayPlayer()
    {
        //Debug.DrawRay();
    }
}