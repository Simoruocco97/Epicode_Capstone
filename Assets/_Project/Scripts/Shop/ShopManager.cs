using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField] private Button buyButton;
    private ShopSlot slot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        buyButton.interactable = false;
    }

    public void SelectSlot(ShopSlot shopSlot)
    {
        slot = shopSlot;
        buyButton.interactable = true;
    }

    public void OnBuy()
    {
        if (slot == null)
            return;

        ShopItem item = slot.GetItem();

        if (PlayerController.Instance.TryGetComponent<PlayerInventory>(out var inventory))
        {
            if (!inventory.SpendCoin(item.Price))
                return;

            switch (item.Upgrade)
            {
                case ShopItem.UpgradeType.Damage:
                    if (PlayerController.Instance.TryGetComponent<PlayerAttack>(out var attack))
                        attack.AddDamage(item.UpgradeAmount);
                    break;

                case ShopItem.UpgradeType.MaxHealth:
                    if (PlayerController.Instance.TryGetComponent<LifeController>(out var life))
                        life.AddMaxHealth(item.UpgradeAmount);
                    break;
            }

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("ShopBuy");
            buyButton.interactable = false;
            slot = null;
        }
    }
}