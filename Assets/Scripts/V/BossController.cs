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
        GameObject instantBoss = Instantiate(prefab, data[id].pos, data[id].rot);
        if (boss = instantBoss.GetComponent<Boss>())
        {
            instantBoss.name = $"Boss {id}";
            boss.Init(data[id]);
            GameManager.instance.boss = instantBoss;
            StartBossRoutine(id);
        }
    }

    public void StartBossRoutine(int id)
    {
        switch (id)
        {
            case 0:
                StartCoroutine(FireBullet(boss.bullet[0]));
                // StartCoroutine(DrawLaser(boss.bullet[1]));
                break;
        }
    }
    public float GetHP()
    {
        float curHealth = boss.health;
        float maxHealth = boss.maxHealth;
        return curHealth / maxHealth;
    }
    IEnumerator FireBullet(GameObject bullet)
    {
        while (boss.isLive)
        {
            yield return new WaitForSeconds(2f);
            GameObject instantBullet = Instantiate(bullet, boss.transform.position, boss.transform.rotation);
            StartCoroutine(instantBullet.GetComponent<Bullet>().FireBullet(instantBullet.transform.position, boss.target.position));
        }
    }

    IEnumerator DrawLaser(GameObject bullet)
    {
        while (boss.isLive)
        {
            yield return new WaitForSeconds(1f);
            GameObject instantBullet = Instantiate(bullet, boss.transform.position, boss.transform.rotation);
            StartCoroutine(instantBullet.GetComponent<Bullet>().DrawLaser(instantBullet.transform.position, boss.target.position));
        }
    }
}
