using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance { get; private set; }

    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeTime = 1f;

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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FadeIn(Action onComplete = null)
    {
        if (blackImage == null)
            return;

        blackImage.DOFade(1f, fadeTime).OnComplete(() => onComplete?.Invoke());
    }

    public void FadeOut()
    {
        if (blackImage == null)
            return;

        blackImage.DOFade(0f, fadeTime).SetUpdate(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeOut();
    }
}