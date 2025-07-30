using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutoBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Ladder") || collision.CompareTag("Elevator"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && !PlayerManager.Instance.isDamage)
        {
            PlayerManager.Instance.OnDamage(collision.transform.position, 0);
            Destroy(gameObject);
        }
    }
}
