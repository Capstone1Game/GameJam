using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    /*----플레이어 메니저에 추가-----*/
    //private bool isLeft = false;
    /*----플레이어 메니저에 추가-----*/

    private Camera mainCam;
    private Vector3 mousePos;
    private Transform parentTransform;
    private float maxoffset = 1f;
    Vector3 tempPos;
    Vector3 prevPos;
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
        //마우스 위치
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        //방향 구하기
        Vector3 dir = (mousePos - parentTransform.position);
        float distance = dir.magnitude;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (distance > maxoffset)//최대 거리 이상으로 넘어가면 maxoffset값으로 고정
        {
            tempPos = parentTransform.position + dir.normalized * maxoffset;
            tempPos.y -= 0.3f;
        }
        else
        {
            tempPos = parentTransform.position + dir;
            tempPos.y -= 0.3f;
        }
        float damage = (tempPos - prevPos).magnitude;
        Debug.Log(damage * 100f);
        prevPos = tempPos;
        transform.position = tempPos;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        
    }
}
