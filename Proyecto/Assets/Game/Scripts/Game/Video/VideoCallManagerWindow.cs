using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;

public class VideoCallManagerWindow : MonoBehaviour , IWindow{

	// Use this for initialization
	private SmartFox smartFox;
	private static JPGConector jpgc;
	private Room currentActiveRoom;
	private Texture2D videoButton;
	public Texture2D videoCloseButton;
	public Texture2D hideVideo;
	public Texture2D showVideo;
	private bool enableVideo = false;
	private int screenH;
	private int screenW;
	private VidFrame videoFrame;
	private VidFrame myVideoFrame;
	private Texture2D myCaptureTexture;
	private WebCamTexture webcam;
	private Color32[] pix;
	private Texture2D texturaVideo;
	private byte[] bytes;
	private bool videoRunning;
	private bool videoTransmitting;
	private Rect videoRect;
	private Rect adviceWindow;
	private bool adviceCloseOn;
	private User us;
	private Texture2D videoRecibido;
	private bool cantTransfer, authorized;
	private bool endTransferOn;
	private RoomVariableManager RoomVariableMgr;
	
	private string senderIP;
	private string tempIP;
	private Rect videoRequestWindow;
	private string senderName;
	private User sender;
	private bool waitingResponse;
	private User receiver;
	private Rect endSuscriptionRect;
	private Rect cantTransferRect;
	private Rect endTransferenceRect;
	
	public GUISkin gSkin;
	private string title;
	
	//variables de dimension del panel de conexiones
	private int xPanel,yPanel,widthPanel,heightPanel;
			
	private const string ComboBoxStyle = "ComboBox";
	
	//tamaños de las cajas
	private float adviceHeight;
	private float minWidth;
	private float videoCloseHeight;
	private float closeQuestionWidth;
	private float endTransferenceWidth;
	private float endTransferenceQuestionWidth;
	private bool windowsCreated;
	private string cantTransferMessage;
	private string endTransferenceMessage;
	private string endTransferenceQuestion;
	private string startCallMessage;
	//textura de video cuando no hay conexion
	public Texture2D videoDefault;
	public Texture2D videoTimeOut;
	public Texture2D myVideoDefault;
	private Texture2D myVideoTexture;
	private VideoSuscribeWindow videoSuscribeWindow;
	private bool videoSuscribeWindowOn;
	private bool buttonEnable;
	
	void Awake(){

	
	}
		
	// Use this for initialization
	void Start () {
		
		senderIP = "127.0.0.1";
		cantTransferMessage = "No se puede transmitir porque no autorizo la camara web.";
		endTransferenceMessage = "Terminar transferencia";
		endTransferenceQuestion = "Desea terminar la transferencia?";
		startCallMessage = "Iniciar llamada";
		windowsCreated = false;
		videoTransmitting = false;
		videoRunning = false;
		endTransferOn = false;
		videoSuscribeWindowOn = false;		
		authorized = true;
		
		widthPanel = 450;
		heightPanel = 180;
				
		if (SmartFoxConnection.IsInitialized){
			smartFox = SmartFoxConnection.Connection;
			currentActiveRoom = smartFox.LastJoinedRoom;	
			RoomVariableMgr = new RoomVariableManager(smartFox,currentActiveRoom);
		}
		webcam = new WebCamTexture(320,240,10);
		pix = new Color32 [webcam.width * webcam.height];
		texturaVideo = new Texture2D(320,240,TextureFormat.RGB24,false);
		myVideoTexture = new Texture2D(320,240,TextureFormat.RGB24,false);
		videoRect = new Rect(screenH/9,screenW/9,320,240);
		videoFrame = new VidFrame(false);
		myVideoFrame = new VidFrame(true);
		jpgc = GameObject.Find("VideoProcessor").GetComponent<JPGConector>();
		videoSuscribeWindow = new VideoSuscribeWindow(gSkin, RoomVariableMgr, videoDefault, videoTimeOut);
		buttonEnable=true;

	}
	
	
	void Update () {
		if (enableVideo || videoSuscribeWindowOn)
			videoButton = hideVideo;
		else
			videoButton = showVideo;
	
	}
	
	public void setMyVideo(byte[] videoCapture){
		if (videoTransmitting){
			myVideoTexture.LoadImage(videoCapture);
			Debug.Log(videoCapture[0]);
			myVideoTexture.Apply();
			myVideoFrame.updateData(myVideoTexture);
		}
	}
	
	public void setEnableVideo(bool toSet){
		enableVideo = toSet;
		videoSuscribeWindowOn = false;
		videoSuscribeWindow.enable(false);
	}
	
	public void stopVideo(){
		videoSuscribeWindow.stopVideo();
	}
	
