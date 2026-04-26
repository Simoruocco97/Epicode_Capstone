using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AnimationManager animator;

    [Header("HP Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int minHealth = 0;
    [SerializeField] private bool fullHpOnStart = true;
    private int currentHealth;
    private bool isDead = false;

    [Header("Event Settings")]
    [SerializeField] private UnityEvent<int, int> onHpChange;
    [SerializeField] private UnityEvent onDeath;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<AnimationManager>();

        if (fullHpOnStart)
            SetHp(maxHealth);
    }

    private void SetHp(int hp)
    {
        currentHealth = Mathf.Clamp(hp, minHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        SetHp(currentHealth - damage);
        onHpChange?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= minHealth)
        {
            DeathFunc();
        }
    }

    private void DeathFunc()
    {
        isDead = true;
        onDeath?.Invoke();

        if (animator != null)
            animator.DeathAnimation();
    }

    public void Suicide()
    {
        TakeDamage(maxHealth);
    }

    public void ResetHealth()
    {
        isDead = false;
        SetHp(maxHealth);
    }

    public bool IsDead() => isDead;
}