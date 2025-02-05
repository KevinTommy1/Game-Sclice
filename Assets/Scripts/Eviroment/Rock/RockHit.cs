using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RockHit : MonoBehaviour
{
    [SerializeField] internal GameObject coin;
    [SerializeField] internal float TimesHit = 0;
    
    public void Damage(float damageAmount)
    {
        TimesHit += damageAmount;
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

    private void CoinDrop(int drops)
    {
        for (int i = 0; i < drops; i++)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void GeoBreak(int drops)
    {
        for (int i = 0; i < drops; i++)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
