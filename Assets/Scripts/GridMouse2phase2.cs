using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this is for p2
public class GridMouse2phase2 : MonoBehaviour
{
   
   private static placeship data = new placeship();
    private hitherormiss hit = ShipActionsP2.hit;
    public SpriteRenderer gridColor;
    int x = 0;
    int y = 0;
    
    

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
        //gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    void OnMouseDown(){
        // greyish
        // data.colorotherguysships();
        hittingplayertime();

        //gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);

        // Net Implementation
        NetTakeTurn tt = new NetTakeTurn();
        tt.targetLocationX = x;
        tt.targetLocationY = y;
        print($"Location being attacked is ({tt.targetLocationX}, {tt.targetLocationY}).");
        Client.Instance.SendToServer(tt);
    }

    void OnMouseUp(){
        // white/transparent
        //gridColor.color = new Color(1, 1, 1, 1);
    }

    void OnMouseExit(){
        // white/transparent
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


        int[] rowcolumn = extractcoordinatename(gridColor);
        if (hit.hitlocalotherplr(1, rowcolumn[0], rowcolumn[1]))
        {
            print("Life is good and we are gonna color either red or blue");
            hit.gridmapforplr(1);
            hit.gridmapforplr(2);
            hit.scanandcolorlocalother(1);

        }
        else
        {
            print("Life is bad and mooshie said we allready hit that spot");
            hit.gridmapforplr(1);
            hit.gridmapforplr(2);
        }


    }
}
