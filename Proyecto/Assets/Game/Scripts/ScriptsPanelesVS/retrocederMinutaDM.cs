using UnityEngine;
using System.Collections;


public class retrocederMinutaDM : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseUpAsButton(){
		avanzarDailyMeetingPanel aux = (avanzarDailyMeetingPanel)GameObject.Find("Button_DownDaily").GetComponent("avanzarDailyMeetingPanel");
		aux.BackDailys();
	}
	
	
}
