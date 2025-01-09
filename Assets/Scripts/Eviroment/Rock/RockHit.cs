using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RockHit : MonoBehaviour
{
    [SerializeField] internal GameObject coin;
    [SerializeField] internal float TimesHit = 0;
    void Start()
    {
        
    }
    void Update()
    {
        if (this.gameObject.tag == "Hit")
        {
            TimesHit += 1f;
            this.gameObject.tag = "hittable";
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
    internal void CoinDrop(float drops)
    {
        for (int i = 0; i == drops; i++)
        {
            Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
        }
    }
    internal void GeoBreak(float drops)
    {
        for (int i = 0; i == drops; i++)
        {
            Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
