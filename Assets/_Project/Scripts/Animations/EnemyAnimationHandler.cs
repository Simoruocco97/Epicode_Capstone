using UnityEngine;

public class EnemyAnimationHandler : AnimationManager
{
    public void MovementAnimation(Vector2 dir)
    {
        animator.SetFloat(verticalSpeedName, dir.y);
        animator.SetFloat(horizontalSpeedName, dir.x);

        animator.SetBool("isMoving", dir != Vector2.zero);
    }

    public void ResetAnimation()
    {
        animator.SetFloat(verticalSpeedName, 0f);
        animator.SetFloat(horizontalSpeedName, 0f);
        animator.SetBool("isMoving", false);
        animator.SetBool("isDead", false);
    }
}