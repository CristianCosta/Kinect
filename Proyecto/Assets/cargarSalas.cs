using UnityEngine;
using System.Collections;
using Sfs2X.Core;
using Sfs2X;
using Sfs2X.Requests;
using Sfs2X.Entities;
using System.Collections.Generic;
using Sfs2X.Entities.Data;

public class cargarSalas : MonoBehaviour {

	private int roomSelection = -1;
	private string[] roomNameStrings;
	private string[] roomFullStrings;
	private string[] pathStrings;
	private Vector2 gameScrollPosition;
	private bool esperandoJoin = false;
	private SmartFox smartFox;
	public static NameManager namemanager = new NameManager();
	public string goToCamara="camBotoneraAscensor";
	public string door = "Vscrump1";
	public GUISkin gSkin;
	private bool yaEntre = false;
	
	List<string> rooms = new List<string> ();
	List<string> roomsFull = new List<string> ();
	List<string> paths= new List<string> ();

	void Start(){
		this.enabled = false;
		//AddEventListeners();
		
	}

	// use this for initialization
	void OnTriggerEnter(Collider other) {
		if (!yaEntre){
			if (SmartFoxConnection.IsInitialized)
			{
				Debug.Log("Ya estaba inicializado!");
				smartFox = SmartFoxConnection.Connection;
			}
			else
			{
				Debug.Log("No estaba inicializado!");
				smartFox = new SmartFox(false);
			}
			AddEventListeners();
			//SetupRoomList();
			GameObject primary = GameObject.Find("Main Camera");
			GameObject secondary = GameObject.Find("camBotoneraAscensor");
			Debug.Log("Intentando cambiar de camara: " + goToCamara);
			if (secondary != null){
				secondary.GetComponent<Camera>().enabled = true;
				primary.GetComponent<Camera>().enabled = false;	
			}
			ExtensionRequest req = new ExtensionRequest("obtenerProyectosPorusuario",new SFSObject(),smartFox.LastJoinedRoom,false);
			smartFox.Send(req);
			yaEntre=true;
		}
	}
	
	void  OnGUI()
	{
		GUI.skin = gSkin;
		int screenW = Screen.width;
		int screenH = Screen.height;
	
	// Game room list
				GUI.Box (new Rect ((float)(screenW*0.1), 20, (float)(screenW*0.8), (float)(screenH*0.7)), "Salas Disponibles");
				GUILayout.BeginArea (new Rect ((float)(screenW *0.15), 40, (float)(screenW*0.75), (float)(screenH*0.65)));
				if (smartFox.RoomList.Count != 1) {

					gameScrollPosition = GUILayout.BeginScrollView (gameScrollPosition, GUILayout.Width ((float)(screenW*0.8)), GUILayout.Height ((float)(screenH*0.65)));
					
					roomSelection = GUILayout.SelectionGrid(roomSelection, roomFullStrings, 1, "RoomListButton");
					if (!esperandoJoin && roomSelection >= 0) {
						//smartFox.Send(new JoinRoomRequest(roomNameStrings[roomSelection], null, smartFox.LastJoinedRoom.Id));
						DoorLogManager.getInstance().insertarDoorLog(LobbyGUI.user, door, roomNameStrings[roomSelection]);
						doorManager.doorBack=door;
						NetworkManager.Instance.changeToState(roomNameStrings[roomSelection]);
						Debug.Log("Mandeee mensaje con "+roomNameStrings[roomSelection] + "id" +smartFox.LastJoinedRoom.Id );		
						esperandoJoin=true;
					}
					GUILayout.EndScrollView ();
					
				} else {
					GUILayout.Label ("No existen salas disponibles");
				}
				GUILayout.EndArea ();
				
	
	
	}
	
	private void AddEventListeners() {
		smartFox.AddEventListener (SFSEvent.EXTENSION_RESPONSE, OnExtensionResponce);
	}
	
	public void OnExtensionResponce (BaseEvent evt){
		
		string cmd = (string)evt.Params ["cmd"];
		if (cmd.Equals("ProyXusuario")){
		
			ISFSObject dataObject = (SFSObject)evt.Params ["params"];
			ISFSArray datos = dataObject.GetSFSArray("proyectos");
			foreach (SFSObject proyectoObject in datos){
				rooms.Add(proyectoObject.GetUtfString("sala"));
				roomsFull.Add(proyectoObject.GetUtfString("nombre"));
				paths.Add(proyectoObject.GetUtfString("path"));

			}
			roomNameStrings = rooms.ToArray();
			roomFullStrings = roomsFull.ToArray();
			pathStrings = paths.ToArray();
			this.enabled = true;
		}
	}
	
	public void OnJoinRoom(BaseEvent evt)
	{
		Room room = (Room)evt.Params["room"];
		Debug.Log ("Intentando cargar " + room.Name);
		//if (room.IsGame) {
			Debug.Log ("Joined game room " + room.Name);
			UnregisterSFSSceneCallbacks();
			if(room.Name.Equals("mercurio"))Application.LoadLevel("principal");
			else Application.LoadLevel(room.Name);
		//}
	}
	
	
	/*private void SetupRoomList () {
		List<string> rooms = new List<string> ();
		List<string> roomsFull = new List<string> ();
		
		List<Room> allRooms = smartFox.RoomManager.GetRoomList();//GetRoomListFromGroup("virtualscrum");
	//obtenerProyectosPorusuario
		Debug.Log("Entre a listarrrr");
		foreach (Room room in allRooms) {
			// Only show game rooms
			//if (!(room.GroupId.Equals("virtualscrum"))) {
			//	continue;
			//}
		
			
			Debug.Log ("Room id: " + room.Id + " has name: " + room.Name);
			
			string nameRoom =namemanager.getName(room.Name.ToString());
			if (!nameRoom.Equals("")){				
				rooms.Add(room.Name);
				rooms.Sort();
				roomsFull.Add(nameRoom + " (" + room.userCount + "/" + room.Maxusers + ")");
				roomsFull.Sort();
			}
			//roomsFull.Add(room.Name + " (" + room.userCount + "/" + room.Maxusers + ")");
		}
		
		roomNameStrings = rooms.ToArray();
		roomFullStrings = roomsFull.ToArray();
		
		if (smartFox.LastJoinedRoom==null) {
			smartFox.Send(new JoinRoomRequest("The Lobby"));
		}
		
	}*/
	
	private void UnregisterSFSSceneCallbacks() {
		// This should be called when switching scenes, so callbacks from the backend do not trigger code in this scene
		smartFox.RemoveAllEventListeners();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
