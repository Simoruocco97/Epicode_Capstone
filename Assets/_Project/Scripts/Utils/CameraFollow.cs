using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (PlayerController.Instance != null)
            virtualCamera.Follow = PlayerController.Instance.transform;
    }
}