using UnityEngine;

public class OrbPool : PoolSystem<Orb>
{
    protected override Orb Create()
    {
        Orb orb = Instantiate(prefab, transform);
        orb.SetPool(this);
        return orb;
    }

    public void SpawnOrb(Vector2 position)
    {
        Orb orb = pool.Get();
        orb.transform.SetPositionAndRotation(position, Quaternion.identity);
    }

    protected override void OnRelease(Orb obj)
    {
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.OnRelease(obj);
    }
}