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
            boss.data = data[id];
            boss.Init();
        }
    }
    void Start()
    {
        CreateBoss(0);
    }
}
