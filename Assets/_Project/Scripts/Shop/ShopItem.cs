using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public UpgradeType Upgrade;
    public int UpgradeAmount;
    public Sprite Icon;
    public string Title;
    public string Description;
    public int Price;

    public enum UpgradeType
    {
        Damage,
        MaxHealth
    }
}