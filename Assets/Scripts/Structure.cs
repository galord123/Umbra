using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{
    private StructureProperties properties;
    public string name;
    public Sprite sprite;
    public int owner;
    public GameObject representation;
    public int x;
    public int y;

    public Structure(StructureProperties properties, int owner)
    {
        name = properties.name;
        this.properties = properties;
        sprite = properties.sprite;
        this.owner = owner;
    }
}
