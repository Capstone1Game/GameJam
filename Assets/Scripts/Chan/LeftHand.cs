using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    private SpriteRenderer spriter;

    public Sprite shieldSprite;
    private Sprite originSprite;

    public AudioClip parryAudio; // parrying sound
    CameraShake Camera;

    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        originSprite = spriter.sprite;
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.isRightClick)
        {
            spriter.sprite = shieldSprite;
        }
        else
            spriter.sprite = originSprite;

        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 7 : 4;
        spriter.flipY = isReverse;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerManager.Instance.isRightClick) return;
        if (other.tag == "BossBullet")
        {
            Destroy(other.gameObject);
            StartCoroutine(HitStop(0.25f));
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(parryAudio, 0.8f);
            Camera.VibrateForTime(0.05f);
        }        
    }

    //패링 시 정지 효과
    IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
