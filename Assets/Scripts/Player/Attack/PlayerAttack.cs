using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float timeBtwAttacks = 0.15f;

    private List<RockHit> rockHits = new List<RockHit>();

    public bool ShouldBeDamaging { get; private set; } = false;

    private RaycastHit2D[] hits;
    private Animator anim;
    private float attackTimeCounter;
    private Coroutine attackCoroutine;

    private CinemachineImpulseSource playerImpulseSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackTimeCounter = timeBtwAttacks;
        playerImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (UserInput.instance.controls.Attack.Attack.WasPressedThisFrame() && attackTimeCounter >= timeBtwAttacks)
        {
            attackTimeCounter = 0f;

            //trigger the attack animation
            anim.SetTrigger("Attack");
            attackCoroutine = StartCoroutine(DamageWhileSlashIsActive());
        }
           
        attackTimeCounter += Time.deltaTime;
    }

    private IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;
        rockHits.Clear();
        while (ShouldBeDamaging)
        {
            attackableLayer = LayerMask.GetMask("Attackable");
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);
            
            foreach (var t in hits)
            {
                RockHit rock = t.collider.gameObject.GetComponent<RockHit>();

                //we found a damageable collider
                if (rock != null && !rockHits.Contains(rock))
                {
                    //apply damage
                    rock.Damage(damageAmount);
                    rockHits.Add(rock);
                }
            }
            yield return null;
        }
        playerImpulseSource.GenerateImpulseWithForce(0.25f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    #region Animation Events

    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }

    #endregion
}
