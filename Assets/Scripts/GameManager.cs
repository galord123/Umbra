using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Player[] players = new Player[2];
    int turn = 1;
    bool action = true;
    void Start()
    {
        
    }

    
    void Update()
    {
        // check if the player has taken his action for the turn
        if (action)
        {

        }
        // if he has taken his action then he can move his units as he wishes 
        else
        {

        }
    }
}
