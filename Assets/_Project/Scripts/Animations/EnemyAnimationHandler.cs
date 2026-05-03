using UnityEngine;

public class EnemyAnimationHandler : AnimationManager
{
    [SerializeField] private SpriteRenderer sr;
    private Vector3 originalScale;

    protected override void Awake()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();

        originalScale = transform.localScale;

        base.Awake();
    }

    public void Flip(float dirX)
    {
        if (dirX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = dirX > 0 ? -originalScale.x : originalScale.y;
            transform.localScale = scale;
        }
    }

    public void MovementAnimation(Vector2 dir)
    {
        animator.SetBool("isMoving", dir != Vector2.zero);
    }


    public void AttackAnimation() => animator.SetTrigger("attack");

    public void StartCooldownAnimation()
    {
        if (HasParameter("isCooldown"))
            animator.SetBool("isCooldown", true);
    }

    public void StopCooldownAnimation()
    {
        if (HasParameter("isCooldown"))
            animator.SetBool("isCooldown", false);
    }

    private bool HasParameter(string parameter)
    {
        foreach (var param in animator.parameters)
            if (param.name == parameter)
                return true;

        return false;
    }
}