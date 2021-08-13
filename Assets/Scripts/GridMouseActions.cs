using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouseActions : MonoBehaviour
{
    public SpriteRenderer gridColor;
    public bool vertical = true;
    public static int shipsize = 5;
    public static GameObject selectedship;
    //change this for diffrent ship sizes

    private void Awake()
    {
       // gridColor = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        selectedship = GameObject.Find("Aircraft_Carrier");
        gridColor = GetComponent<SpriteRenderer>();
    }

    void Update()
    {                     // i tested this function inside of onmouseover but it did not work as intended, only for the 1 tile
        mouserightclick();// we need this to detect input from the mouse but, the memory leak is a result of since it is constantly checking for detection. -- works fine here for now but, if we can find a solution later it would be amazing
    }
    
    // name formatting exampple "X:0, Y0"
    void OnMouseEnter()
    {

       
       // GameObject.Find("Player1BoardParent/X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
        //print("The current tile should now be hovered.");

        if (!isthisaship())
        {
            //print("The current tile should now be hovered.");
            if (this.vertical == false)
            {
                print("we are now doing horizontal only");
                marksides();
            }
            else
            {
                print("we are now doing vertical only");
                marktoptotop();
            }
        }
        
    }
   
    void OnMouseDown() 
    {
        gridColor.color = new Color(0.25f, 0.25f, 0.25f, 1);
        print("The current tile is being clicked " + gridColor.name);
    }

    void OnMouseUp()
    {
        gridColor.color = new Color(0.5f, 0.5f, 0.5f, 1);
       // print("The current tile is no longer being clicked.");
    }  

    void OnMouseExit()
    {
        gridColor.color = new Color(1, 1, 1, 1);
        // print("The last tile hovered should no longer be hovered.");

        if (!isthisaship())
        {
            if (this.vertical == false)
            {
                print("we are now doing horizontal only");
                demarksides();
            }
            else
            {
                print("we are now doing vertical only");
                demarktoptotop();
            }
            //print(tempGridScript.tempGrid[0,0].name);
        }
        else
        { 
        }

    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 
   
    private bool isthisaship()
    {
        if (gridColor.name == "Aircraft_Carrier")
        {
            return true;
        }
        else if (gridColor.name == "Battleship")
        {
            return true;
        }
        else if (gridColor.name == "Cruiser")
        {
            return true;
        }
        else if (gridColor.name == "Submarine")
        {
            return true;
        }
        else if (gridColor.name == "Destroyer")
        {
            return true;
        }
        else
            return false; 
    }
    
    private void mouserightclick()
    { 
        if (Input.GetMouseButtonDown(1))// 0 is left, 1 is right, 3 is middle
        {
            print("we are trying to detect mouseinput right now");
            if (this.vertical)
            {   
                turnship90degrees(vertical);  
                vertical = false;
                demarktoptotop();
                print("You just right clicked, we are horizontal now ");
                
            }
            else
            {
                turnship90degrees(vertical);
                vertical = true;
                demarksides();
                
                print("You just right clicked, we are vertical now ");
            }   
        }
    }

    private void turnship90degrees(bool verticle)
    {
        
        if(vertical)
            selectedship.transform.rotation = Quaternion.Euler(0,0,  90);
        else
            selectedship.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    private void marksides()
    {
        // GameObject.Find("X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        print("we are marking sides and ship size is " + shipsize);
        int[] rowcolumn = extractcoordinatename(gridColor);
        int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);
        if (shipsize == 2)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        if (shipsize == 3)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        if (shipsize == 4)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        if (shipsize == 5)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[4]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    void demarksides()
    {
        // GameObject.Find("X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        int[] rowcolumn = extractcoordinatename(gridColor);
        int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);
        if (shipsize == 2)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 3)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 4)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 5)
        {
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[4]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    private void marktoptotop()
    {
        int[] rowcolumn = extractcoordinatename(gridColor);
        int[] selection = gettilestoptotop(rowcolumn[0], rowcolumn[1], shipsize);

        if (shipsize == 2)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            
        }
        if (shipsize == 3)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        if (shipsize == 4)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        if (shipsize == 5)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[4] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    private void demarktoptotop()
    {
        // GameObject.Find("X:0, Y0").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        int[] rowcolumn = extractcoordinatename(gridColor);
        int[] selection = gettilestoptotop(rowcolumn[0], rowcolumn[1], shipsize);
        if (shipsize == 2)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 3)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 4)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (shipsize == 5)
        {
            GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Player1BoardParent/X:" + selection[4] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    // the extractcoordinatename function will take in a spriterenderer and extract numeracle values from the name for x and y.
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

    // the function top to top is for rows
    int[] gettilestoptotop(int clickpositionrow, int clickpositioncolumn, int shipsize)
    {
        // in the case below clickpositionrow must stay the same 
        int[] selectionrow = { -1000, -1000, -1000, -1000, -1000, -1000, };
        int count = 0; // only horizontal atm
        int evenodd = 0;
        selectionrow[0] = clickpositionrow;
        count++;
        int positiontrackright = clickpositionrow;
        int positiontrackleft = clickpositionrow;

        while (count < shipsize)
        {
            if (evenodd % 2 == 0)
            {
                positiontrackright++;
                if (positiontrackright < 8)
                {
                    selectionrow[count] = positiontrackright;
                    count++;
                }
            }
            else
            {
                positiontrackleft--;
                if (positiontrackleft >= 0)
                {
                    selectionrow[count] = positiontrackleft;
                    count++;
                }
            }
            evenodd++;
        }
        //pirntarre(selectioncolumn); ;
        // x range 0-8 
        //if center position + size
        return selectionrow;
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
