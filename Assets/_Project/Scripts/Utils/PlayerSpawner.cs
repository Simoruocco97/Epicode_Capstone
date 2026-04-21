using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnLeft;
    [SerializeField] private Transform spawnRight;
    [SerializeField] private Transform player;
    [SerializeField] private int sceneToTheRight;

    private void Start()
    {
        if (spawnLeft == null || spawnRight == null || PlayerController.Instance == null)
            return;

        if (player == null)
            player = PlayerController.Instance.transform;

        if (SceneSwapper.LastScene == sceneToTheRight)
            player.position = spawnRight.position;

        else
            player.position = spawnLeft.position;

        PlayerController.Instance.EnableMovement();
    }
}