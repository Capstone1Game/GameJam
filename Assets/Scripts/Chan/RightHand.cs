using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private SpriteRenderer spriter;
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 4 : 7;
        spriter.flipX = isReverse;        
    }
}
