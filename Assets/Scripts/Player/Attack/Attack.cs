using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject attackBox;
    private bool canAttack = true;
    private GameObject newAttackBox;
    
    void Update()   
    {
        if (Input.GetKeyUp(KeyCode.X) && canAttack == true)
        {
            canAttack = false;
            newAttackBox = Instantiate(attackBox,gameObject.transform);
            Swoosh(0.2f);            
            AttackDelay(0.1f);
        }
    }

    //delay before the hitbox is removed, abstraction to make code more readable
    private void Swoosh(float delay)
    {
        StartCoroutine(Slash(delay));
    }
    IEnumerator Slash(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(newAttackBox);
    }


    //delay before you can attack again, abstraction to make code more readable
    private void AttackDelay(float delay)
    {
        StartCoroutine(Delay(delay));     
    } 
    IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}
