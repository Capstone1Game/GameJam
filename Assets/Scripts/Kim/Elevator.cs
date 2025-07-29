using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public enum MoveDirection { Vertical, Horizontal }
    public MoveDirection moveDirection = MoveDirection.Vertical;

    public float moveDistance = 3f; // How far it moves
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveDistance);

        if (moveDirection == MoveDirection.Vertical)
        {
            transform.position = startPos + Vector3.up * offset;
        }
        else // Horizontal
        {
            transform.position = startPos + Vector3.right * offset;
        }
    }
}
