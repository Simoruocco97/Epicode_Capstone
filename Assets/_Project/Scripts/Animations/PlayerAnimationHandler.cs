using UnityEngine;

public class PlayerAnimationHandler : AnimationManager
{
    private Vector2 lastDir = Vector2.zero;

    public void MovementAnimation(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            lastDir = dir;
        }
        animator.SetFloat(horizontalSpeedName, lastDir.x);
        animator.SetFloat(verticalSpeedName, lastDir.y);

        animator.SetBool("isMoving", dir != Vector2.zero);
    }
}
