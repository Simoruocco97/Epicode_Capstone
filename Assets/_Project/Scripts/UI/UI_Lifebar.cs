using UnityEngine;
using UnityEngine.UI;

public class UI_Lifebar : MonoBehaviour
{
    [Header("Lifebar")]
    [SerializeField] private Image fillableLifebar;

    public void UpdateLifebar(int currentHp, int maxHp)
    {
        fillableLifebar.fillAmount = (float)currentHp / maxHp;
    }
}