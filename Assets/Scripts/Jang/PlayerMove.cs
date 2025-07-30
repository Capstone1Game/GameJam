using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PlayerManager.Instance.isLive || PlayerManager.Instance.isDamage) return;
        //Jump
        if (Input.GetButtonDown("Jump") && (PlayerManager.Instance.currentState != PlayerManager.State.Jump))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            PlayerManager.Instance.SetState(PlayerManager.State.Jump);
        }

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = Vector2.zero;
        }

        //Direction sprite
        if (Input.GetButton("Horizontal"))
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(horizontal * maxSpeed, rigid.velocity.y);
            spriteRenderer.flipX = horizontal == -1;
            PlayerManager.Instance.isLeft = (horizontal == -1);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerManager.Instance.isLive || PlayerManager.Instance.isDamage) return;
        //Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���� �ѹ��� �����ϰ� ��
        if ((collision.gameObject.tag == "Elevator") || (collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Ladder"))
            PlayerManager.Instance.SetState(PlayerManager.State.Idle);
    }
}
