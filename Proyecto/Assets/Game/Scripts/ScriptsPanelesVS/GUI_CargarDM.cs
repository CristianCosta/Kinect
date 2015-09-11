using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;


public class GUI_CargarDM : MonoBehaviour {
	
	bool CargarDMWindows = false;
	Rect windowRect;
	string hoy = "";
	string ayer = "";
	string inconvenientes = "";
	
	float maxWidth = 300;
	float maxHeight = 300;
	float inicio_x = 15;
	float inicio_y = 15;
	float offset = 10;
	
	public String serverName = "127.0.0.1";
	public int serverPort = 9933;
	SmartFox sfs;
	
	private DailyMeeting daily;
	
	// Skin User Stories
	public GUISkin skinUS;
	
	void start(){}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
        GUI.skin = skinUS;
		
		if(CargarDMWindows){
			windowRect = new Rect(Screen.width - maxWidth - inicio_x - 20, Screen.height - maxHeight - inicio_y,maxWidth,maxHeight);
			windowRect = GUI.Window(1,windowRect,doNewDMWindow,"Daily Meeting");
		
		}
		
		GUI.skin = oldSkin;
	}
	
	void doNewDMWindow(int windowID){

        hoy = GUIComponents.labelTextArea(new Rect(inicio_x + offset, inicio_y + offset, maxWidth - 40, 60), hoy, "What will you do today?");
        ayer = GUIComponents.labelTextArea(new Rect(inicio_x + offset, inicio_y + offset + 90, maxWidth - 40, 60), ayer, "What did you do yesterday?");
        inconvenientes = GUIComponents.labelTextArea(new Rect(inicio_x + offset, inicio_y + offset + 150, maxWidth - 40, 60), inconvenientes, "Are there any impediments?");
		

		
		if (GUI.Button(new Rect(inicio_x+offset,260,100,20),"Save")){
			
			DailyMeeting dm = new DailyMeeting();
			dm.setAyer(ayer);
			dm.setFecha(System.DateTime.Now.Ticks);
			dm.setHoy(hoy);
			dm.setInconvenientes(inconvenientes);
			dm.setNick(MultiPlayer.Instance.getMyUserName());
			
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("crearDailyMeeting",dm.toSFSObject()));
			CargarDMWindows = false;
			
			this.limpiarVentana();	
			//GUI_CargarDailyPanel cdmp = (GUI_CargarDailyPanel)(GameObject.Find("DailyMeetingPlane")).GetComponent("GUI_CargarDailyPanel");
			//ArrayList dailys = (ArrayList)MultiPlayer.Instance.getListaDailyMeetings();
			//cdmp.inicializar(dailys);

		}
		
		if (GUI.Button(new Rect(inicio_x+offset+120,260,100,20),"Cancel")){
			CargarDMWindows = false;
			this.limpiarVentana();
		}
	
	}
	
	void OnMouseUpAsButton(){
		CargarDMWindows = true;
	}
	
	public long getIdDaily(){
		
		long max = 0;
		ArrayList dailyMeetings = MultiPlayer.Instance.getListaDailyMeetings();
		foreach (DailyMeeting dm in dailyMeetings){
			
				if (dm.getId_proyecto() > max)
						max = dm.getId_proyecto(); 
			}
		
		return max;
	}
	
	void limpiarVentana(){
		ayer = "";
		hoy = "";
		inconvenientes="";
		
	}
	
}