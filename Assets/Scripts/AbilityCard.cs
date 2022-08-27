using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Args = System.Collections.Generic.Dictionary<ArgTokens.ArgTokens, string>;


public class AbilityCard : MonoBehaviour
{
    public Card cardData;
    public Text cardName;
    public Text cardDescription;
    bool selected = false;

    private void Start()
    {
        cardName.text = cardData.name;
        cardDescription.text = cardData.abilityDescription;
    }

    public void CallAbility()
    {
        // prepare the arguments for the action to take.
        if(!selected)
        {
            Args args = cardData.GetArgs();
            selected = true;

            EventSystem.current.SetSelectedGameObject(gameObject);
            FindObjectOfType<GameManager>().TakeAction(cardData.abilityName, args);
        }
        else
        {

        }

    }
}
