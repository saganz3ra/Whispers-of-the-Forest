using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 0f; // Som 2D
        }
    }

    void Start()
    {
        // Inicia sons do menu
        Play("MenuMusic");
        Play("Bonfire");
        Play("Wind");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGame")
        {
            // Para a música do menu
            Stop("MenuMusic");

            // Muta os efeitos de Bonfire e Wind
            Mute("Bonfire");
            Mute("Wind");
        }
    }

    public void Play(string name)
    {
        Debug.Log("Playing sound: " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Effects: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Effects: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying)
        {
            s.source.Pause();
        }
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && !s.source.isPlaying)
        {
            s.source.UnPause();
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Effects: " + name + " not found");
            return;
        }

        if (name == "Bonfire")
            volume = Mathf.Clamp(volume, 0f, 0.2f);
        else
            volume = Mathf.Clamp01(volume);

        s.source.volume = volume;
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = 0f;
        }
    }

    public void Unmute(string name, float originalVolume = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            SetVolume(name, originalVolume);
        }
    }
}
