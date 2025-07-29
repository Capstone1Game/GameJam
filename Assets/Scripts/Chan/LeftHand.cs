using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    private SpriteRenderer spriter;

    public Sprite shieldSprite;
    private Sprite originSprite;
    private PlayerMouse parentMouse;

    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        parentMouse = transform.parent.GetComponent<PlayerMouse>();
        originSprite = spriter.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentMouse.isRightClick)
        {
            spriter.sprite = shieldSprite;
        }
        else
            spriter.sprite = originSprite;

        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 7 : 4;
        spriter.flipY = isReverse;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossBullet")
        {
            Destroy(other.gameObject);
        }        
    }
}
