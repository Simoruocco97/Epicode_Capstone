using System.Collections;
using UnityEngine;

public class FlashOnDamage : MonoBehaviour
{
    [SerializeField] private float flashDuration = 1f;
    [SerializeField] private SpriteRenderer sprite;
    private bool isFlashing = false;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void FlashRed()
    {
        StartCoroutine(FlashingCoroutine());
    }

    private IEnumerator FlashingCoroutine()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            sprite.color = Color.red;

            yield return new WaitForSeconds(flashDuration);

            sprite.color = Color.white;
            isFlashing = false;
        }
    }
}