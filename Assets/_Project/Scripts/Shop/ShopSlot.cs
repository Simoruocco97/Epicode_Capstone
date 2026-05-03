using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;

    [Header("Components")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ShopItem item;

    public void SetupSlot(ShopItem item)
    {
        this.item = item;

        icon.sprite = item.Icon;
        itemName.SetText(item.Title);
        description.SetText(item.Description);
        price.SetText(item.Price.ToString());
    }

    public void OnClick()
    {
        if (playerInventory == null)
            playerInventory = PlayerController.Instance.GetComponent<PlayerInventory>();

        if (playerInventory.GetCoin() >= item.Price)
            ShopManager.Instance.SelectSlot(this);
    }

    public ShopItem GetItem() => item;
}