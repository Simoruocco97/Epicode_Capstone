using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] private LifeController life;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyDrop drop;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float deathDelay = 2f;

    private void Awake()
    {
        if (life == null)
            life = GetComponent<LifeController>();

        if (col == null)
            col = GetComponent<Collider2D>();

        if (drop == null)
            drop = GetComponent<EnemyDrop>();

        if (enemy == null)
            enemy = GetComponent<Enemy>();
    }

    public void HandleDamage()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("EnemyHurt");

        if (enemy != null)
            enemy.Anim.DamageAnimation();
    }

    public void HandleDeath()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("EnemyDeath");

        if (enemy != null)
            enemy.Anim.DeathAnimation();

        drop.Drop();

        enemy.enabled = false;
        DisableEnemy();

        Invoke(nameof(DisableEnemy), deathDelay);
    }

    private void DisableEnemy()
    {
        if (col != null)
            col.enabled = false;

        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;

        gameObject.SetActive(false);
    }
}