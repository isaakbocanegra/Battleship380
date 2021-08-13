using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipplacements : MonoBehaviour{
    void Start(){
        placeship data = new placeship();
        data.placeships(1,3,"010203");
        data.placeships(1,3,"011121");
        data.placeships(1,2,"5152");

        data.printPlayersBoard(1);
    }
}


public class placeship : MonoBehaviour{

    public int[,] plr1board = {  { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0} };
    public int[,] plr2board = {  { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0} };

    public placeship(){
      /*  plr1board= {{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0} };

        plr2board = {  { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0} };*/
        
    }

    public void placeships(int player, int shipsize, string RowColumn){
        if(isCollision(player, shipsize, RowColumn)){
            print(RowColumn +" is taken. Pick somewhere else!");
        }
        else{
            // places ships on coords 
            for (int i = 0; i <shipsize; i++){
                int j =0, k=1;  // j & k are x&y coords
                if(player==1){
                    plr1board[j,k] = 1;
                }
                else if(player==2){
                    plr2board[j,k] = 1;
                }
                j = j+2;
                k = k+2;
            }
        }
    }

    // passing coords as string array "01"  "02", "03"
    private bool isCollision(int player, int shipsize, string RowColumns){
        int num = 0; // stores coord into array
        for (int i = 0; i<shipsize; i++){
            int j =0, k =1;
            if (player==1){
                if(plr1board[j,k] == 1){
                    num++;
                }
            }
            else{
                if(plr2board[j,k] == 1){
                    num++;
                }
            }
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
                print(plr1board[i,0]+" "+plr1board[i,1]+" "+plr1board[i,2]+" "+plr1board[i,3]+" "+plr1board[i,4]+" "+plr1board[i,5]+" "+plr1board[i,6]+" "+plr1board[i,7]);
            }
        }
    }
}