using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.boss == null) return;
        Vector3 pos = GameManager.instance.boss.transform.position;
        pos.y = pos.y - 3f;
        rect.position = Camera.main.WorldToScreenPoint(pos);
    }
}
