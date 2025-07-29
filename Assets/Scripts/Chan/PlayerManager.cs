using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum State { Idle, Jump }
    public State currentState = State.Idle;
    public bool isLeft = false;
    public bool isLive = true;
    public static PlayerManager Instance;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Idle:
                currentState = State.Idle;
                break;
            case State.Jump:
                currentState = State.Jump;
                break;
        }
    }
}
