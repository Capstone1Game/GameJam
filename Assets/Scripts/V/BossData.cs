using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Scriptable Object / BossData")]
public class BossData : ScriptableObject
{
    public int id;
    public string bossName;
    public Vector3 pos; // 생성할 보스 위치
    public Quaternion rot;
    public Sprite sprite;
    public float contactDamage;
    public float[] bulletDamage;
    public float maxHealth; // 최대 체력
    public float moveSpeed; // 이동 속도
    public GameObject[] bullet;
}
