using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipActionsP2 : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public static placeship placer = new placeship();
    public static hitherormiss hit = new hitherormiss();
    public static int isItMyTurn = 0;

    void Start()
    {
        GameObject.Find("P2Aircraft_Carrier").GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
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
        if (gridColor.name.ToString() == "P2Aircraft_Carrier")
        {
            demarkships();
            GridMouseP2.shipname = "P2Aircraft_Carrier";
            GridMouseP2.selectedship= GameObject.Find("P2Aircraft_Carrier");
            GridMouseP2.shipsize = 5;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Battleship")
        {
            demarkships();
            GridMouseP2.shipname = "P2Battleship";
            GridMouseP2.selectedship = GameObject.Find("P2Battleship");
            GridMouseP2.shipsize = 4;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Cruiser")
        {
            demarkships();
            GridMouseP2.shipname = "P2Cruiser";
            GridMouseP2.selectedship = GameObject.Find("P2Cruiser");
            GridMouseP2.shipsize = 3;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Submarine")
        {
            demarkships();
            GridMouseP2.shipname = "P2Submarine";
            GridMouseP2.selectedship = GameObject.Find("P2Submarine");
            GridMouseP2.shipsize = 3;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
        else if (gridColor.name.ToString() == "P2Destroyer")
        {
            demarkships();
            GridMouseP2.shipname = "P2Destroyer";
            GridMouseP2.selectedship = GameObject.Find("P2Destroyer");
            GridMouseP2.shipsize = 2;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP2.shipsize);
        }
    }

    private void demarkships()
    {
        GameObject.Find("P2Aircraft_Carrier").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P2Battleship").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P2Cruiser").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P2Submarine").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P2Destroyer").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);





    }
    private bool isthisaship()
    {
        string shipNameChecker = gridColor.name;
        if(!placer.isthatshipinallready(shipNameChecker)){
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
        }
        return false; 
    }
}
