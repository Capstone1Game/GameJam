using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    /*----플레이어 메니저에 추가-----*/
    //private bool isLeft = false;
    /*----플레이어 메니저에 추가-----*/
    public float velocity;
    private Camera mainCam;
    private Vector3 mousePos;
    private Transform parentTransform;
    private float maxoffset = 1f;
    private SpriteRenderer spriter;
    Rigidbody2D rigid;
    Vector3 targetPos;
    float targetRotZ;
    Vector3 prevPos;
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
    }
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        parentTransform = transform.parent;
    }
    void Update()
    {
        GetMousePosition();
        CalPosition();
        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 3 : 7;
        spriter.flipX = isReverse;
    }
    //물리연산
    void FixedUpdate()
    {
        CalVelocity();
        Moving();
    }

    void GetMousePosition()
    {
        //마우스 위치
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
    }
    void CalPosition()
    {
        //방향 구하기
        Vector3 dir = (mousePos - parentTransform.position);
        float distance = dir.magnitude;
        targetRotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (distance > maxoffset)//최대 거리 이상으로 넘어가면 maxoffset값으로 고정
        {
            targetPos = parentTransform.position + dir.normalized * maxoffset;
            targetPos.y -= 0.3f;
        }
        else
        {
            targetPos = parentTransform.position + dir;
            targetPos.y -= 0.3f;
        }
    }

    void CalVelocity()
    {
        float delta = (targetPos - prevPos).sqrMagnitude;
        if (delta < 0.01f) delta = 0f;
        velocity = Mathf.Clamp(delta * 3f, 0f, 1f);

        prevPos = targetPos;
    }

    void Moving()
    {
        rigid.MovePosition(targetPos);
        rigid.MoveRotation(targetRotZ);
    }
}
