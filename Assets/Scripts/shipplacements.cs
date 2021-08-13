using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipplacements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        placeship data = new placeship();
        data.placeships(1,3,"010203");
        data.placeships(1,3,"011121");
        data.placeships(1,2,"5152");  
        data.placeships(1,3,"222324");
        data.placeships(1,3,"606162");
        data.placeships(1,2,"1415");

        data.printPlayersBoard(1);
    }

}


public class placeship: MonoBehaviour{
    private int[,] plr1board = {

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

        { 0,0,0,0,0,1,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0}
        };

    public placeship(){
        
    }

    public void placeships(int player, int shipsize, string RowColumns){
        if(isCollision(player, shipsize, RowColumns)){

             // RC   ==    515253

            print(RowColumns +" is taken. Pick somewhere else!");
        }
        else{
            print(RowColumns+" has been placed.");
            // places ships on coords 
            int j =0, k=1;  // j & k are x&y coords
            char []tempRC = RowColumns.ToCharArray();  // convert str="1234" to int=1234

            for (int i = 0; i <shipsize; i++){
                int tempJ = tempRC[j]-'0';
                int tempK = tempRC[k]-'0';
                if(player==1){
                    plr1board[tempJ,tempK] = 1;
                }
                else if(player==2){
                    plr2board[tempJ,tempK] = 1;
                }
                j = j+2;
                k = k+2;
            }
        }
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

    public void printPlayersBoard(int player){
        if (player==1){
            for(int i =0; i<8; i++){
                //print("\n");
                print("Row "+i+":"+plr1board[i,0]+" "+plr1board[i,1]+" "+plr1board[i,2]+" "+plr1board[i,3]+" "+plr1board[i,4]+" "+plr1board[i,5]+" "+plr1board[i,6]+" "+plr1board[i,7]);
            }
        }
    }

}