using UnityEngine;

public class ChargerHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private Collider2D col;

    private void Awake()
    {
        if (col == null)
            col = GetComponent<Collider2D>();

        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if(collision.TryGetComponent<LifeController>(out var life))
                life.TakeDamage(damage);
    }

    public void EnableHitbox() => col.enabled = true;
    public void DisableHitbox() => col.enabled = false;
}