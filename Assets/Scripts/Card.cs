using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Args = System.Collections.Generic.Dictionary<ArgTokens.ArgTokens, string>;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string abilityName;
    public string abilityDescription;
    public Sprite sprite;
    public List<CardArg> args;

    public Args GetArgs()
    {
        Args pairs = new Args();

        for (int i = 0; i < args.Count; i++)
        {
            pairs.Add(args[i].arg, args[i].value);
        }

        return pairs;
    }

    [System.Serializable]
    public class CardArg {
        public ArgTokens.ArgTokens arg;
        public string value;

        public CardArg(ArgTokens.ArgTokens arg, string value)
        {
            this.arg = arg;
            this.value = value;
        }
    }

}