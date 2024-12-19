using System;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private bool playerTrigger = false;
    [SerializeField] private bool isFalling;

    private float speed;
    private bool hasStartedFalling = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 - .1f);
        if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 10) &&
            !playerTrigger)
        {
            if (Physics2D.BoxCast(pos, new Vector2(transform.localScale.x, transform.localScale.y), 0, Vector2.down, 10,
                    layerMaskPlayer))
            {
                playerTrigger = true;
            }
        }

        if (playerTrigger && !hasStartedFalling)
        {
            hasStartedFalling = true;
        }

        if (hasStartedFalling)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 10f, layerMaskPlayer) ||
                Physics2D.Raycast(transform.position, Vector2.down, 10f, layerMaskGround))
            {
                isFalling = true;
                transform.position += new Vector3(0, -20, 0) * (speed * Time.deltaTime);
                speed += 0.004f;
            }
            else
            {
                isFalling = false;
                //can be destroyed
            }
        }

        if (isFalling && Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMaskPlayer))
        {
            player.GetComponent<Health>().TakeDamage(1, Vector2.up, 5f, 1f);
        }
        
        Debug.DrawRay(transform.position, Vector2.down * 10f, Color.red);
        
    }

}