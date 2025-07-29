using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum State { Idle, Jump }
    public State currentState = State.Idle;
    public bool isLeft = false;
    public bool isLive = true;
    private bool isDamage = false;
    public static PlayerManager Instance;

    private SpriteRenderer spriter;
    private Rigidbody2D rigid;

    public int currentHealth;
    private int maxHealth = 20;
    Animator anim;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        currentHealth = maxHealth;
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Idle:
                currentState = State.Idle;
                break;
            case State.Jump:
                currentState = State.Jump;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Boss" || other.gameObject.tag == "BossBullet") && !isDamage)
        {
            StartCoroutine(OnDamage(other.transform.position, 10));
        }
    }

    IEnumerator OnDamage(Vector2 targetPos,int damage)
    {
        isDamage = true;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DoDie();
        }
        else
        {
            spriter.color = Color.red;
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1), ForceMode2D.Impulse);
            Debug.Log(dirc);
            yield return new WaitForSeconds(0.1f);
            spriter.color = Color.white;
            spriter.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.5f);
            spriter.color = new Color(1, 1, 1, 1);
            isDamage = false;
        }
    }

    void DoDie()
    {
        isLive = false;
        anim.SetTrigger("DoDeath");
    }
}
