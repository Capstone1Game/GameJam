using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject prefab;
    public BossData[] data;
    public void CreateBoss(int id)
    {
        GameObject instantBoss = Instantiate(prefab);
        if (instantBoss.TryGetComponent(out Boss boss))
        {
            instantBoss.name = $"Boss {id}";
            boss.Init(data[id]);
        }
    }
    void Start()
    {
        CreateBoss(0);
    }
}
