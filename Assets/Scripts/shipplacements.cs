using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* Hard coded testing:

        placeship data = new placeship();
        bool istherecollision = data.placeships(1,3,"010203");
        data.placeships(1,3,"011121");
        data.placeships(1,2,"5152");  
        data.placeships(1,3,"222324");
        data.placeships(1,3,"606162");
        data.placeships(1,2,"1415");

        data.printPlayersBoard(1); */
    }

}


public class placeship : MonoBehaviour {

    public static placeship Instance { set; get; }

    public string[] shipsplaced = { "", "", "", "", "" };
    private int shiplacedcount = 0;
    public static int[,] plr1board = {

        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0}
        };
    private int[,] plr2board = {

        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0}
        };
    public void placethisship(string ship)
    {
        shipsallreadyplaced();
        shipsplaced[shiplacedcount] = ship;
        shiplacedcount++;

    }
    public bool arealltheshipsin()
    { bool allin = true;

        int count = 0;

        while (count < shipsplaced.Length)
        {

            if (shipsplaced[count] == "")
            {

                allin = false;


            }
            count++;
        }

        return allin;
    }
    public bool isthatshipinallready(string ship) // should print falso if there is no ship allready in there by that name
    {
        bool isshipin = false;
        int count = 0;

        foreach (string i in shipsplaced)
        {
            if (ship == shipsplaced[count])
                isshipin = true;

            count++;
        }


        return isshipin;


    }
    public bool placeships(int player, int shipsize, string RowColumns) {
        
        int tempJ = 0;
        int tempK = 0;

        if (isCollision(player, shipsize, RowColumns)) {

            // RC   ==    515253

            print(RowColumns + " is taken. Pick somewhere else!");
            return false;
        }
        else {
            print(RowColumns + " has been placed.");
            // places ships on coords 
            int j = 0, k = 1;  // j & k are x&y coords
            char[] tempRC = RowColumns.ToCharArray();  // convert str="1234" to int=1234

            for (int i = 0; i < shipsize; i++) {
                tempJ = tempRC[j] - '0';
                tempK = tempRC[k] - '0';
                if (NetActions.currentTeam == 0) {
                    plr1board[tempJ, tempK] = 1;
                }
                else if (NetActions.currentTeam == 1) {
                    plr2board[tempJ, tempK] = 1;
                }
                //beginsendinglocalboard(player);
                j = j + 2;
                k = k + 2;
            }
            return true;
        }
    }
    //host board = p1 ,client board = p2 , 
    public void receivedShipNowPlace(int xcoord, int ycoord, int shipNum, int orientation) //if orientation ==1 its verticle else its horizontal
    {
        bool verticlee = false;
        if (orientation == 1)
            verticlee = true; 
        
        bool host = false ;
        int [] rowcolumn = { ycoord, xcoord };
        if (NetActions.currentTeam == 0)
        {
           // print("Congrats you are the host");
            host = true;
        }
        else if (NetActions.currentTeam == 1)
        {
           // print("Congrats you are the client");
            host = false;
        }
        int shipsize = getshipsize(shipNum);

        if(host == true)
            writetoP2board(shipsize, rowcolumn, verticlee);
        else if(host == false)
            writetoP1board(shipsize, rowcolumn, verticlee);


       // int[] rowcolumn = extractcoordinatename(gridColor);
        

        colorotherguysships(host);
      //  print("you recieved a new ship ------------------------------------------------------------------------------");
       // printPlayersBoard(1);
       // print("player 1 board is above------------------- player2 board is below -------------------------------");
      //  printPlayersBoard(2);


        Debug.Log($"Received from ShipPlacement class: ({xcoord},{ycoord}), ship {shipNum} and orientation {orientation}");
        



    }

    public void colorotherguysships(bool host)
    {
        if (host)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    if (plr2board[i, q] == 1)
                        GameObject.Find("Player2BoardParent/X:" + i + ", Y" + q).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);


                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    if (plr1board[i, q] == 1)
                        GameObject.Find("Player1BoardParent/X:" + i + ", Y" + q).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);


                }
            }




        }
    
    }
    
    public void writetoP1board(int shipsize, int [] rowcolumn,bool verticlee)
    {
        if (verticlee)
        {
            int[] selection = gettilestoptotop(rowcolumn[0], rowcolumn[1], shipsize);

            if (shipsize == 2)
            {
                plr1board[selection[0], rowcolumn[1]] = 1;
                plr1board[selection[1], rowcolumn[1]] = 1;
                /* GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1); */

             }
             if (shipsize == 3)
             {
                 plr1board[selection[0], rowcolumn[1]] = 1;
                 plr1board[selection[1], rowcolumn[1]] = 1;
                 plr1board[selection[2], rowcolumn[1]] = 1;
                /*   GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                   GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                   GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1); */
            }
            if (shipsize == 4)
               {
                   plr1board[selection[0], rowcolumn[1]] = 1;
                   plr1board[selection[1], rowcolumn[1]] = 1;
                   plr1board[selection[2], rowcolumn[1]] = 1;
                   plr1board[selection[3], rowcolumn[1]] = 1;
                /*   GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 5)
              {
                  plr1board[selection[0], rowcolumn[1]] = 1;
                  plr1board[selection[1], rowcolumn[1]] = 1;
                  plr1board[selection[2], rowcolumn[1]] = 1;
                  plr1board[selection[3], rowcolumn[1]] = 1;
                  plr1board[selection[4], rowcolumn[1]] = 1;
                /*   GameObject.Find("Player1BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + selection[4] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }


        }
          else
          {

              int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);

              if (shipsize == 2)
              {
                  plr1board[rowcolumn[0], selection[0]] = 1;
                  plr1board[rowcolumn[0], selection[1]] = 1;
                  /*  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                   GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 3)
             {
                 plr1board[rowcolumn[0], selection[0]] = 1;
                 plr1board[rowcolumn[0], selection[1]] = 1;
                 plr1board[rowcolumn[0], selection[2]] = 1;
                 /* GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1); */
            }
            if (shipsize == 4)
             {
                 plr1board[rowcolumn[0], selection[0]] = 1;
                 plr1board[rowcolumn[0], selection[1]] = 1;
                 plr1board[rowcolumn[0], selection[2]] = 1;
                 plr1board[rowcolumn[0], selection[3]] = 1;
                 /* GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 5)
             {
                 plr1board[rowcolumn[0], selection[0]] = 1;
                 plr1board[rowcolumn[0], selection[1]] = 1;
                 plr1board[rowcolumn[0], selection[2]] = 1;
                 plr1board[rowcolumn[0], selection[3]] = 1;
                 plr1board[rowcolumn[0], selection[4]] = 1;

                 /*
                 GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player1BoardParent/X:" + rowcolumn[0] + ", Y" + selection[4]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1); */
            }


        }

    }


    public void writetoP2board(int shipsize , int[] rowcolumn, bool verticlee)
    {
        if (verticlee)
        {
            int[] selection = gettilestoptotop(rowcolumn[0], rowcolumn[1], shipsize);

            if (shipsize == 2)
            {
                plr2board[selection[0], rowcolumn[1]] = 1;
                plr2board[selection[1], rowcolumn[1]] = 1;


               /* GameObject.Find("Player2BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                GameObject.Find("Player2BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/

            }
            if (shipsize == 3)
            {
                plr2board[selection[0], rowcolumn[1]] = 1;
                plr2board[selection[1], rowcolumn[1]] = 1;
                plr2board[selection[2], rowcolumn[1]] = 1;
                /*GameObject.Find("Player2BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                GameObject.Find("Player2BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                GameObject.Find("Player2BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 4)
            {
                plr2board[selection[0], rowcolumn[1]] = 1;
                plr2board[selection[1], rowcolumn[1]] = 1;
                plr2board[selection[2], rowcolumn[1]] = 1;
                plr2board[selection[3], rowcolumn[1]] = 1;
                /* GameObject.Find("Player2BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 5)
            {
                plr2board[selection[0], rowcolumn[1]] = 1;
                plr2board[selection[1], rowcolumn[1]] = 1;
                plr2board[selection[2], rowcolumn[1]] = 1;
                plr2board[selection[3], rowcolumn[1]] = 1;
                plr2board[selection[4], rowcolumn[1]] = 1;
                /*  GameObject.Find("Player2BoardParent/X:" + selection[0] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[1] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[2] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[3] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + selection[4] + ", Y" + rowcolumn[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }


        }
        else
        {

            int[] selection = gettilesnextto(rowcolumn[0], rowcolumn[1], shipsize);

            if (shipsize == 2)
            {
                plr2board[rowcolumn[0], selection[0]] = 1;
                plr2board[rowcolumn[0], selection[1]] = 1;


                /* GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 3)
            {
                plr2board[rowcolumn[0], selection[0]] = 1;
                plr2board[rowcolumn[0], selection[1]] = 1;
                plr2board[rowcolumn[0], selection[2]] = 1;
                /*  GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 4)
            {
                plr2board[rowcolumn[0], selection[0]] = 1;
                plr2board[rowcolumn[0], selection[1]] = 1;
                plr2board[rowcolumn[0], selection[2]] = 1;
                plr2board[rowcolumn[0], selection[3]] = 1;
                /*  GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                  GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }
            if (shipsize == 5)
            {
                plr2board[rowcolumn[0], selection[0]] = 1;
                plr2board[rowcolumn[0], selection[1]] = 1;
                plr2board[rowcolumn[0], selection[2]] = 1;
                plr2board[rowcolumn[0], selection[3]] = 1;
                plr2board[rowcolumn[0], selection[4]] = 1;
                /* GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[0]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[1]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[2]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[3]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                 GameObject.Find("Player2BoardParent/X:" + rowcolumn[0] + ", Y" + selection[4]).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);*/
            }


        }

    }

    public int getshipsize(int  shipnum)
    {

        if (shipnum == 1 )
        {
            return 2; 
        }
        else if (shipnum == 2)
        {
            return 3; 
            
            
        }
        else if (shipnum == 3)
        {
            return 3; 
        }
        else if (shipnum == 4 )
        {
            return 4; 
        }
        else  
        {
            return 5; 
        }

       
    }



   private  int[] gettilestoptotop(int clickpositionrow, int clickpositioncolumn, int shipsize)
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



    private bool isCollision(int player, int shipsize, string RowColumns){
        int num = 0; // stores coord into array
        int j =0, k =1;
        char []tempRC = RowColumns.ToCharArray();  // convert str="1234" to int=1234
        for (int i = 0; i<shipsize; i++){
            
            int tempJ = tempRC[j]-'0';
            int tempK = tempRC[k]-'0';
            //print(tempJ);
            if (player==1){
                if(plr1board[tempJ,tempK] == 1){
                    num++;
                }
            }
            else{
                if(plr2board[tempJ,tempK] == 1){
                    num++;
                }
            }
            j = j+2;
            k = k+2;
        }
        if(num == 0){
            return false;
        }
        return true;
    }

    public void shipsallreadyplaced()
    {
        print("ships in so far are " + shipsplaced[0] + " " + shipsplaced[1] + " " + shipsplaced[2] + " " + " " + shipsplaced[3] + " " + " " + shipsplaced[4] + " ");
    }

    public void printPlayersBoard(int player){
        if (player==1){
            for(int i =0; i<8; i++){
                //print("\n");
                print("Row "+i+":"+plr1board[i,0]+" "+plr1board[i,1]+" "+plr1board[i,2]+" "+plr1board[i,3]+" "+plr1board[i,4]+" "+plr1board[i,5]+" "+plr1board[i,6]+" "+plr1board[i,7]);
            }
        }
    }
    public void prinarre(int [] arre)
{
        int count = 0;
        while (count > arre.Length)
        {
            print("important arre selection " + arre[count]);
        }
        
        
    }

}