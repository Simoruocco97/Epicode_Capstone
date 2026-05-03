using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    protected virtual void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void DamageAnimation() => animator.SetTrigger("isDamaged");

    public void DeathAnimation() => animator.SetTrigger("isDead");
}