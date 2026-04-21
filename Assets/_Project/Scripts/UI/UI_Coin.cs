using TMPro;
using UnityEngine;

public class UI_Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerInventory inventory;

    private void Awake()
    {
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        if (text == null)
            Debug.LogWarning($"Text non assegnato su {gameObject.name}");
    }

    public void UpdateCoin()
    {
        text.SetText(inventory.GetCoin().ToString());
    }
}
