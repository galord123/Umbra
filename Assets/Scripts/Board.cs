using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid grid;
    private const int modifier = 3;
    public List<GameObject> createdRepresentions;
    public Sprite[] sprites;
    [Header("Templates")]
    public GameObject textTamplate;
    void Start()
    {

        createdRepresentions = new List<GameObject>();
        grid = new Grid(6, 9);
        grid.SetUnit(1, 2, new Unit(FindObjectOfType<PropertiesManager>().GetUnitProperties("Thug"), 1));
        InstantiateBoard();
    }
    public void InstantiateBoard()
    {
        int bias = 3;
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {

                GameObject tile = new GameObject();
                createdRepresentions.Add(tile);
                tile.transform.position = new Vector2(i * modifier - bias, j * modifier - bias);
                SpriteRenderer sprite = tile.AddComponent<SpriteRenderer>();
                sprite.sprite = sprites[grid.GetTile(i, j).GetHashCode()];
                tile.transform.localScale = new Vector2(3, 3);

                if (grid.GetUnit(i, j) != null)
                {
                    DrawUnit(i, j);


                }
                Structure structurePresent = grid.GetStructure(i, j);
                if (structurePresent != null)
                {
                    GameObject structure = new GameObject("present structure");
                    structurePresent.representation = structure;
                    createdRepresentions.Add(structure);
                    structure.transform.position = new Vector2(i * modifier - bias, j * modifier - bias);
                    SpriteRenderer unitSprite = structure.AddComponent<SpriteRenderer>();
                    unitSprite.sprite = structurePresent.sprite;
                    unitSprite.sortingOrder = 2;
                }

            }

        }
    }



    // draws a unit at a spesific x and y
    public void DrawUnit(int x, int y)
    {
        Unit unitPresent = grid.GetUnit(x, y);
        GameObject unit = new GameObject("present unit");
        unitPresent.representation = unit;
        createdRepresentions.Add(unit);
        unit.transform.position = new Vector2(x * modifier, y * modifier);
        SpriteRenderer unitSprite = unit.AddComponent<SpriteRenderer>();
        unitSprite.sprite = unitPresent.sprite;
        unitSprite.sortingOrder = 3;

        GameObject unitCanvas = new GameObject("canvas");
        GameObject text = GameObject.Instantiate(textTamplate);
        //// later Will be for health bar.
        //GameObject healthBarObj = GameObject.Instantiate(healthBar);
        //healthBarObj.GetComponent<HealthBar>().SetMaxHealth(unitPresent.Maxhealth);
        //healthBarObj.GetComponent<HealthBar>().SetHealth(unitPresent.health);
        //if (unitPresent.owner == -1)
        //{
        //    healthBarObj.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.gray;
        //}
        //else
        //{
        //    healthBarObj.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Object.FindObjectOfType<GameManager>().playerColors[unitPresent.owner];
        //}

        text.transform.SetParent(unitCanvas.transform);
        //healthBarObj.transform.SetParent(unitCanvas.transform);
        unitCanvas.transform.SetParent(unit.transform);


        unitCanvas.AddComponent<Canvas>();
        unitCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        unitCanvas.GetComponent<Canvas>().sortingOrder = 4;
        unitCanvas.transform.localPosition = new Vector3(0, 0, 0);
        unitCanvas.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);

        text.transform.localPosition = new Vector3(0, 0, 0);
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
        text.GetComponent<Text>().text = unitPresent.owner.ToString();

        //healthBarObj.transform.localPosition = new Vector3(0, 15f, 0);
        //healthBarObj.transform.localScale = new Vector3(0.16f, 0.04f, 0.04f);
    }


    private void MoveRepresantation(Unit unit)
    {
        //Object.FindObjectOfType<AudioManager>().Play("Move");
        if (unit != null && unit.representation != null)
            unit.representation.transform.LeanMove(new Vector3(unit.x * modifier, unit.y * modifier, 0), 0.2f).setEaseInOutCirc();
        else
        {
            Debug.LogError("Grid::MoveRepresantation: Could not move unit represantion ");
        }
    }

    private void AttackRepresantation(Unit unit, int x, int y)
    {
        unit.representation.transform.LeanMove(new Vector2(x * modifier, y * modifier), 0.1f).setEaseInOutCirc();
        //Object.FindObjectOfType<AudioManager>().Play("Attack");
        unit.representation.transform.LeanMove(new Vector2(unit.x * modifier, unit.y * modifier), 0.1f).setEaseInOutCirc();
    }

}
