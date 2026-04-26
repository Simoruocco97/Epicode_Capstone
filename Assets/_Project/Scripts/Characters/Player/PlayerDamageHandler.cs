using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimationHandler anim;

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
            AudioManager.Instance.PlaySFXSound("GameoverSound");

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
            attack.enabled = true;

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