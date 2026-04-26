using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private OrbPool pool;

    private void Awake()
    {
        if (pool == null)
            pool = FindAnyObjectByType<OrbPool>();
    }

    public void Drop()
    {
        if (pool == null)
            return;

        Vector3 spawnPos = transform.position;
        pool.SpawnOrb(spawnPos);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("CoinDrop");
    }
}