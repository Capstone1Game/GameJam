using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    GameObject target;
    float dir;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        TargetInit();
        gameObject.SetActive(true);
    }

    void TargetInit()
    {
        switch (gameObject.name)
        {
            case "BossHealth":
                target = GameManager.instance.boss;
                dir = -3f;
                break;
            case "PlayerHealth":
                target = GameManager.instance.player;
                dir = 0.8f;
                break;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            TargetInit();
        }
        Vector3 pos = target.transform.position;
        pos.y = pos.y + dir;
        rect.position = Camera.main.WorldToScreenPoint(pos);
    }
}
