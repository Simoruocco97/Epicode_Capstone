using System.Collections;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pressE;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private UI_PauseMenu pauseMenu;
    private bool playerInRange = false;
    private bool isOpened = false;

    private void Awake()
    {
        if (shopUI != null)
            shopUI.SetActive(false);

        if (pauseMenu == null)
            pauseMenu = FindAnyObjectByType<UI_PauseMenu>();
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        pressE.SetActive(!isOpened);

        if (Input.GetKeyDown(KeyCode.E) && !isOpened && pauseMenu != null)
        {
            isOpened = true;
            pauseMenu.ShopOpened();
            pressE.SetActive(false);
            shopUI.SetActive(true);
            PlayerController.Instance.DisableMovement();
            PlayerController.Instance.GetComponent<PlayerAttack>().enabled = false;
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("ShopOpen");
            CursorUnlock();
        }
    }

    public void CloseShop()
    {
        isOpened = false;
        pauseMenu.ShopClosed();
        shopUI.SetActive(false);
        PlayerController.Instance.EnableMovement();
        StartCoroutine(ResumeDelay());
        CursorLock();
    }

    private IEnumerator ResumeDelay()
    {
        yield return new WaitForEndOfFrame();
        PlayerController.Instance.GetComponent<PlayerAttack>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
            if (pressE != null)
                pressE.SetActive(false);

            if (isOpened && shopUI != null)
            {
                isOpened = false;
                shopUI.SetActive(false);
                PlayerController.Instance.EnableMovement();
                pauseMenu.ShopClosed();
                CursorLock();
            }
        }
    }

    public bool GetPlayerInRange => playerInRange;

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
}