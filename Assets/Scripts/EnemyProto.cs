using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerShockwave();
        }
    }

    public GameObject shockwavePrefab;

    void TriggerShockwave()
    {
        Vector3 spawnPos = transform.position;

        spawnPos.y -= 0.5f;

        // Right
        GameObject right = Instantiate(shockwavePrefab, spawnPos, Quaternion.identity);
        right.GetComponent<ShockwaveMover>().SetDirection(Vector2.right);

        // Left
        GameObject left = Instantiate(shockwavePrefab, spawnPos, Quaternion.identity);
        left.GetComponent<ShockwaveMover>().SetDirection(Vector2.left);
    }
}
