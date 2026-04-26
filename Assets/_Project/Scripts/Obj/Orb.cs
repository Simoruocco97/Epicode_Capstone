using UnityEngine;

public class Orb : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    private Transform player;
    private OrbPool pool;

    [Header("Orb infos")]
    [SerializeField] private int maxValue = 100;
    [SerializeField] private int minValue = 20;
    [SerializeField] private float orbSpeed = 0.5f;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerController.Instance != null)
            player = PlayerController.Instance.transform;

        if (player == null)
            return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * orbSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent<PlayerInventory>(out var inventory))
            {
                inventory.AddCoin(GetValue());
            }
            pool.Release(this);
        }
    }

    public void SetPool(OrbPool pool)
    {
        this.pool = pool;
    }

    private int GetValue() => Random.Range(minValue, maxValue);
}
