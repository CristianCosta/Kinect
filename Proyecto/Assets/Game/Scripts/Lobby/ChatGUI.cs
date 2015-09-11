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
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class ChatGUI : MonoBehaviour , IWindow{
	
	private SmartFox smartFox;
	
	public GUITexture chatButton;
		
	public Texture2D [] frames;
	public float framesPerSecond = 10.0f;
	public Texture2D hideChat;
	public Texture2D showChat;
	GUIStyle fbStyle;
	GUIStyle fbStyle1;
	GUIStyle fbStyle2;
	public static Boolean logedfb= false;
	public static Boolean loggp= false;
	public static Boolean logedtw= false;
	GUIStyle gpstyle;
	GUIStyle gpstyle1;
	GUIStyle gpstyle2;
	GUIStyle twitStyle;
	GUIStyle twitStyle1;
	GUIStyle twitStyle2;
	
	private string newMessage = "";
	private ArrayList messages = new ArrayList();
	// Locker to use for messages collection to ensure its cross-thread safety
	private System.Object messagesLocker = new System.Object();
	private Vector2 chatScrollPosition;
	public GUISkin gSkin;
	
	public static bool enableChat = false;
	private OauthServiceConnector oauth2ServiceConnector;
	private OauthServiceConnector oauth1ServiceConnector;
	
    private static bool nuevoMensaje = false;
	public bool enNavegador=false;
	public bool estadoAnterior=false;
	private static ChatGUI instance;
	public static ChatGUI Instace {
		get {
			return instance;
		}
	}
	
	void Awake(){
		instance = this;	
	}
	
	
	void Start(){
	        //reemplazar el seteo por la funcion que se deba aplicar para saber el estado
		
		this.oauth1ServiceConnector= new OauthServiceConnector(SocialNetworkConfig.getConfig(ConfigurationConstants.SERVER_URL_OAUTH1));
		this.oauth2ServiceConnector= new OauthServiceConnector(SocialNetworkConfig.getConfig(ConfigurationConstants.SERVER_URL_OAUTH2));

	
		
		chatScrollPosition = Vector2.zero;
		//bool debug = false;
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
			smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
		}
	
	
	}
		
	void FixedUpdate() {
		
		int screenH = Screen.height;
		int screenW = Screen.width;
		chatButton.transform.position = new Vector3(.5f,.5f,.0f);
		Rect newInset = new Rect(screenW/2-screenH/9-10,0,screenH/9,screenH/9);
		chatButton.pixelInset = newInset; 	
		
		if (smartFox != null)
			smartFox.ProcessEvents();
	}

 
	
	void OnPublicMessage(BaseEvent evt) {
		try {
			string message = (string)evt.Params["message"];
			User sender = (User)evt.Params["sender"];
			// We use lock here to ensure cross-thread safety on the messages collection 
			lock (messagesLocker) {
				messages.Add(sender.Name + " dijo: " + message);
			}
			
			chatScrollPosition.y = Mathf.Infinity;
			//Debug.Log("User " + sender.Name + " dijo: " + message + "- nameGame " + NetworkManager.Instance.getName());
				
		//	if (!sender.Name.Equals(NetworkManager.Instance.getName())){
				nuevoMensaje = true;	
			//}
		}
		catch (Exception ex) {
			Debug.Log("Exception handling public message: "+ex.Message+ex.StackTrace);
		}
	}
	
