using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource footstepsSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip mainMenuBackground;
    [SerializeField] private AudioClip inGameBackground;
    [SerializeField] private AudioClip bossBackground;
    [SerializeField] private List<Sound> sfxSounds;

    [Header("PlayerPrefs")]
    private const string SFX_VOL_KEY = "SfxVolume";
    private const string BG_VOL_KEY = "BgVolume";
    private const string FOOT_VOL_KEY = "FootstepVolume";

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

    private void Start()
    {
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOL_KEY, 1f));
        SetBackgroundVolume(PlayerPrefs.GetFloat(BG_VOL_KEY, 1f));
        SetFootstepVolume(PlayerPrefs.GetFloat(FOOT_VOL_KEY, 1f));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            PlayBackgroundMusic(mainMenuBackground);
        else if (backgroundSource.clip != inGameBackground)
            PlayBackgroundMusic(inGameBackground);
        else if (scene.name == "BossRoom")
            PlayBackgroundMusic(bossBackground);
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

    public void PlayFootstepSound(string soundName)
    {
        if (footstepsSource.isPlaying) 
            return;

        var sound = sfxSounds.Find(t => t.ClipName == soundName);

        if (sound != null)
        {
            footstepsSource.clip = sound.AudioClip;
            footstepsSource.pitch = Random.Range(0.85f, 1.15f);
            footstepsSource.time = Random.Range(0f, sound.AudioClip.length * 0.3f);
            footstepsSource.Play();
        }
    }

    public void StopFootstepSound()
    {
        if (footstepsSource.isPlaying)
        {
            footstepsSource.pitch = 1f;
            footstepsSource.Stop();
        }
    }

    public void SetSFXVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(SFX_VOL_KEY, value);
    }

    public void SetBackgroundVolume(float value)
    {
        backgroundSource.volume = value;
        PlayerPrefs.SetFloat(BG_VOL_KEY, value);
    }

    public void SetFootstepVolume(float value)
    {
        footstepsSource.volume = value;
        PlayerPrefs.SetFloat(FOOT_VOL_KEY, value);
    }
}