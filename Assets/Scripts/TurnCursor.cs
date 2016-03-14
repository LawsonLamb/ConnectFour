using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnCursor : MonoBehaviour {
	public Board _board;
	//Board _board;
	public List<Vector2> ColPos;
	public int index;
	public TileType currentType;
	SpriteRenderer renderer;
	public bool turn;
	// Use this for initialization
	void Start () {
	//	ColPos = new Vector2[_board.Height];
		//index =0;
		renderer = transform.GetComponent<SpriteRenderer>();
		SetColPos();
	}
	
	// Update is called once per frame
	void Update () {
		
		CursorUpdate();

	}


	[ContextMenu("SetPos")]
	void SetColPos(){

		ColPos = new List<Vector2>();
		index =0;
		Vector3 tempPos;
		Vector2 pos;
		for(int row = 0;row <=_board.Height;row++){
			int temp =  _board.Height-1;
			//print(row +" ," + temp);
			tempPos = new Vector3();
				tempPos = _board.Grid[row,_board.Height-1].transform.position;
			 
			pos = new Vector2(tempPos.x,tempPos.y+2.0f);

			ColPos.Add(pos);


		}


	}


	public void moveCursor(){
		
		this.transform.position = ColPos[index];
	

	}


	public void ChangeTurn(){
		
		if(turn){

			currentType = TileType.Red;
		}

		if(!turn){
			currentType = TileType.Black;


		}







}

	public void CursorUpdate(){


		if(Input.GetKeyDown(KeyCode.LeftArrow)){

			index--;
			if(index<0)
				index = _board.GridWidth-1;

		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){

			index++;
			if(index>_board.GridWidth-1)
				index = 0;

		}

		if(Input.GetKeyDown(KeyCode.Return)){
			_board.PlaceTile(index,currentType);
			//ChangeTurn();
			turn =!turn;
		}
		ChangeTurn();
		switch(currentType){

		case TileType.Empty:
			renderer.color = Color.white;
			break;
		case TileType.Black:
			renderer.color = Color.black;
			break;
		case TileType.Red:
			renderer.color = Color.red;
			break;

		}

		moveCursor();

	}
}