
using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


public class GUI_activarDetalleMinuta : MonoBehaviour{
		
	private bool showMinutaWindows;  
	private float maxWidth = 350;
	private float maxHeight = 450;
	private float x = 15;
	private float y = 15;
	private Rect windowRect;
	private DailyMeeting dailyMeeting;	
	
	void start(){
		showMinutaWindows = false;
	}
	
	void update(){}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if(showMinutaWindows){
			windowRect = new Rect(x,y,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doNewMinutaWindow,"Daily Meeting Details");
		}
		GUI.skin = oldSkin;
		
	}
		
	void doNewMinutaWindow(int WindowID){
		
		if(dailyMeeting != null){
			System.DateTime date = new System.DateTime(System.Convert.ToInt64(dailyMeeting.getFecha()));
			string fecha = date.ToString("dd-MM-yyyy");
			GUIComponents.labelDetail(new Rect(20,55,310,30),"Date: ", fecha);
			GUIComponents.labelDetail(new Rect(20,85,310,40),"Scrum member: ", dailyMeeting.getNick());
            GUIComponents.labelDetail(new Rect(20, 135, 310, 40), "What did you do yesterday?: ", dailyMeeting.getAyer());
            GUIComponents.labelDetail(new Rect(20, 185, 310, 40), "What will you do today?: ", dailyMeeting.getHoy());
            GUIComponents.labelDetail(new Rect(20, 235, 310, 40), "Are there any impediments in your way?:", dailyMeeting.getInconvenientes());
		}
		else {
			  string vacio = "";			
			  GUIComponents.labelDetail(new Rect(20,80,400,50),"There is no meeting... ",vacio);
			 }
		
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-30,140,20),"Cancel")){
			showMinutaWindows = false;
		}
		
	}
	
	void OnMouseUpAsButton(){
		showMinutaWindows = true;
	}
	
	void setDailyMeeting(DailyMeeting dailyMeeting){
		this.dailyMeeting = dailyMeeting;
	}
}


