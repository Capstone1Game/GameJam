using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    public int damage = 5;
    Rigidbody2D myRigid;
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().OnDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            myRigid.gravityScale = 1f;
        }
    }
}
