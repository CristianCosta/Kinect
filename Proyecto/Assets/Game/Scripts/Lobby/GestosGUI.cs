
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

public class GestosGUI : MonoBehaviour {
	
	
	public GUITexture gestosButton;		
	
	GUIStyle fbStyle;
	GUIStyle fbStyle1;
	GUIStyle fbStyle2;
	GUIStyle gpstyle;
	GUIStyle gpstyle1;
	GUIStyle gpstyle2;
	
	private string newMessage = "";
	public float velocidad;
	private Rect positionMenu;
	private Vector2 chatScrollPosition;
	public GUISkin gSkin;
	public Texture [] gestos;
	private bool enableGestos = false;
	private bool menuListo = false;
	private float currentY;
	private bool ventanaDesplegada = false;
	private bool first = true;
	private bool canMoveWindows = true;
	private bool toggleBoolExplicar = true;
	private Texture textureExplicar;
	private Texture textureSaludar;
	private Texture textureAplaudir;
	private static GestosGUI instance;
	public static GestosGUI Instace {
		get {
			return instance;
		}
	}
	
	void Awake(){
		instance = this;	
	}
	
	void Start()
	{
             //reemplazar el seteo por la funcion que se deba aplicar para saber el estado
		
		int screenH = Screen.height;
		int screenW = Screen.width;
		//gestosButton.transform.position = new Vector3(.5f,.5f,.0f);
		
		Rect newInset = new Rect(screenW/2 - gestosButton.pixelInset.width/2,0,gestosButton.pixelInset.width,gestosButton.pixelInset.height);
		gestosButton.pixelInset = newInset; 		
		textureExplicar = gestos[0];
		textureSaludar= gestos[2];
		textureAplaudir = gestos[4];
		chatScrollPosition = Vector2.zero;	
		positionMenu = new Rect(screenW/2 - (screenW /3),screenH -(screenH *2/10) - 25, screenW*2/3 , (screenH *2/8) - 5);
		//
	}
	

 
	

	
	public void updateGestos(){
		if (canMoveWindows.Equals(true)){
			enableGestos = !enableGestos;
		
			if (enableGestos){			
					currentY = Screen.height;
					int screenW = Screen.width;
					//int screenH = Screen.height;
					//if (first){
						Rect newInset = new Rect(screenW/2 - gestosButton.pixelInset.width/2,gestosButton.pixelInset.y-22.7089f,gestosButton.pixelInset.width,gestosButton.pixelInset.height);
						gestosButton.pixelInset = newInset; 	
						first = false;
					//}
			}
				//gestosButton.texture = hideChat;
			else{
				
				int screenW = Screen.width;
				int screenH = Screen.height;
				currentY = screenH -(screenH *2/10) - 25;
				Rect newInset = new Rect(screenW/2 - gestosButton.pixelInset.width/2,gestosButton.pixelInset.y,gestosButton.pixelInset.width,gestosButton.pixelInset.height);
				gestosButton.pixelInset = newInset; 	
			
			}
		}
	}
	
	
	
