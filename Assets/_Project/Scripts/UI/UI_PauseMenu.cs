using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsTab;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject player;
    [SerializeField] private int mainMenuIndex = 0;

    private void Awake()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (optionsTab != null)
            optionsTab.SetActive(false);

        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();

        if (player == null)
            player = FindAnyObjectByType<PlayerController>().gameObject;

        CursorLock();
    }

    private void Update()
    {
        if (optionsTab == null || pauseMenu == null || player == null)
            return;

        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (player.TryGetComponent<LifeController>(out var life))
        {
            if (life.IsDead())
                return;
        }

        if (optionsTab.activeSelf)
        {
            UnlockCanvas();
            optionsTab.SetActive(false);
        }
        else if (pauseMenu.activeSelf)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        UnlockCanvas();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        optionsTab.SetActive(false);
        CursorLock();
    }

    public void Options()
    {
        if (canvasGroup == null)
            return;

        LockCanvas();
        optionsTab.SetActive(true);
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuIndex);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        CursorUnlock();
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnlockCanvas()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    private void LockCanvas()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}