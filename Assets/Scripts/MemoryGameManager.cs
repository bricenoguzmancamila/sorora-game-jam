using System.Collections;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private Sprite cardBack;

    private Card firstCard;
    private Card secondCard;

    private bool canClick = true;

    public void SelectCard(Card card)
    {
        // No permitir clicks mientras estamos comprobando una pareja
        if (!canClick)
            return;

        // No hacer nada si la carta ya fue encontrada
        if (card.isMatched)
            return;

        // No permitir seleccionar la misma carta dos veces
        if (card == firstCard)
            return;

        // Mostrar la carta
        card.Show();

        // Primera carta
        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        // Segunda carta
        secondCard = card;

        StartCoroutine(CheckCards());
    }

    private IEnumerator CheckCards()
    {
        canClick = false;

        // Esperar un segundo para que el jugador vea las cartas
        yield return new WaitForSeconds(1f);

        // Comprobar si son iguales
        if (firstCard.GetFrontSprite() == secondCard.GetFrontSprite())
        {
            // Son pareja
            firstCard.isMatched = true;
            secondCard.isMatched = true;

            Debug.Log("¡Pareja encontrada!");
        }
        else
        {
            // No son pareja
            firstCard.Hide(cardBack);
            secondCard.Hide(cardBack);

            Debug.Log("No coinciden");
        }

        // Reiniciar
        firstCard = null;
        secondCard = null;

        canClick = true;
    }

    public Sprite GetCardBack()
    {
        return cardBack;
    }
}