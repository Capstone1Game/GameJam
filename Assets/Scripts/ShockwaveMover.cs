using UnityEngine;

public class ShockwaveMover : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 1f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
