using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int weaponDamage = 10;
    private PlayerMouse parentMouse;
    public int damage;
    private SpriteRenderer spriter;

    private Vector2 weaponPos = new Vector2(0.2f, 0f);
    private Vector2 weaponReversePos = new Vector2(-0.2f, 0f);
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        parentMouse = transform.parent.parent.GetComponent<PlayerMouse>();
    }
    void Update()
    {
        spriter.sortingOrder = PlayerManager.Instance.isLeft ? 3 : 6;

        if (PlayerManager.Instance.isLeft && parentMouse.isRightClick)
        {
            spriter.flipY = true;
            transform.localPosition = weaponReversePos;
        }
        else
        {
            spriter.flipY = false;
            transform.localPosition = weaponPos;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            damage = CalculateDamage();
            other.GetComponent<Boss>().OnDamage(damage);
        }
        else if (other.tag == "BossBullet")
        {
            Rigidbody2D bulletRigid = other.GetComponent<Rigidbody2D>();
            if (bulletRigid == null) return;
            Vector2 reverseForce = -bulletRigid.velocity;
            bulletRigid.AddForce(reverseForce, ForceMode2D.Impulse);
        }
    }
    //무기의 대미지를 받고 움직이는 속도에 따라 0~1의
    int CalculateDamage()
    {
        float velocity = parentMouse.velocity;
        int temp = (int)((float)weaponDamage * velocity);
        if (temp < 1) temp = 1;
        return temp;
    }
}
