using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour {
	public GameBoard _board;
	public TileType type;

	Text score;


	// Use this for initialization
	void Start () {
		score = this.transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(type){

		case TileType.Black:
			score.text= _board.BlackScore.ToString();
			break;

		case TileType.Red:
			score.text= _board.RedScore.ToString();
			break;

		}
	}
}
