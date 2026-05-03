using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float knockbackForce = 5;
    private PlayerSlashPool pool;
    private int damage = 2;

    private void Awake()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            if (other.TryGetComponent<LifeController>(out var life))
                life.TakeDamage(damage);

            if (other.TryGetComponent(out Rigidbody2D rb) && !other.CompareTag("Shooter") && !other.CompareTag("Boss"))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    public void SetFlip(float direction)
    {
        sr.flipX = direction < 0;
    }

    public void SetPool(PlayerSlashPool pool)
    {
        this.pool = pool;
    }

    public void SetUp(int damage)
    {
        this.damage = damage;
        Invoke(nameof(ReturnToPool), 0.2f);
    }

    private void ReturnToPool()
    {
        if (pool != null)
            pool.Release(this);
        else
            Destroy(gameObject);
    }
}