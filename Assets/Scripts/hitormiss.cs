using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class hitormiss : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hit, miss;
    public Color invis;
    public Color vis; 
    void Start()
    { 
        ///everything below is done to test the hitormiss class



        print("naruto is amazing" +1);

        int[,] plr1board = {

        { 0,0,0,0,1,1,1,1},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,1,1,1,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0}
        };

        int[,] plr2board = {

        { 0,2,2,2,2,2,2,2},
        { 0,0,0,2,2,0,2,2},
        { 2,1,0,0,0,0,2,2},
        { 2,1,2,0,0,0,2,2},
        { 2,1,2,0,0,0,2,2},
        { 2,2,2,0,0,0,2,2},
        { 2,2,2,0,0,0,2,2},
        { 2,2,2,0,0,0,2,2}
        };

        hitherormiss data = new hitherormiss(plr1board, plr2board , hit, miss, vis, invis);

        data.hitplr(1, 0, 5);
        data.hitplr(2, 0, 0);
        data.hitplr(2, 1, 0);
        data.hitplr(2, 1, 1);
        data.hitplr(2, 1, 1);
        data.hitplr(2, 2, 1);
        data.hitplr(2, 3, 1);
        data.hitplr(2, 4, 1);

        int isthereawinner = data.detectwinner(); 



        //everything above is done to test the hitormiss class
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
}

//this class needs to be initiated with player1 and 2s boards at start
// transform whatever array you want to import into a 2d int array for with the format below
// 0 means water
// 1 means there is a ship there
// -1 means there is a ship that got hit there
//  2 means a shot got shot there and was nothing there
//       2 0 0 0
/// exe  0 1 1 0
///      2 0 0 0  
//in this example, there is a ship in the middle of the map and there were misses that happen on the upper and lower left

public class hitherormiss : MonoBehaviour 
{
    private Color invis ,vis; 
    private Text hit, miss;


    //doublearray with map and ship positios for player 1
    private int[,] plr1arre = new int[8, 8];

    //doublearray with map and ship positios for player 2
    private int[,] plr2arre = new int[8, 8];

    public hitherormiss(int[,] plr1arree, int[,] plr2arree, Text hit, Text miss, Color vis , Color invis)
    {
        this.vis = vis;
        this.invis = invis; 
        this.hit = hit;
        this.miss = miss; 
        copy2dforplrarray(plr1arree, 1);
        copy2dforplrarray(plr2arree, 2);
        gridmapforplr(1);
        gridmapforplr(2);
    }

    private void gridmapforplr(int plrnumber)
    {
        if (plrnumber == 1)
        {
            print(" time to see what player1s grid looks like so far");
            for (int i = 0; i < 8; i++)
            {
                print(this.plr1arre[i, 0] + " " + this.plr1arre[i, 1] + " " + this.plr1arre[i, 2] + " " + this.plr1arre[i, 3] + " " + this.plr1arre[i, 4] + " " + this.plr1arre[i, 5] + " " + this.plr1arre[i, 6] + " " + this.plr1arre[i, 7]);


            }


        }
        else {
            print(" time to see what player2s grid looks like so far");
            for (int i = 0; i < 8; i++)
            {
                print(this.plr2arre[i, 0] + " " + this.plr2arre[i, 1] + " " + this.plr2arre[i, 2] + " " + this.plr2arre[i, 3] + " " + this.plr2arre[i, 4] + " " + this.plr2arre[i, 5] + " " + this.plr2arre[i, 6] + " " + this.plr2arre[i, 7]);


            }

        }
    }


     private void copy2dforplrarray(int[,] arretocopy, int plr)
    {

        if (plr == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++) {

                    this.plr1arre[i, q] = arretocopy[i, q] ;

                    
                
                
                }
            }



        }
        else
        {

            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {

                    this.plr2arre[i, q] = arretocopy[i, q];




                }
            }

        }

    }

    public void hitplr(int plrnumberwearehiting, int row, int column)
    {

        if (plrnumberwearehiting == 1)
        {
            if (this.plr1arre[row, column] == 1)
            {
                this.plr1arre[row, column] = -1;
                // hit.color = vis;
                print("There was a hit at row" + column + " and at column " + row);
                print("the new board now for player 1 is ");
                gridmapforplr(plrnumberwearehiting);



            }
            /// above is if we hit a ship 
            /// 

            if (this.plr1arre[row, column] == 0)
            {
                this.plr1arre[row, column] = 2;
                // hit.color = vis;
                print("There was a miss at row" + column + " and at column " + row);
                print("the new board now for player 1 is ");
                gridmapforplr(plrnumberwearehiting);

            }
            /// above is if we hit empty water

        }
        else if (plrnumberwearehiting==2)
        {
            if (this.plr2arre[row, column] == 1)
            {
                this.plr2arre[row, column] = -1;
                // hit.color = vis;
                print("There was a hit at y" + column + " and at x " + row);
                print("the new board now for player 2 is ");
                gridmapforplr(plrnumberwearehiting);
            }
            /// above is if we hit a ship 
            /// 

            if (this.plr2arre[row, column] == 0)
            {
                this.plr2arre[row, column] = 2;
                // hit.color = vis;
                print("There was a miss at row" + column + " and at column " + row);
                print("the new board now for player 2 is ");
                gridmapforplr(plrnumberwearehiting);

            }
            /// above is if we hit empty water
        }



    }

    // if you want information on a spcific tile for a specific player
    public int gettileinfo(int plrinfowewant, int tilerow, int tilecolumn)
    {
        if (plrinfowewant == 1)
        {
            return plr1arre[tilerow, tilecolumn];


        }
        else 
        { 
            return plr2arre[tilerow, tilecolumn];
        }

        
    }


    // if this function returns 0, no wins were detected. If it returns 1, player1 won, if it returns 2- player2 has won
    public int detectwinner()
    {
        bool plr1winfound = true;
        bool plr2winfound = true;

        for (int i = 0; i < 8; i++)
        {
            for (int p = 0; p < 8; p++)
            {
                if (plr1arre[i, p] == 1)
                    plr2winfound = false;

                if (plr2arre[i, p] == 1)
                    plr1winfound = false;

            }
        
        }


        if (plr1winfound || plr2winfound)
        {
            print("A win was detected");

            if (plr1winfound)
            {
                print("plr 1 has won returning 1");
                return 1;
            }
            else
            {
                print("plr 2 has won returning 1");
                return 2;
            }
        }
        else
        {
            print("no one has won yet");
            return 0;
        }


    }

}