using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int weaponDamage = 10;
    private PlayerMouse parentMouse;
    public int damage;
    void Start()
    {
        parentMouse = transform.parent.GetComponent<PlayerMouse>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            damage = CalculateDamage();
            other.GetComponent<Boss>().OnDamage(damage);
            Debug.Log(damage);
        }
    }
    //무기의 대미지를 받고 움직이는 속도에 따라 0~1의
    int CalculateDamage()
    {
        float velocity = parentMouse.velocity;
        int temp = (int)((float)weaponDamage * velocity);
        if (temp < 1) temp = 1;
        return temp;
    }
}
