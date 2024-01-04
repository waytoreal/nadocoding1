using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Card> allCards;

    private Card flippedCard;

    private bool isFlipping = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Board board = FindObjectOfType<Board>();
        allCards = board.GetCards();

        StartCoroutine("FlipAllCardRoutine");
    }

    IEnumerator FlipAllCardRoutine()
    {
        isFlipping = true;

        yield return new WaitForSeconds(0.5f);

        FlipAllCards();

        yield return new WaitForSeconds(3f);
        
        FlipAllCards();
        
        yield return new WaitForSeconds(0.5f);

        isFlipping = false;
    }

    void FlipAllCards()
    {
        foreach (Card card in allCards)
        {
            card.FlipCard();
        }
    }

    public void CardClicked(Card card)
    {
        if (isFlipping)
        {
            return;
        }

        card.FlipCard();

        if (flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            // check match
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        isFlipping = true;
        if (card1.cardID == card2.cardID)
        {
            card1.SetMathced();
            card2.SetMathced();
        }
        else
        {
            yield return new WaitForSeconds(1);

            card1.FlipCard();
            card2.FlipCard();

            yield return new WaitForSeconds(0.4f);
        }
        isFlipping = false;
        flippedCard = null;
    }
}
