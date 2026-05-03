using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ReturnToMain : MonoBehaviour
{

[SerializeField] private int mainMenuIndex = 0;

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuIndex);
    }
}
