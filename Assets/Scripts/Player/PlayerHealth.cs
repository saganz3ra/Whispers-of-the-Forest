using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    [Header("Áudios de Dano")]
    public AudioClip hurtClip;
    public AudioClip painClip;

    private AudioSource hurtSource;
    private AudioSource painSource;

    public GameObject deathScreen;

    private void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            hurtSource = sources[0];
            painSource = sources[1];
        }
        else
        {
            Debug.LogError("Você precisa adicionar dois AudioSources no jogador.");
        }
    }

    private void Start()
    {
        // Inicialização adicional se necessário
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);

        Debug.Log("Recebendo dano: " + amount);

        PlayDamageSounds();

        if (health <= 0)
        {
            Die();
        }
    }

    private void PlayDamageSounds()
    {
        if (hurtSource != null && hurtClip != null)
        {
            Debug.Log("Tocando hurt");
            hurtSource.PlayOneShot(hurtClip);
        }
        else
        {
            Debug.LogWarning("hurtClip ou hurtSource nulo");
        }

        if (painSource != null && painClip != null)
        {
            Debug.Log("Tocando pain");
            painSource.PlayOneShot(painClip);
        }
        else
        {
            Debug.LogWarning("painClip ou painSource nulo");
        }
    }

    private void Die()
    {
        Debug.Log("Jogador morreu.");

        if (deathScreen != null)
            deathScreen.SetActive(true);

        // Parar som e destruir o AudioManager
        if (AudioManagerInGame.Instance != null)
        {
            AudioManagerInGame.Instance.musicSource.Stop();
            Destroy(AudioManagerInGame.Instance.gameObject);
        }

        StartCoroutine(LoadMenuAfterDelay());
    }

    IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Interface");
    }
}
