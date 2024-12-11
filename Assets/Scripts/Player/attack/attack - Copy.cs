using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject attackBox;
    void Update()
    {
       if (Input.GetKeyUp(KeyCode.X))
        {
          Debug.Log("yappers");
          GameObject hitbox = Instantiate(attackBox);
          Thread.Sleep(1000000000);
          Destroy(hitbox);
          Thread.Sleep(200);
          Debug.Log("yippers");
        }
    }
     IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);   
    } 



}
