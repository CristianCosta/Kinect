using UnityEngine;
using System;
using System.Xml;
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
using Sfs2X.Entities.Data;
//using UnityEditor;
using System.Security.Cryptography;

public class LobbyGUI : MonoBehaviour {

	/*
	public static OauthServiceConfiguration WebServiceConfiguration;
	public static SocialConfiguration FacebookConfiguration;
	public static SocialConfiguration GooglePlusConfiguration;
	
	public static int videoPort = 8051;
*/
	private SmartFox smartFox;
	public SmartFox getSmartFox()
	{return smartFox;}

	public string zone = "UNICEN_TANDIL";
	public static string user;
	
//	private string serverName = "ing.exa.unicen.edu.ar";
//	private string serverIP = "170.210.119.40";
	
//	private string serverName = "localhost";
//	private string serverName = "192.168.1.100";
	//public static string serverIP = "127.0.0.1";
	
//	public static string serverIP = "192.168.1.100";
	
//	private string serverName = "taller2012.no-ip.org";
//	private string serverIP = "190.50.106.193";
	
	//private string serverName = "ing.exa.unicen.edu.ar";
	//private string serverIP = "170.210.119.40";
	

//	public static string serverIP = "192.168.0.10";
	
	//cambio------------------------------------------------------------
	/*private string serverName = "taller.isistan.unicen.edu.ar";
	public static string serverIP = "200.5.106.56";
	public string server2IP = "10.1.4.156"; */
	//------------------------------------------------------------------
	
	
	//
	
//	public string server2IP = "10.1.4.156"; 
	//public int serverPort = 9933;
	//--------LobbyConfig.xml
	
	
	private string serverName;// = LobbyConf.getConfig(ConfigurationConstants.SERVER_NAME);
	public static string serverIP;// = LobbyConf.getConfig(ConfigurationConstants.SERVER_IP);
	public string server2IP;// = LobbyConf.getConfig(ConfigurationConstants.SERVER_2_IP);
	public int serverPort;// = Convert.ToInt32(LobbyConf.getConfig(ConfigurationConstants.SERVER_PORT));
	public static int videoPort;// = Convert.ToInt32(LobbyConf.getConfig(ConfigurationConstants.SERVER_VIDEO_PORT));
	
	private static string username = "";
	private string contrasenia = "";
	private string contraseniaHash = "";
	private string loginErrorMessage = "";
	public static bool isLoggedIn = false;
	private bool isJoining = false;
	private static bool conecto = false;
	private static bool smartfoxConnection = false;
	//private string webServicesBaseUrl= "http://ing.exa.unicen.edu.ar:8080";
		//cambio----------------------------------------------------------------------
	//private string webServicesBaseUrl= "http://taller.isistan.unicen.edu.ar:8086";
	//----------------------------------------------------------------------------
	
	private string newMessage = "";
	private ArrayList messages = new ArrayList();

	// Locker to use for messages collection to ensure its cross-thread safety
	private System.Object messagesLocker = new System.Object();
		
	public GUISkin gSkin;
	
	public static Room currentActiveRoom;
				
	private Vector2 gameScrollPosition, userScrollPosition, chatScrollPosition;
	private int roomSelection = -1;
	private string[] roomNameStrings;
	private string[] roomFullStrings;
	public static NameManager namemanager = new NameManager();
	public static int gamesConfigFilesPort = 8086;
	private bool esperandoJoin = false;
	private static bool godUser=false;
	
	void Awake() {
		Cursor.visible = true;
		Screen.lockCursor = false;
	}
	
