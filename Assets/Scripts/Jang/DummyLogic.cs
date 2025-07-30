using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class DummyLogic : MonoBehaviour
{
    Animator anim;
    float detectLine = 5f;
    public bool isShooter = false;
    public bool isDetect = false;
    public Transform target;
    public GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance < detectLine)
        {
            if (!isDetect)
            {
                isDetect = true;
                Debug.Log("detected");
                StartCoroutine(FireBullet());   
            }
        }
        else
        {
            isDetect = false;
        }
    }

    IEnumerator FireBullet()
    {
        Debug.Log("startFire");
        while (isShooter && isDetect)
        {
            yield return new WaitForSeconds(2f);
            GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation);
            instBullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * 10f, ForceMode2D.Impulse);
            StartCoroutine(FireBullet());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "shovel" || collision.gameObject.tag == "ThrowingObject")
        {
            anim.Play("Straw Training Dummy Hit", -1, 0f);
            Debug.Log(collision.gameObject.name);
        }
    }

    IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        //줍기 아이템 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, detectLine);
    }
}
