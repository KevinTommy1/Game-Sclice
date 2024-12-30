using System;
using UnityEngine;


public class StateMachineExample : MonoBehaviour
{
    public enum State
    {
        aaaa, bbbb,cccc,dddd,eeee,ffff
    }

    public State currentState { get; private set; }
    private State previousState;
    [SerializeField]   private State defaultState =State.aaaa;

    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        previousState = currentState;
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
        HandleState();
    }

    private void SetState()
    {
        throw new NotImplementedException();
    }

    private void HandleState()
    {
        if (currentState != previousState)
        {
            HandleStateChange();
            previousState = currentState;
        }
        else
        {
            switch (currentState)
            {
                case State.aaaa:
                    break;
                case State.bbbb:
                    break;
                case State.cccc:
                    break;
                case State.dddd:
                    break;
                case State.eeee:
                    break;
                case State.ffff:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case State.aaaa:
                break;
            case State.bbbb:
                break;
            case State.cccc:
                break;
            case State.dddd:
                break;
            case State.eeee:
                break;
            case State.ffff:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
