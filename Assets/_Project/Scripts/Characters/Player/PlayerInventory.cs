using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UnityEvent coinChange;
    private int coinCounter = 0;

    public int GetCoin() => coinCounter;

    public void AddCoin(int coin)
    {
        coinCounter += coin;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("CoinPickup");
        coinChange.Invoke();
    }

    public bool SpendCoin(int amount)
    {
        if (coinCounter < amount)
            return false;
        
        coinCounter -= amount;
        coinChange.Invoke();
        return true;
    }

    public void ResetCoin()
    {
        coinCounter = 0;
        coinChange.Invoke();
    }
}