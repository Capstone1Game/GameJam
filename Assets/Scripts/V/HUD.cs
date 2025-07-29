using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { BossHealth }
    public InfoType type;
    Slider BossHealth;

    void Awake()
    {
        BossHealth = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        // if (!GameManager.instance.isLive) return;
        switch (type)
        {
            case InfoType.BossHealth:
                float value = GameManager.instance.bossController.GetHP();
                BossHealth.value = value;
                if (value <= 0) BossHealth.gameObject.SetActive(false);
                break;
        }
    }
}

