using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CursorUI : MonoBehaviour {
	public BoardUI _board;
	public List<Vector2> ColPos;
	public int index=0;
	public TileType currentType;
	public bool turn;
	SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		renderer = transform.GetComponent<SpriteRenderer>();
		SetColPos();
	}
	
	// Update is called once per frame
	void Update () {
		CursorUpdate();
	}
	[ContextMenu("SetPos")]
	public void SetColPos(){

		ColPos = new List<Vector2>();
		index =0;
		RectTransform rect;
		Vector2 pos;
		for(int row = 0;row <=_board.Height;row++){
			

			rect= _board.Grid[_board.Width-1,row].GetComponent<RectTransform>();
			print(_board.Grid[_board.Width-1,row].name);

			pos = new Vector2(rect.localPosition.x,this.GetComponent<RectTransform>().anchoredPosition.y);

			ColPos.Add(pos);


		}


	}

	public void moveCursor(){

		this.GetComponent<RectTransform>().anchoredPosition = ColPos[index];


	}
	public void CursorUpdate(){


		if(Input.GetKeyDown(KeyCode.LeftArrow)){

			index--;
			if(index<0)
				index = _board.Height-1;

		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){

			index++;
			if(index>_board.Height-1)
				index = 0;

		}

		if(Input.GetKeyDown(KeyCode.Return)){
			//_board.PlaceTile(index,currentType);
			//ChangeTurn();
			turn =!turn;
		}
		ChangeTurn();
		switch(currentType){

		case TileType.Empty:
			//renderer.color = Color.white;
			break;
		case TileType.Black:
			//renderer.color = Color.black;
			break;
		case TileType.Red:
			//renderer.color = Color.red;
			break;

		}

		moveCursor();

	}
	public void ChangeTurn(){

		if(turn){

			currentType = TileType.Red;
		}

		if(!turn){
			currentType = TileType.Black;


		}







	}

}
