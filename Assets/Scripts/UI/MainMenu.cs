using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public Text rowText;
    public Text colText;
    public Text connectText;
    public GameBoard board;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void OnClick()
    {
 
        board.ROW = int.Parse(rowText.text);

        board.COL = int.Parse(colText.text);

        board.Connect = int.Parse(connectText.text);
        board.BuildBoard();
        this.gameObject.SetActive(false);
         
    }
}
