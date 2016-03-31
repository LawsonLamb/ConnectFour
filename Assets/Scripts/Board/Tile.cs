using UnityEngine;
using System.Collections;
[System.Serializable]
public class Tile : MonoBehaviour {
    [SerializeField]
	public TileType type;
	public int Column;
	public int Row;
  

    public Color ContainerImage
	{
		get{return transform.GetChild(0).GetComponent<SpriteRenderer>().color;}

		set{ transform.GetChild(0).GetComponent<SpriteRenderer>().color= value;}

	}

    public void SetType(TileType Type)
    {

        type = Type;
    }

	// Use this for initialization
	void Start () {
      //  ResetTile();

	}
	
	// Update is called once per frame
	void Update () {

        setType();
	
	}

    public void setType()
    {
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

    public void ResetTile()
    {
        type = TileType.Empty;
        setType();
    }

    







}
