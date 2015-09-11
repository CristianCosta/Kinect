using UnityEngine;
using System.Collections;

//DATOS DE LA CARTA
public class Card{
	
	
	private string path;	//De la imagen de la carta
	private string val; 
	
	
	public void setID(string i){
		
		this.val = i; 
		
	}
	
	public string getID(){
	
		return this.val; 
	
	}
	
	public void setPath(string p){
		
		this.path = p; 
		
	}
	
	public string getPath(){
	
		return this.path;
	
	}
	
	public bool equals(Card card2){
		
		if(this.getID() == card2.getID()){
			
			return true;
			
		}
		
		return false; 
		
	}
	
	public Card(string id){
		
		this.val = id; 
		
	}
	
	public Card(){
		
		
	}
	
	/*void Start(){
	
		//this.current = transform; //Permite ubicar donde ira la carta en el nivel
		observers = new ArrayList(); 
		this.NotifyEvent += new NotifyHandler(notify);
		
		this.flip = new OTSound("flip");	//Sonido de giro de carta
		this.flip.Idle(); 
	
	}
	
	void OnMouseDown(){
	
		Debug.Log("ALLBOYS");
		this.notify(); 
		
		this.flip.Play(); 

		//TODO QUE GIRE LA CARTA 
	}
	
	void Update(){
	}*/
	
}
