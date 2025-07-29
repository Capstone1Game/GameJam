using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    //왼손 오른손 전환을 위한 오브젝트
    private GameObject leftHand;
    private GameObject rightHand;
    public bool isRightClick = false;
    //왼손 선택이 아닐때 기본 위치
    Vector2 leftPos = new Vector2(0.25f, -0.37f);
    Vector2 leftReversePos = new Vector2(0.15f, -0.37f);
    //오른손 선택이 아닐때 기본 위치
    Vector2 rightPos = new Vector2(-0.1f, -0.37f);
    Vector2 rightReversePos = new Vector2(-0.2f, -0.37f);

    public float velocity;
    private Camera mainCam;
    private Vector3 mousePos;
    private Transform parentTransform;
    private float maxoffset = 1f;
    Rigidbody2D rigid;
    Vector3 targetPos;
    float targetRotZ;
    Vector3 prevPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
    }
    void Start()
    {
        leftHand = transform.Find("Hand_Left").gameObject;
        rightHand = transform.Find("Hand_Right").gameObject;
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        parentTransform = transform.parent;
        leftHand.transform.position = transform.parent.position;
        rightHand.transform.position = transform.parent.position;
    }
    void Update()
    {
        if (!PlayerManager.Instance.isLive)
        {
            transform.gameObject.SetActive(false);
            return;
        }
        //오른쪽 버튼 입력받음
            isRightClick = Input.GetMouseButton(1);

        GetMousePosition();
        CalPosition();
    }
    //물리연산
    void FixedUpdate()
    {
        if (!PlayerManager.Instance.isLive) return;
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
        velocity = Mathf.Clamp(delta * 2f, 0f, 1f);

        prevPos = targetPos;
    }

    void Moving()
    {
        bool isReverse = PlayerManager.Instance.isLeft;
        if (isRightClick)
        {
            Rigidbody2D leftRigid = leftHand.GetComponent<Rigidbody2D>();
            leftRigid.MovePosition(targetPos);
            leftRigid.MoveRotation(targetRotZ);

            rightHand.transform.localPosition = isReverse ? rightReversePos :rightPos;
            rightHand.transform.localRotation = Quaternion.identity;
        }
        else
        {
            leftHand.transform.localPosition = isReverse ? leftReversePos : leftPos;
            leftHand.transform.localRotation = Quaternion.identity;

            Rigidbody2D rightRigid = rightHand.GetComponent<Rigidbody2D>();
            rightRigid.MovePosition(targetPos);
            rightRigid.MoveRotation(targetRotZ);
        }
    }
}
