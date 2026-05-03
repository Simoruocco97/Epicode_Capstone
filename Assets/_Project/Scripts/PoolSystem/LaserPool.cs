using UnityEngine;

public class LaserPool : PoolSystem<Laser>
{
    protected override Laser Create()
    {
        Laser laser = Instantiate(prefab, transform);
        return laser;
    }

    public Laser SpawnLaser(Vector2 position, Vector2 target, BossFSM boss)
    {
        Laser laser = pool.Get();
        laser.transform.position = position;
        laser.SetBoss(boss);
        laser.SetPool(this);
        laser.SetDirection(target);
        return laser;
    }
}