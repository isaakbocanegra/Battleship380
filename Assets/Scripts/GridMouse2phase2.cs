using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouse2phase2 : MonoBehaviour
{
    int x = 0;
    int y = 0;
    
    public SpriteRenderer gridColor;

    void Start(){
        gridColor = GetComponent<SpriteRenderer>();
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
