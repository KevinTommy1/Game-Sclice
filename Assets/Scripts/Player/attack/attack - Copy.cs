using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class attack : MonoBehaviour
{
    bool canattack = true;
    GameObject NewAttackBox;
    public GameObject attackBox;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && canattack == true)
        {
            canattack = false;
            NewAttackBox = Instantiate(attackBox,gameObject.transform);
            Attack(1.2f);            
            AttackDelay(0.1f);
        }
    }

    private void AttackDelay(float delay)
    {
        StartCoroutine(Delay(delay));     
    }
    IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canattack = true;
    }

    private void Attack(float delay)
    {
        StartCoroutine(Slash(delay));
    }

    IEnumerator Slash(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(NewAttackBox);
    }
}
