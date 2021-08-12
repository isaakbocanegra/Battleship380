using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouseActions : MonoBehaviour
{
    public SpriteRenderer gridColor;

    void Start()
    {
        gridColor = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        print("The hovered tile should now be red");
    }

    void OnMouseExit()
    {
        gridColor.color = new Color(1, 1, 1, 1);
    }
}
