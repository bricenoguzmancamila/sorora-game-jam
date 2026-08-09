using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteraction : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private GameObject interactionText;

    private bool playerIsNear = false;

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;

            if (interactionText != null)
            {
                interactionText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;

            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }
}