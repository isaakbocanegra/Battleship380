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
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    void OnMouseDown(){
        // greyish
       // data.colorotherguysships();
        hit.gridmapforplr(1); // this prints the functions
        hit.gridmapforplr(2);

        gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);

        // Net Implementation
        NetTakeTurn tt = new NetTakeTurn();
        tt.targetLocationX = x;
        tt.targetLocationY = y;
        print($"Location being attacked is ({tt.targetLocationX}, {tt.targetLocationY}).");
        Client.Instance.SendToServer(tt);
    }

    void OnMouseUp(){
        // white/transparent
        gridColor.color = new Color(1, 1, 1, 1);
    }

    void OnMouseExit(){
        // white/transparent
        gridColor.color = new Color(1, 1, 1, 1);
    }
}
