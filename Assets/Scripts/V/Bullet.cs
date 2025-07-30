using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type { FireBall, Laser }
    public Type type;
    public float travelTime = 4f;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    LineRenderer lineRenderer;
    Collider2D coll;
    void Awake()
    {
        switch (type)
        {
            case Type.FireBall:
                rigid = GetComponent<Rigidbody2D>();
                spriter = GetComponent<SpriteRenderer>();
                coll = GetComponent<Collider2D>();
                break;
            case Type.Laser:
                lineRenderer = GetComponent<LineRenderer>();
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerManager.Instance.isDamage)
        {
            PlayerManager.Instance.OnDamage(collision.transform.position, (int)GameManager.instance.bossController.GetBulletDamage(0));
            Destroy(gameObject);
        }
    }



    public IEnumerator FireBullet(Vector3 pos, Vector3 dir)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        float dx = dir.x - pos.x;
        float dy = dir.y - pos.y;

        float maxHeight = 7;

        float timeToApex = Mathf.Sqrt(2f * maxHeight / gravity);
        float totalTime = timeToApex + Mathf.Sqrt(2f * (maxHeight - dy) / gravity);

        // 초기 속도 계산
        float vx = dx / totalTime;
        float vy = gravity * timeToApex;

        float time = 0f;

        while (time < totalTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp(time, 0f, totalTime);

            float x = vx * t;
            float y = vy * t - 0.5f * gravity * t * t;
            Vector3 offset = new Vector3(x, y, 0f);
            if (this != null)
                transform.position = pos + offset;
            yield return null;
        }
        if (this != null) Destroy(gameObject);
        yield break;
    }

    public IEnumerator DrawLaser(Vector3 pos, Vector3 dir)
    {
        lineRenderer.positionCount = 2;
        Vector3 dirVec = (dir - pos).normalized;
        float time = 0f;
        while (time < travelTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / travelTime);
            Vector3 curPos = pos + dirVec * t * 20;

            lineRenderer.SetPosition(0, pos);
            lineRenderer.SetPosition(1, curPos);

            yield return null;
        }
        Destroy(gameObject);
    }
}
