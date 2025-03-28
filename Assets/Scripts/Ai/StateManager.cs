using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public States currentState;

    void Update()
    {
        RunStateMachine();

    }

    private void RunStateMachine()
        {
            States nextState = currentState?.RunCurrentState();

            if(nextState != null)
            {
  	            SwitchState(nextState);
             }
        }
            

    private void SwitchState(States nextState)
    {
        currentState = nextState;

    }


}