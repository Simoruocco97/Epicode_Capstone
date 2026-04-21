using UnityEngine;

public class EnemyPool : PoolSystem<Enemy>
{
    protected override Enemy Create()
    {
        Enemy enemy = Instantiate(prefab, transform);
        enemy.GetComponent<EnemyDamageHandler>().SetPool(pool);
        return enemy;
    }

    public void SpawnEnemy(Vector2 position)
    {
        Enemy enemy = pool.Get();
        enemy.transform.SetPositionAndRotation(position, Quaternion.identity);

        if (enemy.TryGetComponent<Collider2D>(out var collider))
            collider.enabled = true;

        if (enemy.TryGetComponent<LifeController>(out var life))
            life.ResetHealth();

        if (enemy.TryGetComponent<EnemyAnimationHandler>(out var animation))
            animation.ResetAnimation();
    }
}