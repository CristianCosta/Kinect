
using System;
using System.Collections;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Variables;
using System.Collections.Generic;

// The Neywork manager sends the messages to server and handles the response.
public class NetworkManager : MonoBehaviour
{
	private bool running = false;
	
	public readonly static string ExtName = "sfsU3D";  // The server extension we work with
	public readonly static string ExtClass = "main.java.ar.edu.unicen.exa.server.U3DExtension"; // The class name of the extension
	private string userName;
	private static NetworkManager instance;
	private bool send = true;
	public static NetworkManager Instance {
		get {
			return instance;
		}
	}
	
	private SmartFox smartFox;  // The reference to SFS client
	
	void Awake() {
		instance = this;	
	}
	
	void Start() {
		smartFox = SmartFoxConnection.Connection;
		if (smartFox == null) {
			Application.LoadLevel("The Lobby");
			return;
		}	
		Debug.Log("ARRANCANDO: Network Manager");
		SubscribeDelegates();
		Debug.Log("ARRANCANDO: Suscripto");
		if (!smartFox.LastJoinedRoom.Name.Contains("Menu")){
			Debug.Log("ARRANCANDO: Haciendo Spawn");
			SendSpawnRequest();
		}
		
		TimeManager.Instance.Init();
		
		running = true;
	}
			
	// This is needed to handle server events in queued mode
	void FixedUpdate() {
		if (!running) return;
		smartFox.ProcessEvents();
	}
	public string getName(){return userName;}	
	
	
	private void SubscribeDelegates() {
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
	}
	
	private void UnsubscribeDelegates() {
		smartFox.RemoveAllEventListeners();
	}
	
	/// <summary>
	/// Send the request to server to spawn my player
	/// </summary>y
	public void SendSpawnRequest() {
		Room room = smartFox.LastJoinedRoom;
		Transform spawnposition = GameObject.FindGameObjectWithTag("Respawn").transform;
		NetworkTransform nt = NetworkTransform.FromTransform (spawnposition);
		SFSObject dt = new SFSObject ();
		nt.ToSFSObject (dt);
		ExtensionRequest request = new ExtensionRequest("spawnMe", dt , room);
		Debug.Log("ARRANCANDO: Por Enviar");
		smartFox.Send(request);
		Debug.Log("ARRANCANDO: Enviado");
	}
	
	
	/// <summary>
	/// Send local transform to the server
	/// </summary>
	/// <param name="ntransform">
	/// A <see cref="NetworkTransform"/>
	/// </param>
	public void SendTransform(ISFSArray buffer) {
		if(send){
			Room room = smartFox.LastJoinedRoom;
			ISFSObject data = new SFSObject();
			data.PutSFSArray("transformBuffer",buffer);
			ExtensionRequest request = new ExtensionRequest("sendTransform", data, room, true); // True flag = UDP
			smartFox.Send(request);
		}
	}
	
	/// <summary>
	/// Send local animation state to the server
	/// </summary>
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="layer">
	/// A <see cref="System.Int32"/>
	/// </param>
	public void SendAnimationState(string message, int layer, string laser) {
		Room room = smartFox.LastJoinedRoom;
		ISFSObject data = new SFSObject();
		if (laser.Equals(""))
			laser = "vacio";
		
		data.PutUtfString("msg", message+":"+laser);
		data.PutInt("layer", layer);
//		Debug.Log("send Animation!!"+message);
		ExtensionRequest request = new ExtensionRequest("sendAnim", data, room);
		//smartFox.Send(request);
	}
	
	/// <summary>
	/// Request the current server time. Used for time synchronization
	/// </summary>	
	public void TimeSyncRequest() {
		Room room = smartFox.LastJoinedRoom;
		ExtensionRequest request = new ExtensionRequest("getTime", new SFSObject(), room);
		smartFox.Send(request);
	}
	
	/// <summary>
	/// When connection is lost we load the login scene
	/// </summary>
	private void OnConnectionLost(BaseEvent evt) {
		UnsubscribeDelegates();
		Screen.lockCursor = false;
		Cursor.visible = true;
		Application.LoadLevel("The Lobby");
	}
	
