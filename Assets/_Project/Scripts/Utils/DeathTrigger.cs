using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<LifeController>(out var life))
            life.Suicide();
    }
}