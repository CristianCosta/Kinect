using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
	
	private float remainingTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		remainingTime-=1*Time.deltaTime;
		ShowTime();
		if (remainingTime<=0){
			LevelManager.LoadGameOver();
		}	
	}

	public float RemainingTime {
		get {
			return this.remainingTime;
		}
		set {
			remainingTime = value;
			ShowTime();
		}
	}
	
	private void ShowTime(){
		GetComponent<OTTextSprite>().text=((int)remainingTime).ToString();
	}
}
