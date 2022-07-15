using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles 
{
    public enum tiles
    {
        none = 0,
        sea,
        coast,
        plains,
        trees,
        mountain,
        hills
    }

    public enum units
    {
        none = 0,
        settler,
        warrior
    }

    public enum structures
    {
        none = 0,
        city
    }

    public enum Size
    {
        small = 10,
        medium = 15,
        large = 20
    }
}