	void startTransmitting(){
		jpgc.enabled = true;
		if (!cantTransfer && authorized){
			videoTransmitting = true;
			CallManager.initiateCall();
			jpgc.play();
		} else {
			cantTransfer = true;
		}
		myVideoFrame.updateData(myVideoDefault);
	}
	
	public void stopTransmitting(){
		jpgc.enabled = false;
		CallManager.terminateCall();
		videoTransmitting = false;
		RoomVariableMgr.deleteConection();
		myVideoFrame.setEnabled(true);
	}
	
	void endTransferenceWindow(int WindowID){
		GUI.BringWindowToFront(WindowID);
		GUI.Box(new Rect(0,0,endTransferenceQuestionWidth+20,adviceHeight*5f),"");
		GUI.Label(new Rect(10,30,endTransferenceQuestionWidth,adviceHeight),endTransferenceQuestion);
		if (GUI.Button(new Rect(10,adviceHeight*3f,60,videoCloseHeight*1.5f),"Si")){
			endTransferOn = false;
			stopTransmitting();
		}
		if (GUI.Button(new Rect(endTransferenceQuestionWidth-70,adviceHeight*3f,60,videoCloseHeight*1.5f),"No")){
			endTransferOn = false;
		}
	}
	
	
	void cantTransferWindow (int WindowID){
		GUI.BringWindowToFront (WindowID);
		GUI.Box (new Rect(0,0,endTransferenceQuestionWidth*1.2f,adviceHeight*4f+10),"");
		GUI.Label(new Rect(20,30,endTransferenceQuestionWidth*1.2f-40,adviceHeight*3f),cantTransferMessage);
		if (GUI.Button(new Rect(endTransferenceQuestionWidth/2-30,adviceHeight*4f-videoCloseHeight,60,videoCloseHeight),"OK"))
			cantTransfer = false;
	}
	
	public void setTexture(Texture2D vr){
		videoSuscribeWindow.setTexture(vr);
	}
	
	public void setCantTransfer(bool toSet){
		if (toSet){
			cantTransfer = toSet;
			authorized = !toSet;
			if (videoTransmitting){
				stopTransmitting();
			}
		} 
	}
	
