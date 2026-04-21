using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask terrain;
    [SerializeField] private float groundDistance;
    private bool IsGrounded = true;

    [Header("Components")]
    [SerializeField] private Transform groundCheckPoint;

    public bool CheckIsGrounded() => IsGrounded;

    private void Awake()
    {
        terrain = LayerMask.GetMask("Terrain");
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) 
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundDistance);
    }

    private void Update()
    {
        if (groundCheckPoint == null) 
            return;

        IsGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundDistance, terrain);
    }
}