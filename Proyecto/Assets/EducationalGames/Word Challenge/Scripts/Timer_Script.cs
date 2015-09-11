using UnityEngine;
using System.Collections;

public class Timer_Script : MonoBehaviour {
	
	float endTime; 
	private GUIText counter;
	private bool pause;
	private int maxValue;
	
	private bool centrado_99, centrado_9;
	
	public Game game;
	
	public Controller_Camera camCont;
	// Use this for initialization
	void Start () {
		pause = true;
		///endTime = Time.time+210f; 
		
		counter = GameObject.Find("timerText").GetComponent<GUIText>(); 
		counter.material.SetColor("_Color", Color.white);
		
		centrado_99 = false;
		centrado_9 = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Lo posiciona
		counter.pixelOffset = new Vector2(-332+camCont.getOddSetInGameClock_X(), 252+camCont.getOddSetInGameClock_Y());
		if (!pause){
			int timeLeft = (int)(endTime - Time.time); 
		
			centrarTexto(timeLeft);
			
			if(timeLeft > 0){
				counter.text = timeLeft.ToString(); 
			}
			else{
				counter.text = "0"; 
				game.finishGame("clockTimeOut");
			}
		}
	}
	
	public void setPause (bool value){
		pause = value;
	}
	
	public void restartCount (){
		counter.text = maxValue.ToString();
		endTime = Time.time+maxValue; 
		
		centrado_99 = false;
		centrado_9 = false;
	}
	
	public void setMaxValue(int newValue){
		maxValue = newValue;
	}
	
	public void addTime(int additionalTime){
		endTime += additionalTime;
	}
		
	public void centrarTexto(int tiempo){
		float x_ant = counter.pixelOffset.x;
		float y_ant = counter.pixelOffset.y;
		
		if (tiempo == 99 && !centrado_99){
			counter.pixelOffset = new Vector2(x_ant+camCont.getOffSetInGameClockCenter_99(),y_ant);
			centrado_99 = true; //ya centrÃ³ -> no hay que volver a centrar
		}
		else if (tiempo == 9 && !centrado_9){
			counter.pixelOffset = new Vector2(x_ant+camCont.getOffSetInGameClockCenter_9(),y_ant);
			centrado_9 = true;
		}
	}
	
	public int getTimeisShowing(){
		return int.Parse(counter.text);
	}
		
}
