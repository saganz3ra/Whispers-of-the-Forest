using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public States currentState;
    private bool isReady = false;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        isReady = true;
    }

    void Update()
    {
        if (!isReady) return;

        if (currentState != null)
        {
            States nextState = currentState.RunCurrentState();
            if (nextState != currentState)
            {
                if (nextState is ChaseState)
                {
                    AudioManagerInGame.Instance.PlayChaseMusic();
                }
                else if (currentState is ChaseState && !(nextState is ChaseState))
                {
                    AudioManagerInGame.Instance.StopChaseMusic();
                }

                currentState = nextState;
            }
        }
    }
}