	public void OnJoinRoom(BaseEvent evt)
	{
		Debug.Log("CambiarUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
		Room room = (Room)evt.Params["room"];
		// If we joined a game room, then we either created it (and auto joined) or manually selected a game to join
		//if (room.IsGame) {
			Debug.Log ("Joined game room " + room.Name);
			UnsubscribeDelegates();
			RoomVariable rv = room.GetVariable("level");
			String lvl;
			if(rv != null) lvl = rv.GetStringValue();
			else lvl = room.Name;
			Debug.Log("Level: _______ " + lvl);
			//DoorLogManager.getInstance().insertarDoorLog(LobbyGUI.user, doorManager.doorBack, doorManager.nextScene);
			Application.LoadLevel(lvl);
			/*if (lvl.Equals("principal")){
				smartFox.RemoveAllEventListeners();
				//smartFox.RemoveEventListener(SFSEvent.EXTENSION_RESPONSE, SFSExtensionManager.OnExtensionResponse);
				//smartFox.RemoveEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
				//Debug.Log("Estoy aca");
				GameObject.Find("Multi").AddComponent("MultiPlayer");
		}*/
		//}
	}
	
	// This method handles all the responses from the server
	private void OnExtensionResponse(BaseEvent evt) {
		try {
			string cmd = (string)evt.Params["cmd"];
			ISFSObject dt = (SFSObject)evt.Params["params"];
											
			if (cmd == "spawnPlayer") {
				Debug.Log("RESPUESTA: SPAWNPLAYER");
				HandleInstantiatePlayer(dt);
				}else if (cmd == "transform") {
					HandleTransform(dt);
					}else if (cmd == "notransform") {
						HandleNoTransform(dt);
						}else if (cmd == "anim") {
								HandleAnimation(dt);
								}else if (cmd == "time") {
										HandleServerTime(dt);
									}else if (cmd == "disconnect") {
										OnUserDisconnect(dt);
			}
			
		}
		catch (Exception e) {
			Debug.Log("Exception handling response: "+e.Message+" >>> "+e.StackTrace);
		}
		
	}
	
	// Instantiating player (our local FPS model, or remote 3rd person model)
	private void HandleInstantiatePlayer(ISFSObject dt) {
		ISFSObject playerData = dt.GetSFSObject("player");

		int userId = playerData.GetInt("id");
		int score = playerData.GetInt("score");
		string avatar = playerData.GetUtfString("avatar");
		Debug.Log("DaToS: UID " + userId + "; Avatar " + avatar);
	//	string textura = playerData.GetUtfString("textura");
		//Debug.Log("avatar en handle ="+avatar);
		
		/* prueba*/
		if (avatar.Equals("masculino"))
				avatar = "Jane-20";
			
	//	Debug.Log("textura en handle ="+textura);
		NetworkTransform ntransform = NetworkTransform.FromSFSObject(playerData);
						
		User user = smartFox.UserManager.GetUserById(userId);
		userName = user.Name;
		Debug.Log("Nombreeeeeeeeeeeeeee:"+userName);
		if (userId == smartFox.MySelf.Id) {
//			Debug.Log("my id = " + userId);
			PlayerManager.Instance.SpawnPlayer(ntransform, userName, score, avatar);
		}
		else {
			string animacion = dt.GetUtfString("anim");
			PlayerManager.Instance.SpawnEnemy(userId, ntransform, userName, score, avatar,animacion);
		}
	}
	
	// Updating transform of the remote player from server
	private void HandleTransform(ISFSObject dt) {
		//Debug.Log("transform!!");
		int userId = dt.GetInt("id");
		//
		if (userId != smartFox.MySelf.Id) {
			// Update transform of the remote user object
		
			NetworkTransformReceiver recipient = PlayerManager.Instance.GetRecipient(userId); 
			if (recipient==null) Debug.Log("me mameeeeee");
			else Debug.Log("me llegan datos de "+userId);
			ISFSArray data = dt.GetSFSArray("transform");
			//NetworkTransform ntransform = NetworkTransform.FromSFSObject(data);
				if (recipient!=null) {	
					//recipient.ReceiveTransform(ntransform);
					recipient.addObject(data);
					//System.Threading.Thread.Sleep((int)Time.deltaTime);
				}
			
			
			
			/*ISFSArray buff = dt.GetSFSArray("transformBuffer");
			if (buff == null) Debug.Log("ES NULLLLLLLL - BUFFER");
			Debug.Log("SIZE BUFFER: "+buff.Size());
			int num = 0;
			foreach (SFSObject obj in buff){
				Debug.Log("Paquete NRO: " + num);
				num++;
				NetworkTransform ntransform = NetworkTransform.FromSFSObject(obj);
				if (recipient!=null) {
					recipient.addObject(ntransform);
				}
			}	*/
		}
	}
	
	// Server rejected transform message - force the local player object to what server said
	private void HandleNoTransform(ISFSObject dt) {
		int userId = dt.GetInt("id");
		NetworkTransform ntransform = NetworkTransform.FromSFSObject(dt);
		
		if (userId == smartFox.MySelf.Id) {
			// Movement restricted!
			// Update transform of the local object
			ntransform.Update(PlayerManager.Instance.GetPlayerObject().transform);
		}
	}
	
	// Synchronize the time from server
	private void HandleServerTime(ISFSObject dt) {
		long time = dt.GetLong("t");
		TimeManager.Instance.Synchronize(Convert.ToDouble(time));
	}
	
	// Synchronizing remote animation
	private void HandleAnimation(ISFSObject dt) {
		int userId = dt.GetInt("id");
		string msg = dt.GetUtfString("msg");
		int layer = dt.GetInt("layer");
		
//		Debug.Log("userID"+ userId + " " + layer + " " + msg);
		if (userId != smartFox.MySelf.Id) {
			PlayerManager.Instance.SyncAnimation(userId, msg, layer);
		}
	}

	// When a user leaves room destroy his object
	private void OnUserLeaveRoom(BaseEvent evt) {
		User user = (User)evt.Params["user"];
				
		PlayerManager.Instance.DestroyEnemy(user.Id);
		Debug.Log("User "+user.Name+" left");
	}

	private void OnUserDisconnect(ISFSObject dt){
		int user = dt.GetInt("user");
		PlayerManager.Instance.DestroyEnemy(user);
		Debug.Log("User "+user+" left");
	}
	
	void OnApplicationQuit() {
		UnsubscribeDelegates();
	}
	
	public void changeToState(string newState){
		Debug.Log("CambiarOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
		send=false;
		smartFox.Send(new JoinRoomRequest(newState, null, smartFox.LastJoinedRoom.Id));
	}
}
