using System;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private LayerMask layerMaskGround;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private bool playerTrigger = false;
    [SerializeField] private bool isFalling;
    private float speed;

    private void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 - .1f);
        if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 10) && !playerTrigger) 
        {
            if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 10, layerMaskPlayer))
            {
                playerTrigger = true;
            }
        }

        if (playerTrigger)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 10f, layerMaskPlayer))
            {
                isFalling = true;
                transform.position += new Vector3(0, -20, 0) * (speed * Time.deltaTime);
                speed += 0.004f;
            }
            else 
            {
                isFalling = false;
            }
        }
        Debug.DrawRay(transform.position, Vector2.down * 10f, Color.red);
        HitPlayer();
    }

    private void HitPlayer()
    {
        if (!isFalling) return;
        if (!Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMaskPlayer)) return;
        var player = GameObject.FindWithTag("Player");
        player.GetComponent<Health>().TakeDamage(1);
    }
    
}