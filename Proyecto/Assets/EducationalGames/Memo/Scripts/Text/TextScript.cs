using UnityEngine;
using System.Collections;

public class TextScript {
	
	private GUIText text;	//A modificar
	
	public TextScript(string name){
		
		this.text = GameObject.Find(name).GetComponent<GUIText>();
		
	}
	
	public void setText(string t){
		
		this.text.text = t; 
		
	}
	
	public string getText(){
	
		return this.text.text; 
	
	}
	
}
