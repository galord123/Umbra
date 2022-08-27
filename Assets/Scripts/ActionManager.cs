using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArgTokens.ArgTokens;
using Args = System.Collections.Generic.Dictionary<ArgTokens.ArgTokens, string>;
public class ActionManager : MonoBehaviour
{
    Board board;
    PropertiesManager PropertiesManager;
    Args args;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        PropertiesManager = FindObjectOfType<PropertiesManager>();
    }

    public void TakeAction(string actionName, Args args)
    {
        this.args = args;
        Invoke(actionName, 0f);
    }

    public void SummonCreature()
    {
        Debug.Log("summoned a " + args[creatureName] + " for player " + args[currentPlayer]);
        if (!args.ContainsKey(ArgTokens.ArgTokens.targetSquare))
        {
            Debug.LogError("tried to summon a creature to no square!");
        }
        string[] targetSquare = args[ArgTokens.ArgTokens.targetSquare].Split(',');
        board.AddUnit(int.Parse(targetSquare[0]), int.Parse(targetSquare[1]), PropertiesManager.GetUnitProperties(args[creatureName]), int.Parse(args[currentPlayer]));
    }
}
