using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AudioOptionsMenu : MonoBehaviour
{
    [Header("Audio Configuration")]
    public List<string> musicNames = new List<string> { "Wind", "Bonfire" +
        "" };

    [Header("UI")]
    public Button muteButton;
    public TMP_Text muteButtonText;
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public Slider volumeSlider;

    private float previousVolume = 1f;
    private bool isMuted = false;
    private bool isPaused = false;

    void Start()
    {
        if (muteButton != null)
            muteButton.onClick.AddListener(ToggleMute);

        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);

        if (volumeSlider != null)
        {
            float initialVolume = GetAverageVolume();
            volumeSlider.value = initialVolume;
            volumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void ToggleMute()
    {
        if (AudioManager.instance == null) return;

        if (!isMuted)
        {
            previousVolume = GetAverageVolume();
            foreach (string name in musicNames)
            {
                AudioManager.instance.SetVolume(name, 0f);
            }
            if (muteButtonText != null)
                muteButtonText.text = "Toggle Music";
            isMuted = true;
        }
        else
        {
            foreach (string name in musicNames)
            {
                AudioManager.instance.SetVolume(name, previousVolume);
            }
            if (muteButtonText != null)
                muteButtonText.text = "Mute Music";
            isMuted = false;
        }
    }

    public void TogglePause()
    {
        if (AudioManager.instance == null) return;

        if (!isPaused)
        {
            foreach (string name in musicNames)
            {
                AudioManager.instance.Pause(name);
            }
            if (pauseButtonText != null)
                pauseButtonText.text = "Continue Music";
            isPaused = true;
        }
        else
        {
            foreach (string name in musicNames)
            {
                AudioManager.instance.UnPause(name);
            }
            if (pauseButtonText != null)
                pauseButtonText.text = "Pause Music";
            isPaused = false;
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (AudioManager.instance == null) return;

        foreach (string name in musicNames)
        {
            AudioManager.instance.SetVolume(name, volume);
        }

        isMuted = volume <= 0f;

        if (muteButtonText != null)
            muteButtonText.text = isMuted ? "Activate Music" : "Mute Music";
    }

    public void StopMusic()
    {
        if (AudioManager.instance == null) return;

        foreach (string name in musicNames)
        {
            AudioManager.instance.Stop(name);
        }
    }

    private float GetAverageVolume()
    {
        if (AudioManager.instance == null) return 1f;

        float total = 0f;
        int count = 0;

        foreach (string name in musicNames)
        {
            Sound s = System.Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
            if (s != null)
            {
                total += s.source.volume;
                count++;
            }
        }

        return count > 0 ? total / count : 1f;
    }
}
