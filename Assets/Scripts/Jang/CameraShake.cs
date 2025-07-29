using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = new Vector3(0f, 0f, -5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShakeTime>0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