	void Start()
	{
		Debug.Log(">>>> Start LobbyGUI");
		serverName = LobbyConf.getConfig(ConfigurationConstants.SERVER_NAME);
		serverIP = LobbyConf.getConfig(ConfigurationConstants.SERVER_IP);
		server2IP = LobbyConf.getConfig(ConfigurationConstants.SERVER_2_IP);
		serverPort = Convert.ToInt32(LobbyConf.getConfig(ConfigurationConstants.SERVER_PORT));
		videoPort = Convert.ToInt32(LobbyConf.getConfig(ConfigurationConstants.SERVER_VIDEO_PORT));
	
		//if (Application.isWebPlayer) { //ESTO NO HAY QUE HACERLO EN STANDALONE PORQUE NO SIRVE Y SI SE HACE DA ERROR Y NO PERMITE LOGGUEARSE (no se ve nada en el lobby)
			Debug.Log(serverIP + " " +server2IP + " " + serverPort);
			
			if (Security.PrefetchSocketPolicy(serverIP, serverPort, 6000)){
					Debug.Log("funco el primero! " + serverIP + " " + serverPort);
			}else if (Security.PrefetchSocketPolicy(serverIP, 8080, 6000)){
					Debug.Log("funco el segundo! " + server2IP + " " + serverPort);
			//}else{
			//		Debug.Log(" NO FUNCO NINGUNO");
			} else if (Security.PrefetchSocketPolicy(server2IP, serverPort, 6000)){
					Debug.Log("funco el primero! - red UNICEN");
					serverIP = server2IP;
			}else if (Security.PrefetchSocketPolicy(server2IP, 8080, 6000)){
					Debug.Log("funco el segundo! - red UNICEN");
					serverIP = server2IP;
			}else{
					Debug.Log(" NO FUNCO NINGUNO");
					serverIP = server2IP;
			}
		//}
		
		bool debug = false;
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
			Debug.Log("SmartFox Connected");
			smartfoxConnection= true;
		}
		else
		{
			smartFox = new SmartFox(debug);
		}
			
		smartFox.AddLogListener(LogLevel.INFO, OnDebugMessage);
		
		if (isLoggedIn){// && (currentActiveRoom!=null))
			PrepareLobby();
			currentActiveRoom = smartFox.RoomManager.GetRoomById(4);	
			AddEventListeners();

		}
		//Web Service Configuration
		
