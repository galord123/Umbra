using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    public readonly int width;
    public readonly int height;
    private Tiles.tiles[,] mapGrid;
    private Unit[,] unitGrid;
    private Structure[,] structureGrid;


    public Grid(int x, int y)
    {
        //objects = new List<GameObject>();
        mapGrid = new Tiles.tiles[x, y];
        unitGrid = new Unit[x, y];
        structureGrid = new Structure[x, y];
        width = x;
        height = y;
        GenerateRandomTerrain();

    }


    private void GenerateRandomTerrain()
    {
        float noise = 0;
        float noiseX, noiseY;
        float offsetX = Random.Range(0, 100);
        float offsetY = Random.Range(0, 100);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                noiseX = i / (width > 10 ? (float)width : (float)10) * 5f + offsetX;
                noiseY = j / (height > 10 ? (float)height : (float)10) * 5f + offsetY;
                noise = Mathf.PerlinNoise(noiseX, noiseY);
                //Debug.Log(noise + " " + noiseX + " " + noiseY);
                if (noise < 0.2)
                {
                    mapGrid[i, j] = Tiles.tiles.sea;
                }
                else if (noise < 0.4)
                {
                    mapGrid[i, j] = Tiles.tiles.coast;
                }
                else if (noise < 0.8)
                {
                    int choice = Random.Range(0, 10);
                    if (choice == 0)
                    {
                        mapGrid[i, j] = Tiles.tiles.hills;
                    }
                    else
                    {
                        mapGrid[i, j] = Tiles.tiles.plains;
                    }
                }
                else
                {
                    mapGrid[i, j] = Tiles.tiles.mountain;
                }

            }
        }

        offsetX = Random.Range(0, 100);
        offsetY = Random.Range(0, 100);


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                noiseX = i / (width > 10 ? (float)width : (float)10) * 5f + offsetX;
                noiseY = j / (height > 10 ? (float)height : (float)10) * 5f + offsetY;
                noise = Mathf.PerlinNoise(noiseX, noiseY);

                if (mapGrid[i, j] == Tiles.tiles.plains && noise > 0.6)
                {
                    mapGrid[i, j] = Tiles.tiles.trees;
                }


            }
        }





    }

    
    public void SetUnit(int x, int y, Unit unit)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return;
        }

        if (unit == null)
        {
            //if (unitGrid[x, y] != null)
            //{
            //    Object.DestroyImmediate(unitGrid[x, y].representation);
            //    unitGrid[x, y].representation = null;
            //}
            unitGrid[x, y] = null;
            return;
        }

        unit.x = x;
        unit.y = y;
        unitGrid[x, y] = unit;

        
    }


    
    


    //public void Clear()
    //{
    //    foreach (GameObject g in objects)
    //    {
    //        Object.Destroy(g);
    //    }
    //    objects.Clear();
    //}

    public void AddStructure(int x, int y, Structure structure)
    {
        //Debug.Log(x + " " + y);
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return;
        }

        structure.x = x;
        structure.y = y;
        structureGrid[x, y] = structure;


        //GameObject unit = new GameObject("present structure");
        //structure.representation = unit;
        //objects.Add(unit);
        //unit.transform.position = new Vector2(x * modifier, y * modifier);
        //SpriteRenderer unitSprite = unit.AddComponent<SpriteRenderer>();
        //unitSprite.sprite = structureGrid[x, y].sprite;
        //unitSprite.sortingOrder = 2;

    }

    //public void MoveUnit(Unit unit, Vector3 pos)
    //{
    //    int x, y;
    //    x = Mathf.FloorToInt((pos.x / 3) + 0.5f);
    //    y = Mathf.FloorToInt(pos.y / 3 + 0.5f);
    //    MoveUnit(unit, x, y);
    //}


    //// Moves a unit to a new point.
    //public void MoveUnit(Unit unit, int x, int y)
    //{
    //    bool attckInRange = false;
    //    // check if the x and y are within range
    //    if (x < 0 || y < 0 || x >= width || y >= height)
    //    {
    //        return;
    //    }

    //    if (Utils.GetDistance(unit.x, unit.y, x, y) <= unit.unitProperties.range)
    //    {
    //        if (unitGrid[x, y] != null && unitGrid[x, y].owner != unit.owner && unit.unitProperties.canFight)
    //            attckInRange = true;
    //    }

    //    // the unit can only move to adjacent tiles
    //    if (!(unit.x + 1 == x && unit.y == y || unit.x - 1 == x && unit.y == y || unit.x == x && unit.y + 1 == y || unit.x == x && unit.y - 1 == y) && !attckInRange)
    //    {
    //        return;
    //    }

    //    // check if the unit has no movement or there is no unit
    //    if (unit == null || unit.movementPoints == 0)
    //    {
    //        return;
    //    }

    //    if (mapGrid[x, y] == Tiles.tiles.mountain || mapGrid[x, y] == Tiles.tiles.sea)
    //    {
    //        return;
    //    }

    //    // check for available space
    //    if (unitGrid[x, y] == null)
    //    {
    //        unitGrid[unit.x, unit.y] = null;
    //        unitGrid[x, y] = unit;
    //        unit.x = x;
    //        unit.y = y;
    //        unit.movementPoints -= 1;

    //        MoveRepresantation(unit);

    //    }
    //    // if the place was not available and it contains an enemy attack it
    //    else if (unitGrid[x, y].owner != unit.owner && unit.unitProperties.canFight)
    //    {
    //        //check if its in the range of attack
    //        if (!attckInRange)
    //        {
    //            return;
    //        }

    //        Object.FindObjectOfType<GameManager>().aiManager.AddToWar(unitGrid[x, y].owner, unit.owner);

    //        // reduce the enemy's health by the units attack
    //        unitGrid[x, y].health -= unit.unitProperties.attack;
    //        unitGrid[x, y].RefreshHealthBar();
    //        AttackRepresantation(unit, x, y);

    //        unit.movementPoints = 0;
    //        // check if the enemy has hitpoints left
    //        if (unitGrid[x, y].health > 0)
    //        {
    //            unit.health -= unitGrid[x, y].unitProperties.defence;
    //            unit.RefreshHealthBar();
    //            AttackRepresantation(unitGrid[x, y], unit.x, unit.y);
    //            if (unit.health <= 0)
    //            {
    //                RemoveUnit(unit.x, unit.y);
    //                Object.Destroy(unit.representation);
    //                unitGrid[unit.x, unit.y] = null;
    //            }
    //        }
    //        else
    //        {
    //            if (unit.unitProperties.range == 1)
    //            {
    //                RemoveUnit(x, y);
    //                Object.Destroy(unitGrid[x, y].representation);
    //                unitGrid[x, y] = unit;
    //                unitGrid[unit.x, unit.y] = null;
    //                unit.x = x;
    //                unit.y = y;
    //                MoveRepresantation(unit);
    //            }
    //            else
    //            {
    //                RemoveUnit(x, y);
    //                Object.Destroy(unitGrid[x, y].representation);
    //                unitGrid[x, y] = null;
    //            }
    //        }


    //    }

    //    // if we landed on an enemy structure
    //    Structure currentStructure = structureGrid[unit.x, unit.y];
    //    if (currentStructure != null && currentStructure.owner != unit.owner)
    //    {
    //        Object.Destroy(currentStructure.representation);

    //        // Add to war List
    //        Object.FindObjectOfType<GameManager>().aiManager.AddToWar(structureGrid[unit.x, unit.y].owner, unit.owner);

    //        if (currentStructure.name == "City")
    //        {
    //            if (mapGrid[unit.x, unit.y] == Tiles.tiles.trees)
    //            {
    //                GameObject.FindObjectOfType<GameManager>().players[structureGrid[x, y].owner].woodProducers -= 1;
    //            }
    //            GameObject.FindObjectOfType<GameManager>().players[structureGrid[x, y].owner].citys -= 1;
    //        }
    //        else if (currentStructure.name == "Lumber Mill")
    //        {

    //            // TODO: move WOOD_PER_MILL to consts 
    //            GameObject.FindObjectOfType<GameManager>().players[structureGrid[x, y].owner].woodProducers -= BoardManager.WOOD_PER_MILL;
    //        }


    //        Object.FindObjectOfType<BoardManager>().playerStructures[structureGrid[unit.x, unit.y].owner].Remove(structureGrid[unit.x, unit.y]);


    //        structureGrid[unit.x, unit.y] = null;
    //        MoveRepresantation(unit);
    //    }

    //    if (unit != null && unit.owner != -1 && unit.movementPoints == 0 && !Object.FindObjectOfType<GameManager>().players[unit.owner].isAI)
    //    {
    //        unit.representation.LeanColor(Color.gray, 0.2f);

    //    }


    //}

    //private void RemoveUnit(int x, int y)
    //{
    //    if (unitGrid[x, y].owner == -1)
    //        return;

    //    Object.FindObjectOfType<BoardManager>().playerUnits[unitGrid[x, y].owner].Remove(unitGrid[x, y]);
    //}


    //private void MoveRepresantation(Unit unit)
    //{
    //    Object.FindObjectOfType<AudioManager>().Play("Move");
    //    if (unit != null && unit.representation != null)
    //        unit.representation.transform.LeanMove(new Vector3(unit.x * modifier, unit.y * modifier, 0), 0.2f).setEaseInOutCirc();
    //    else
    //    {
    //        Debug.LogError("Grid::MoveRepresantation: Could not move unit represantion ");
    //    }
    //}

    //private void AttackRepresantation(Unit unit, int x, int y)
    //{
    //    unit.representation.transform.LeanMove(new Vector2(x * modifier, y * modifier), 0.1f).setEaseInOutCirc();
    //    Object.FindObjectOfType<AudioManager>().Play("Attack");
    //    unit.representation.transform.LeanMove(new Vector2(unit.x * modifier, unit.y * modifier), 0.1f).setEaseInOutCirc();
    //}


    public void Refresh(int owner)
    {
        foreach (Unit unit in unitGrid)
        {
            if (unit?.owner == owner)
            {
                unit.RefreshMovement();
                //unit.representation.LeanColor(Color.white, 0.2f);
            }

        }
    }

    //public void ShowMovement(Unit unit)
    //{
    //    CreateSelect(unit.x + 1, unit.y);
    //    CreateSelect(unit.x - 1, unit.y);
    //    CreateSelect(unit.x, unit.y + 1);
    //    CreateSelect(unit.x, unit.y - 1);

    //}

    //public void HideMovement()
    //{
    //    foreach (GameObject g in selecttions)
    //    {
    //        if (g != null)
    //            GameObject.Destroy(g);
    //    }
    //    selecttions.Clear();
    //}

    //private void CreateSelect(int x, int y)
    //{
    //    if (x < 0 || y < 0 || x >= width || y >= height)
    //    {
    //        return;
    //    }

    //    if (mapGrid[x, y] == Tiles.tiles.mountain || mapGrid[x, y] == Tiles.tiles.sea)
    //    {
    //        return;
    //    }

    //    GameObject selectHighlightObj = new GameObject("present structure");
    //    //Debug.Log(selectHighlightObj);
    //    selecttions.Add(selectHighlightObj);
    //    //Debug.Log(selectHighlightObj);
    //    selectHighlightObj.transform.position = new Vector2(x * modifier, y * modifier);
    //    SpriteRenderer selectHighlightSprite = selectHighlightObj.AddComponent<SpriteRenderer>();
    //    selectHighlightSprite.sprite = selectHighlight;
    //    selectHighlightSprite.sortingOrder = 4;
    //}


    public void SetUnit(Vector3 pos, Unit unit)
    {
        int x, y;
        x = Mathf.FloorToInt((pos.x / 3) + 0.5f);
        y = Mathf.FloorToInt(pos.y / 3 + 0.5f);
        //Debug.Log(x + " " + y);
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return;
        }
        unit.x = x;
        unit.y = y;
        unitGrid[x, y] = unit;

        //GameObject unitObj = new GameObject("present unit");
        //objects.Add(unitObj);
        //unitObj.transform.position = new Vector2(x * modifier, y * modifier);
        //SpriteRenderer unitSprite = unitObj.AddComponent<SpriteRenderer>();
        //unitSprite.sprite = unit.sprite;
        //unitSprite.sortingOrder = 3;

    }

    public Tiles.tiles GetTile(int x, int y)
    {
        return mapGrid[x, y];
    }

    public Unit GetUnit(Vector3 pos)
    {
        int x, y;
        x = Mathf.FloorToInt((pos.x / 3) + 0.5f);
        y = Mathf.FloorToInt(pos.y / 3 + 0.5f);
        //Debug.Log(x + " " + y);
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        return unitGrid[x, y];
    }

    public Structure GetStructure(Vector3 pos)
    {
        int x, y;
        x = Mathf.FloorToInt((pos.x / 3) + 0.5f);
        y = Mathf.FloorToInt(pos.y / 3 + 0.5f);
        //Debug.Log(x + " " + y);
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        return structureGrid[x, y];
    }

    public Structure GetStructure(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        return structureGrid[x, y];
    }

    public Unit GetUnit(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        return unitGrid[x, y];
    }
}
