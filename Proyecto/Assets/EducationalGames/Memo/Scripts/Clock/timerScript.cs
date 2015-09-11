using UnityEngine;
using System.Collections;

public class timerScript : MonoBehaviour {
	
	private float endTime; 
	private TextMesh counter; 
	
	private float initTime; 
	
	private TextScript ts; 
	
	public int timeLeft; 
	
	private GameCore gc; 
	
	public void setInitTime(float t){
	
		this.initTime = t; 
		this.endTime = Time.time+t; 
	
	}
	
	public void setCore(GameCore g){
		
		this.gc = g; 
		
	}
	
	public int getInitTime(){
	
		return (int)this.initTime; 
	
	}
	
	public int getTime(){
	
		return timeLeft; 
	
	}
	
	// Use this for initialization
	void Start () {
		
		/*this.initTime = 40; 
		this.endTime = Time.time+40; */
	}
	
	// Update is called once per frame
	void Update () {

		this.timeLeft = (int)(endTime - Time.time); 
		if(this.timeLeft <= 0){ 
		
			this.gc.setEndGame();	//Indica al core el fin de juego
		
		}
		
	}
}
