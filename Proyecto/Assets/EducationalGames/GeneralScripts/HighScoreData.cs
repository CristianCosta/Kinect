using UnityEngine;
using System.Collections;

//Información de los puntajes altos
//Se mostrará en la escena HighScores
public class HighScoreData{
	
	private string name;
	private string score;
	
	public HighScoreData(string n, string s){
		
		this.name = n;
		this.score = s; 
		
	}
	
	public string getName(){
	
		return this.name;
	
	}
	
	public string getScore(){
	
		return this.score; 
	
	}
	
}
