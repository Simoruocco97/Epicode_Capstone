using UnityEngine;
using UnityEngine.UI;

public class UI_AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider footstepSlider;

    private void Start()
    {
        if (sfxSlider == null || backgroundSlider == null || footstepSlider == null)
            return;

        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        backgroundSlider.value = PlayerPrefs.GetFloat("BgVolume", 1f);
        footstepSlider.value = PlayerPrefs.GetFloat("FootstepVolume", 1f);

        if (AudioManager.Instance == null)
            return;

        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        backgroundSlider.onValueChanged.AddListener(AudioManager.Instance.SetBackgroundVolume);
        footstepSlider.onValueChanged.AddListener(AudioManager.Instance.SetFootstepVolume);
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance == null)
            return;

        sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSFXVolume);
        backgroundSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetBackgroundVolume);
        footstepSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetFootstepVolume);
    }
}