	void OnGUI(){
		 if (KeyControl.Instance != null){
                       if (KeyControl.Instance.isLaserEnabledVS() && buttonEnable)
                               buttonEnable = false;
                       
                       if (!KeyControl.Instance.isLaserEnabledVS() && !buttonEnable)
                                       buttonEnable = true;
               }

		if (smartFox == null) return;	
		xPanel=(screenW-widthPanel)/2;
		yPanel=(screenH-heightPanel)/2;
		screenH = Screen.height;
		screenW = Screen.width;
	    GUI.skin = gSkin;

		GUI.skin.GetStyle("Label").CalcMinMaxWidth(new GUIContent(endTransferenceQuestion),out minWidth, out endTransferenceQuestionWidth);
		adviceHeight = GUI.skin.GetStyle("Label").CalcHeight(new GUIContent(endTransferenceQuestion),endTransferenceQuestionWidth);
		GUI.skin.GetStyle("Button").CalcMinMaxWidth(new GUIContent(endTransferenceMessage),out minWidth, out endTransferenceWidth);
		videoCloseHeight = GUI.skin.GetStyle("Button").CalcHeight(new GUIContent(endTransferenceMessage),endTransferenceWidth);
		
		if (!windowsCreated){
			endTransferenceRect = new Rect((screenW-endTransferenceQuestionWidth)/2-10,screenH/2-adviceHeight*3f,endTransferenceQuestionWidth+20,adviceHeight*7f);
			cantTransferRect = new Rect ((screenW-endTransferenceQuestionWidth*1.2f)/2-10,screenH/2-adviceHeight*2f,endTransferenceQuestionWidth*1.2f,adviceHeight*4f+10);
			windowsCreated = true;
		}
		
		videoSuscribeWindow.OnGUI();
		if(buttonEnable){
		if (GUI.Button(new Rect(screenW-screenH/9-10,screenH/9+30,screenH/9,screenH/9),videoButton, GUI.skin.GetStyle("VideoButton"))){
			//conectar con el server, y si anda hacer lo de abajo.			
			if (videoSuscribeWindowOn){
				Debug.Log("en el if");
				videoSuscribeWindowOn = false;
				videoSuscribeWindow.enable(false);
			} else {
				Debug.Log("en el else");
				if(isVisible())
					((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(this, "video llamada");
				else
					((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).show(this, "video llamada");
				GameObject.Find("VideoProcessor").GetComponent<TutorialWindow>().enabled = false;
			}
			
		}}
		
		
		if (videoTransmitting){			
			GUI.Label(new Rect(screenW-190,10,180,20),"Transmitiendo...",GUI.skin.GetStyle("Button"));
			if (GUI.Button(new Rect(screenW-endTransferenceWidth-10,screenH/9*2+35+videoCloseHeight,endTransferenceWidth,videoCloseHeight),endTransferenceMessage))
				endTransferOn = true;
			myVideoFrame.OnGUI();
		}

	        
		if (cantTransfer)
			GUI.Window(5,cantTransferRect,cantTransferWindow,"");
		
		if (endTransferOn)
			GUI.Window (6,endTransferenceRect,endTransferenceWindow,"");
		
		if (enableVideo) {
			//dibujo ventana exterior
			
			int conectionsCount = videoSuscribeWindow.getConectionsCount();
			GUI.Box (new Rect(xPanel,yPanel,widthPanel,heightPanel),"");
			if (GUI.Button(new Rect(xPanel+10,yPanel+heightPanel-100,endTransferenceWidth,videoCloseHeight),"Configuracion")){
				//GameConfiguration gc = GameObject.Find("Configuration").GetComponent<GameConfiguration>();
				GameConfiguration.Instace.setCurrentPanel(3);
				//gc.setCurrentPanel(3);
				((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).show(GameConfiguration.Instace, "gameConfiguration");
			}
			if (GUI.Button(new Rect(xPanel+10,yPanel+heightPanel-130,endTransferenceWidth,videoCloseHeight),"Tutorial")){
				enableVideo= false;
				GameObject.Find("VideoProcessor").GetComponent<TutorialWindow>().enabled = true;
				
			}
			if (!videoTransmitting){
				if (GUI.Button(new Rect(xPanel+10, yPanel+heightPanel-40, endTransferenceWidth, videoCloseHeight),startCallMessage)){
					startTransmitting();
					if (videoTransmitting)
						RoomVariableMgr.addConection();
				}
			}
			else{
				if (!myVideoFrame.enabled){
					if (GUI.Button(new Rect(xPanel+10, yPanel+heightPanel-70, endTransferenceWidth, videoCloseHeight),"Mostrar mi video"))
						myVideoFrame.setEnabled(true);
				} else{
					if (GUI.Button(new Rect(xPanel+10, yPanel+heightPanel-70, endTransferenceWidth, videoCloseHeight),"Cerrar mi video"))
						myVideoFrame.setEnabled(false);
				}
				if (GUI.Button(new Rect(xPanel+10, yPanel+heightPanel-40, endTransferenceWidth, videoCloseHeight),endTransferenceMessage))
					endTransferOn = true;
			}
			GUI.Label(new Rect(xPanel+widthPanel-(endTransferenceWidth+20),yPanel+40,endTransferenceWidth,heightPanel-110),new GUIContent("Transmiciones activas: "+conectionsCount));
			if(!videoSuscribeWindow.videoRunning){
				if (GUI.Button(new Rect(xPanel+widthPanel-(endTransferenceWidth+20),yPanel+heightPanel-40,endTransferenceWidth,videoCloseHeight),"Suscribirse")){
					videoSuscribeWindowOn = true;
					videoSuscribeWindow.enable(true);
					enableVideo = false;
				}
			}else{
				if (GUI.Button(new Rect(xPanel+widthPanel-(endTransferenceWidth+20),yPanel+heightPanel-40,endTransferenceWidth,videoCloseHeight),"Terminar suscripcion"))
					videoSuscribeWindow.closeQuestionOn = true;		
				if (GUI.Button(new Rect(xPanel+widthPanel-(endTransferenceWidth+20), yPanel+heightPanel-70, endTransferenceWidth, videoCloseHeight),"Ver conexiones")){
					videoSuscribeWindowOn = true;					
					videoSuscribeWindow.enable(true);
					enableVideo = false;
				}
				if (videoSuscribeWindow.videoFrame.enabled){
					if (GUI.Button(new Rect(xPanel+widthPanel-(endTransferenceWidth+20), yPanel+heightPanel-100, endTransferenceWidth, videoCloseHeight),"Cerrar video"))
						videoSuscribeWindow.videoFrame.setEnabled(false);
				}else{
					if (GUI.Button(new Rect(xPanel+widthPanel-(endTransferenceWidth+20), yPanel+heightPanel-100, endTransferenceWidth, videoCloseHeight),"Mostrar video"))
						videoSuscribeWindow.videoFrame.setEnabled(true);
				}
			}

		}

		
	}
	
	public void show(){
		this.enableVideo = true;
	}
	
	public void hide(){
		this.enableVideo = false;
	}
	
	public bool isVisible(){
		return this.enableVideo;
	}
	
	
	void OnApplicationQuit(){
		CallManager.setConfigurated(false);
		if (videoSuscribeWindow.videoRunning){
			UDPReceive.stopReceiving();
			stopVideo();
		}
		if (videoTransmitting){
			stopTransmitting();
		}
	}


}
