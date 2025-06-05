using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public States currentState;
    private bool isReady = false;

    [Header("Chase Music Settings")]
    public float musicRadius = 15f;
    public float fadeDuration = 1f;
    public Transform player;

    private bool isChaseMusicPlaying = false;
    private Coroutine fadeCoroutine;

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
        if (!isReady || player == null) return;

        if (currentState != null)
        {
            States nextState = currentState.RunCurrentState();

            // Verifica se mudou de estado
            if (nextState != currentState)
            {
                currentState = nextState;
            }

            // Verifica se está em estado de perseguição
            if (currentState is ChaseState)
            {
                float distance = Vector3.Distance(transform.position, player.position);

                if (!isChaseMusicPlaying && distance <= musicRadius)
                {
                    if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                    fadeCoroutine = StartCoroutine(FadeInChaseMusic());
                }
                else if (isChaseMusicPlaying && distance > musicRadius)
                {
                    if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                    fadeCoroutine = StartCoroutine(FadeOutChaseMusic());
                }
            }
            else
            {
                if (isChaseMusicPlaying)
                {
                    if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                    fadeCoroutine = StartCoroutine(FadeOutChaseMusic());
                }
            }
        }
    }

    private IEnumerator FadeInChaseMusic()
    {
        var audio = AudioManagerInGame.Instance.musicSource;
        var clip = AudioManagerInGame.Instance.chaseMusic;

        audio.Stop();
        audio.clip = clip;
        audio.loop = true;
        audio.volume = 0f;
        audio.Play();

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(0f, 0.3f, t / fadeDuration);
            yield return null;
        }

        audio.volume = 0.3f;
        isChaseMusicPlaying = true;
    }

    private IEnumerator FadeOutChaseMusic()
    {
        var audio = AudioManagerInGame.Instance.musicSource;
        float startVolume = audio.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audio.Stop();
        audio.clip = null;
        isChaseMusicPlaying = false;
    }
}
