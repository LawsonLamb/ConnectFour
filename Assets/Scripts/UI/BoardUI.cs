using UnityEngine;
using System.Collections;

public class BoardUI : MonoBehaviour {
	public int Width;
	public int Height;
	public int Connect;
	public GameObject TilePrefab;
	public GameObject[,] Grid;
	// Use this for initialization
	void Start () {
		GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateBoard(){
		destoryChildren();
		Grid = new GameObject[Width,Height];

		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				Grid[x,y] = Instantiate (TilePrefab) as GameObject;
				Grid[x,y].transform.SetParent (this.transform, false);
				Grid[x,y].name = "Tile " + x + "," + y;

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

		
		}
	}
}
