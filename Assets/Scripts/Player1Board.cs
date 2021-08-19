using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Board : MonoBehaviour
{
    // Game Logic:
    public const int GRID_COUNT_X = 8;
    public const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    public GameObject[,] gridSpaces1;
    public Sprite tempGridSpaces;
    private SpriteRenderer layer;

    //Player 1 Board Parent Creation
    public static GameObject p1BoardParent;
    
    // Player 1 ship Color and Sprite Assigner
    SpriteRenderer p1Ship_SpriteRenderer;

    private void Awake()
    {
        p1BoardParent = new GameObject();
        p1BoardParent.name = "Player1BoardParent";
        GenerateShipGrid(GRID_SPACE_SIZE, GRID_COUNT_X, GRID_COUNT_Y);
        GameObject tempObj = GameObject.Find("Player1Board");
        Destroy(tempObj);
        p1BoardParent.SetActive(false);
    }

    // Generates the Battleship Board
    private void GenerateShipGrid(float gridSpaceSize, int gridCountX, int gridCountY)
    {
        gridSpaces1 = new GameObject[gridCountX, gridCountY];
        for(int x = 0; x < gridCountX; x++)
            for(int y = 0; y < gridCountY; y++)
                gridSpaces1[x,y] = GenerateSingleGridSpace(gridSpaceSize, y, x); // swap these x and y -- optional for upright
    }

    private GameObject GenerateSingleGridSpace(float gridSpaceSize, int x, int y)
    {
        GameObject gridSpaceObject = new GameObject(string.Format("X:{0}, Y{1}", y, x));  //swap the names -- optional for upright
        gridSpaceObject.transform.parent = p1BoardParent.gameObject.transform;
        gridSpaceObject.transform.position = new Vector2((float) (x-1)*1.33f, (float) (y-3)*-1.33f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        gridSpaceObject.AddComponent<BoxCollider2D>();
        gridSpaceObject.AddComponent<GridMouseP1>();

        layer = gridSpaceObject.GetComponent<SpriteRenderer>();
        layer.sortingLayerName = "Grid";

        return gridSpaceObject;
    }
}
