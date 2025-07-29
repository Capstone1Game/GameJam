using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public float moveDistance = 3f; // How far it moves vertically
    public float speed = 2f;
    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveDistance);
        transform.position = startPos + Vector3.up * offset;
    }
}
