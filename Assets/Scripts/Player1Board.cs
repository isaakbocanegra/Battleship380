using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Board : MonoBehaviour
{
    // Game Logic:
    private const int GRID_COUNT_X = 8;
    private const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    public GameObject[,] gridSpaces1;
    public Sprite tempGridSpaces;

    //Player 1 Board Parent Creation
    GameObject p1BoardParent;
    
    // Player 1 ship Color and Sprite Assigner
    SpriteRenderer p1Ship_SpriteRenderer;

    private void Awake()
    {
        p1BoardParent = new GameObject();
        p1BoardParent.name = "Player1Board";
        GenerateShipGrid(GRID_SPACE_SIZE, GRID_COUNT_X, GRID_COUNT_Y);
        GameObject tempObj = GameObject.Find("Player1Board");
        Destroy(tempObj);
    }

    private void GenerateShipGrid(float gridSpaceSize, int gridCountX, int gridCountY)
    {
        gridSpaces1 = new GameObject[gridCountX, gridCountY];
        for(int x = 0; x < gridCountX; x++)
            for(int y = 0; y < gridCountY; y++)
                gridSpaces1[x,y] = GenerateSingleGridSpace(gridSpaceSize, x, y);
    }

    private GameObject GenerateSingleGridSpace(float gridSpaceSize, int x, int y)
    {
        GameObject gridSpaceObject = new GameObject(string.Format("X:{0}, Y{1}", x, y));
        gridSpaceObject.transform.parent = p1BoardParent.gameObject.transform;
        gridSpaceObject.transform.position = new Vector2((float) x*1.327f, (float) y*1.327f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        gridSpaceObject.AddComponent<BoxCollider2D>();

        return gridSpaceObject;
    }
}
