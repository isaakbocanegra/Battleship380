using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Board : MonoBehaviour
{
    // Game Logic:
    public const int GRID_COUNT_X = 8;
    public const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    public GameObject[,] gridSpaces2;
    public Sprite tempGridSpaces;
    private SpriteRenderer layer;

    //Player 1 Board Parent Creation
    public static GameObject p2BoardParent;
    
    // Player 1 ship Color and Sprite Assigner
    SpriteRenderer p2Ship_SpriteRenderer;

    private void Awake()
    {
        p2BoardParent = new GameObject();
        p2BoardParent.name = "Player2BoardParent";
        GenerateShipGrid(GRID_SPACE_SIZE, GRID_COUNT_X, GRID_COUNT_Y);
        GameObject tempObj = GameObject.Find("Player2Board");
        Destroy(tempObj);
        p2BoardParent.SetActive(false);
    }

    // Generates the Battleship Board
    private void GenerateShipGrid(float gridSpaceSize, int gridCountX, int gridCountY)
    {
        gridSpaces2 = new GameObject[gridCountX, gridCountY];
        for(int x = 0; x < gridCountX; x++)
            for(int y = 0; y < gridCountY; y++)
                gridSpaces2[x,y] = GenerateSingleGridSpace(gridSpaceSize, y, x); // swap these x and y -- optional for upright
    }

    private GameObject GenerateSingleGridSpace(float gridSpaceSize, int x, int y)
    {
        GameObject gridSpaceObject = new GameObject(string.Format("X:{0}, Y{1}", y, x));  //swap the names -- optional for upright
        gridSpaceObject.transform.parent = p2BoardParent.gameObject.transform;
        gridSpaceObject.transform.position = new Vector2((float) (x+20)*1.33f, (float) (y-3)*-1.33f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        gridSpaceObject.AddComponent<BoxCollider2D>();
        gridSpaceObject.AddComponent<Animator>();
        gridSpaceObject.AddComponent<GridMouseP2>();
        layer = gridSpaceObject.GetComponent<SpriteRenderer>();
        layer.sortingLayerName = "Grid";

        return gridSpaceObject;
    }
}
