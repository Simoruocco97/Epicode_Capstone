using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip mainMenuBackground;
    [SerializeField] private AudioClip inGameBackground;
    [SerializeField] private List<Sound> sfxSounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            PlayBackgroundMusic(mainMenuBackground);
        else if (backgroundSource.clip != inGameBackground)
            PlayBackgroundMusic(inGameBackground);
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        if (clip != null && backgroundSource != null)
        {
            backgroundSource.Stop();
            backgroundSource.clip = clip;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundSource.isPlaying)
        {
            backgroundSource.Stop();
        }
    }

    public void PlaySFXSound(string soundToPlay)
    {
        var sound = sfxSounds.Find(t => t.ClipName == soundToPlay);

        if (sound != null)
            audioSource.PlayOneShot(sound.AudioClip);
    }
}