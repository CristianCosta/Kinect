using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class helpTutorial : MonoBehaviour {

private SmartFox smartFox;

public GUISkin gSkin;
private static helpTutorial instance;
private Vector2 userScrollPosition;
public bool enableHelp = false;

public Texture2D [] Images = new Texture2D[10];
private bool toggleTxt = false;
private bool LastToggleTxt = false;
private bool toggleBoolNew;

	
public GUIStyle g = new GUIStyle();	

public static helpTutorial Instace {
get {
	return instance;
	}
}

void Awake(){
	instance = this;
}

void Start()
{
	TutorialManagerDB.getInstance().getTutorial(LobbyGUI.user,tutorialRequestCallback);
}

public void updateHelp(){
	enableHelp = !enableHelp;
		
	if (LastToggleTxt != toggleTxt){
		LastToggleTxt = toggleTxt;
		if (toggleTxt)
			TutorialManagerDB.getInstance().insertarTutorial(LobbyGUI.user,"si");
		else
			TutorialManagerDB.getInstance().insertarTutorial(LobbyGUI.user,"no");
	
	}
	
}
	

void Update(){
	if (Input.GetKeyDown(KeyCode.H)){
		updateHelp();
	}
}

public void tutorialRequestCallback(string data){
	if (data.Equals("si")){
		enableHelp = true;
		toggleTxt = true;			
	}
	LastToggleTxt = toggleTxt;
}
	
void OnGUI()
{
	if (enableHelp){
		GUI.skin = gSkin;
		int screenW = Screen.width;
		int screenH = Screen.height;
		GUI.Box(new Rect(screenW*10/40,screenH*8/40,screenW*20/40,screenH*24/40), "Ayuda");
		GUILayout.BeginArea(new Rect (screenW*11/40,screenH*10/40,screenW*18/40,screenH*19/40));
			userScrollPosition = GUILayout.BeginScrollView (userScrollPosition, GUILayout.Width (screenW*18/40), GUILayout.Height (screenH*20/40));
			GUILayout.BeginVertical ();
			List<Texture2D> userList = new List<Texture2D>();
			
			for (int i=0;i<Images.Length; i++){
				userList.Add(Images[i]);
			}
			
			g.fixedWidth = (int)screenW*17/40;
			g.fixedHeight = (int)screenH*10/40;
			foreach (Texture2D background in userList) {
				
				GUILayout.Label (background,g);
				
				}
			GUILayout.EndVertical ();
			GUILayout.EndScrollView ();
			GUILayout.EndArea ();
		if (GUI.Button(new Rect(screenW*15/40,screenH*30/40,90,25), "Salir")){
			updateHelp();
		}
		
		GUI.color = Color.black;
		toggleBoolNew = GUI.Toggle(new Rect (screenW*22/40, screenH*30/40, 210, 25), toggleTxt, "Mostrar al iniciar");
		toggleTxt = toggleBoolNew;
		// Check if the toggle was toggled
		
		}
	}
	
}