	void Update(){
		
		if (Input.GetMouseButtonDown(1) && enableGestos){
			if (canMoveWindows.Equals(true))
				enableGestos = false;
			//gestosButton.texture = showChat;		
		}
		if (Input.touchCount > 0 && canMoveWindows.Equals(true))
			for (int i = 0; i < Input.touchCount; i++){
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began &&
				gestosButton.HitTest(touch.position)){
					updateGestos();
				}
			}
	}
	
	void OnMouseDown(){
		updateGestos();
	}
	
	private void drawButtons(float currentY){
		int screenW = Screen.width;
		//int screenH = Screen.height;
		
		if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 10,currentY+45, 128, 56),textureExplicar)){
				if (textureExplicar.Equals(gestos[0])){
						textureExplicar = gestos[1];	
						textureSaludar = gestos[2];
						textureAplaudir = gestos[4];
						RootMotionCharacter.runAnimation = "explicar";
				}
				else{
					textureExplicar = gestos[0];
					RootMotionCharacter.runAnimation = "";
				}
		}
	  
		if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 163,currentY+45, 128, 56),textureSaludar)){
			if (textureSaludar.Equals(gestos[2])){
					textureSaludar = gestos[3];	
					textureExplicar = gestos[0];
					textureAplaudir = gestos[4];
					RootMotionCharacter.runAnimation = "saludar";
				}
				else{
					textureSaludar = gestos[2];
					RootMotionCharacter.runAnimation = "";
				}
		}
		
		if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 316,currentY+45, 128, 56),textureAplaudir)){
				if (textureAplaudir.Equals(gestos[4])){
					textureAplaudir = gestos[5];	
					textureExplicar = gestos[0];
					textureSaludar = gestos[2];
					RootMotionCharacter.runAnimation = "aplaudir";
				}
				else{
					textureAplaudir = gestos[4];
					RootMotionCharacter.runAnimation = "";
				}
		}
		
		
	}
	
	// Finally draw all the lobby GUI
	void OnGUI()
	{
		if (KeyControl.Instance != null){
			if (gestosButton.enabled){
				if (KeyControl.Instance.isLaserEnabledVS() || ChatGUI.enableChat)
					gestosButton.enabled = false;
			}else{
				if (!KeyControl.Instance.isLaserEnabledVS() && !ChatGUI.enableChat)
					gestosButton.enabled = true;
	
			}
		}

		GUI.skin = gSkin;

		int screenW = Screen.width;
		int screenH = Screen.height;
				
		if (enableGestos){
			//Debug.Log(menuListo);
			if (!menuListo){
				
				if (!ventanaDesplegada){
					
					//22.7089
					Rect currentPos = new Rect(screenW/2 - (screenW /3),currentY, screenW*2 /3 , (screenH *2/8) - 5);
					//Debug.Log(gestosButton.pixelInset.y);
					if (currentPos.y > positionMenu.y){
						canMoveWindows = false;
						currentY-=velocidad;
						Rect newInset = new Rect(screenW/2 - gestosButton.pixelInset.width/2,gestosButton.pixelInset.y+velocidad,gestosButton.pixelInset.width,gestosButton.pixelInset.height);
						gestosButton.pixelInset = newInset; 	
						
						GUI.Box(new Rect(screenW/2 - (screenW /3),currentY, screenW *2/3 , (screenH *2/8) - 5), "");
						
						drawButtons(currentY);
						
					}
					else{	
						menuListo = true;
						ventanaDesplegada = true;
						canMoveWindows = true;
					}
					
				}
				//GUILayout.BeginArea (new Rect(screenW/2 - (screenW /3)/2 + 10, screenH -(screenH *2/5) , screenW /3 -20, (screenH *2/5) - 40));
					
			//	GUILayout.EndArea();
			}
			else{
			
			
				GUI.Box(new Rect(screenW/2 - (screenW /3),screenH -(screenH *2/10) - 25, screenW* 2 /3 , (screenH *2/8) - 5), "");


				if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 10,screenH -(screenH *2/10) - 25 +45, 128, 56),textureExplicar)){
					if (textureExplicar.Equals(gestos[0])){
						textureExplicar = gestos[1];	
						textureSaludar = gestos[2];
						textureAplaudir = gestos[4];
						RootMotionCharacter.runAnimation = "explicar";
					}
					else{
						textureExplicar = gestos[0];
						RootMotionCharacter.runAnimation = "";
					}
				}
			

			  
				if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 163,screenH -(screenH *2/10) - 25 +45, 128, 56),textureSaludar)){
					if (textureSaludar.Equals(gestos[2])){
						textureSaludar = gestos[3];	
						textureExplicar = gestos[0];
						textureAplaudir = gestos[4];
						RootMotionCharacter.runAnimation = "saludar";
					}
					else{
						textureSaludar = gestos[2];
						RootMotionCharacter.runAnimation = "";
					}
				}
				
				if (GUI.Button(new Rect(screenW/2 - (screenW /3) + 316,screenH -(screenH *2/10) - 25 +45, 128, 56),textureAplaudir)){
					if (textureAplaudir.Equals(gestos[4])){
						textureAplaudir = gestos[5];	
						textureExplicar = gestos[0];
						textureSaludar = gestos[2];
						RootMotionCharacter.runAnimation = "aplaudir";
					}
					else{
						textureAplaudir = gestos[4];
						RootMotionCharacter.runAnimation = "";
					}
				}

			
			}
					
		}else{
			//Debug.Log("ven " + ventanaDesplegada);
			if (ventanaDesplegada){
				//22.7089
				Rect currentPos = new Rect(screenW/2 - (screenW /3),currentY, screenW *2/3 , (screenH *2/8) - 5);
				//Debug.Log(gestosButton.pixelInset.y);
				//Debug.Log(currentPos.y +" " + Screen.height);
				if (currentPos.y < Screen.height){
					canMoveWindows = false;
					currentY+=velocidad;
					if (gestosButton.pixelInset.y > 0){
						Rect newInset = new Rect(screenW/2 - gestosButton.pixelInset.width/2,gestosButton.pixelInset.y-velocidad,gestosButton.pixelInset.width,gestosButton.pixelInset.height);
						gestosButton.pixelInset = newInset; 	
					}
					GUI.Box(new Rect(screenW/2 - (screenW /3),currentY, screenW *2/3 , (screenH *2/8) - 5), "");
					
					drawButtons(currentY);
				}
				else{
					menuListo = false;
					ventanaDesplegada = false;
					canMoveWindows = true;
				}
			}
					
		}			
	}
}
