using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { BossHealth, PlayerHealth }
    public InfoType type;
    Slider healthSlider;

    void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        // if (!GameManager.instance.isLive) return;
        switch (type)
        {
            case InfoType.BossHealth:
                float value = GameManager.instance.bossController.GetHP();
                healthSlider.value = value;
                if (value <= 0) healthSlider.gameObject.SetActive(false);
                break;
            case InfoType.PlayerHealth:
                value = PlayerManager.Instance.GetHP();
                healthSlider.value = value;
                if (value <= 0) healthSlider.gameObject.SetActive(false);
                break;
        }
    }
}

