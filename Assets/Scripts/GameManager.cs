using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static ArgTokens.ArgTokens;
using Args = System.Collections.Generic.Dictionary<ArgTokens.ArgTokens, string>;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Player[] players = new Player[2];
    Board board;
    CardsManager cardsManager;
    PropertiesManager propertiesManager;
    ActionManager actionManager;
    [HideInInspector]
    public int currentTurn = 0;
    [HideInInspector]
    public int turn = 0;
    bool action = true;
    bool selectingSquare = false; 
    string lastActionName = "";
    Args lastArgs = new Args();

    public List<Phase> playerOneCards = new List<Phase>();
    public List<Phase> playerTwoCards = new List<Phase>();

    public Text turnText;
    public Text resourcesText;
    public List<Action> startOfTurn = new List<Action>(); 
    public List<Action> endOfTurn = new List<Action>();

    Vector2Int currentSquare = new Vector2Int(0, 0);

    private void Awake()
    {
        board = FindObjectOfType<Board>();
        cardsManager = FindObjectOfType<CardsManager>();
        propertiesManager = FindObjectOfType<PropertiesManager>();
        actionManager = FindObjectOfType<ActionManager>();
    }
    void Start()
    {
        players[0] = new Player(playerOneCards);
        players[1] = new Player(playerTwoCards);
        cardsManager.RefreshCards(3, players[currentTurn]);
        UpdateGui(currentTurn);
    }

    private void Update()
    {
        Vector2Int currentMouseSquare = board.GetCurrentMouseSquare();
        if (currentMouseSquare != currentSquare)
        {
            currentSquare = currentMouseSquare;
            board.DrawSquareHightlight(currentSquare);
        }


        if (Input.GetMouseButtonDown(0))  
        {
            if (selectingSquare)
            {

                // Show the square the user is currently on.
                //board.select = true;
                Vector2 square = board.GetCurrentMouseSquare();
                if (square != new Vector2(-1, -1))
                {
                    lastArgs[targetSquare] = square.x + "," + square.y;
                    if (!board.HasUnit((int)square.x, (int)square.y))
                    {
                        lastArgs[emptyTargetSquare] = square.x + "," + square.y;
                    }
                    Debug.Log("selected square " + square.x + "," + square.y + "for action: " + lastActionName);
                    TakeAction(lastActionName, lastArgs);

                }
            }
        }
    }

    public void TakeAction(string actionName, Args args)
    {
        if (action)
        {
            // check if the action needs a target square
            if (args.ContainsKey(targetSquare) && args[targetSquare] == "none" || (args.ContainsKey(emptyTargetSquare) && args[emptyTargetSquare] == "none"))
            {
                selectingSquare = true;
                lastActionName = actionName;
                lastArgs = args;
            }
            else
            {
                Debug.Log("taken action " + actionName);
                // add all of the relevent arguments.
                args.Add(currentPlayer, "" + currentTurn);
                // TODO: search for the action name in the action manager?
                actionManager.TakeAction(actionName, args);
                // TODO: disable all cards cause we have taken an action
                cardsManager.DisableCards();
                action = false;
            }
        }   
    }
    
    public void EndTurn()
    {


        turn += currentTurn;
        // update the turn counter.
        currentTurn += 1;
        currentTurn %= 2;
        UpdateGui(currentTurn);
        // refresh the player's units movment.
        board.Refresh(currentTurn);

        // draw cards for the current player.
        cardsManager.RefreshCards(3, players[currentTurn]);
        action = true;
        foreach (Action startAction in startOfTurn)
        {
            startAction();
        }
    }

    private void UpdateGui(int currentTurn)
    {
        resourcesText.text = string.Format("wood - {0}\nstone - {1}\ncrystals - {2}\ncoins - {3}", players[currentTurn].wood, players[currentTurn].stones, players[currentTurn].crystals, 0);
        turnText.text = "Player " + (currentTurn + 1);
    }
}
