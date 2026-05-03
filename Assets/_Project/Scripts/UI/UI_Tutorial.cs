using UnityEngine;

public class UI_Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;

    private void Awake()
    {
        if (tutorial != null)
            tutorial.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            tutorial.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            tutorial.SetActive(false);
    }
}