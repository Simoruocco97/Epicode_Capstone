using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MoveX { get; private set; }
    public bool IsActive { get; private set; } = true;

    private void Update()
    {
        if (!IsActive)
        {
            MoveX = 0f;
            return;
        }

        MoveX = Input.GetAxisRaw("Horizontal");
    }

    public void StopInput() => IsActive = false;
}