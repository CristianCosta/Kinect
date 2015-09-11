using UnityEngine;
using System.Collections;

public class ShowBackground : MonoBehaviour {
	
	private OTSprite current;
	private Color c; 
	
	// Use this for initialization
	void Start () {		
		this.current = (OTSprite)GetComponent<OTSprite>(); 
		this.c = Color.white; 		
	}
	
	public Color getColor(){	
		return this.c; 	
	}
	
	// Update is called once per frame
	void Update () {		
	}
	
	/*void OnMouseOver () {
		//renderer.material.color=Color.red;
		this.c = Color.cyan;
	}*/
	
	void OnMouseEnter(){	
		current.depth = -1; 
		c = Color.cyan; 	
	}
	
	void OnMouseExit(){	
		this.current.depth = 4; 
		this.c = Color.white; 	
	}
}
