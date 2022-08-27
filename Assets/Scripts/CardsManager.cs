using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class CardsManager : MonoBehaviour
{
    public GameObject cardsContainer;
    public GameObject cardPrefab;
    List<GameObject> cards = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshCards(int amount, Player player)
    {
        // clear the current cards from the container.
        foreach( var card in cards)
        {
            Destroy(card);
        }
        cards.Clear();

        // make new random cards for the player.
        System.Random random = new System.Random();
        foreach (var data in player.GetCards().OrderBy(x => random.Next()).Take(amount))
        {
            GameObject card = Instantiate(cardPrefab, cardsContainer.transform);
            card.GetComponent<AbilityCard>().cardData = data;
            card.GetComponent<AbilityCard>().enabled = true;
            
             


            cards.Add(card);
        }
    }

    public void DisableCards()
    {
        foreach(var card in cards)
        {
            card.GetComponent<Button>().interactable = false;
        }
    }
}
