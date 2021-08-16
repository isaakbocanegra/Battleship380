using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMousephase2 : MonoBehaviour
{
    public SpriteRenderer gridColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {


        

    }
    void OnMouseDown()
    {
        gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);


      
    }

    void OnMouseUp()
    {
        gridColor.color = new Color(1, 1, 1, 1);
        // print("The current tile is no longer being clicked.");
    }

    void OnMouseExit()
    {
        gridColor.color = new Color(1, 1, 1, 1);
        // print("The last tile hovered should no longer be hovered.");
    }
}
