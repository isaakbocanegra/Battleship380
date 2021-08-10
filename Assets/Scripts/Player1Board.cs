using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Board : MonoBehaviour
{
    // Game Logic:
    private const int GRID_COUNT_X = 8;
    private const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    public GameObject[,] gridSpaces1;
    private Camera currentCamera;
    private Vector2Int currentHover;
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

    private void Update()
    {
        Debug.Log(Input.mousePosition);
        if(!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Grid")))
        {
            // Get indexes of tile that mouse is on
            Vector2Int hitPosition = LookupGridIndex(info.transform.gameObject);

            // If we're hovering a tile after not hovering any tile, the following occurs:
            if(currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                gridSpaces1[hitPosition.x,hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            // If we're already hovering a tile, change last hovered, and move to next / none
            if(currentHover != -Vector2Int.one)
            {
                gridSpaces1[currentHover.x,currentHover.y].layer = LayerMask.NameToLayer("Grid");
                currentHover = hitPosition;
                gridSpaces1[hitPosition.x,hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        }
        else
        {
            if(currentHover != -Vector2Int.one)
            {
                gridSpaces1[currentHover.x,currentHover.y].layer = LayerMask.NameToLayer("Grid");
                currentHover = -Vector2Int.one;
            }
        }
    }

    // Generates the Battleship Board
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
        gridSpaceObject.transform.position = new Vector2((float) (x-25)*1.33f, (float) y*1.33f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        
        gridSpaceObject.layer = LayerMask.NameToLayer("Grid");
        gridSpaceObject.AddComponent<BoxCollider>();
        gridSpaceObject.AddComponent<Button>();
        

        return gridSpaceObject;
    }

    // Operations
    private Vector2Int LookupGridIndex(GameObject hitInfo)
    {
        for(int x = 0; x < GRID_COUNT_X; x++)
            for(int y = 0; y < GRID_COUNT_Y; y++)
                if(gridSpaces1[x,y] == hitInfo)
                    return new Vector2Int(x, y);

        return -Vector2Int.one; // An invalid, should crash the game (hope it doesnt lol)
    }
}
