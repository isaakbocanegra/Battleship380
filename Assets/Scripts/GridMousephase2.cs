using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMousephase2 : MonoBehaviour
{
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