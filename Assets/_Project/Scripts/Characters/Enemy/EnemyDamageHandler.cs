using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] private LifeController life;
    [SerializeField] private EnemyDrop drop;
    [SerializeField] private EnemyFSM enemyFSM;
    [SerializeField] private float deathDelay = 2f;

    private void Awake()
    {
        if (life == null)
            life = GetComponent<LifeController>();

        if (drop == null)
            drop = GetComponent<EnemyDrop>();

        if (enemyFSM == null)
            enemyFSM = GetComponent<EnemyFSM>();
    }

    public void HandleDamage()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("EnemyHurt");

        if (enemyFSM != null)
        {
            enemyFSM.Anim.DamageAnimation();
            enemyFSM.OnHit();
        }
    }

    public void HandleDeath()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("EnemyDeath");

        if (enemyFSM != null)
        {
            enemyFSM.Anim.DeathAnimation();
            enemyFSM.enabled = false;
        }

        if (TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        drop.Drop();
        Invoke(nameof(DisableEnemy), deathDelay);
    }

    private void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}