public void updateChat(){
	//	enableChat = !enableChat;
		if (enableChat){


				((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(this, "chat");
			chatButton.texture = showChat;
		}
		else{
			((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).show(this, "chat");
			chatButton.texture = hideChat;}

	}
	
	private void cambiarIcono(){
	
		float index = Time.time * framesPerSecond;
		int pos = Convert.ToInt32( index) % frames.Length;
		chatButton.texture = frames[pos];
		
	}
	
	void Update(){
		
		if (Input.GetKeyDown(KeyCode.Return) && !enableChat && !enNavegador){

			((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).show(this, "chat");
			chatButton.texture = hideChat;
		}
		if (Input.GetMouseButtonDown(1) && enableChat){

			((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(this, "chat");
			chatButton.texture = showChat;		
		}
		if (Input.touchCount > 0)
			for (int i = 0; i < Input.touchCount; i++){
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began &&
				chatButton.HitTest(touch.position)){
					updateChat();
				}
			}
	}
	
	void OnMouseDown(){
		updateChat();
	}
	
	// Finally draw all the lobby GUI
	void OnGUI()
	{  if (KeyControl.Instance != null){
                       if (KeyControl.Instance.isLaserEnabledVS() && chatButton.enabled)
                               chatButton.enabled = false;
                       
                       if (!KeyControl.Instance.isLaserEnabledVS() && !chatButton.enabled)
                                       chatButton.enabled = true;
               }

		if (smartFox == null) return;
		GUI.skin = gSkin;
		fbStyle1 = GUI.skin.GetStyle("fbon");
		fbStyle2 = GUI.skin.GetStyle("fboff");
		gpstyle1 = GUI.skin.GetStyle ("gpoff");
		gpstyle2 = GUI.skin.GetStyle ("gpon");
		twitStyle1= GUI.skin.GetStyle("twitoff");
		twitStyle2= GUI.skin.GetStyle("twiton");
		int screenW = Screen.width;
		int screenH = Screen.height;
		
		if(logedfb)
			fbStyle = fbStyle1;
		else
			fbStyle = fbStyle2;
		
		if(loggp)
			gpstyle = gpstyle2;
		else
			gpstyle = gpstyle1;

		if(logedtw)
			twitStyle=twitStyle2;
		else
			twitStyle=twitStyle1;

		//RootMotionCharacter.runAnimation = "";
		if (enableChat){
			RootMotionCharacter.runAnimation = "chatear";
		//	EmoticonInfo.posEmotionIcon = 1;
			nuevoMensaje = false;	
			// Chat history
			GUI.Box(new Rect(screenW/2 - (screenW /3)/2,screenH -(screenH *2/5) - 25, screenW /3 , (screenH *2/5) - 25), "Chat");
			
			GUILayout.BeginArea (new Rect(screenW/2 - (screenW /3)/2 + 10, screenH -(screenH *2/5) , screenW /3 -20, (screenH *2/5) - 40));
				chatScrollPosition = GUILayout.BeginScrollView (chatScrollPosition, GUILayout.Width (screenW /3 -20), GUILayout.Height ((screenH *2/5) - 55));
					// We use lock here to ensure cross-thread safety on the messages collection 
					lock (messagesLocker) {
						foreach (string message in messages)
							GUILayout.Label(message);
					}
				GUILayout.EndScrollView ();
			GUILayout.EndArea();
	
			// Send message
			newMessage = GUI.TextField(new Rect(screenW/2 - (screenW /3)/2, screenH - 29, screenW /3 - 75, 20), newMessage, 75);
			if (GUI.Button(new Rect(screenW/2 + (screenW /3 -45)/2 -38 ,screenH - 30 , 80, 28), "Enviar")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
			{
				smartFox.Send( new PublicMessageRequest(newMessage) );
				newMessage = "";
			}
			
			if (GUI.Button(new Rect(screenW/2 + (screenW /3)/2 +7 ,screenH -(screenH *2/5), screenH /10 , screenH /10),"",fbStyle)){
				
				facebookButtonClicked();
			}

			if (GUI.Button(new Rect(screenW/2 + (screenW /3)/2 +7, screenH -(screenH *2/5) +7 +screenH /10, screenH /10 , screenH /10),"",twitStyle)){
			
				twitterButtonClicked();
			}			
		}else{
				
			if (nuevoMensaje)
				cambiarIcono();
		
			if (RootMotionCharacter.runAnimation.Equals("chatear")){
				//EmoticonInfo.posEmotionIcon = 0;
				RootMotionCharacter.runAnimation = "";
			}
		}
					
	}
	
	//------------------------------------------------





	
	public void postCommentF(ISFSObject sFSObject){

		
		string data = SFSExtensionManager.getInstance().dataToString(sFSObject);

		this.oauth2ServiceConnector.postRequest(ConfigurationConstants.FACEBOOK, "https://graph.facebook.com/me/feed", data, "message=" + newMessage + "&link=http://ing.exa.unicen.edu.ar:8080/U3DWebServices/iFrame&name=U3D&type=link&caption=U3D", sucessComment);
		newMessage= "";
		smartFox.Send( new PublicMessageRequest("Se publico en Facebook!") );
	}
			
	public void sucessComment(string data){
		Debug.Log(data);
	}
	
	public void facebookButtonClicked(){
		if (logedfb) {	
			TokenManager.getInstance().getToken(LobbyGUI.user, "Facebook", postCommentF);
		}
	}
	
	//------------------------------------------------
	
	
	//------------------------------------------------
	
	/*public void getInformation(ISFSObject iSFSObject){
		string data = SFSExtensionManager.getInstance().dataToString(iSFSObject);
		//this.oauth2ServiceConnector.getRequest(LobbyGUI.GooglePlusConfiguration, "https://www.googleapis.com/plus/v1/people/me", data, sucessInformation);
	}*/
	
	//------------------------------------------------
	
	
	//------------------------------------------------
	// Twitter
	
	public void postCommentT(ISFSObject sFSObject){
		
		string data = SFSExtensionManager.getInstance().dataToString(sFSObject);
		
		this.oauth1ServiceConnector.postRequest(ConfigurationConstants.TWITTER, "https://api.twitter.com/1.1/statuses/update.json", data, "status=" + newMessage, sucessComment);
		newMessage= "";
		smartFox.Send( new PublicMessageRequest("Se publico en Twitter!") );
	}
	
	public void twitterButtonClicked() {
		if (logedtw) {
			TokenManager.getInstance().getToken(LobbyGUI.user, "Twitter", postCommentT);
		}
	}
	//-------------------------------------------------
	
	public void sucessInformation(string data){
		smartFox.Send( new PublicMessageRequest(data) );
		Debug.Log(data);
	}
	public void hide(){
		enableChat = false;
	}
	public void show(){
		enableChat = true;
	}
	public bool isVisible(){
		return enableChat;
	}
	
}
