using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Collider2D col;
    [SerializeField] private UnityEvent onDeath;

    private void Awake()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        if (col == null)
            col = GetComponent<Collider2D>();
    }

    public void HandleDamage()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("PlayerDamage");
    }

    public void HandleDeath()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("GameoverSound");

        DisablePlayer();
        onDeath?.Invoke();
    }

    private void DisablePlayer()
    {
        if (playerController == null || col == null)
            return;

        playerController.enabled = false;
        col.enabled = false;
    }
}