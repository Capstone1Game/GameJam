using UnityEngine;
using System.Collections.Generic;

public class ElevatorPlatform : MonoBehaviour
{
    public enum MoveDirection { Vertical, Horizontal }
    public MoveDirection moveDirection = MoveDirection.Vertical;

    public float moveDistance = 3f; // How far it moves
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 lastPosition;
    private List<Rigidbody2D> riders = new List<Rigidbody2D>();

    void Start()
    {
        startPos = transform.position;
        lastPosition = startPos;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveDistance);
        Vector3 newPos = moveDirection == MoveDirection.Vertical
            ? startPos + Vector3.up * offset
            : startPos + Vector3.right * offset;

        Vector3 delta = newPos - transform.position;

        // Move riders with the platform
        foreach (Rigidbody2D rb in riders)
        {
            rb.position += new Vector2(delta.x, delta.y);
        }

        transform.position = newPos;
        lastPosition = newPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null && !riders.Contains(rb))
            {
                riders.Add(rb);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null && riders.Contains(rb))
            {
                riders.Remove(rb);
            }
        }
    }
}
