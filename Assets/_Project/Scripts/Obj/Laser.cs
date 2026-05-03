using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D col;
    [SerializeField] private int damage = 30;
    private BossFSM boss;
    private LaserPool laserPool;

    private void Awake()
    {
        if (col == null)
            col = GetComponent<Collider2D>();

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<LifeController>(out var playerLife))
                playerLife.TakeDamage(damage);
        }
    }

    public void SetBoss(BossFSM boss) => this.boss = boss;

    public void SetPool(LaserPool laserPool) => this.laserPool = laserPool;

    public void ActivateCollider() => col.enabled = true;

    public void DeactivateCollider() => col.enabled = false;

    public void DisableLaser()
    {
        if (boss != null)
            boss.GoToCooldown();

        laserPool.Release(this);
    }

    public void SetDirection(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}