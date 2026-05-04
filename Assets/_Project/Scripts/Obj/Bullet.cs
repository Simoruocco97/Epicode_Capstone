using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockback = 2f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifeTime = 5f;
    private BulletPool pool;
    private float bulletTimer = 0f;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= bulletLifeTime)
        {
            bulletTimer = 0f;
            pool.Release(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<LifeController>(out var playerLife))
                playerLife.TakeDamage(damage);
            if (collision.TryGetComponent<Rigidbody2D>(out var playerRb))
            {
                Vector2 dir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(dir * knockback, ForceMode2D.Impulse);
            }
            bulletTimer = 0f;
            pool.Release(this);
        }
    }
    public void SetPool(BulletPool pool)
    {
        this.pool = pool;
    }
    public void SetUp(Vector2 dir)
    {
        if (rb == null)
            return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
    }
}