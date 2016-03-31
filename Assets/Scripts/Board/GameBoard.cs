using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public class GameBoard : MonoBehaviour {

    public int ROW = 7;
    public int COL = 6;
    
    public int Connect = 4;
    public int Depth = 3;
    public GameObject TilePrefab;
    public Tile[,] Grid;
    public Board board;
    public int Space = 2;
    public int RedScore = 0;
    public int BlackScore = 0;
    public List<Tile> PlacedTiled;
    Node root;

    #region monobehaviors
    // Use this for initialization
    void Start()
    {
        PlacedTiled = new List<Tile>();
        board = new Board(ROW, COL, Connect);
        BuildBoard();


    }

    // Update is called once per frame
    void Update()
    {



    }

    #endregion



    #region Board Building
    [ContextMenu("BuildBoard")]
    public void BuildBoard()
    {
        destoryChildren();
        PlacedTiled.Clear();
        Grid = new Tile[ROW, COL];
        SpriteRenderer renderer = TilePrefab.GetComponent<SpriteRenderer>();
        Vector2 TileSize = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
        for (int x = 0; x< ROW; x++)
        {

            for (int y = 0; y < COL; y++)
            {

                GameObject go = Instantiate(TilePrefab, new Vector3((x * TileSize.x) * Space, (y * TileSize.y) * Space, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(this.transform);
                go.transform.name = "Tile " + x + "," + y;
                go.GetComponent<Tile>().Row = x;
                go.GetComponent<Tile>().Column = y;

                Grid[x, y] = go.GetComponent<Tile>();
                
            }


        }







    }

    void destoryChildren()
    {
        int count = this.transform.childCount;
        if (count == 0)
        {

        }
        else {
            while (this.transform.childCount > 0)
            {
                GameObject.DestroyImmediate(this.transform.GetChild(0).gameObject);
            }

            //		GameObject.DestroyImmediate(this.transform.GetChild(0));
        }
    }

    public void setBoard()
    {
        board.FillBoardFromGB(Grid);
    }
    #endregion



    #region Board Checking


    void checkGridforTile()
    {
      //  print("Check grid");

        if (PlacedTiled.Count >= 0)
        {
           // print(PlacedTiled.Count);
            for (int i = 0; i < PlacedTiled.Count; i++)
            {
                //print(PlacedTiled[i].GetComponent<Tile>().Row + "," + PlacedTiled[i].GetComponent<Tile>().Column);
                TileType _type = CheckBoard(PlacedTiled[i].Row, PlacedTiled[i].Column);
                if (_type != TileType.Empty)
                {
                    setScore(_type);

                    BuildBoard();

                    break;


                }
                else {
                    continue;
                }

            }
        }


    }


    TileType CheckBoard(int x, int y)
    {

        List<Tile> Horizontal = CheckHorizontal(x, y);
        List<Tile> Vertical = CheckVertical(x, y);
        SetBackgroundToColor(Horizontal, Color.green);
        SetBackgroundToColor(Vertical, Color.green);

        if (Horizontal.Count > 0)
            return Horizontal[0].type;
        else if (Vertical.Count > 0)
            return Vertical[0].type;




        else {

            return TileType.Empty;
        }




    }

    List<Tile> CheckHorizontal(int x, int y)
    {
        List<Tile> matches = new List<Tile>();
        matches.Add(Grid[x, y]);
        //Check Left
        for (int row = x - 1; row >= 0; row--)
        {



            if (Grid[x, y].GetComponent<Tile>().type == Grid[row, y].GetComponent<Tile>().type)
            {

                matches.Add(Grid[row, y]);

            }

            else {
                break;
            }
        }
        for (int row = x + 1; row < ROW; row++)
        {


            if (Grid[x, y].GetComponent<Tile>().type == Grid[row, y].GetComponent<Tile>().type)
            {
                matches.Add(Grid[row, y]);

            }

            else {

                break;
            }

        }
        if (matches.Count < Connect)
        {
            SetBackgroundToColor(matches, Color.white);
            matches.Clear();

        }




        return matches;



    }

    List<Tile> CheckVertical(int x, int y)
    {

        List<Tile> matches = new List<Tile>();
        matches.Add(Grid[x, y]);

        //Check Down
        for (int col = y - 1; col >= 0; col--)
        {

            if (Grid[x, y].GetComponent<Tile>().type == Grid[x, col].GetComponent<Tile>().type)
            {

                print("Match: " + x + "," + col);
                matches.Add(Grid[x, col]);


            }
            else {

                break;

            }


        }
        //Check UP
        for (int col = y + 1; col < COL; col++)
        {

            if (Grid[x, y].GetComponent<Tile>().type == Grid[x, col].GetComponent<Tile>().type)
            {


                print("Match: " + x + "," + col);
                matches.Add(Grid[x, col]);

            }
            else {




                break;

            }
        }

        if (matches.Count < Connect)
        {
            SetBackgroundToColor(matches, Color.white);

            matches.Clear();

        }



        return matches;


    }

    void CheckSideWays(int x, int y)
    {

        Grid[x, y].GetComponent<Tile>().ContainerImage = Color.yellow;
        //CheckUPleft
        print("Blue");
        for (int row = x + 1, col = y + 1; row < ROW && col < COL; row++, col++)
        {


            Grid[row, col].GetComponent<Tile>().ContainerImage = Color.blue;

            print(row + "," + col);
        }

        //CheckdownRight

        print("Red");
        for (int row = x - 1, col = y - 1; (row >= 0 && col >= 0); row--, col--)
        {

            print(row + "," + col);
            Grid[row, col].GetComponent<Tile>().ContainerImage = Color.red;

        }


        print("Black");
        //check UpRight
        for (int row = x + 1, col = y - 1; row <= ROW - 1 && col >= 0; row++, col--)
        {


            Grid[row, col].GetComponent<Tile>().ContainerImage = Color.black;
            print(row + "," + col);
        }


        print("Green");
        //check DownLeft
        for (int row = x - 1, col = y + 1; row >= 0 && col < COL; row--, col++)
        {



            Grid[row, col].GetComponent<Tile>().ContainerImage = Color.green;
            print(row + "," + col);
        }



    }


    #endregion







    void SetBackgroundToColor(List<Tile> matches, Color color)
    {

        foreach (Tile go in matches)
        {
           go.ContainerImage = color;

        }


    }

    void setScore(TileType type)
    {

        switch (type)
        {

            case TileType.Black:
                BlackScore += 1;
                break;
            case TileType.Red:
                RedScore += 1;
                break;


        }

    }


   public void EvaluateBoard(TileType currentType)
    {
        board.currentTurn = currentType;
        setBoard();
     //   print(board.CheckforWin());
       print(board.CheckBoardState());


        //print(Evaluatue(currentType));




    }

    public int[] Move(TileType Move)
    {
        setBoard();
        board.currentTurn =Move;
        root = new Node(ROW, COL,Connect);
        root.board.FillBoard(ref board.Grid);
        // int[] data = MiniMax(Depth, Move, root);
    
        
        int[] data = MiniMaxAB(Depth, Move, root, int.MinValue, int.MaxValue);
        print(string.Format("Score {0} at {1}", data[0], data[1]));
        return data;
    }

    public int[] MiniMax(int depth, TileType CurrentType, Node root)
    {
        int bestScore = (TileType.Black == CurrentType) ? int.MinValue + 1 : int.MaxValue - 1;
        List<int> nextMoves = root.board.GenerateMoves(ref root.board);

        int currentScore;
        int bestRow = -1;

        if (!nextMoves.Any() || depth == 0)
        {
            bestScore = root.board.CheckBoardState();
        }

        else {
           

            foreach (int tile in nextMoves)
            {

                Node node = new Node(ROW, COL,Connect);

                node.board.FillBoard(ref root.board.Grid);
                node.board.PlaceTile(tile, CurrentType);
          
                root.children.Add(node);
                if (CurrentType == TileType.Red)
                {
                    currentScore = MiniMax(depth - 1, TileType.Black, node)[0];
                    if (currentScore < bestScore)
                    {
                        bestScore = currentScore;
                        bestRow = tile;

                    }
                }

                else if (CurrentType == TileType.Black)
                {

                    currentScore = MiniMax(depth - 1, TileType.Red, node)[0];

                    if (currentScore > bestScore)
                    {
                        bestScore = currentScore;
                        bestRow = tile;

                    }

                }


            }







        }

            nextMoves.Clear();



        return new int[] { bestScore, bestRow };

    }

    public int[] MiniMaxAB(int depth, TileType CurrentType, Node root, int alpha,int beta)
    {


        int bestScore = (TileType.Black == CurrentType) ? int.MinValue / 2 : int.MaxValue / 2;
        int bestRow = -1;
        //int bestScore = 0;


        List<int> nextMoves = root.board.GenerateMoves(ref root.board);
        int currentScore;


        if (!nextMoves.Any() || depth == 0)
        {
            bestScore = root.board.CheckBoardState();
        }

        else {
            if (CurrentType == TileType.Red)
            {
                //  bestScore = int.MinValue+1;

                foreach (int tile in nextMoves)
                {
                    Node node = new Node(ROW, COL, Connect);
                    node.board.FillBoard(ref root.board.Grid);
                    node.board.PlaceTile(tile, CurrentType);
                    root.children.Add(node);

                    currentScore = MiniMaxAB(depth - 1, TileType.Black, node,alpha,beta)[0];
                    // bestScore = Math.Max(currentScore, bestScore);
                    if (bestScore > currentScore)
                    {
                        bestScore = currentScore;
                        bestRow = tile;

                    }

                  alpha= Math.Max(alpha, bestScore);

                    if(beta<=alpha)
                    {
                       
                        break;
                    }
               
                }

            }
              



            else if (CurrentType == TileType.Black)
            {
               // bestScore = int.MaxValue - 1;

                foreach (int tile in nextMoves)
                {
                    Node node = new Node(ROW, COL, Connect);
                    node.board.FillBoard(ref root.board.Grid);
                    node.board.PlaceTile(tile, CurrentType);
                    root.children.Add(node);

                    currentScore = MiniMaxAB(depth - 1, TileType.Red, node,alpha,beta)[0];
                    //bestScore = Math.Min(currentScore, bestScore);
                    if (bestScore < currentScore)
                    {
                        bestScore = currentScore;
                        bestRow = tile;
                    }

                   beta = Math.Min(beta, bestScore);
                    if(beta<= alpha){
                        break;
                    }
              
                }


                }

              

            }

   
           
        
        return new int[] { bestScore, bestRow };
    }



    public void PlaceTile(int row, TileType type)
    {

        for (int col = 0; col < COL; col++)
        {
           // print("Placed Tile at: " + row + " , " + col);
            if (Grid[row, col].type == TileType.Empty)
            {
                //print(Grid[col,row].name);
                Grid[row, col].type = type;

                PlacedTiled.Add(Grid[row, col]);
            //    checkGridforTile();
                break;

            }

        }






    }


    


}
