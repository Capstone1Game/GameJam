using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    Vector2 leftPos = new Vector2(0.25f, -0.37f);
    Vector2 leftReversePos = new Vector2(0.15f, -0.37f);
    private SpriteRenderer spriter;
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 7 : 4;
        transform.localPosition = isReverse ? leftReversePos : leftPos;
        spriter.flipX = isReverse;
    }
}
