using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player; // 임시 코드
    public BossController bossController;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bossController.CreateBoss(0); // 임시 코드
    }
}
