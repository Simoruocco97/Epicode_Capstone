using UnityEngine;
public abstract class EnemyFSM : Enemy
{
    [Header("Patrol Points")]
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    [Header("FSM Settings")]
    [SerializeField] protected float idleTime = 1.5f;
    protected Transform currentTransform;
    protected bool playerInRange = false;
    protected float currentTime = 0f;
    protected State currentState;

    protected enum State
    {
        Idle,
        Patrol,
        Action,
        Cooldown
    }

    protected virtual void ChangeState(State newState)
    {
        if (currentState == newState)
            return;

        if (newState == State.Idle || newState == State.Cooldown)
            currentTime = 0f;

        if (newState == State.Action)
            OnEnterAction();

        if (anim != null)
        {
            switch (newState)
            {
                case State.Idle:
                    anim.MovementAnimation(Vector2.zero);
                    anim.StopCooldownAnimation();
                    break;

                case State.Patrol:
                    anim.MovementAnimation(Vector2.right);
                    break;

                case State.Cooldown:
                    anim.StartCooldownAnimation();
                    break;
            }
        }
        currentState = newState;
    }

    protected override void Awake()
    {
        base.Awake();
        currentTransform = pointA;
        currentState = State.Patrol;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleFunc();
                break;

            case State.Patrol:
                PatrolFunc();
                break;

            case State.Action:
                ActionFunc();
                break;

            case State.Cooldown:
                CooldownFunc();
                break;
        }
    }

    protected virtual void IdleFunc()
    {
        currentTime += Time.deltaTime;
        if (currentTime > idleTime)
            ChangeState(State.Patrol);
    }

    protected virtual void PatrolFunc()
    {
        Vector2 dir = (currentTransform.position - transform.position).normalized;
        MoveTowards(dir);
        anim.Flip(dir.x);

        if (Mathf.Abs(transform.position.x - currentTransform.position.x) < 0.1f)
        {
            currentTransform = currentTransform == pointA ? pointB : pointA;
            ChangeState(State.Idle);
        }

        if (playerInRange)
            ChangeState(State.Action);
    }

    protected abstract void MoveTowards(Vector2 dir);
    protected abstract void OnEnterAction();
    protected abstract void ActionFunc();
    protected abstract void CooldownFunc();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}