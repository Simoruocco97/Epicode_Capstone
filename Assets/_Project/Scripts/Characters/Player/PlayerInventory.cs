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
        coinChange.Invoke();
        Debug.Log($"Moneta raccolta! Monete totali: {coinCounter}");
    }
}
