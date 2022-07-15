using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public int maxMove;
    public int movementPoints;
    public Sprite sprite;
    public int owner;
    public int health;
    public int Maxhealth;
    public int x;
    public int y;
    public UnitProperties unitProperties;

    public GameObject representation;

    public Unit(UnitProperties unitProperties, int owner)
    {
        this.unitProperties = unitProperties;
        maxMove = unitProperties.maxMove;
        movementPoints = maxMove;
        sprite = unitProperties.sprite;
        this.owner = owner;
        Maxhealth = unitProperties.maxHealth;
        health = Maxhealth;
    }


    
    public void Heal(int amount)
    {
        if (movementPoints > 0 && !(health == Maxhealth))
        {

            // TODO: add awesome sound effect
            
            health += amount;
            if(health > Maxhealth)
                health = Maxhealth;

            movementPoints = 0;
            //if (!Object.FindObjectOfType<GameManager>().players[owner].isAI)
            //    representation.LeanColor(Color.gray, 0.2f);
        }
    }

    public void RefreshMovement()
    {
        movementPoints = maxMove;
    }
}
