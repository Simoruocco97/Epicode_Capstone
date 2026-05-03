using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    [SerializeField] private int damage = 30;

    private void Awake()
    {
        if (col == null)
            col = GetComponent<Collider2D>();
        
        col.enabled = false;
    }

    public void ActivateCollider() => col.enabled = true;
    public void DeactivateCollider() => col.enabled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<LifeController>(out var life))
                life.TakeDamage(damage);
        }
    }
}
