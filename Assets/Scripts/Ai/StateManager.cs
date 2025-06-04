using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public States currentState;

    void Update()
    {
        if (currentState != null)
        {
            States nextState = currentState.RunCurrentState();
            if (nextState != currentState)
            {
                currentState = nextState;
            }
        }
    }
}
