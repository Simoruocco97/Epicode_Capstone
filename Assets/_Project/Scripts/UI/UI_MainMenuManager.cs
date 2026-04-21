using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsTab;
    [SerializeField] private GameObject creditsTab;
    [SerializeField] private int sceneToLoad = 1;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();

        if (optionsTab != null)
            optionsTab.SetActive(false);

        if (creditsTab != null) 
            creditsTab.SetActive(false);

        if (mainMenu != null)
            mainMenu.SetActive(true);
    }

    private void Update()
    {
        if (optionsTab == null || creditsTab == null)
            return;

        if ((optionsTab.activeSelf || creditsTab.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
            CloseAllTabs();
    }

    public void OnStartPress()
    {
        Time.timeScale = 1.0f;

        if (ScreenFade.Instance != null)
            ScreenFade.Instance.FadeIn(() => SceneManager.LoadScene(sceneToLoad));
        else
            SceneManager.LoadScene(sceneToLoad);
    }

    public void OnOptionPress()
    {
        if (optionsTab != null)
            optionsTab.SetActive(true);

        if (mainMenu != null && canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnCreditsPress()
    {
        if (creditsTab != null)
            creditsTab.SetActive(true);

        if (mainMenu != null && canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnExitPress()
    {
        Debug.Log("Sei uscito dal gioco");
        Application.Quit();
    }

    private void CloseAllTabs()
    {
        if (optionsTab != null)
            optionsTab.SetActive(false);

        if (creditsTab != null)
            creditsTab.SetActive(false);

        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}