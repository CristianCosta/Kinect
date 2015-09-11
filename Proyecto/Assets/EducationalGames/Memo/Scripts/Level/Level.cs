using UnityEngine;
using System.Collections;
using System.Xml; 

//Esta clase va a generer y contener el nivel
public class Level {
	
	private string background;	//Imagen de fondo
	
	private int ID;		//Numero de nivel asociado
	private float time;	//Tiempo asociado al nivel	
	private int pairs; 	//Cantidad de pares
	
	public static int pointsPerPair = 20;	//Puntos por matching pair
	public static int pointsPerLevel = 100;	//Puntos por nivel terminado
	
	public Level(){	
		
	}
	
	public void setPairs(int p){
	
		this.pairs = p;
	
	}
	
	public int getPairs(){
	
		return this.pairs; 
	
	}
	
	public void setTime(float t){
	
		this.time = t; 

	}
	
	public float getTime(){
	
		return this.time;
	
	}
	
	public void setBackground(string s){
		
		this.background = s; 
		
	}
	
	public string getBackground(){
	
		return this.background;
	
	}
	
	public void setID(int i){
		
		this.ID = i; 
		
	}
	
	public int getID(){
	
		return this.ID; 
	
	}
	
	
}
