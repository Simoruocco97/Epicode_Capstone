using UnityEngine;

public class PlayerAnimationHandler : AnimationManager
{
    [SerializeField] private SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void SetGrounded(bool grounded) => animator.SetBool("isGrounded", grounded);

    public void MovementAnimation(Vector2 dir)
    {
        if (dir != Vector2.zero)
            sr.flipX = dir.x < 0;

        animator.SetBool("isMoving", dir != Vector2.zero);
    }

    public void JumpAnimation() => animator.SetTrigger("Jump");

    public void SetReset() => animator.SetTrigger("isReset");
}