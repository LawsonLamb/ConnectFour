using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnCursor : MonoBehaviour {
	
	
	SpriteRenderer renderer;
	
	// Use this for initialization
	void Start () {
	
		renderer = transform.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	

	}


    

    public Color TurnColor
    {
        get { return renderer.color; }

        set { renderer.color = value; }


    }
    
}