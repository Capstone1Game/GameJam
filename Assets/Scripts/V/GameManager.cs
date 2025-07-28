using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player; // 임시 코드
    public int damage; // 임시 코드
    void Awake()
    {
        instance = this;
    }
}
