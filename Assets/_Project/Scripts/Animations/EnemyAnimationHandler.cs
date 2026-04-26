using UnityEngine;

public class EnemyAnimationHandler : AnimationManager
{
    [SerializeField] private SpriteRenderer sr;

    protected override void Awake()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();

        base.Awake();
    }

    public void Flip(float dirX)
    {
        if (dirX != 0)
            sr.flipX = dirX > 0;
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