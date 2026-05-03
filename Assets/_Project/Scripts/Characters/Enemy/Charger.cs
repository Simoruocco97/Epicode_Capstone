using UnityEngine;

public class Charger : EnemyFSM
{
    [SerializeField] private float chargeSpeed = 5f;
    [SerializeField] private float maxChargeCooldown = 5f;
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private float chargeOffset = 0.5f;
    private Vector2 chargeTarget;
    private float chargeTimer = 0f;

    protected override void MoveTowards(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    protected override void OnEnterAction()
    {
        if (PlayerController.Instance == null) 
            return;

        Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;
        chargeTarget = (Vector2)PlayerController.Instance.transform.position - dir * chargeOffset;
    }

    protected override void ActionFunc()
    {
        chargeTimer += Time.deltaTime;
        Vector2 dir = (chargeTarget - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(dir.x * chargeSpeed, rb.velocity.y);
        anim.Flip(dir.x);

        if (Mathf.Abs(transform.position.x - chargeTarget.x) < 0.2f || chargeTimer >= maxChargeCooldown)
        {
            chargeTimer = 0f;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            anim.AttackAnimation();
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("ChargerHit");
            ChangeState(State.Cooldown);
        }
    }

    protected override void CooldownFunc()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= cooldownTime)
        {
            anim.StopCooldownAnimation();
            if (playerInRange)
                ChangeState(State.Action);
            else
                ChangeState(State.Patrol);
        }
    }

    public override void OnHit()
    {
        chargeTimer = 0f;
        base.OnHit();
    }
}