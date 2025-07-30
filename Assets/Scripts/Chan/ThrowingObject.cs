using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    public int damage = 5;
    public int maxThrowCount = 5;
    Rigidbody2D myRigid;

    CameraShake Camera;
    public AudioClip throwHitAudio; // 던진거 맞을 때 사운드
    public AudioClip throwDestroyAudio; // 던진거 없어질 때 사운드
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().OnDamage(damage);
            maxThrowCount--;
            if(maxThrowCount == 0)
            {
                StartCoroutine(Broken());
                Camera.VibrateForTime(0.05f);
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(throwHitAudio, 0.8f);
                Camera.VibrateForTime(0.05f);
            }
        }
        else if (other.gameObject.tag == "Dummy")
        {
            maxThrowCount--;
            if (maxThrowCount == 0)
            {
                StartCoroutine(Broken());
                Camera.VibrateForTime(0.05f);
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(throwHitAudio, 0.8f);
                Camera.VibrateForTime(0.05f);
            }
        }
        else
        {
            myRigid.gravityScale = 1f;
        }
    }

    IEnumerator Broken()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(throwDestroyAudio, 0.8f);
        yield return new WaitForSeconds(throwDestroyAudio.length);
        Destroy(gameObject);
    }
}
