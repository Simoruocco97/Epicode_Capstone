using UnityEngine;

public class BossFSM : Enemy
{
    [Header("Components")]
    [SerializeField] private BossAnimationHandler bossAnim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform laserTransform;
    [SerializeField] private LaserPool laserPool;
    [SerializeField] private BulletPool armPool;

    [Header("Settings")]
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private float meleeAttackRange = 1.5f;
    private bool hasFiredLaser = false;
    private bool hasThrown = false;
    private bool hasAttackedMelee = false;
    private float cooldownTimer = 0f;
    private Vector2 savedPlayerPos;
    private State currentState = State.Idle;

    private enum State
    {
        Idle,
        SelectAttack,
        ArmThrow,
        Laser,
        Melee,
        Cooldown,
    }

    private void ChangeState(State newState)
    {
        if (newState == currentState)
            return;

        CancelInvoke(nameof(GoToCooldown));

        if (newState == State.ArmThrow)
        {
            hasThrown = false;
            bossAnim.ArmThrowAnimation();
        }

        if (newState == State.Laser)
            hasFiredLaser = false;

        if (newState == State.Melee)
            hasAttackedMelee = false;

        if (newState == State.Cooldown)
        {
            cooldownTimer = 0f;
            bossAnim.CooldownAnimation();
        }

        rb.velocity = Vector2.zero;

        currentState = newState;

        if (newState == State.SelectAttack)
            SelectAttackFunc();

    }

    protected override void Awake()
    {
        if (laserPool == null)
            laserPool = FindAnyObjectByType<LaserPool>();

        if (armPool == null)
            armPool = FindAnyObjectByType<BulletPool>();

        if (bossAnim == null)
            bossAnim = GetComponent<BossAnimationHandler>();

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        base.Awake();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleFunc();
                break;

            case State.ArmThrow:
                ArmThrowFunc();
                break;

            case State.Laser:
                LaserFunc();
                break;

            case State.Melee:
                MeleeFunc();
                break;

            case State.Cooldown:
                CooldownFunc();
                break;
        }
    }

    private void IdleFunc()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("BossAwakening");
    }

    private void SelectAttackFunc()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                ChangeState(State.Melee);
                break;

            case 1:
                ChangeState(State.Laser);
                break;

            case 2:
                ChangeState(State.ArmThrow);
                break;
        }
    }

    private void ArmThrowFunc()
    {
        if (PlayerController.Instance == null || hasThrown)
            return;

        hasThrown = true;

        Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;
        armPool.SpawnBullet(transform.position, dir);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("BossProjectile");
        Flip(dir);
        ChangeState(State.Cooldown);
    }

    private void LaserFunc()
    {
        if (!hasFiredLaser)
        {
            hasFiredLaser = true;

            if (PlayerController.Instance != null)
            {
                savedPlayerPos = PlayerController.Instance.transform.position;
                Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;
                Flip(dir);
            }

            bossAnim.LaserAnimation();
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("BossLaser");
            laserPool.SpawnLaser(laserTransform.position, savedPlayerPos, this);
            ChangeState(State.Cooldown);
        }
    }

    private void MeleeFunc()
    {
        if (PlayerController.Instance == null)
            return;

        Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x * speed, 0);
        Flip(dir);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < meleeAttackRange)
        {
            rb.velocity = Vector2.zero;

            if (!hasAttackedMelee)
            {
                hasAttackedMelee = true;
                bossAnim.MeleeAnimation();
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlaySFXSound("BossMelee");
            }
        }
    }

    private void CooldownFunc()
    {
        rb.velocity = Vector2.zero;

        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldownTime)
        {
            cooldownTimer = 0;
            ChangeState(State.SelectAttack);
        }
    }

    public void GoToCooldown()
    {
        if (currentState == State.Cooldown)
            return;

        ChangeState(State.Cooldown);
    }

    private void Flip(Vector2 dir)
    {
        if (dir.x != 0)
            sr.flipX = dir.x < 0;
    }
}