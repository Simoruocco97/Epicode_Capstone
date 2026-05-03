using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimationHandler anim;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<PlayerAnimationHandler>();

        if (playerController == null)
            playerController = GetComponent<PlayerController>();
    }

    public void HandleDamage()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("PlayerDamage");

        if (anim != null)
            anim.DamageAnimation();
    }

    public void HandleDeath()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("PlayerDeath");

        DisablePlayer();

        if (ScreenFade.Instance == null)
            return;

        ScreenFade.Instance.FadeIn(() =>
        {
            UI_Gameover.Instance.ShowGameover();
            ScreenFade.Instance.FadeOut();
        });
    }

    public void ResetPlayer()
    {
        if (playerController != null)
        {
            playerController.EnableMovement();
            playerController.enabled = true;
        }

        if (TryGetComponent<PlayerAttack>(out var attack))
        {
            attack.enabled = true;
            attack.ResetDamage();
        }

        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;

        if (TryGetComponent<LifeController>(out var life))
            life.ResetHealth();

        if (TryGetComponent<PlayerInventory>(out var inventory))
            inventory.ResetCoin();

        if (spawnPoint != null)
            transform.position = spawnPoint.position;
        else
            transform.position = Vector3.zero;

        if (anim != null)
            anim.SetReset();
    }

    private void DisablePlayer()
    {
        if (playerController != null)
        {
            playerController.DisableMovement();
            playerController.enabled = false;
        }

        if (TryGetComponent<PlayerAttack>(out var attack))
            attack.enabled = false;
    }
}