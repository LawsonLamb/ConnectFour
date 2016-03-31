using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenu : MonoBehaviour {
   public Slider slider;
   public  Text text;
 //   public DebugBoard board;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
    }
  public  void SetValue()
    {
        text.text = slider.value.ToString();
      //  board.tileID = (int)slider.value;
      //  board.SetBoard();
       // board.tile.SetScore();
      //  scoreText.text = board.tile.Score.ToString();
        

    }
}
