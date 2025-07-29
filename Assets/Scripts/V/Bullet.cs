using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type { FireBall, Laser }
    public Type type;
    public float travelTime = 1.5f;
    public float arcHeight = 3f;
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
        if (collision.CompareTag("Ground") || collision.CompareTag("Ladder") || collision.CompareTag("Elevator"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && !PlayerManager.Instance.isDamage)
        {
            PlayerManager.Instance.OnDamage(collision.transform.position, (int) GameManager.instance.bossController.GetBulletDamage(0));
            Destroy(gameObject);
        }
    }



    public IEnumerator FireBullet(Vector3 pos, Vector3 dir)
    {
        float time = 0f;
        Vector3 peakPos = (pos - dir) * 0.5f; // 최고점 기준 위치

        while (time < travelTime && this != null)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / travelTime);

            Vector3 currentPos = Vector3.Lerp(pos, dir, t);

            // 포물선 곡선 추가 (Y축 높이)
            float height = arcHeight * 4 * t * (1 - t);  // 최대 높이: arcHeight
            currentPos.y += height;
            transform.position = currentPos;



            Vector3 lookTarget = (t < 0.5f) ? dir : peakPos;

            Vector3 toLook = transform.position - lookTarget;

            if (Mathf.Abs(t - 0.5f) < 0.02f)
            {
                toLook.x = transform.position.x;
            }
            float angle = Mathf.Atan2(toLook.y, toLook.x) * Mathf.Rad2Deg;


            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        if (this != null)
            transform.position = dir;
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
