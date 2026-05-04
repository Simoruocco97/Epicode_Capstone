using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsTab;
    [SerializeField] private int sceneToLoad = 1;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();

        if (optionsTab != null)
            optionsTab.SetActive(false);

        if (mainMenu != null)
            mainMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (optionsTab == null)
            return;

        if ((optionsTab.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
            CloseAllTabs();
    }

    public void OnStartPress()
    {
        Time.timeScale = 1.0f;

        if (PlayerController.Instance != null)
            PlayerController.Instance.GetComponent<PlayerAttack>().enabled = true;

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

    public void OnExitPress()
    {
        Application.Quit();
    }

    private void CloseAllTabs()
    {
        if (optionsTab != null)
            optionsTab.SetActive(false);

        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}