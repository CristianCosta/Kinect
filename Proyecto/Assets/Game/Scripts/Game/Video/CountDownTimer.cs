using UnityEngine;
using System.Collections;

public class CountDownTimer : Object {
	
	private float timer;
	private float startTime;
	private float currentTime;
	private bool running;
	// Use this for initialization
	public CountDownTimer(float countDownTime){
		timer = countDownTime;
		running = false;
	}
	
	// Update is called once per frame
	public void start(){
		startTime = Time.time;
		running = true;
	}
	
	public bool timedOut(){
		if (running){
			float timerRunned = Time.time-startTime;
			if (timerRunned>timer)
				return true;
			else
				return false;
		} else {
			return false;
		}
	}
	
	public void stop(){
		running = false;
	}
}
