using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TileUI : MonoBehaviour {
	public TileType type;
	public Color ContainerImage
	{
		get{return transform.GetChild(0).GetComponent<Image>().color;}

		set{ transform.GetChild(0).GetComponent<Image>().color= value;}

	}

	// Use this for initialization
	void Start () {
		type = TileType.Empty;
	}
	
	// Update is called once per frame
	void Update () {


		switch(type){

		case TileType.Empty:
			transform.GetComponent<Image>().color = Color.white;
			break;
		case TileType.Black:
			transform.GetComponent<Image>().color = Color.black;
			break;
		case TileType.Red:
			transform.GetComponent<Image>().color = Color.red;
			break;


		}

	
	}
}
