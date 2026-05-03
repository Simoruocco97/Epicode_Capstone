public class BossAnimationHandler : AnimationManager
{
    public void AwakenAnimation() => animator.SetTrigger("Awakening");

    public void ArmThrowAnimation()
    {
        animator.SetTrigger("ArmThrow");
    }

    public void LaserAnimation()
    {
        animator.SetTrigger("Laser");
    }

    public void MeleeAnimation()
    {
        animator.SetTrigger("Melee");
    }

    public void CooldownAnimation() => animator.SetTrigger("isCooldown");
}