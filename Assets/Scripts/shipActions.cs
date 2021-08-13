using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipActions : MonoBehaviour
{
    public SpriteRenderer gridColor;
    

    void Start()
    {
        gridColor = GetComponent<SpriteRenderer>();
    }

    // Fuck Vardan's custom method, this shit works without making everything print like 50 million times
    private void OnMouseDown()
    {
            if(isthisaship())
            {
                whatistheshipsize();
            }
    }

    private void whatistheshipsize()
    {
        if (this.name.ToString() == "Aircraft_Carrier")
        {
            GridMouseActions.selectedship= GameObject.Find("Aircraft_Carrier");
            GridMouseActions.shipsize = 5;
            print("the ship size currently is " + GridMouseActions.shipsize);
        }
        else if (gridColor.name.ToString() == "Battleship")
        {
            GridMouseActions.selectedship = GameObject.Find("Battleship");
            GridMouseActions.shipsize = 4;
            print("the ship size currently is " + GridMouseActions.shipsize);
        }
        else if (gridColor.name.ToString() == "Cruiser")
        {
            GridMouseActions.selectedship = GameObject.Find("Cruiser");
            GridMouseActions.shipsize = 3;
            print("the ship size currently is " + GridMouseActions.shipsize);
        }
        else if (gridColor.name.ToString() == "Submarine")
        {
            GridMouseActions.selectedship = GameObject.Find("Submarine");
            GridMouseActions.shipsize = 3;
            print("the ship size currently is " + GridMouseActions.shipsize);
        }
        else if (gridColor.name.ToString() == "Destroyer")
        {
            GridMouseActions.selectedship = GameObject.Find("Destroyer");
            GridMouseActions.shipsize = 2;
            print("the ship size currently is " + GridMouseActions.shipsize);
        }
    }

    private bool isthisaship()
    {
        if (gridColor.name == "Aircraft_Carrier")
        {
            GridMouseActions.shipsize = 5;
            return true;
        }
        else if (gridColor.name == "Battleship")
        {
            GridMouseActions.shipsize = 4;
            return true;
        }
        else if (gridColor.name == "Cruiser")
        {
            GridMouseActions.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "Submarine")
        {
            GridMouseActions.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "Destroyer")
        {
            GridMouseActions.shipsize = 3;
            return true;
        }
        else
            return false; 
    }
}
