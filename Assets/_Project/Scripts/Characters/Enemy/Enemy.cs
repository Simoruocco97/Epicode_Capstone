using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected LifeController enemyLife;
    [SerializeField] protected EnemyAnimationHandler anim;
    [SerializeField] protected Rigidbody2D rb;

    [Header("Enemy Info")]
    [SerializeField] protected int enemyDamage = 10;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float damageTimer = 3f;
    [SerializeField] protected float knockback = 2f;
    private float lastDamageTime = Mathf.NegativeInfinity;

    protected virtual void Awake()
    {
        if (enemyLife == null)
            enemyLife = GetComponent<LifeController>();

        if (anim == null)
            anim = GetComponentInChildren<EnemyAnimationHandler>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time >= lastDamageTime + damageTimer)
        {
            lastDamageTime = Time.time;
            if (other.gameObject.TryGetComponent<LifeController>(out var playerLife))
                playerLife.TakeDamage(enemyDamage);

            if (other.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                rb.AddForce(dir * knockback, ForceMode2D.Impulse);
            }
        }
    }

    public EnemyAnimationHandler Anim => anim;
}