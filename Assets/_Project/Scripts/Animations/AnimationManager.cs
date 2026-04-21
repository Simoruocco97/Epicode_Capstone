using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected string verticalSpeedName = "vSpeed";
    [SerializeField] protected string horizontalSpeedName = "hSpeed";

    protected virtual void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void DeathAnimation()
    {
        animator.SetBool("isDead", true);
    }
}