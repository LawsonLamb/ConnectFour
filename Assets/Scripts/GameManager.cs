using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameBoard _board;
    public Board b;
    public int RedScore = 0;
    public int BlackScore = 0;
    public static int ROW;
    public static int COL;
 
    public TurnCursor cursor;
    List<Vector2> ColPos;
   public int index;
    public TileType currentType;
    bool turn;
    
    
    // Use this for initialization
    void Start () {
        ROW = _board.COL;
        COL = _board.ROW;
        SetColPos();
	}
	
	// Update is called once per frame
	void Update () {

        CursorUpdate();
	
	}

    public void setScore(TileType type)
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

    #region Cursor
    public void ChangeTurn()
    {

        if (turn)
        {

            currentType = TileType.Red;
            int[] move = b.Move(currentType);
            _board.PlaceTile(move[1], currentType);

            turn = !turn;

        }

        if (!turn)
        {
            currentType = TileType.Black;


        }







    }

    public void moveCursor()
    {

        cursor.transform.position = ColPos[index];


    }

    public void CursorUpdate()
    {


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            index--;
            if (index < 0)
                index = _board.ROW - 1;

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            index++;
            if (index > _board.ROW - 1)
                index = 0;

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            _board.PlaceTile(index, currentType);
           
           
            turn = !turn;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _board.EvaluateBoard(currentType);
        }
        ChangeTurn();
        switch (currentType)
        {

            case TileType.Empty:
                cursor.TurnColor = Color.white;
                break;
            case TileType.Black:
                cursor.TurnColor = Color.black;
                break;
            case TileType.Red:
                cursor.TurnColor = Color.red;

                break;

        }

        moveCursor();

    }

    void SetColPos()
    {

        ColPos = new List<Vector2>();
        index = 0;
        Vector3 tempPos;
        Vector2 pos;
        for (int row = 0; row <= _board.COL; row++)
        {
            int temp = _board.COL - 1;
            //print(row +" ," + temp);
            tempPos = new Vector3();
            tempPos = _board.Grid[row, _board.COL - 1].transform.position;

            pos = new Vector2(tempPos.x, tempPos.y + 2.0f);

            ColPos.Add(pos);


        }


    }

    #endregion







}
