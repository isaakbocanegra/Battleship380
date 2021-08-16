using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipActionsP1 : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public static placeship placer= new placeship(); 
    

    void Start()
    {
        gridColor = GetComponent<SpriteRenderer>();
    }

    // fiz needs to write more print statements bro 
    private void OnMouseDown()
    {
            if(isthisaship())
            {
                whatistheshipsize();
            }
    }

    private void whatistheshipsize()
    {
        if (this.name.ToString() == "P1Aircraft_Carrier")
        {
            GridMouseP1.shipname = "P1Aircraft_Carrier";
            GridMouseP1.selectedship= GameObject.Find("P1Aircraft_Carrier");
            GridMouseP1.shipsize = 5;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Battleship")
        {
            GridMouseP1.shipname = "P1Battleship";
            GridMouseP1.selectedship = GameObject.Find("P1Battleship");
            GridMouseP1.shipsize = 4;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Cruiser")
        {
            GridMouseP1.shipname = "P1Cruiser";
            GridMouseP1.selectedship = GameObject.Find("P1Cruiser");
            GridMouseP1.shipsize = 3;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Submarine")
        {
            GridMouseP1.shipname = "P1Submarine";
            GridMouseP1.selectedship = GameObject.Find("P1Submarine");
            GridMouseP1.shipsize = 3;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Destroyer")
        {
            GridMouseP1.shipname = "P1Destroyer";
            GridMouseP1.selectedship = GameObject.Find("P1Destroyer");
            GridMouseP1.shipsize = 2;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
    }

    private bool isthisaship()
    {
        if (gridColor.name == "P1Aircraft_Carrier")
        {
            GridMouseP1.shipsize = 5;
            return true;
        }
        else if (gridColor.name == "P1Battleship")
        {
            GridMouseP1.shipsize = 4;
            return true;
        }
        else if (gridColor.name == "P1Cruiser")
        {
            GridMouseP1.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "P1Submarine")
        {
            GridMouseP1.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "P1Destroyer")
        {
            GridMouseP1.shipsize = 3;
            return true;
        }
        else
            return false; 
    }
}
