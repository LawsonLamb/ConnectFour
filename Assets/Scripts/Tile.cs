using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public TileType type;
	public int Column;
	public int Row;

	public Color ContainerImage
	{
		get{return transform.GetChild(0).GetComponent<SpriteRenderer>().color;}

		set{ transform.GetChild(0).GetComponent<SpriteRenderer>().color= value;}

	}

	// Use this for initialization
	void Start () {
		type = TileType.Empty;

	}
	
	// Update is called once per frame
	void Update () {

		switch(type){

		case TileType.Empty:
			transform.GetComponent<SpriteRenderer>().color = Color.white;
			break;
		case TileType.Black:
			transform.GetComponent<SpriteRenderer>().color = Color.black;
			break;
		case TileType.Red:
			transform.GetComponent<SpriteRenderer>().color = Color.red;
			break;


		}
	
	}







}
