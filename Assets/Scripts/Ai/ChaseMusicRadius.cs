using UnityEngine;
using System.Collections;

public class ChaseMusicRadius : MonoBehaviour
{
    public Transform player;
    public float chaseRadius = 20f;

    private bool isPlaying = false;
    private AudioSource musicSource;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        musicSource = AudioManagerInGame.Instance.musicSource;
    }

    private void Update()
    {
        if (player == null || musicSource == null || AudioManagerInGame.Instance.chaseMusic == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < chaseRadius && !isPlaying) // Toca quando estiver dentro
        {
            StartChaseMusic();
        }
        else if (distance >= chaseRadius && isPlaying) // Para quando sair do raio
        {
            StopChaseMusic();
        }
    }

    void StartChaseMusic()
    {
        isPlaying = true;
        musicSource.clip = AudioManagerInGame.Instance.chaseMusic;
        musicSource.volume = 0f;
        musicSource.Play();
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn(musicSource, 0.5f, 1f)); // Fade in por 1s até 50%
    }

    void StopChaseMusic()
    {
        isPlaying = false;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut(musicSource, 0f, 1f));
    }

    IEnumerator FadeIn(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = 0f;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOut(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = null;
    }
}
