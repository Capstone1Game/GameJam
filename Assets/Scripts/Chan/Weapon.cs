using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Weapon : MonoBehaviour
{
    private int weaponDamage = 10;
    private PlayerMouse parentMouse;
    public int damage;

    public AudioClip[] arrAudioForSmallattack;
    public AudioClip[] arrAudioForBigattack;
    CameraShake Camera;

    private SpriteRenderer spriter;

    private Vector2 weaponPos = new Vector2(0.2f, 0f);
    private Vector2 weaponReversePos = new Vector2(-0.2f, 0f);
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        parentMouse = transform.parent.parent.GetComponent<PlayerMouse>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
    }
    void Update()
    {
        spriter.sortingOrder = PlayerManager.Instance.isLeft ? 3 : 6;

        if (PlayerManager.Instance.isLeft && PlayerManager.Instance.isRightClick)
        {
            spriter.flipY = true;
            transform.localPosition = weaponReversePos;
        }
        else
        {
            spriter.flipY = false;
            transform.localPosition = weaponPos;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        int sel = Random.Range(0, 3);
        AudioClip audioForSmallAttack = arrAudioForSmallattack[sel];
        AudioClip audioForBigAttack = arrAudioForBigattack[sel];

        if (other.tag == "Boss" && !PlayerManager.Instance.isDamage && PlayerManager.Instance.isLive)
        {
            damage = CalculateDamage();
            other.GetComponent<Boss>().OnDamage(damage);

            if (damage == weaponDamage) // max공격일 때 타격감위해 0.1초 멈추기
            {
                StartCoroutine(HitStop(0.25f));
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(audioForBigAttack, 1f);
                Camera.VibrateForTime(0.05f);
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(audioForSmallAttack, 0.8f);
            }
        }
        else if (other.tag == "Dummy")
        {
            damage = CalculateDamage();
            Debug.Log(damage);

            if (damage == weaponDamage) // max공격일 때 타격감위해 0.1초 멈추기
            {
                StartCoroutine(HitStop(0.25f));
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(audioForBigAttack, 1f);
                Camera.VibrateForTime(0.05f);
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(audioForSmallAttack, 0.8f);
            }
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

    //타격 시 일정 데미지 이상을 줄 때 정지 효과
    IEnumerator HitStop(float duration)
    {
        if (PlayerManager.Instance.isDamage) yield break;

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
