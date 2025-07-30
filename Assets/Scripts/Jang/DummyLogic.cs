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
        if (collision.gameObject.name == "shovel" || collision.gameObject.tag == "ThrowingObject")
        {
            anim.Play("Straw Training Dummy Hit", -1, 0f); // ���� �ǰ� �ִϸ��̼� ���
            Debug.Log(collision.gameObject.name);
            //StartCoroutine(HitStop(0.2f)); // �ǰ� �� �ð� ���� ���߱�
        }
    }

    IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

}
