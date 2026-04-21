using UnityEngine;

public class PlayerSlashPool : PoolSystem<PlayerSlash>
{
    protected override PlayerSlash Create()
    {
        PlayerSlash slash = Instantiate(prefab, transform);
        slash.SetPool(this);
        return slash;
    }

    public void SpawnSlash(Vector3 position, int damage, float direction)
    {
        PlayerSlash slash = pool.Get();
        slash.transform.SetPositionAndRotation(position, Quaternion.identity);
        slash.SetFlip(direction);
        slash.SetUp(damage);
    }
}