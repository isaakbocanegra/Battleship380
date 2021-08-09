using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Board : MonoBehaviour
{
    // Game Logic:
    private const int GRID_COUNT_X = 8;
    private const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    private GameObject[,] gridSpaces;
    public Sprite tempGridSpaces;
    GameObject p2BoardParent;
    
    // Player 1 ship Color and Sprite Assigner
    SpriteRenderer p2Ship_SpriteRenderer;

    private void Awake()
    {
        p2BoardParent = new GameObject();
        p2BoardParent.name = "Player2Board";
        GenerateShipGrid(GRID_SPACE_SIZE, GRID_COUNT_X, GRID_COUNT_Y);
        GameObject tempObj = GameObject.Find("Player2Board");
        Destroy(tempObj);
    }

    private void GenerateShipGrid(float gridSpaceSize, int gridCountX, int gridCountY)
    {
        gridSpaces = new GameObject[gridCountX, gridCountY];
        for(int x = 0; x < gridCountX; x++)
            for(int y = 0; y < gridCountY; y++)
                gridSpaces[x,y] = GenerateSingleGridSpace(gridSpaceSize, x, y);
    }

    private GameObject GenerateSingleGridSpace(float gridSpaceSize, int x, int y)
    {
        GameObject gridSpaceObject = new GameObject(string.Format("X:{0}, Y{1}", x, y));
        gridSpaceObject.transform.parent = p2BoardParent.gameObject.transform;
        gridSpaceObject.transform.position = new Vector2((float) (x+25)*1.3f, (float) y*1.3f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        gridSpaceObject.AddComponent<BoxCollider2D>();

        return gridSpaceObject;
    }
}
