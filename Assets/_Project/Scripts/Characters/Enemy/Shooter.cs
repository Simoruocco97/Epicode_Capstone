using UnityEngine;

public class Shooter : EnemyFSM
{
    [Header("Components")]
    [SerializeField] private BulletPool pool;
    private Vector2 playerPos;

    [Header("Shooter Settings")]
    [SerializeField] private float flySpeed = 2f;
    [SerializeField] private float shootCooldown = 3f;
    private float shootTimer = 0f;

    protected override void Awake()
    {
        if (pool == null)
            pool = FindAnyObjectByType<BulletPool>();

        base.Awake();
    }

    protected override void MoveTowards(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, dir.y * flySpeed);
        anim.Flip(dir.x);
    }

    protected override void OnEnterAction()
    {
        if (PlayerController.Instance == null)
            return;

        playerPos = PlayerController.Instance.transform.position;
    }

    protected override void ActionFunc()
    {
        if (pool == null)
            return;

        Vector2 dir = (playerPos - (Vector2)transform.position).normalized;
        pool.SpawnBullet(transform.position, dir);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("ShooterHit");
        anim.AttackAnimation();
        ChangeState(State.Cooldown);
    }

    protected override void CooldownFunc()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            shootTimer = 0f;
            if (playerInRange)
                ChangeState(State.Action);
            else
                ChangeState(State.Patrol);
        }
    }

    public override void OnHit() { }
}