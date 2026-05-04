using System.Collections;
using UnityEngine;

public class RestArea : MonoBehaviour
{
    [SerializeField] private GameObject pressE;
    [SerializeField] private float blackscreenDuration = 3f;
    private LifeController playerLife;
    private bool playerInRange = false;
    private bool isResting = false;

    private void Awake()
    {
        if (pressE != null)
            pressE.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInRange = true;
        playerLife = collision.GetComponent<LifeController>();

        if (pressE != null)
            pressE.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInRange = false;
        playerLife = null;

        if (pressE != null)
            pressE.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !isResting && Input.GetKeyDown(KeyCode.E))
            StartCoroutine(Rest());
    }

    private IEnumerator Rest()
    {
        isResting = true;

        if (pressE != null) 
            pressE.SetActive(false);

        ScreenFade fade = ScreenFade.Instance;
        bool fadeFinished = false;

        if (fade != null)
        {
            fade.FadeIn(() => fadeFinished = true);
            yield return new WaitUntil(() => fadeFinished);
        }

        if (playerLife != null)
            playerLife.ResetHealth();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXSound("RestSound");

        yield return new WaitForSeconds(blackscreenDuration);

        if (fade != null)
        {
            fade.FadeOut();
            yield return new WaitForSeconds(fade.FadeTime);
        }

        isResting = false;
        if (playerInRange && pressE != null) pressE.SetActive(true);
    }
}