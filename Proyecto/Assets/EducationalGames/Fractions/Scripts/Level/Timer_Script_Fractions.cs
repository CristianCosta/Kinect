using UnityEngine;
using System.Collections;

public class Timer_Script_Fractions : MonoBehaviour {
	
	float endTime; 
	
	private bool pause;
	private int maxValue;
	
	public TextMesh counter;
	
	public GameCore_Fractions game;
	
	// Use this for initialization
	void Start () {
		
		//pause = true; //para pausar al inicio
		pause = false;
		
		///endTime = Time.time+210f; 

	}
	
	// Update is called once per frame
	void Update () {
		//Lo posiciona
		if (!pause){
			int timeLeft = (int)(endTime - Time.time); 

			if(timeLeft > 0){
				counter.text = timeLeft.ToString(); 
			}
			else{
				counter.text = "0"; 
				game.attendEvent("clockTimeOut");
			}
		}
	}
	
	public void setPause (bool value){
		pause = value;
	}
	
	public void restartCount (){
		counter.text = maxValue.ToString();
		endTime = Time.time+maxValue; 
		this.setPause(false);
	}
	
	public void setMaxValue(int newValue){
		maxValue = newValue;
	}
	
	public void addTime(int additionalTime){
		endTime += additionalTime;
	}
	
	public int getTimeisShowing(){
		return int.Parse(counter.text);
	}
		
}
