using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouseActions : MonoBehaviour
{
    public SpriteRenderer gridColor;
    
    void Start()
    {
        gridColor = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        print("The current tile should now be hovered.");
        //print(tempGridScript.tempGrid[0,0].name);
    }

    void OnMouseDown()
    {
        gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);
        print("The current tile is being clicked.");
    }

    void OnMouseUp()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        print("The current tile is no longer being clicked.");
    }

    void OnMouseExit()
    {
        gridColor.color = new Color(1, 1, 1, 1);
        print("The last tile hovered should no longer be hovered.");
    }
}
