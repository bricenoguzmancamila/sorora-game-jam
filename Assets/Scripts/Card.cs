using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image image;
    private Sprite frontSprite;

    public bool isMatched = false;
    public bool isFaceUp = false;

    private MemoryGameManager gameManager;

    private void Awake()
    {
        image = GetComponent<Image>();

        // Guardar la imagen original
        frontSprite = image.sprite;

        // Buscar el Game Manager
        gameManager = FindFirstObjectByType<MemoryGameManager>();

        // Agregar el click automáticamente
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }

    private void Start()
    {
        // Empezar con la carta boca abajo
        Hide(gameManager.GetCardBack());
    }

    private void OnCardClicked()
    {
        gameManager.SelectCard(this);
    }

    public void Show()
    {
        image.sprite = frontSprite;
        isFaceUp = true;
    }

    public void Hide(Sprite backSprite)
    {
        image.sprite = backSprite;
        isFaceUp = false;
    }

    public Sprite GetFrontSprite()
    {
        return frontSprite;
    }
}