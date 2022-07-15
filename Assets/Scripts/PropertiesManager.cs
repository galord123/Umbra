using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropertiesManager : MonoBehaviour
{
    public UnitProperties[] UnitProperties;

    public UnitProperties GetUnitProperties(string name)
    {
        UnitProperties s = Array.Find(UnitProperties, property => property.name == name);
        if (s == null) {
            Debug.LogError("could not find property: " + name);
        }
        return s;
    }
    
}
