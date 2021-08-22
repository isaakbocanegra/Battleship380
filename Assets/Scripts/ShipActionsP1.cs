using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipActionsP1 : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public static placeship placer= new placeship();
    public static hitherormiss hit = new hitherormiss();
    public static int isItMyTurn = 1;

    void Start()
    {
        GameObject.Find("P1Aircraft_Carrier").GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
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
        if (gridColor.name.ToString() == "P1Aircraft_Carrier")
        {
            demarkships();
            GridMouseP1.shipname = "P1Aircraft_Carrier";
            GridMouseP1.selectedship= GameObject.Find("P1Aircraft_Carrier");
            GridMouseP1.shipsize = 5;
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Battleship")
        {
            demarkships();
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            GridMouseP1.shipname = "P1Battleship";
            GridMouseP1.selectedship = GameObject.Find("P1Battleship");
            GridMouseP1.shipsize = 4;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Cruiser")
        {
            demarkships();
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            GridMouseP1.shipname = "P1Cruiser";
            GridMouseP1.selectedship = GameObject.Find("P1Cruiser");
            GridMouseP1.shipsize = 3;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Submarine")
        {
            demarkships();
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            GridMouseP1.shipname = "P1Submarine";
            GridMouseP1.selectedship = GameObject.Find("P1Submarine");
            GridMouseP1.shipsize = 3;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
        else if (gridColor.name.ToString() == "P1Destroyer")
        {
            demarkships();
            gridColor.GetComponent<SpriteRenderer>().color = new Color(0.9617175f, 0.9622642f, 0.5310609f, 1);
            GridMouseP1.shipname = "P1Destroyer";
            GridMouseP1.selectedship = GameObject.Find("P1Destroyer");
            GridMouseP1.shipsize = 2;
            print("the ship size currently is " + GridMouseP1.shipsize);
        }
    }
    private void demarkships()
    {
        GameObject.Find("P1Aircraft_Carrier").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P1Battleship").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P1Cruiser").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P1Submarine").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("P1Destroyer").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);





    }
    private bool isthisaship()
    {
        string shipNameChecker = gridColor.name;
        if(!placer.isthatshipinallready(shipNameChecker)){
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
        }
        return false;
    }
}
