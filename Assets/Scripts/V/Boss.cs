using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossData data;
    public enum State { Attack } // 보스의 행동 상태
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    public int id;
    public string bossName;
    public Vector3 location; // 생성할 보스 위치
    public float damage;
    public float health; // 현재 체력
    public float maxHealth; // 최대 체력
    public float moveSpeed; // 이동 속도

    public bool isLive;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;
    Collider2D coll;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        isLive = true;
    }
    public void Init()
    {
        id = data.id;
        bossName = data.bossName;
        location = data.location;
        damage = data.damage;
        health = data.maxHealth;
        maxHealth = data.maxHealth;
        moveSpeed = data.moveSpeed;
        spriter.sprite = data.sprite;
        anim.runtimeAnimatorController = animCon[id];
        gameObject.transform.position = data.location;
    }

}
