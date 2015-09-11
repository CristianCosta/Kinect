using UnityEngine;
using System.Collections;
using System;

public class GUI_CargarCampoDailyMeeting: MonoBehaviour
{
	
	float maxWidth = 350;
	float maxHeight = 300;
	float inicio_x = 15;
	float inicio_y = 15;
	float offset = 10;
	string ScrumMember = "";
	string ayer = "";
	string hoy = "";
	string inconveniente = "";
	Rect windowRect;
	bool commentWindows=false;
	
	void start(){}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if(commentWindows){
			windowRect = new Rect(inicio_x,inicio_y,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doNewCommentWindow,"Nueva Comentario Individual");
		}
		GUI.skin = oldSkin;
		
	}
	
	void OnMouseUp(){
		//Se activa al presionar el botón, creando una nueva ventana
		commentWindows=true;		
	}
	
	void doNewCommentWindow(int windowID){
		ScrumMember = GUIComponents.labelTextField(new Rect(80,25,maxWidth-40,20),ScrumMember,"ScrumMember",60);
		ayer = GUIComponents.labelTextArea(new Rect(inicio_x+offset,70,maxWidth-40,80),ayer,"¿Que hizo ayer?");
		hoy = GUIComponents.labelTextArea(new Rect(inicio_x+offset,160,140,20),hoy,"¿Que va a hacer?");
		inconveniente = GUIComponents.labelTextArea(new Rect(inicio_x+offset,180,180,100),inconveniente,"Inconvenientes");
	
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-30,140,20),"Cancelar")){
			commentWindows=false;
		}
	}
	
}


