using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ReturnToMain : MonoBehaviour
{

[SerializeField] private int mainMenuIndex = 0;

    public void ReturnToMain()
    {
        Time.timeScale = 1f;

        if (PlayerController.Instance != null)
            PlayerController.Instance.GetComponent<PlayerAttack>().enabled = true;

        SceneManager.LoadScene(mainMenuIndex);
    }
}