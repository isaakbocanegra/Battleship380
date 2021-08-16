using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipActionsP2 : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public static placeship placer = new placeship();
    

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
        if (this.name.ToString() == "P2Aircraft_Carrier")
        {
            GridMouseP2.shipname = "P2Aircraft_Carrier";
            GridMouseP2.selectedship= GameObject.Find("P2Aircraft_Carrier");
            GridMouseP2.shipsize = 5;
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Battleship")
        {
            GridMouseP2.shipname = "P2Battleship";
            GridMouseP2.selectedship = GameObject.Find("P2Battleship");
            GridMouseP2.shipsize = 4;
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Cruiser")
        {
            GridMouseP2.shipname = "P2Cruiser";
            GridMouseP2.selectedship = GameObject.Find("P2Cruiser");
            GridMouseP2.shipsize = 3;
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Submarine")
        {
            GridMouseP2.shipname = "P2Submarine";
            GridMouseP2.selectedship = GameObject.Find("P2Submarine");
            GridMouseP2.shipsize = 3;
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Destroyer")
        {
            GridMouseP2.shipname = "P2Destroyer";
            GridMouseP2.selectedship = GameObject.Find("P2Destroyer");
            GridMouseP2.shipsize = 2;
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
    }

    private bool isthisaship()
    {
        if (gridColor.name == "P2Aircraft_Carrier")
        {
            GridMouseP2.shipsize = 5;
            return true;
        }
        else if (gridColor.name == "P2Battleship")
        {
            GridMouseP2.shipsize = 4;
            return true;
        }
        else if (gridColor.name == "P2Cruiser")
        {
            GridMouseP2.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "P2Submarine")
        {
            GridMouseP2.shipsize = 2;
            return true;
        }
        else if (gridColor.name == "P2Destroyer")
        {
            GridMouseP2.shipsize = 3;
            return true;
        }
        else
            return false; 
    }
}