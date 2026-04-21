using UnityEngine;
using UnityEngine.Pool;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] private LifeController life;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyDrop drop;
    [SerializeField] private float deathDelay = 2f;
    [SerializeField] private Enemy enemy;
    private ObjectPool<Enemy> pool;

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
            AudioManager.Instance.PlaySFXSound("FruitImpact");
    }

    public void HandleDeath()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("EnemyDeath");

        drop.TryDrop();

        DisableEnemy();

        if (pool != null)
            Invoke(nameof(ReturnToPool), deathDelay);
        else
            Destroy(gameObject, deathDelay);
    }

    private void DisableEnemy()
    {
        if (col != null)
            col.enabled = false;

        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;
    }

    public void SetPool(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }

    private void ReturnToPool()
    {
        pool?.Release(enemy);
    }
}
