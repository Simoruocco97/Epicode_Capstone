using UnityEngine.SceneManagement;
using UnityEngine;

public class BossDamageHandler : MonoBehaviour
{
    [SerializeField] private LifeController life;
    [SerializeField] private BossAnimationHandler bossAnim;
    [SerializeField] private Collider2D col;
    [SerializeField] private FlashOnDamage flash;
    [SerializeField] private float deathDelay = 3f;
    [SerializeField] private int endSceneIndex = 6;

    private void Awake()
    {
        if (life == null)
            life = GetComponent<LifeController>();

        if (bossAnim == null)
            bossAnim = GetComponentInChildren<BossAnimationHandler>();

        if (col == null)
            col = GetComponent<Collider2D>();

        if (flash == null)
            flash = GetComponent<FlashOnDamage>();
    }

    public void HandleDamage()
    {
        flash.FlashRed();
    }

    public void HandleDeath()
    {
        transform.position = new Vector2(transform.position.x, 0);
        bossAnim.DeathAnimation();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("BossDeath");

        if (PlayerController.Instance != null)
            PlayerController.Instance.UnlockDoubleJump();

        col.enabled = false;
        GetComponent<BossFSM>().enabled = false;
        Invoke(nameof(LoadEndScene), deathDelay);
    }

    private void LoadEndScene()
    {
        if (ScreenFade.Instance != null)
            ScreenFade.Instance.FadeIn(() => SceneManager.LoadScene(endSceneIndex));
        else
            SceneManager.LoadScene(endSceneIndex);
    }
}