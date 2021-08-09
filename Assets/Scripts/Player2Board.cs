using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Board : MonoBehaviour
{
    // Game Logic:
    private const int GRID_COUNT_X = 8;
    private const int GRID_COUNT_Y = 8;
    private const int GRID_SPACE_SIZE = 1;
    public GameObject[,] gridSpaces2;
    private Camera currentCamera;
    private Vector2Int currentHover;
    public Sprite tempGridSpaces;

    //Player 2 Board Parent creation
    GameObject p2BoardParent;
    
    // Player 2 ship Color and Sprite Assigner
    SpriteRenderer p2Ship_SpriteRenderer;

    private void Awake()
    {
        p2BoardParent = new GameObject();
        p2BoardParent.name = "Player2Board";
        GenerateShipGrid(GRID_SPACE_SIZE, GRID_COUNT_X, GRID_COUNT_Y);
        GameObject tempObj = GameObject.Find("Player2Board");
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

        RaycastHit hitInfo;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Grid")))
        {
            // Get indexes of tile that mouse is on
            Vector2Int hitPosition = LookupGridIndex(hitInfo.transform.gameObject);

            // If we're hovering a tile after not hovering any tile, the following occurs:
            if(currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                gridSpaces2[hitPosition.x,hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            // If we're already hovering a tile, change last hovered, and move to next / none
            if(currentHover != -Vector2Int.one)
            {
                gridSpaces2[currentHover.x,currentHover.y].layer = LayerMask.NameToLayer("Grid");
                currentHover = hitPosition;
                gridSpaces2[hitPosition.x,hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        }
        else
        {
            if(currentHover != -Vector2Int.one)
            {
                gridSpaces2[currentHover.x,currentHover.y].layer = LayerMask.NameToLayer("Grid");
                currentHover = -Vector2Int.one;
            }
        }
    }

    private void GenerateShipGrid(float gridSpaceSize, int gridCountX, int gridCountY)
    {
        gridSpaces2 = new GameObject[gridCountX, gridCountY];
        for(int x = 0; x < gridCountX; x++)
            for(int y = 0; y < gridCountY; y++)
                gridSpaces2[x,y] = GenerateSingleGridSpace(gridSpaceSize, x, y);
    }

    private GameObject GenerateSingleGridSpace(float gridSpaceSize, int x, int y)
    {
        GameObject gridSpaceObject = new GameObject(string.Format("X:{0}, Y{1}", x, y));
        gridSpaceObject.transform.parent = p2BoardParent.gameObject.transform;
        gridSpaceObject.transform.position = new Vector2((float) (x+25)*1.327f, (float) y*1.327f);
        gridSpaceObject.AddComponent<SpriteRenderer>().sprite = tempGridSpaces;
        
        gridSpaceObject.layer = LayerMask.NameToLayer("Grid");
        gridSpaceObject.AddComponent<BoxCollider2D>();
        gridSpaceObject.AddComponent<Button>();

        return gridSpaceObject;
    }

    // Operations
    private Vector2Int LookupGridIndex(GameObject hitInfo)
    {
        for(int x = 0; x < GRID_COUNT_X; x++)
            for(int y = 0; y < GRID_COUNT_Y; y++)
                if(gridSpaces2[x,y] == hitInfo)
                    return new Vector2Int(x, y);

        return -Vector2Int.one; // An invalid, should crash the game (hope it doesnt lol)
    }
}
