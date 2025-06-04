using UnityEngine;

public class AudioManagerInGame : MonoBehaviour
{
    public static AudioManagerInGame Instance;

    public AudioSource sfxSource;

    [Header("Damage Sounds")]
    public AudioClip hurtSound;
    public AudioClip painSound;

    [Header("Quest")]
    public AudioClip questCompleteSound;

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
        // Toca aleatoriamente entre hurt e pain
        AudioClip clip = Random.value < 0.5f ? hurtSound : painSound;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayQuestComplete()
    {
        sfxSource.PlayOneShot(questCompleteSound);
    }
}
