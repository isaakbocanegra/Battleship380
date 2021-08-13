using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouseActions : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public bool verticle = false;
    public int shipsize = 3;

    void Update()
    {
       


    }

    void Start()
    {
        
        gridColor = GetComponent<SpriteRenderer>();
    }

    

    // name formatting exampple "X:0, Y0"
    void OnMouseEnter()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        //print("The current tile should now be hovered.");

        marksides();

        //print(tempGridScript.tempGrid[0,0].name);
    }

   
    void OnMouseDown() 
    {
       
        gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);

        print("The current tile is being clicked");
    }

    void OnMouseUp()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
       // print("The current tile is no longer being clicked.");












    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))// 0 is left, 1 is right, 3 is middle
        {
            if (verticle)
            {
                
                verticle = false;
                demarksides();
            }
            else
            {
                verticle = true;
            }
            //print("You just right clicked ");
        }

    }

   

    void OnMouseExit()
    {
        gridColor.color = new Color(1, 1, 1, 1);
        // print("The last tile hovered should no longer be hovered.");

        demarksides();
        //print(tempGridScript.tempGrid[0,0].name);


    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
 public void cleansides()
    { 
    
    }
    void marksides()
    {
        // GameObject.Find("X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);


        int[] rowcolumn = extractcordinatename(gridColor);
        int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);
        if (shipsize == 3)
        {
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

    }

    void demarksides()
    {

        // GameObject.Find("X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);


        int[] rowcolumn = extractcordinatename(gridColor);
        int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);
        if (shipsize == 3)
        {
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

    }

    // the extractcordinatename function will take in a spriterenderer and extract numeracle values from the name for x and y.
    int[] extractcordinatename(SpriteRenderer obj)
    {
        int[] rowcolumn = { -1, -1 };
        bool foundfirst = false;
        string cordinates = obj.name.ToString();
        char[] data = cordinates.ToCharArray();
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


        //print("you extracted row cordinate " + rowcolumn[0] + " and column cordinate " + rowcolumn[1]);


        return rowcolumn;
    }
    int[] gettilesnextto(int clickpositionrow, int clickpositioncolumn, int shipsize)
    {
        // in the case below clickpositionrow must stay the same 
        int[] selectioncolumn = { -1000, -1000, -1000, -1000, -1000, -1000, };
        int count = 0; // only horizontal atm
        int evenodd = 0;
        selectioncolumn[0] = clickpositioncolumn;
        count++;
        int positiontrackright = clickpositioncolumn;
        int positiontrackleft = clickpositioncolumn;

        while (count < shipsize)
        {
            if (evenodd % 2 == 0)
            {
                positiontrackright++;
                if (positiontrackright < 8)
                {
                    selectioncolumn[count] = positiontrackright;
                    count++;
                }
            }
            else
            {
                positiontrackleft--;
                if (positiontrackleft >= 0)
                {
                    selectioncolumn[count] = positiontrackleft;
                    count++;
                }

            }
            evenodd++;

        }
        //pirntarre(selectioncolumn); ;
        // x range 0-8 
        //if center position + size
        return selectioncolumn;
    }

    void pirntarre(int[] arre)
    {
        //print("Printing array below ");
        foreach (var item in arre)
        {
            print(item);
        }
    }

}
