using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int wood = 0;
    public int stones = 0;
    public int crystals = 0;
    public int phase = 0;
    private List<Phase> cards;

    public Player(List<Phase> cards)
    {
        this.cards = cards;
    }

    public List<Card> GetCards()
    {
        return cards[phase].cards;
    }
}
