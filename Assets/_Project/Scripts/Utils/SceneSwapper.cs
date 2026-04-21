using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public static int LastScene { get; private set; }

    [SerializeField] private int LevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ScreenFade.Instance != null)
        {
            PlayerController.Instance.DisableMovement();
            LastScene = SceneManager.GetActiveScene().buildIndex;
            ScreenFade.Instance.FadeIn(() => SceneManager.LoadScene(LevelIndex));
        }
    }
}