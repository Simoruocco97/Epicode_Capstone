using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LifeController enemyLife;
    [SerializeField] private LifeController playerLife;
    [SerializeField] private EnemyAnimationHandler anim;
    [SerializeField] private Rigidbody2D rb;

    [Header("Player Info")]
    [SerializeField] private Transform playerTransform;

    [Header("Enemy Info")]
    [SerializeField] private int enemyDamage = 1;
    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        if (enemyLife == null)
            enemyLife = GetComponent<LifeController>();

        if (anim == null)
            anim = GetComponentInChildren<EnemyAnimationHandler>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (playerLife == null)
                playerLife = player.GetComponent<LifeController>();

            if (playerTransform == null)
                playerTransform = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null || !enemyLife.IsAlive())
            return;

        if (!playerLife.IsAlive())
            return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        if (rb != null)
            rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));

        if (anim != null)
            anim.MovementAnimation(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.TryGetComponent<LifeController>(out var playerLife))
            playerLife.TakeDamage(enemyDamage);

        if (enemyLife != null)
            enemyLife.Suicide();
    }
}