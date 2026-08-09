using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string cardID;

    public Sprite frontSprite;
    public Sprite backSprite;

    private Image image;
    private bool isFlipped = false;
    private bool isMatched = false;

    private MemoryGameManager gameManager;

    void Start()
    {
        image = GetComponent<Image>();

        // Al inicio mostrar la parte de atrás de la carta
        image.sprite = backSprite;

        // La carta empieza escondida
        isFlipped = false;
        isMatched = false;

        gameManager = FindObjectOfType<MemoryGameManager>();
    }

    public void OnCardClicked()
    {
        // No permitir tocar si el juego aún no empezó
        if (!gameManager.CanPlay())
            return;

        // No permitir tocar cartas ya volteadas o encontradas
        if (isFlipped || isMatched)
            return;

        Flip();

        gameManager.CardSelected(this);
    }

    public void Flip()
    {
        image.sprite = frontSprite;
        isFlipped = true;
    }

    public void Hide()
    {
        image.sprite = backSprite;
        isFlipped = false;
    }

    public void Match()
    {
        isMatched = true;
    }

    public string GetCardID()
    {
        return cardID;
    }

    // Reinicia la carta cuando el jugador selecciona YES
    public void ResetCard()
    {
        isFlipped = false;
        isMatched = false;

        image.sprite = frontSprite;
    }
}