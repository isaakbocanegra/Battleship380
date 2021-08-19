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

    public void receivedShipNowPlace(int xcoord, int ycoord, int shipNum, int orientation)
    {
        Debug.Log($"Received from ShipPlacement class: ({xcoord},{ycoord}), ship {shipNum} and orientation {orientation}");
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

}