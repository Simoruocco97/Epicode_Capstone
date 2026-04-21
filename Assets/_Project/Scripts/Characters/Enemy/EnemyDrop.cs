using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int dropChance = 50;

    private void Awake()
    {
        if (coin == null)
            Debug.LogWarning($"Nessun gameobject assegnato al drop di {gameObject.name}");
    }

    private bool HasDropped()
    {
        return Random.Range(0, 100) < dropChance;
    }

    public void TryDrop()
    {
        if (HasDropped())
        {
            Vector3 spawnPos = transform.position;

            Instantiate(coin, spawnPos, Quaternion.identity);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("CoinDrop");
        }
    }
}