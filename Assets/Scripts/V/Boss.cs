using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum State { Idle, Attack, KnockBack } // 보스의 행동 상태
    public State state;
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

    void FixedUpdate()
    {
        // if (!GameManager.instance.isLive) return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // if (!GameManager.instance.isLive) return;
        if (!isLive)
        {
            return;
        }
        spriter.flipX = target.position.x < rigid.position.x;
    }

    public void Init(BossData data)
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

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Idle:
                state = State.Idle;
                break;
            case State.Attack:
                state = State.Attack;
                break;
            case State.KnockBack:
                state = State.KnockBack;
                break;
        }
    }
    IEnumerator KnockBack()
    {
        yield return null;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        dirVec.y = 0;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Reposition(location));
    }

    IEnumerator Reposition(Vector3 dirVec)
    {
        float speed;
        if (moveSpeed == 0f) { speed = 1f; }
        else { speed = moveSpeed; }
        while (Vector3.Distance(transform.position, dirVec) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dirVec, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDamage(int damage)
    {
        if (!isLive) return;
        health -= damage;
        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        }
        StartCoroutine(KnockBack());
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
