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
    public bool isDamage = false;
    public static PlayerManager Instance;
    public GameManager gameManager;

    private SpriteRenderer spriter;
    private Rigidbody2D rigid;

    public int currentHealth;
    private int maxHealth = 100;
    Animator anim;

    public bool isRightClick = false;
    public bool isLeftClick = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        currentHealth = maxHealth;
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //오른쪽 버튼 입력받음
        isRightClick = Input.GetMouseButton(1);
        isLeftClick = Input.GetMouseButton(0);
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

    void OnCollisionStay2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Boss") && !isDamage)
        {
            OnDamage(other.transform.position, (int)GameManager.instance.bossController.GetContactDamage());
        }
    }

    public float GetHP()
    {
        float damage = (float)currentHealth / maxHealth;
        return damage;
    }
    public void OnDamage(Vector2 targetPos, int damage)
    {
        if (isDamage) return;
        isDamage = true;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            anim.SetBool("DoDeath", true);
            return;

        }
        else
        {
            StartCoroutine(KnockBack(targetPos));
        }
    }
    public IEnumerator KnockBack(Vector2 targetPos)
    {
        spriter.color = Color.red;
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 3), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        spriter.color = Color.white;
        spriter.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.2f);
        spriter.color = new Color(1, 1, 1, 1);
        isDamage = false;
    }

    void DoDie()
    {
        isLive = false;
        gameManager.GameOver();
    }
}
