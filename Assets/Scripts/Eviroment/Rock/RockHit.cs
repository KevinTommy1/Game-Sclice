using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RockHit : MonoBehaviour
{
    [SerializeField] internal GameObject coin;
    [SerializeField] internal float TimesHit = 0;

    private void Update()
    {
        if (gameObject.CompareTag("Hit"))
        {
            TimesHit += 1f;
            gameObject.tag = "Hittable";
        }
     
        switch(TimesHit)
        {
            case 1:
                CoinDrop(3);
                break;
            case 2:
                CoinDrop(3);
                break;
            case 3:
                GeoBreak(6);
                break;
        }  
    }

    internal void CoinDrop(int drops)
    {
        for (int i = 0; i == drops; i++)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
        }
    }
    
    internal void GeoBreak(int drops)
    {
        for (int i = 0; i == drops; i++)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            //anim.play("RockBreak");
        }
    }
}
