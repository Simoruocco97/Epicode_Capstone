using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Gameover : MonoBehaviour
{
    public static UI_Gameover Instance { get; private set; }

    [SerializeField] private GameObject gameoverMenu;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private int mainMenuIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        if (gameoverMenu != null)
            gameoverMenu.SetActive(false);

        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();

        CursorLock();
        LockCanvas();
    }

    public void ShowGameover()
    {
        if (gameoverMenu != null)
            gameoverMenu.SetActive(true);

        CursorUnlock();
        UnlockCanvas();

        Time.timeScale = 0f;
    }

    private void HideGameover()
    {
        if (gameoverMenu != null)
            gameoverMenu.SetActive(false);

        CursorLock();
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        if (PlayerController.Instance != null)
        {
            if (PlayerController.Instance.TryGetComponent<LifeController>(out var life))
                life.ResetHealth();
            if (PlayerController.Instance.TryGetComponent<PlayerDamageHandler>(out var damage))
                damage.ResetPlayer();
        }

        ScreenFade.Instance.FadeIn(() =>
        {
            HideGameover();
            LockCanvas();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;

        if (PlayerController.Instance != null)
        {
            if (PlayerController.Instance.TryGetComponent<LifeController>(out var life))
                life.ResetHealth();
            if (PlayerController.Instance.TryGetComponent<PlayerDamageHandler>(out var damage))
                damage.ResetPlayer();
        }

        ScreenFade.Instance.FadeIn(() =>
        {
            HideGameover();
            CursorUnlock();
            SceneManager.LoadScene(mainMenuIndex);
        });
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
