using UnityEngine;

public class ShopInitializer : MonoBehaviour
{
    [SerializeField] private ShopSlot[] slots;
    [SerializeField] private ShopItem[] items;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetupSlot(items[i]);
        }
    }
}
