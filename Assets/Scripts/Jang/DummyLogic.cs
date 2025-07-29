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
            anim.Play("Straw Training Dummy Hit", -1, 0f); // 더미 피격 애니메이션 재생
            Debug.Log(collision.gameObject.name);
            //StartCoroutine(HitStop(0.2f)); // 피격 시 시간 조금 멈추기
        }
    }

    IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

}
