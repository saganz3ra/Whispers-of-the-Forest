using UnityEngine;

public class AudioManagerInGame : MonoBehaviour
{
    public static AudioManagerInGame Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Damage Sounds")]
    public AudioClip hurtSound;
    public AudioClip painSound;

    [Header("Quest")]
    public AudioClip questCompleteSound;

    [Header("Chase Music")]
    public AudioClip chaseMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDamage()
    {
        AudioClip clip = Random.value < 0.5f ? hurtSound : painSound;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayQuestComplete()
    {
        sfxSource.PlayOneShot(questCompleteSound);
    }

    public void PlayChaseMusic()
    {
        if (musicSource != null && chaseMusic != null)
        {
            musicSource.loop = true;
            musicSource.volume = 0.3f; // Define o volume da música de chase (valor entre 0.0 e 1.0)
            musicSource.clip = chaseMusic;
            musicSource.Play();
        }
    }
    public void StopChaseMusic()
    {
        if (musicSource != null)
        {
            musicSource.loop = false;
            musicSource.Stop();
            musicSource.clip = null;
        }
    }
}