		/*WebServiceConfiguration= new OauthServiceConfiguration();
    	WebServiceConfiguration.redirectUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/callback";
	    WebServiceConfiguration.registerUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/registerClient";
        WebServiceConfiguration.authUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/authUrl";
        WebServiceConfiguration.waitAuthUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/waitForAuthCode";
        WebServiceConfiguration.accessTokenUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/getHTMLResponseAccessToken";
        WebServiceConfiguration.refreshTokenUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/getJSONResponseRefreshToken";
        WebServiceConfiguration.getRequestUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/getRequest";
        WebServiceConfiguration.postRequestUrl= webServicesBaseUrl + "/U3DWebServices/rest/oauth/postRequest";		
*/
		
	}

    public void fRequestCallback(ISFSObject iSFSObject){
		string data = SFSExtensionManager.getInstance().dataToString(iSFSObject);
		Debug.Log("Facebook Token: " + data);
		if (data != null){
			SocialNetworkWindow.facebookEnabled= true;
			SocialNetworkWindow.facebookMail= "Cuenta Asociada";
			ChatGUI.logedfb= true;
		}
	}
	
	public void gRequestCallback(ISFSObject iSFSObject){
		string data = SFSExtensionManager.getInstance().dataToString(iSFSObject);
		Debug.Log("Google Token: " + data);
		if (data != null){
			SocialNetworkWindow.googlePlusEnabled= true;
			SocialNetworkWindow.googlePlusMail= "Cuenta Asociada";
			ChatGUI.loggp= true;
		}
	}
	
	public void tRequestCallback(ISFSObject iSFSObject){
		string data = SFSExtensionManager.getInstance().dataToString(iSFSObject);
		Debug.Log("Twitter Token: " + data);
		if (data != null){
			SocialNetworkWindow.twitterEnabled = true;
			SocialNetworkWindow.twitterMail = "Cuenta Asociada";
			ChatGUI.logedtw = true;
		}
	}
	
	private void initSocialNetworks(){
		//Social Networks Configuration
		/*
		FacebookConfiguration= new SocialConfiguration();
		FacebookConfiguration.user= LobbyGUI.user + "Facebook";
		FacebookConfiguration.authServer= "https://www.facebook.com/dialog/oauth";
		//FacebookConfiguration.clientId= "283673508413047";
		//FacebookConfiguration.clientSecret= "24fa54b543f4b7e8fd08fcdbb7da83de";
		//cambio---------------------------------------------------------------------
		FacebookConfiguration.clientId= "283673508413047";
		FacebookConfiguration.clientSecret= "24fa54b543f4b7e8fd08fcdbb7da83de";
		//---------------------------------------------------------------------------
		FacebookConfiguration.scope= "publish_stream";
		FacebookConfiguration.tokenServer= "https://graph.facebook.com/oauth/access_token";
		FacebookConfiguration.redirectUri= WebServiceConfiguration.redirectUrl;
		
		GooglePlusConfiguration= new SocialConfiguration();
		GooglePlusConfiguration.user= LobbyGUI.user + "Google";
		GooglePlusConfiguration.authServer= "https://accounts.google.com/o/oauth2/auth";
		//GooglePlusConfiguration.clientId= "819134736582-duop328vs6098lnneru66ae528s9ov73.apps.googleusercontent.com";
		//GooglePlusConfiguration.clientSecret= "cY9A6sXBC7lTXxbiUoBlUnQR";
		//cambio-------------------------------------------------------------------------------------
		GooglePlusConfiguration.clientId= "435676876034-dq9qvb7fsahpmr0fh98g0tikqphuv0g6.apps.googleusercontent.com";
		GooglePlusConfiguration.clientSecret= "FVMGaOCi8vsG_QSQElvYz-_c";
		//-------------------------------------------------------------------------------------------
		GooglePlusConfiguration.scope= "https://www.googleapis.com/auth/plus.me";
		GooglePlusConfiguration.tokenServer= "https://accounts.google.com/o/oauth2/token";
		GooglePlusConfiguration.redirectUri= WebServiceConfiguration.redirectUrl;
		
		//Social networks buttons state
		// ACA PRUEBO EL GET CustomizationManager.getInstance().getTextura(LobbyGUI.user);
		// ACA PRUEBO EL INSERT CustomizationManager.getInstance().insertarTextura(LobbyGUI.user, "maxy");
*/
		Debug.Log("   >>>  initSocialNetworks: " );

		TokenManager.getInstance().getToken(LobbyGUI.user, "Facebook", fRequestCallback);
		TokenManager.getInstance().getToken(LobbyGUI.user, "GooglePlus", gRequestCallback);
		TokenManager.getInstance().getToken(LobbyGUI.user, "Twitter", tRequestCallback);
	}

	void OnExtensionResponse (BaseEvent evt)
	{
		string cmd = (string)evt.Params["cmd"];
		try {
			
			ISFSObject dt = (SFSObject)evt.Params["params"];
			Debug.Log("entroooooo "+cmd);								
			if (cmd == "isGod") {
				godUser = dt.GetBool("isGod");
			}
		}catch (Exception e) {
			Debug.Log("Exception handling "+ cmd +" response:"+e.Message+" >>> "+e.StackTrace);
		}
	}
	
	private void AddEventListeners() {
		
		//smartFox.RemoveAllEventListeners();
		
		smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		smartFox.AddEventListener(SFSEvent.LOGOUT, OnLogout);
		smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
		smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		smartFox.AddEventListener(SFSEvent.ROOM_CREATION_ERROR, OnCreateRoomError);
		smartFox.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
		smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
		smartFox.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
		smartFox.AddEventListener(SFSEvent.ROOM_REMOVE, OnRoomDeleted);
		smartFox.AddEventListener(SFSEvent.USER_COUNT_CHANGE, OnUserCountChange);
		smartFox.AddEventListener(SFSEvent.UDP_INIT, OnUdpInit);		
	}
	
	void FixedUpdate() {
		if (smartFox != null)
			smartFox.ProcessEvents();
	}
	
	private void UnregisterSFSSceneCallbacks() {
		// This should be called when switching scenes, so callbacks from the backend do not trigger code in this scene
		smartFox.RemoveAllEventListeners();
	}

	
	public void OnConnection(BaseEvent evt) {
		bool success = (bool)evt.Params["success"];
		string error = (string)evt.Params["errorMessage"];
		
		Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");
		loginErrorMessage = "On Connection callback got: " + success + " (error : <" + error + ">)";

		if (success) {

			SmartFoxConnection.Connection = smartFox;
			Debug.Log("Sending login request "+username+"  "+contraseniaHash);
			smartFox.Send(new LoginRequest(username, contraseniaHash, zone));
			user= username;
	
		}else{
			loginErrorMessage="usuarioConectado";
		}
	}

	public void OnConnectionLost(BaseEvent evt) {
		Debug.Log("OnConnectionLost");
		isLoggedIn = false;
		isJoining = false;
		conecto = false;
		loginErrorMessage = "";
		currentActiveRoom = null;
		UnregisterSFSSceneCallbacks();
	}
	
	void OnApplicationQuit(){
		Debug.Log("OnConnectionQUIT");
		isLoggedIn = false;
		isJoining = false;
		conecto = false;
		loginErrorMessage = "";
		currentActiveRoom = null;
		smartFox.Disconnect();
	}	

	// Various SFS callbacks
	public void OnLogin(BaseEvent evt) {
		try {
			if (!evt.Params.ContainsKey("success") || (bool)evt.Params["success"]){
						
				Debug.Log("Logged in successfully");
				// Startup up UDP
				smartFox.InitUDP(serverIP, serverPort);
				initSocialNetworks();
				smartFox.Send(new ExtensionRequest("isGod",new SFSObject()));
			}
		}
		catch (Exception ex) {
			Debug.Log("Exception handling login request: "+ex.Message+" "+ex.StackTrace);
		}
	}

	public void OnLoginError(BaseEvent evt) {
		string msg  = (string)evt.Params["errorMessage"];
		Debug.Log("Login error: "+ msg);
		//loginErrorMessage=(string)evt.Params["errorMessage"];
		if (msg.Contains("is already logged in"))
			loginErrorMessage = "Usuario ya conectado";
		else
			loginErrorMessage="Nick/Password Incorrecto";		
	}
	
	public void OnUdpInit(BaseEvent evt) {
		if (evt.Params.ContainsKey("success") && !(bool)evt.Params["success"]) {
			loginErrorMessage = (string)evt.Params["errorMessage"];
			Debug.Log("UDP error: "+loginErrorMessage);
		} else {
			Debug.Log("UDP ok");
			PrepareLobby();	
		}
		//PrepareLobby();
	}
	
	void OnLogout(BaseEvent evt) {
		Debug.Log("OnLogout");
		isLoggedIn = false;
		isJoining = false;
		conecto = false;
		loginErrorMessage = "";
		currentActiveRoom = null;
		smartFox.Disconnect();
	}
	
	public void OnDebugMessage(BaseEvent evt) {
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}

	public void OnJoinRoom(BaseEvent evt)
	{
		
		Room room = (Room)evt.Params["room"];
		currentActiveRoom = room;
		// If we joined a game room, then we either created it (and auto joined) or manual1ly selected a game to join
		if (room.IsGame) {
			Debug.Log ("Joined game room " + room.Name);
			UnregisterSFSSceneCallbacks();
			Debug.Log("Estoy entrando a: " + room.Name);
			if((room.Name.Equals("mercurio"))||(room.Name.Equals("neptuno")))Application.LoadLevel("principal");
			else Application.LoadLevel(room.Name);

		}
		else if (room.Name.Equals("VSConfig")){
			UnregisterSFSSceneCallbacks();
			Application.LoadLevel(room.Name);
		}
		
		
	}

	
	public void OnCreateRoomError(BaseEvent evt) {
		string error = (string)evt.Params["errorMessage"];
		Debug.Log("Room creation error; the following error occurred: " + error);
	}

	public void OnUserEnterRoom(BaseEvent evt) {
		User user = (User)evt.Params["user"];
		lock (messagesLocker) {
			messages.Add(user.Name + " joined room");
		}
	}

	private void OnUserLeaveRoom(BaseEvent evt) {
		User user = (User)evt.Params["user"];
		lock (messagesLocker) {
			messages.Add(user.Name + " left room");
		}
	}

	public void OnRoomAdded(BaseEvent evt) {
		Room room = (Room)evt.Params["room"];
		// Update view (only if room is game)
		if ( room.IsGame ) {
			SetupRoomList();
		}
	}
	
	public void OnUserCountChange(BaseEvent evt) {
		Room room = (Room)evt.Params["room"];
		if (room.IsGame ) {
			SetupRoomList();
		}
	}

	/*
	* Handle a room that was removed
	*/
	public void OnRoomDeleted(BaseEvent evt) {
		SetupRoomList();
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
			Debug.Log("User " + sender.Name + " dijo: " + message);
		}
		catch (Exception ex) {
			Debug.Log("Exception handling public message: "+ex.Message+ex.StackTrace);
		}
	}

	void androidGUI(){
		
		GUI.skin = gSkin;
		
		int screenW = Screen.width;
		int screenH = Screen.height;
			
		GUI.Label(new Rect(screenW/2-(Math.Min(screenW-100,512))/2, 10, 
		          (Math.Min(screenW-100,512)), (int)(Math.Min(screenW-100,512)*.25)), "", "U3DLogoMobile");	
		
				
		// Login
		if (!isLoggedIn) {
			
			GUI.Label(new Rect(screenW/2-screenW/3, screenH/4, screenW*2/3, 25), "Nick");
			username = GUI.TextField(new Rect(screenW/2-screenW/3, screenH/4+30, screenW*2/3, 25), username, 25);
			
			GUI.Label(new Rect(screenW/2-screenW/3, screenH/4+60, screenW*2/3, 25), "ContraseÃ±a");
			contrasenia = GUI.PasswordField(new Rect(screenW/2-screenW/3, screenH/4+90, screenW*2/3,25), contrasenia, '*');
			
			GUI.Label(new Rect(screenW/8, screenH/4+180, 300, 100), loginErrorMessage);

			if (GUI.Button(new Rect(screenW/2-(screenW/4)/2, screenH/4+120, screenW/4, 30), "Login")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n')){
				smartFox.Connect(serverName, serverPort);
				AddEventListeners();
				
			}
				
					
		}
		else if (isJoining)
		{
			// Standard view
			if (GUI.Button(new Rect(580, 478, 90, 24), "Salir"))
			{
				smartFox.Send( new LogoutRequest() );
			}

		}
		else if (currentActiveRoom!=null)
		{
			
			// Game room list		
			GUI.Box (new Rect (10, 120 , screenW - 20, screenH - 200), "Salas Disponibles");
			if (smartFox.RoomList.Count != 1) {
				roomSelection = GUI.SelectionGrid(new Rect(20, 150, screenW - 40, screenH - 250),roomSelection, roomFullStrings, 1);
				if (roomSelection >= 0 && roomNameStrings[roomSelection] != currentActiveRoom.Name) {
					smartFox.Send(new JoinRoomRequest(roomNameStrings[roomSelection], null, smartFox.LastJoinedRoom.Id));
				}				
			} else {
				GUI.Label (new Rect(20, 150, screenW - 80, screenH - 250),"No existen salas disponibles");
			}
			
			// Standard view
			if (GUI.Button (new Rect (screenW /2 - 50, screenH - 60, 100, 50), "Salir")) {
				smartFox.Send( new LogoutRequest());
			}
		}
	}
	
	
	// Finally draw all the lobby GUI
	void OnGUI()
	{
		int screenW = Screen.width;
		int screenH = Screen.height;
		//Debug.Log("current active room"+currentActiveRoom);
	
		//GUI.Label(new Rect(10, 10, 1000, 650), loginErrorMessage);
		//if (GameConfiguration.Instace.enabled)
		//	return;
		
		if (smartFox == null) {loginErrorMessage = "hay algo mal que no anda bien";return;}
				GUI.skin = gSkin;
		
		
		GUI.Label(new Rect(10,screenH-screenH/3, 
		          screenH/3, screenH/3), "", "UNICENLogo");		
		
		GUI.Label(new Rect(2*screenW/3,screenH-screenW/3/2, 
		          screenW/3, screenW/3/2), "", "ISISTANLogo");
		
		
		
		if (Application.platform == RuntimePlatform.Android){
			androidGUI();
			return;	
		} 
	else 
		{

//			GUI.Label(new Rect(screenW/2-(Math.Min(screenW-100,512))/2, 10, 
//		          (Math.Min(screenW-100,512)), (int)(Math.Min(screenW-100,512)*.5)), "", "U3DLogo");	

//			GUI.Label(new Rect(screenW/2-(Math.Min(screenW-100,512))/2, 10, 
//			                   (Math.Min(screenW-100,512)), (int)(Math.Min(screenW-100,512)*.5)), "", "VirtualScrumLogo");	

			GameObject vsLogo = GameObject.Find("VirtualScrumLogo");
			GUITexture vsTexture = (GUITexture) vsLogo.GetComponent("GUITexture");
			vsLogo.transform.position = new Vector3( (screenW/2-vsTexture.pixelInset.width/2)/screenW , (screenH/2-vsTexture.pixelInset.height/2)/screenH, 0f);

			GameObject vsBackground = GameObject.Find("VirtualScrumBackground");
			GUITexture vsBackgroundTexture = (GUITexture) vsBackground.GetComponent("GUITexture");
			vsBackgroundTexture.pixelInset = new Rect(0,0,screenW,screenH);

					
			if (!isLoggedIn) {	
				
				if (loginErrorMessage.Equals("Nick/Password Incorrecto") || loginErrorMessage.Equals("Usuario ya conectado")){	
				// Error list
					GUI.Box (new Rect (screenW/2 - screenW/6, 2*screenH/3 - 100, screenW/3, 170), "Error");
					
					GUILayout.BeginArea (new Rect (screenW/2 - screenW/6 + 10, 2*screenH/3 - 80, screenW/2, 160));
						userScrollPosition = GUILayout.BeginScrollView (userScrollPosition, GUILayout.Width (screenW/2-20), GUILayout.Height (160));
						GUILayout.BeginVertical ();
					
						GUILayout.Label (loginErrorMessage);
							
						GUILayout.EndVertical ();
						GUILayout.EndScrollView ();
					GUILayout.EndArea ();
				
				if (GUI.Button(new Rect(screenW/2 - 50, 2*screenH/3+90, 100, 24), "Aceptar") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
					loginErrorMessage = "";	
				//EditorUtility.DisplayDialog("Error", "Usuario y/o Pass Invalido/s", "Ok","Cancelar");
			}else
						
				
			 if (loginErrorMessage.Equals("")){
				
				GUI.Label(new Rect(screenW/2 - 200,2*screenH/3, screenW/4, 50), "Nick: ");
				username = GUI.TextField(new Rect(screenW/2-100, 2*screenH/3, 200, 25), username, 25);
				
				GUI.Label(new Rect(screenW/2 - 200, 2*screenH/3+30,screenW/4, 50), "Password: ");
				Byte[] originalBytes;
				Byte[] encodedBytes;
				MD5 md5;
				contrasenia = GUI.PasswordField(new Rect(screenW/2-100, 2*screenH/3+30, 200, 25), contrasenia, '*');
				//Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
				md5 = new MD5CryptoServiceProvider();
				originalBytes = ASCIIEncoding.Default.GetBytes(contrasenia);
				encodedBytes = md5.ComputeHash(originalBytes);
					
				//Convert encoded bytes back to a 'readable' string
				StringBuilder sBuilder = new StringBuilder();
					
				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < encodedBytes.Length; i++)
				{
					sBuilder.Append(encodedBytes[i].ToString("x2"));
				}
				
				// Return the hexadecimal string.
				

				contraseniaHash = sBuilder.ToString();
				
				//GUI.Label(new Rect(screenW/2 - 200, 2*screenH/3+60, screenW/4, 50), "Invertir Mouse Y: ");
				//GameConfiguration.Instace.InvertMouseY = 
				//	GUI.Toggle(new Rect(screenW/2-100, 2*screenH/3+60, 200, 20), GameConfiguration.Instace.InvertMouseY, "");
				
			
				if (GUI.Button(new Rect(screenW/2 - 50, 2*screenH/3+90, 100, 24), "Login")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n')){
					AddEventListeners();
					
					if (conecto){
						Debug.Log("asdasdsadads");
						conecto = false;
						smartFox.Send(new LoginRequest(username, contraseniaHash, zone));
					}
					else {
						Debug.Log("Rama else");
						
						smartFox.Connect(serverIP, serverPort);
						//Debug.Log("is conected = " + smartFox.IsConnected);
						if (smartFox.IsConnected){
							conecto = true;		
							//smartFox.Send(new LoginRequest(username, contrasenia, zone));
						}else if (smartFox.IsConnecting){
								Debug.Log("Conectando" + serverIP + " " + serverPort);
							} else{
								Debug.Log("No Conecto" + serverIP + " " + serverPort);
							}
					}
				}
							
				//	CustomizationManager.getInstance().getAvatar(LobbyGUI.user,tRequestCallback);
						
			  }
			}
			else if (isJoining)
			{
				// Standard view
				if (GUI.Button(new Rect(580, 478, 90,25), "Salir"))
				{
					smartFox.Send( new LogoutRequest() );
				}
	
			}
			else if (currentActiveRoom!=null)
			{
								
				// User list
				GUI.Box (new Rect (screenW - screenW/3,130, screenW/3, 170), "Usuarios");
				
				GUILayout.BeginArea (new Rect (screenW - screenW/3+10, 145, screenW/3-20, 160));
					userScrollPosition = GUILayout.BeginScrollView (userScrollPosition, GUILayout.Width (screenW/3-20), GUILayout.Height (160));
					GUILayout.BeginVertical ();
				
						List<User> userList = currentActiveRoom.UserList;
				
						foreach (User user in userList) {
							GUILayout.Label (user.Name);
						}
					GUILayout.EndVertical ();
					GUILayout.EndScrollView ();
				GUILayout.EndArea ();
				
				
				// Game room list
				GUI.Box (new Rect (screenW - screenW/3, 300, screenW/3, 220), "Salas Disponibles");
				GUILayout.BeginArea (new Rect (screenW - screenW/3+10, 340, screenW/3-20, 200));
				if (smartFox.RoomList.Count != 1) {
					// We always have 1 non-game room - Main Lobby
					gameScrollPosition = GUILayout.BeginScrollView (gameScrollPosition, GUILayout.Width (screenW/3-20), GUILayout.Height (160));
					
					roomSelection = GUILayout.SelectionGrid (roomSelection, roomFullStrings, 1, "RoomListButton");
					if (!esperandoJoin && roomSelection >= 0 && roomNameStrings[roomSelection] != currentActiveRoom.Name) {
						smartFox.Send(new JoinRoomRequest(roomNameStrings[roomSelection], null, smartFox.LastJoinedRoom.Id));
						esperandoJoin=true;
					}
					GUILayout.EndScrollView ();
					
				} else {
					GUILayout.Label ("No existen salas disponibles");
				}
				GUILayout.EndArea ();
				
			
				
				// Standard view
				if (GUI.Button (new Rect (screenW - 105, 90, 85, 28), "Salir")) {
					smartFox.Send( new LogoutRequest());
				}
				
				// Camarin view
				if (GUI.Button (new Rect (screenW - 225, 90, 100, 28), "Customizar")) {
					//smartFox.Send( new LogoutRequest());
					//Debug.Log("custom");
					smartFox.Send(new JoinRoomRequest("Camarin", null, smartFox.LastJoinedRoom.Id));					
				}

				if (godUser){
					if (GUI.Button (new Rect (screenW - 375, 90, 130, 28), "Configurar VS")) {
						smartFox.Send(new JoinRoomRequest("VSConfig", null, smartFox.LastJoinedRoom.Id));	
					}
				}
							
				// Chat history
				GUI.Box(new Rect(10, 130, screenW*3/5, 390), "Chat");
	
				GUILayout.BeginArea (new Rect(20, 160, screenW*3/5, 350));
					chatScrollPosition = GUILayout.BeginScrollView (chatScrollPosition, GUILayout.Width (screenW*3/5), GUILayout.Height (350));
						GUILayout.BeginVertical();
						
						// We use lock here to ensure cross-thread safety on the messages collection 
						lock (messagesLocker) {
							foreach (string message in messages)
							{
								GUILayout.Label(message);
							}
						}
				
						GUILayout.EndVertical();
					GUILayout.EndScrollView ();
				GUILayout.EndArea();
	
				// Send message
				newMessage = GUI.TextField(new Rect(10, 530, 370, 27), newMessage, 50);
				if (GUI.Button(new Rect(390, 528, 90, 28), "Enviar")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
				{
					smartFox.Send( new PublicMessageRequest(newMessage) );
					newMessage = "";
				}
						
			}
		}
	}
	
	private void PrepareLobby() {
		Debug.Log("Prepare Lobby");
		SetupRoomList();
		isLoggedIn = true;
	}
	
	
	private void SetupRoomList () {
		List<string> rooms = new List<string> ();
		List<string> roomsFull = new List<string> ();
		
		List<Room> allRooms = smartFox.RoomManager.GetRoomList();
		
		foreach (Room room in allRooms) {
			// Only show game rooms
			if (!room.IsGame) {
				continue;
			}
			
			Debug.Log ("Room id: " + room.Id + " has name: " + room.Name);
			
			string nameRoom =namemanager.getName(room.Name.ToString());
			if (!nameRoom.Equals("")){				
				rooms.Add(room.Name);
				rooms.Sort();
				roomsFull.Add(nameRoom + " (" + room.UserCount + "/" + room.MaxUsers + ")");
				roomsFull.Sort();
			}
			//roomsFull.Add(room.Name + " (" + room.UserCount + "/" + room.MaxUsers + ")");
		}
		
		roomNameStrings = rooms.ToArray();
		roomFullStrings = roomsFull.ToArray();
		
		if (smartFox.LastJoinedRoom==null) {
			smartFox.Send(new JoinRoomRequest("The Lobby"));
		}
		
	}
}