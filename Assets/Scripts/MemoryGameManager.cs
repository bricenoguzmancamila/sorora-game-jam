using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    // Drag cards
    public Card[] cards;

    // Arrastra GameOverPanel aquí desde el Inspector
    public GameObject gameOverPanel;

    // El jugador empieza con 3 intentos
    private int attempts = 3;

    // Original positions
    private List<Vector2> positions = new List<Vector2>();

    // Game control
    private Card firstCard;
    private Card secondCard;

    private bool waiting = false;
    private bool gameStarted = false;

    void Start()
    {
        // Save original positions
        foreach (Card card in cards)
        {
            RectTransform rt = card.GetComponent<RectTransform>();
            positions.Add(rt.anchoredPosition);
        }

        // Esconder la pantalla de Game Over al iniciar
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Press Enter to start game
        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Volvemos a tener 3 intentos
        attempts = 3;

        // Limpiamos las cartas seleccionadas
        firstCard = null;
        secondCard = null;

        waiting = false;
        gameStarted = true;

        // Esconder Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Reiniciar todas las cartas
        foreach (Card card in cards)
        {
            card.ResetCard();
        }

        // Mezclar y posteriormente esconder
        StartCoroutine(ShuffleAndHide());
    }

    public bool CanPlay()
    {
        return gameStarted && !waiting;
    }

    IEnumerator ShuffleAndHide()
    {
        // Mientras se están mostrando las cartas,
        // no permitimos seleccionarlas
        waiting = true;

        // Shuffle positions
        List<Vector2> shuffledPositions = new List<Vector2>(positions);

        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPositions.Count);

            Vector2 temp = shuffledPositions[i];
            shuffledPositions[i] = shuffledPositions[randomIndex];
            shuffledPositions[randomIndex] = temp;
        }

        // Assign new positions
        for (int i = 0; i < cards.Length; i++)
        {
            RectTransform rt = cards[i].GetComponent<RectTransform>();
            rt.anchoredPosition = shuffledPositions[i];
        }

        // Show cards for 3 secs
        yield return new WaitForSeconds(3f);

        // Hide cards
        foreach (Card card in cards)
        {
            card.Hide();
        }

        // Ahora el jugador puede seleccionar cartas
        waiting = false;
    }

    public void CardSelected(Card card)
    {
        if (waiting)
            return;

        // Primera carta
        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        // Evitar seleccionar la misma carta
        if (card == firstCard)
            return;

        // Segunda carta
        secondCard = card;

        StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        waiting = true;

        // Esperar 1 segundo para que el jugador vea
        // la segunda carta
        yield return new WaitForSeconds(1f);

        if (firstCard.GetCardID() == secondCard.GetCardID())
        {
            // =========================
            // CORRECT PAIR
            // =========================

            firstCard.Match();
            secondCard.Match();
        }
        else
        {
            // =========================
            // INCORRECT PAIR
            // =========================

            firstCard.Hide();
            secondCard.Hide();

            // Perder un intento
            attempts--;

            Debug.Log("Intentos restantes: " + attempts);

            // Revisar si perdió los 3 intentos
            if (attempts <= 0)
            {
                GameOver();
            }
        }

        firstCard = null;
        secondCard = null;

        // Solo permitir continuar si todavía quedan intentos
        if (attempts > 0)
        {
            waiting = false;
        }
    }

    void GameOver()
    {
        // Detener el juego
        gameStarted = false;
        waiting = true;

        // Mostrar pantalla Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Debug.Log("GAME OVER");
    }

    // =============================
    // YES BUTTON
    // =============================

    public void RetryGame()
    {
        // Detener cualquier Coroutine anterior
        StopAllCoroutines();

        // Comenzar nuevamente
        StartGame();
    }

    // =============================
    // NO BUTTON
    // =============================

    public void QuitGame()
    {
        Debug.Log("Closing game...");

        // Cierra el juego cuando esté compilado
        Application.Quit();

        // Si estamos probando dentro de Unity,
        // detener Play Mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}