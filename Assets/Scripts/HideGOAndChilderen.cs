using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HideGOAndChilderen : MonoBehaviour
{
    [SerializeField] private bool hide = false;
    private bool hidden = false;
    private GameObject[] children;
    private SpriteRenderer[] childrenSprites;
    private GameObject self;
    private SpriteRenderer selfSprite;

    private void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
        childrenSprites = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        if (hide != hidden)
        {
            hidden = hide;
            
            if (hide)
            {
                selfSprite.enabled = false;
                for (int i = 0; i < childrenSprites.Length; i++)
                {
                    childrenSprites[i].enabled = false;
                }
            }
            else
            {
                selfSprite.enabled = true;
                for (int i = 0; i < childrenSprites.Length; i++)
                {
                    childrenSprites[i].enabled = true;
                }
            }
        }
    }
}
