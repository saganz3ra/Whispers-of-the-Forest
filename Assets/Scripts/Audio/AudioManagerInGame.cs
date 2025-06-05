using UnityEngine;
using UnityEngine.SceneManagement;

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
            return;
        }

        // Escuta mudanças de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se for a cena de Interface, destrói o AudioManager
        if (scene.name == "Interface")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Evita memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayDamage()
    {
        AudioClip clip = Random.value < 0.5f ? hurtSound : painSound;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayQuestComplete()
    {
        if (sfxSource == null || questCompleteSound == null)
        {
            Debug.LogWarning("sfxSource ou questCompleteSound está nulo!");
            return;
        }

        float volumeDesejado = 1.0f; // máximo possível (0 a 1)
        sfxSource.PlayOneShot(questCompleteSound, volumeDesejado);

        Debug.Log("Som de missão completa tocado com volume: " + volumeDesejado);
    }

    public void PlayChaseMusic()
    {
        if (musicSource != null && chaseMusic != null)
        {
            musicSource.loop = true;
            musicSource.volume = 0.3f;
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
