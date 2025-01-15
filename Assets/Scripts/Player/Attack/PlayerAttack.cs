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

    private List<IDamageable> iDamageables = new List<IDamageable>();

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
            anim.SetTrigger("attack");
        }
           
        attackTimeCounter += Time.deltaTime;
    }

    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;

        while (ShouldBeDamaging)
        {
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                //we found a damageable collider
                if (iDamageable != null && !iDamageables.Contains(iDamageable))
                {
                    //apply damage

                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }
            }

            yield return null;
        }

        ReturnAttackablesToDamageable();

        playerImpulseSource.GenerateImpulseWithForce(0.25f);

    }

    private void ReturnAttackablesToDamageable()
    {
        iDamageables.Clear();
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
