using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VideoSuscribeWindow : Object {
	
	private GUISkin gSkin;
	private int xWindow,yWindow,totalWidth,totalHeight,xListUsers,menuWidth,listWidth;
	private bool windowsCreated;
	public bool videoRunning;
	private bool adviceWindowOn;
	public bool closeQuestionOn;
	private bool enableWindow;
	private float adviceHeight;
	private float minWidth;
	private float adviceWidth;
	private float endSuscriptionQWidth;
	private float endSuscriptionMWidth;
	private int ScreenW;
	private int ScreenH;
	private Rect adviceRect;
	private Rect endSuscriptionRect;
	public VidFrame videoFrame;
	private RoomVariableManager RoomVariableMgr;
	private List<GUIContent> conectionsList;
	private Vector2 userScrollPosition ,conectionScrollPosition;
	private int selectedConection, tempSelectedConection;
	private int selectedUser, tempSelectedUser;
	private string senderIP;
	private Texture2D videoDefault;
	private string adviceMessage;
	private string endSuscriptionMessage;
	private string endSuscriptionQuestion;
	private Texture2D videoRecibido;
	private Texture2D videoTimedOut;
	private float videoCloseHeight;
	private const string ComboBoxStyle = "ComboBox";
	private bool timedOut;
	private CountDownTimer countDownTimer;
	
	public VideoSuscribeWindow(GUISkin skin, RoomVariableManager rvm, Texture2D videoDef, Texture2D videoTimeOut){
		gSkin = skin;
		RoomVariableMgr = rvm;
		windowsCreated = false;
		adviceMessage = "No se selecciono usuario";
		endSuscriptionMessage = "Terminar suscripcion";		
		endSuscriptionQuestion = "Desea terminar la suscripcion?";		
		videoFrame = new VidFrame(false);
		videoRunning = false;
		adviceWindowOn = false;
		closeQuestionOn = false;
		totalWidth = 500;
		menuWidth = totalWidth/3;
		listWidth = totalWidth-menuWidth;
		totalHeight = 400;
		conectionsList = new List<GUIContent>();
		userScrollPosition = Vector2.zero;
		conectionScrollPosition = Vector2.zero;
		selectedConection = -1;
		tempSelectedConection = -1;
		selectedUser = -1;
		tempSelectedUser = -1;
		videoDefault = videoDef;
		enableWindow = false;
		timedOut = false;
		videoTimedOut = videoTimeOut;
		countDownTimer = new CountDownTimer(10);
	}

	void suscribe(){		
		CallManager.suscribe(senderIP);	
		videoRunning = true;
		setTexture(videoDefault);
	}
	
	public void setTexture(Texture2D vr){
		videoRecibido = vr;		
		countDownTimer.start();
	}
	
	public void stopVideo(){
		videoRunning = false;
		CallManager.unsuscribe(senderIP);
		RoomVariableMgr.deleteSuscriber(null);
		setTexture(videoDefault);
		videoFrame.setEnabled(true);
		countDownTimer.stop();
	}
	
	void adviceWindow(int WindowID){
		GUI.BringWindowToFront(WindowID);
		GUI.Box(new Rect(0,0,adviceWidth+20,adviceHeight*5f),"");
		GUI.Label(new Rect(10,30,adviceWidth,adviceHeight),adviceMessage);
		if (GUI.Button(new Rect(adviceWidth/2-50,adviceHeight*3f,100,adviceHeight*1.5f),"OK"))
			adviceWindowOn = false;
	}
	
	void closeVideoWindow(int WindowID){
		GUI.BringWindowToFront(WindowID);
		GUI.Box(new Rect(0,0,endSuscriptionQWidth+20,adviceHeight*5f),"");
		GUI.Label(new Rect(10,30,endSuscriptionQWidth,adviceHeight),endSuscriptionQuestion);
		if (GUI.Button(new Rect(10,adviceHeight*3f,60,adviceHeight*1.5f),"Si")){
			closeQuestionOn = false;
			stopVideo();
			setTexture(videoDefault);
		}
		if (GUI.Button(new Rect(endSuscriptionQWidth-70,adviceHeight*3f,60,adviceHeight*1.5f),"No")){
			closeQuestionOn = false;
		}
	}
	
	public void enable (bool toSet){
		enableWindow = toSet;
	}
	public void OnGUI(){			
	    GUI.skin = gSkin;
		
		ScreenW = Screen.width;
		ScreenH = Screen.height;
		
		xWindow = (ScreenW-totalWidth)/2;
		yWindow = (ScreenH-totalHeight)/2;
		xListUsers = xWindow+menuWidth;
		conectionsList = RoomVariableMgr.conectionsList;
		
		GUI.skin.GetStyle("Label").CalcMinMaxWidth(new GUIContent(adviceMessage),out minWidth,out adviceWidth);
		adviceHeight = GUI.skin.GetStyle("Label").CalcHeight(new GUIContent(adviceMessage),adviceWidth);
		GUI.skin.GetStyle("Button").CalcMinMaxWidth(new GUIContent(endSuscriptionMessage),out minWidth,out endSuscriptionMWidth);
		videoCloseHeight = GUI.skin.GetStyle("Button").CalcHeight(new GUIContent(endSuscriptionMessage),endSuscriptionMWidth);
		GUI.skin.GetStyle("Label").CalcMinMaxWidth(new GUIContent(endSuscriptionQuestion),out minWidth, out endSuscriptionQWidth);

		if (!windowsCreated){
			adviceRect = new Rect ((ScreenW-adviceWidth)/2-10,ScreenH/2-adviceHeight*3f,adviceWidth+20,adviceHeight*7f);
			endSuscriptionRect = new Rect((ScreenW-endSuscriptionQWidth)/2-10,ScreenH/2-adviceHeight*3f,endSuscriptionQWidth+20,adviceHeight*7f);			
			windowsCreated = true;
		}
		
		if (videoRunning) {
			if (GUI.Button(new Rect(ScreenW-endSuscriptionMWidth-10,ScreenH/9*2+30,endSuscriptionMWidth,videoCloseHeight),endSuscriptionMessage))
				closeQuestionOn = true;
			videoFrame.OnGUI();
			if (countDownTimer.timedOut())
				setTexture(videoTimedOut);
			videoFrame.updateData(videoRecibido);
		}
		
		if (adviceWindowOn)
			GUI.Window(1,adviceRect,adviceWindow,"");
		
		if (closeQuestionOn)
			GUI.Window(23,endSuscriptionRect,closeVideoWindow,"");
		
		if (enableWindow) {
			//dibujo ventana exterior
		
			GUI.Box (new Rect (xWindow, yWindow, totalWidth,totalHeight ), "");
			
			//dibujo marco izquierdo
			
			GUI.Box (new Rect (xWindow, yWindow, menuWidth,totalHeight ), "Videoconferencias");		
				
			float heightAux =100;		
			if (conectionsList.Count >0){
				if (selectedConection != -1){
					if (selectedConection > conectionsList.Count-1){
						tempSelectedConection = -1;
						selectedConection = -1;
					}
				heightAux = GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(conectionsList.ToArray()[0], 1f)* (conectionsList.ToArray().Length+1);						
				}
			}
			
			Rect listRect = new Rect( xWindow+5, yWindow + 30, menuWidth -25 , heightAux-10 );
		
			conectionScrollPosition = GUI.BeginScrollView(new Rect(xWindow+5, yWindow+25, menuWidth-5, totalHeight-30), conectionScrollPosition, listRect);
	 
			tempSelectedConection = GUI.SelectionGrid( listRect, tempSelectedConection, conectionsList.ToArray(), 1 ,ComboBoxStyle);
			
			GUI.EndScrollView();
			
			if(selectedConection != tempSelectedConection){
				//solo cambiar la conexion, nada mas
				selectedConection = tempSelectedConection;
				selectedUser = -1;
				tempSelectedUser = -1;
			}
	
			//dibujo marco derecho
			
			GUI.Box (new Rect (xListUsers, yWindow, listWidth,totalHeight ), "Usuarios");
			if(GUI.Button(new Rect(xWindow+totalWidth-40,yWindow+40,20,20),"<"))
				GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>().setEnableVideo(true);
			if (conectionsList.Count >0){
				if (selectedConection != -1){

				// listar los usuarios conectados a la conferencia
					List<string> suscriptores = RoomVariableMgr.getSuscribers(((GUIContent)conectionsList.ToArray()[selectedConection]).text);
					if (suscriptores.Count > 0){
						heightAux = GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(new GUIContent(suscriptores.ToArray()[0]), 1f)* (suscriptores.Count+1);							
						listRect = new Rect( xListUsers+5, yWindow + 30, listWidth -20 , heightAux-15 );				
						userScrollPosition = GUI.BeginScrollView(new Rect(xListUsers+5, yWindow+25, listWidth-5, totalHeight-50), userScrollPosition, new Rect (0,0,listWidth -25 , heightAux+50));
						heightAux = GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(new GUIContent(suscriptores[0]), 1f);
						int i = 0;
						i = GUI.SelectionGrid(new Rect(10,10,listWidth-20,heightAux*(suscriptores.Count+1)),i,suscriptores.ToArray(),1 ,ComboBoxStyle);
						GUI.EndScrollView();
					}
						
					
					if (!videoRunning)	{
						if (GUI.Button(new Rect(xListUsers+(listWidth/2)-endSuscriptionMWidth/2, yWindow+totalHeight-30, endSuscriptionMWidth, videoCloseHeight),"Suscribirse")){
							senderIP = RoomVariableMgr.getConectionIP(((GUIContent)conectionsList.ToArray()[selectedConection]).text);
							Debug.Log("se usa la ip: " +senderIP);
						//deberia empezar a recibir si hay alguien transmitiendo
							RoomVariableMgr.addSuscriber(((GUIContent)conectionsList.ToArray()[selectedConection]).text);
							suscribe();
						}
					}
					else{
						if (GUI.Button(new Rect(xListUsers+(listWidth/2)-endSuscriptionMWidth/2, yWindow+totalHeight-30, endSuscriptionMWidth, videoCloseHeight),endSuscriptionMessage))
							closeQuestionOn = true;
						if (videoFrame.enabled){
							if (GUI.Button(new Rect(xListUsers+(listWidth/2)-endSuscriptionMWidth/2, yWindow+totalHeight-60, endSuscriptionMWidth, videoCloseHeight),"Cerrar video"))
								videoFrame.setEnabled(false);
						}else{
							if (GUI.Button(new Rect(xListUsers+(listWidth/2)-endSuscriptionMWidth/2, yWindow+totalHeight-60, endSuscriptionMWidth, videoCloseHeight),"Mostrar video"))
								videoFrame.setEnabled(true);
						}
					}
				}
			
			}
		

		}

		
	}
	
	public int getConectionsCount(){
		return conectionsList.Count;
	}
}
