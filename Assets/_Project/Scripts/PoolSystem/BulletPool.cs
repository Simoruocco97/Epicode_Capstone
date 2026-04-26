using UnityEngine;

public class BulletPool : PoolSystem<Bullet>
{
    protected override Bullet Create()
    {
        Bullet bullet = Instantiate(prefab, transform);
        bullet.SetPool(this);
        return bullet;
    }

    public void SpawnBullet(Vector2 position, Vector2 dir)
    {
        Bullet bullet = pool.Get();
        bullet.transform.SetPositionAndRotation(position, Quaternion.identity);
        bullet.SetUp(dir);
    }

    protected override void OnRelease(Bullet obj)
    {
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.OnRelease(obj);
    }
}