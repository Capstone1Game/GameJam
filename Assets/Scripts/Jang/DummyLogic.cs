using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLogic : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "shovel")
        {
            anim.Play("Straw Training Dummy Hit", -1, 0f);
            Debug.Log(collision.gameObject.name);
        }
    }

}
