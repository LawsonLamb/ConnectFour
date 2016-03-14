using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Board : MonoBehaviour {
	public int GridWidth;
	public int Height;
	public int Connect;
	public GameObject TilePrefab;
	public GameObject[,] Grid;
	public int Space = 2;
	public int RedScore =0;
	public int BlackScore=0;
	public int Testx =0;
	public int Testy =0;
	public List<GameObject> PlacedTiled;

	#region monobehaviors
	// Use this for initialization
	void Start () {
		PlacedTiled= new List<GameObject>();
		BuildBoard();


	}

	// Update is called once per frame
	void Update () {
		

	
	}

	#endregion



	[ContextMenu("Check Tiles")]
	void testTile(){
		
	
		BuildBoard();
		CheckSideWays(Testx,Testy);
	

	}


	#region Board Building
	[ContextMenu("BuildBoard")]
	public void BuildBoard(){
		destoryChildren();
		PlacedTiled.Clear();
		Grid = new GameObject[GridWidth,Height];
		SpriteRenderer renderer = TilePrefab.GetComponent<SpriteRenderer>();
		Vector2 TileSize = new Vector2(renderer.bounds.size.x,renderer.bounds.size.y);
		for(int y= 0; y< Height;y++){

			for(int x =0; x<GridWidth;x++){

				GameObject go = Instantiate (TilePrefab, new Vector3 ((x * TileSize.x)*Space, (y * TileSize.y)*Space, 0), Quaternion.identity) as GameObject ;
				go.transform.SetParent(this.transform);
				go.transform.name = "Tile " + x + "," + y;
				go.GetComponent<Tile>().Row = x;
				go.GetComponent<Tile>().Column = y;

				Grid[x,y] = go;
			}


		}




	


	}

	void destoryChildren(){
		int count  = this.transform.childCount;
		if(count==0){

		}
		else{
			while(this.transform.childCount>0){
				GameObject.DestroyImmediate(this.transform.GetChild (0).gameObject);
			}

//		GameObject.DestroyImmediate(this.transform.GetChild(0));
		}
	}
	#endregion


	public void PlaceTile(int row,TileType type){
		
		for(int col=0; col<Height;col++){
			print("Placed Tile at: "+row + " , "+ col);
			if(Grid[row,col].GetComponent<Tile>().type == TileType.Empty){
				//print(Grid[col,row].name);
				Grid[row,col].GetComponent<Tile>().type = type;

				PlacedTiled.Add(Grid[row,col]);
					checkGridforTile();
						break;

			}

		}






	}

	#region Board Checking


	void checkGridforTile(){
		print("Check grid");

		if(PlacedTiled.Count>=0){
			print(PlacedTiled.Count);
			for(int i= 0;i<PlacedTiled.Count;i++){
				//print(PlacedTiled[i].GetComponent<Tile>().Row + "," + PlacedTiled[i].GetComponent<Tile>().Column);
				TileType _type = CheckBoard(PlacedTiled[i].GetComponent<Tile>().Row,PlacedTiled[i].GetComponent<Tile>().Column);
				if(_type != TileType.Empty){
					setScore(_type);

					BuildBoard();

					break;


				}
				else{
					continue;
				}

			}
		}


	}


	TileType CheckBoard(int x,int y){

		List<GameObject> Horizontal = CheckHorizontal(x,y);
		List<GameObject>  Vertical = CheckVertical(x,y);
		SetBackgroundToColor(Horizontal,Color.green);
		SetBackgroundToColor(Vertical,Color.green);

		if(Horizontal.Count>0)
			return Horizontal[0].GetComponent<Tile>().type;
		else if(Vertical.Count>0)
			return Vertical[0].GetComponent<Tile>().type;




		else{

			return TileType.Empty;
		}




	}

	 List<GameObject> CheckHorizontal(int x,int y){
		List<GameObject> matches = new List<GameObject>();
		matches.Add(Grid[x,y]);
		//Check Left
		for (int row = x-1; row >= 0; row--){



			if(Grid[x,y].GetComponent<Tile>().type == Grid[row,y].GetComponent<Tile>().type){

				matches.Add(Grid[row,y]);
		
			}

			else{
				break;
			}
		}
		for (int row = x+ 1; row< GridWidth; row++){


			if(Grid[x,y].GetComponent<Tile>().type == Grid[row,y].GetComponent<Tile>().type){
				matches.Add(Grid[row,y]);
			
			}

			else{

			break;
			}
		
		}
		if (matches.Count < Connect){
			SetBackgroundToColor(matches,Color.white);
			matches.Clear();

		}




		return matches;



	}

	 List<GameObject> CheckVertical(int x,int y){

		List<GameObject> matches = new List<GameObject>();
		matches.Add(Grid[x,y]);

	//Check Down
		for (int col = y-1; col >= 0; col--){

			if(Grid[x,y].GetComponent<Tile>().type == Grid[x,col].GetComponent<Tile>().type){

				print("Match: "+ x+ "," +col);
				matches.Add( Grid[x,col]);
		

			}
			else{

				break;

			}


		}
	//Check UP
		for (int col = y+ 1; col < Height; col++){
			
			if(Grid[x,y].GetComponent<Tile>().type == Grid[x,col].GetComponent<Tile>().type){


				print("Match: "+ x+ "," +col);
				matches.Add( Grid[x,col]);

			}
			else{


			

				break;

			}
		}

		if (matches.Count < Connect){
			SetBackgroundToColor(matches,Color.white);

			matches.Clear();

		}



		return matches;


	}


	void CheckSideWays(int x,int y){

		Grid[x,y].GetComponent<Tile>().ContainerImage = Color.yellow;
		//CheckUPleft
		print("Blue");
		for(int row =x+1,col =y+1;row<GridWidth && col<Height;row++,col++){


				Grid[row,col].GetComponent<Tile>().ContainerImage = Color.blue;

			print(row+ ","+ col);			
		}

		//CheckdownRight
	
		print("Red");
		for (int row = x-1, col=y-1; (row >=0 && col>=0); row--,col--){

			print(row + ","+ col);
			Grid[row,col].GetComponent<Tile>().ContainerImage = Color.red;

		}


		print("Black");
		//check UpRight
		for(int row =x+1,col =y-1;row<=GridWidth-1 && col>=0;row++,col--){


			Grid[row,col].GetComponent<Tile>().ContainerImage = Color.black;
			print(row + ","+ col);
		}


		print("Green");
		//check DownLeft
		for(int row =x-1, col =y+1;row>=0 && col<Height;row--,col++){
			


				Grid[row,col].GetComponent<Tile>().ContainerImage = Color.green;
			print(row + ","+ col);
		}



	}


	#endregion




	void SetBackgroundToColor(List<GameObject> matches,Color color){

		foreach(GameObject go in matches){
			go.GetComponent<Tile>().ContainerImage = color;

		}


	}

	void setScore(TileType type){

		switch(type){

		case TileType.Black:
			BlackScore+=1;
			break;
		case TileType.Red:
			RedScore+=1;
			break;


		}

	}








}
