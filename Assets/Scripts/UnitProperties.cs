using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "UnitProperties")]
public class UnitProperties : ScriptableObject
{
    public new string name;
    public int maxMove;
    public Sprite sprite;
    public int maxHealth;
    public bool canFight;
    public int attack;
    public int range = 1;
    public int defence;
    public int carryCapacity = 0;
}


