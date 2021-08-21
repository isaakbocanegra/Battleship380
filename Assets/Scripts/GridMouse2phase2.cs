using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this is for p2
public class GridMouse2phase2 : MonoBehaviour
{
   
   private static placeship data = new placeship();
    private hitherormiss hit = ShipActionsP2.hit;
    public SpriteRenderer gridColor;
    

    void Start(){

        print("gridmousephase2 has been started and loaded------------------------------");

        data.clearallboardsofcolors();
       gridColor = GetComponent<SpriteRenderer>();
        hit.copy2dforplrarray(data.getboard(1),1);
        hit.copy2dforplrarray(data.getboard(2), 2);
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter(){
        int[] rowcolumn = extractcoordinatename(gridColor);
        //gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        if (hit.gettileinfo(1, rowcolumn[0], rowcolumn[1]) == 0|| hit.gettileinfo(1, rowcolumn[0], rowcolumn[1]) == 1)
            gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    void OnMouseDown(){
        // greyish
        // data.colorotherguysships();
        if(ShipActionsP2.isItMyTurn == 1)
        {
            ShipActionsP2.isItMyTurn = 0;
            hittingplayertime();
        }

        //gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);
    }

    void OnMouseUp(){
        // white/transparent
        //gridColor.color = new Color(1, 1, 1, 1);
    }

    void OnMouseExit(){
        // white/transparent
        int[] rowcolumn = extractcoordinatename(gridColor);
        //gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        if (hit.gettileinfo(1, rowcolumn[0], rowcolumn[1]) == 0 || hit.gettileinfo(1, rowcolumn[0], rowcolumn[1]) == 1)
            gridColor.color = new Color(1f, 1f, 1f, 1);
        // gridColor.color = new Color(1, 1, 1, 1);
    }


    ////////////////////////////////////////////////////////////////////nonmainfunctions below
    ///// the extractcoordinatename function will take in a spriterenderer and extract numeracle values from the name for x and y.
    int[] extractcoordinatename(SpriteRenderer obj)
    {
        int[] rowcolumn = { -1, -1 };
        bool foundfirst = false;
        string coordinates = obj.name.ToString();
        char[] data = coordinates.ToCharArray();
        foreach (char c in data)
        {
            if (char.IsDigit(c))
            {
                if (foundfirst == false)
                {
                    foundfirst = true;
                    rowcolumn[0] = (int)char.GetNumericValue(c);
                }
                else
                    rowcolumn[1] = (int)char.GetNumericValue(c);
            }
        }

        //print("you extracted row coordinate " + rowcolumn[0] + " and column coordinate " + rowcolumn[1]);
        return rowcolumn;
    }

    private void hittingplayertime()
    {
        // Net Implementation
        NetTakeTurn tt = new NetTakeTurn();


        int[] rowcolumn = extractcoordinatename(gridColor);
        if (hit.hitlocalotherplr(1, rowcolumn[0], rowcolumn[1]))
        {
            tt.targetLocationX = rowcolumn[1];
            tt.targetLocationY = rowcolumn[0];
            print("Life is good and we are gonna color either red or blue");
            hit.gridmapforplr(1);
            hit.gridmapforplr(2);
            hit.scanandcolorlocalother(1);
            print($"Location being attacked is ({tt.targetLocationX}, {tt.targetLocationY}).");
            Client.Instance.SendToServer(tt);
        }
        else
        {
            print("Life is bad and mooshie said we allready hit that spot");
            hit.gridmapforplr(1);
            hit.gridmapforplr(2);
        }
    }
}
