using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    //Drag cards
    public Card[] cards;

    //Original positions
    private List<Vector2> positions = new List<Vector2>();

    //Game control
    private Card firstCard;
    private Card secondCard;

    private bool waiting = false;
    private bool gameStarted = false;

    void Start()
    {
        //Save original positions
        foreach (Card card in cards)
        {
            RectTransform rt = card.GetComponent<RectTransform>();
            positions.Add(rt.anchoredPosition);
        }
    }

    void Update()
    {
        //Press enter to start game
        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(ShuffleAndHide());
        }
    }

    public bool CanPlay()
    {
        return gameStarted && !waiting;
    }

    IEnumerator ShuffleAndHide()
    {
        //Shuffle positions
        List<Vector2> shuffledPositions = new List<Vector2>(positions);

        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPositions.Count);

            Vector2 temp = shuffledPositions[i];
            shuffledPositions[i] = shuffledPositions[randomIndex];
            shuffledPositions[randomIndex] = temp;
        }

        //Assign new positions
        for (int i = 0; i < cards.Length; i++)
        {
            RectTransform rt = cards[i].GetComponent<RectTransform>();
            rt.anchoredPosition = shuffledPositions[i];
        }

        //Show cards for 3 secs
        yield return new WaitForSeconds(3f);

        //Hide cards
        foreach (Card card in cards)
        {
            card.Hide();
        }
    }

    public void CardSelected(Card card)
    {
        if (waiting)
            return;

        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        if (card == firstCard)
            return;

        secondCard = card;

        StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        waiting = true;

        yield return new WaitForSeconds(1f);

        if (firstCard.GetCardID() == secondCard.GetCardID())
        {
            // Correct pair
            firstCard.Match();
            secondCard.Match();
        }
        else
        {
            // Incorrect pair
            firstCard.Hide();
            secondCard.Hide();
        }

        firstCard = null;
        secondCard = null;

        waiting = false;
    }
}