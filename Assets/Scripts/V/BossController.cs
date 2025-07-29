using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject prefab;
    public BossData[] data;
    private Boss boss;
    public void CreateBoss(int id)
    {
        GameObject instantBoss = Instantiate(prefab);
        if (boss = instantBoss.GetComponent<Boss>())
        {
            instantBoss.name = $"Boss {id}";
            boss.Init(data[id]);
            StartBossRoutine(id);
        }
    }

    public void StartBossRoutine(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log(boss.name);
                StartCoroutine(FireBullet(boss.bullet));
                break;
        }
    }

    IEnumerator FireBullet(GameObject bullet)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            GameObject instantBullet = Instantiate(bullet);
            instantBullet.transform.SetParent(boss.transform);
            StartCoroutine(instantBullet.GetComponent<Bullet>().FireBullet(instantBullet.transform.position, boss.target.position));
        }
    }
}
