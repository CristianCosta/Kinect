using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {
	
	private GameCore cs;
	
	public void setCore(GameCore c){
		
		this.cs = c; 
		
	}
	
	void OnMouseDown(){

		this.cs.setEndGame();	//Informa al core que debe finalizar el juego
		
	}
	
}
