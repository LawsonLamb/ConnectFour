using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board
{
    public TileType[,] Grid;
    public int ROW;
    public int COL;
    public int Connect;
    public TileType currentTurn;

    public Board(int row, int col, int Connect)
    {
        ROW = row;
        COL = col;
        this.Connect = Connect;
        Grid = new TileType[ROW, COL];
    }

    public void FillBoardFromGB(Tile[,] Tiles)
    {

        for (int x = 0; x < ROW; x++)
        {

            for (int y = 0; y < COL; y++)
            {

                Grid[x, y] = Tiles[x, y].type;
            }


        }
    }

    public void FillBoard(ref TileType[,] grid)
    {
       
        for (int x = 0; x < ROW; x++)
        {

            for (int y = 0; y < COL; y++)
            {
                Grid[x, y] = grid[x, y];
            }

        }

      ;

    }

    public List<int> GenerateMoves(ref Board board)
    {

        List<int> nextMoves = new List<int>();
       // if (board.CheckforWin() <= Connect - 1)
        //{


            for (int x = 0; x < ROW; x++)
            {

                for (int y = 0; y < COL; y++)
                {
                    if (board.Grid[x, y] == TileType.Empty)
                    {
                        nextMoves.Add(x);
                    }

                }

            }
       // }

        return nextMoves;

    }


    public void PlaceTile(int row, TileType type)
    {

        for (int col = 0; col < COL; col++)
        {
          
            if (Grid[row, col] == TileType.Empty)
            {
               
                Grid[row, col] = type;
                currentTurn = type;
            
                //    checkGridforTile();
                break;

            }

        }






    }


    #region CheckForWin
    public int CheckforWin()
    {
        return EvaluatueWin(currentTurn);

    }

     int EvaluatueWin(TileType currentType)
    {
        int score = 0;


        for (int x = 0; x < Grid.GetLength(0); x++)
        {

            for (int y = 0; y < Grid.GetLength(1); y++)
            {

                score += CheckRow(currentType, x, y);

                score += CheckCol(currentType, x, y);

                score += CheckDiagonalUP(currentType, x, y);

                score += CheckDiagonalDown(currentType, x, y);




            }

        }

        return score;


    }

     private int CheckRow(TileType currentType, int x, int y)
    {
        int score = 0;

       
        if (x <= Grid.GetLength(0) - Connect)
        {
            for (int i = 0; i < Connect; i++)
            {

                if (currentType == Grid[x + i, y])
                {
                    score++;


                    if (score >= Connect)
                    {
                      //  Debug.Log("WIN " + "Score:" + score);
                        return score;
                    }
                }

                else {
                    score = 0;

                    break;

                }
            }
        }
        else
        {
            score = 0;
            return score;

        }




        return score;
    }

    private int CheckCol(TileType currentType, int x, int y)
    {
        int score = 0;

        if (y <= Grid.GetLength(1) - Connect)
        {
            // print("Row :  " + y+ " Score:" + score);
            for (int i = 0; i < Connect; i++)
            {

                if (currentType == Grid[x, y + i])
                {


                    score++;
                    if (score >= Connect)
                    {

                        return score;
                    }

                }
                else
                {

                    score = 0;
                    break;

                }

            }
        }






        return score;






    }

    private int CheckDiagonalUP(TileType currentType, int x, int y)
    {
        int score = 0;

        if (x < Grid.GetLength(0) - Connect && y < Connect - 1)
        {

            for (int i = 0; i < Connect; i++)
            {

                if (currentType == Grid[x + i, y + i])
                {
                    score++;
                    if (score >= Connect)
                    {
                        return score;
                    }

                }
                else
                {
                    break;
                }
            }

        }

        return score;
    }

    private  int CheckDiagonalDown(TileType currentType, int x, int y)
    {
        int score = 0;

        if (x <= Connect - 1 && y <= Connect - 1)
        {

            for (int i = 0; i < Connect; i++)
            {
                if (x - i >= 0 && y - i >= 0)
                {

                    if (currentType == Grid[x - i, y - i])
                    {
                        score++;
                        if (score >= Connect)
                        {
                            return score;
                        }
                        //  Grid[x + i, y + i].ContainerImage = Color.green;
                    }
                    else
                    {
                        score = 0;
                        break;
                        //   break;
                    }
                }
                else
                {
                    score = 0;
                    break;
                }
            }

        }
        else
        {
            score = 0;

        }

        return score;
    }

    #endregion


    #region Check State
    public int CheckBoardState()
    {
        int score = 0;


        for (int x = 0; x < Grid.GetLength(0); x++)
        {

            for (int y = 0; y < Grid.GetLength(1); y++)
            {

                score += stateVertical(currentTurn, x, y);

                score += stateHorizontal(currentTurn, x, y);

                score += stateDiagonalUP(currentTurn, x, y);

                score += stateDiagonalDown(currentTurn, x, y);



            }

        }

        return score;

      
    }

    public void UndoMoves(int row, ref TileType[,] grid)
    {
        for (int col = 0; col < COL; col++)
        {
            if (grid[row, col] != TileType.Empty)
            {
                grid[row, col] = TileType.Empty;
                break;
            }
        }


    }

    private int stateVertical(TileType currentType,int x, int y)
    {
        int score = 0;




        if (y <= Grid.GetLength(1) - Connect)
        {
            // print("Row :  " + y+ " Score:" + score);
            for (int i = 0; i < Connect; i++)
            {

                if (currentType == Grid[x, y + i])
                {


                    score++;
                    if (score >= Connect)
                    {

                        return score;
                    }

                }
                else if(Grid[x, y + i] == TileType.Empty)
                {

                    score = 0;
                    //break;

                }

                else
                {
                    score--;
                }

            }
        }





        return score;

    }
   
    private int stateHorizontal(TileType currentType,int x,int y)
    {
        int score = 0;

     
        if (x <= Grid.GetLength(0) - Connect)
        {
            for (int i = 0; i < Connect; i++)
            {

                if (currentType == Grid[x + i, y])
                {
                    score++;
                

                }

                else if(Grid[x + i, y] == TileType.Empty) {

                   score = 0;
                  //  break;

                }

                else
                {
                    score--;
                }


            }
        }
        else
        {
            score = 0;
            return score;

        }


        return score;

    }

    private int stateDiagonalUP(TileType currentType,int x,int y)
    {
        int score = 0;

        if (x < Grid.GetLength(0) - Connect  && y < Connect - 1)
        {
                 //Debug.Log(x+ " , " + y);
            
            for (int i = 0; i < Connect; i++)
            { int temp = x + i;
                int temp2 = y + 1;
               
                if (currentType == Grid[x + i, y + i])
                {
                    score++;
                 
                }
                else if(Grid[x + i, y + i ]== TileType.Empty)
                {
                    score = 0;
                }
                else
                {
                    score--;
                }
            }

        }
       

        return score;
    }

    private int stateDiagonalDown(TileType currentType, int x, int y)
    {
        int score = 0;

        if (x <= Connect - 1 && y <= Connect - 1)
        {

            for (int i = 0; i < Connect; i++)
            {
                if (x - i >= 0 && y - i >= 0)
                {

                    if (currentType == Grid[x - i, y - i])
                    {
                        score++;
                   
                    }
                    else if(Grid[x - i, y - i] == TileType.Empty)
                    {
                        score = 0;
                     //   break;
                        //   break;
                    }
                    else
                    {
                        score--;
                    }
                }
                else
                {
                    score = 0;
                    break;
                }
            }

        }
        else
        {
            score = 0;

        }

        return score;
    }

    #endregion
}

