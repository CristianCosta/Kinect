	using UnityEngine;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;
	using System.Text;
	using Sfs2X.Core;
	using Sfs2X;
	using Sfs2X.Requests;
	using Sfs2X.Entities;
	using Sfs2X.Entities.Data;
using System.Security.Cryptography;
	
	
	public class dashbordVSConfig : MonoBehaviour{
		
		private int projectSelection = -1;
		private int userSelection = -1;
		private int userSelectionVincular = -1;
		private int projectSelectionVincular = -1;
		private int rolSelectionVincular=-1;
		private string[] projectNameStrings;
		private string[] projectFullStrings;
		private Vector2 projectScrollPosition;
		private Vector2 userScrollPosition;
		private Vector2 userScrollPositionVincular;
		private Vector2 projectScrollPositionVincular;
		private Vector2 rolScrollPositionVincular;
		private SmartFox smartFox;
		List<string> projects = new List<string> ();
		List<string> projectsFull = new List<string> ();
		public GUISkin gSkin;
		Rect windowRect;
		int screenW = Screen.width;
		int screenH = Screen.height;
		
		float inicio_x;
		float inicio_y ;
		private static string nombreProyecto="";
		private static string salaProyecto="";
		private static string pathProyecto="";
		private static string userName="";
		private static string userPass="";
	
		private string[] usersNameStrings;
		private string[] usersFullStrings;
		private string[] allUsersNameStrings;
		private string[] allUsersFullStrings;
		List<string> users = new List<string> ();
		List<string> allUsers = new List<string> ();
		List<string> usersFull = new List<string> ();
		private bool mostrarMensaje = false;
		private string	errorMessage = "";
		private string contraseniaHash = "";
		 
	
		// Use this for initialization
		void Start () {
			
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
			smartFox.AddEventListener (SFSEvent.EXTENSION_RESPONSE, OnExtensionResponce);
			ExtensionRequest req = new ExtensionRequest("listarProyectos",new SFSObject(),smartFox.LastJoinedRoom,false);
			smartFox.Send(req);
			req = new ExtensionRequest("listarUsuarios",new SFSObject(),smartFox.LastJoinedRoom,false);
			smartFox.Send(req);
			
		
		
		}
		
		public void OnExtensionResponce (BaseEvent evt){
			
			
			string cmd = (string)evt.Params ["cmd"];
			
			ISFSObject dataObject = (SFSObject)evt.Params ["params"];
		
			switch (cmd) {
			case "ListaProyectos":
				listAllProject(dataObject);
				break;
			case "UsuariosXProyecto":
				listUserXProject(dataObject);
				break;
			case "ListarUsuarios":
				listAllUsers(dataObject);
				break;
			case "SalaYaExiste":
				Debug.Log("SalaExiste");
				mostrarMensaje=true;	
				errorMessage="Error, sala ya existe";
				break;	
			case "CrearSalaFallo":
				Debug.Log("CrearSalaFallo");
				mostrarMensaje=true;	
				errorMessage="Error, crear sala fallo";
				break;
			case "SalaCreada":
				Debug.Log("SalaCreada");
				mostrarMensaje=true;	
				errorMessage="Sala creada";
				break;
			case "UsuarioCreado":
				Debug.Log("usuariocreado");
				mostrarMensaje=true;	
				errorMessage="Ususario creado";
				break;
			case "CrearUsuarioFallo":
				Debug.Log("CrearUsuarioFallo");
				mostrarMensaje=true;	
				errorMessage="Error, crear usuario fallo";
				break;
			case "SalaNoExiste":
				Debug.Log("SalaNoExiste");
				mostrarMensaje=true;	
				errorMessage="Error, sala no existe";
				break;
			case "EliminarSalaFallo":
				Debug.Log("EliminarSalaFallo");
				mostrarMensaje=true;	
				errorMessage="Error, eliminar sala fallo";
				break;
			case "SalaEliminada":
				Debug.Log("SalaEliminada");
				mostrarMensaje=true;	
				errorMessage="Sala eliminada";
				break;
			case "RolAsignado":
				Debug.Log("RolAsignado");
				mostrarMensaje=true;	
				errorMessage="Rol asignado";
				break;
			case "RolNoAsignado":
				Debug.Log("RolNoAsignado");
				mostrarMensaje=true;	
				errorMessage="Error,rol no asignado";
				break;
			case "RolNoExiste":
				Debug.Log("RolNoExiste");
				mostrarMensaje=true;	
				errorMessage="Error, rol no existe";
				break;
			case "UsuarioNoExiste":
				Debug.Log("RolNoExiste");
				mostrarMensaje=true;	
				errorMessage="Error, usuario no existe";
				break;
			case "UsuarioNoEliminado":
				Debug.Log("");
				mostrarMensaje=true;	
				errorMessage="Error, usuario no eliminado";
				break;
			case "UsuarioEliminado":
				Debug.Log("");
				mostrarMensaje=true;	
				errorMessage="Usuario Eliminado";
				break;
			}
	
		}
		
		public void listAllUsers(ISFSObject dataObject){
			ISFSArray datos = dataObject.GetSFSArray("usuarios");
			foreach (SFSObject proyectoObject in datos){
					allUsers.Add(proyectoObject.GetUtfString("nick"));
				}
			allUsersFullStrings = allUsers.ToArray();
		}
		
		public void listAllProject(ISFSObject dataObject){
			ISFSArray datos = dataObject.GetSFSArray("proyectos");
				foreach (SFSObject proyectoObject in datos){
					projects.Add(proyectoObject.GetUtfString("sala"));
					projectsFull.Add(proyectoObject.GetUtfString("nombre"));
				}
				projectNameStrings = projects.ToArray();
				projectFullStrings = projectsFull.ToArray();
			
		}
		
		
		public void listUserXProject (ISFSObject dataObject) {
				ISFSArray datos = dataObject.GetSFSArray("usuarios");
				users.Clear();
				usersFull.Clear();
				foreach (SFSObject proyectoObject in datos){
					users.Add(proyectoObject.GetUtfString("nick"));
					usersFull.Add(proyectoObject.GetUtfString("rol"));
				}
				usersNameStrings =users.ToArray();
				usersFullStrings = usersFull.ToArray();
		}
		
			
		
		// Update is called once per frame
		void Update () {
		
		}
		
		void FixedUpdate() {
			if (smartFox != null) {
				smartFox.ProcessEvents();			
			}
			
		
		}
		
		void  OnGUI()
		{
			GUI.skin = gSkin;
			
			GUI.Label(new Rect(10,screenH-screenH/3, 
			          screenH/3, screenH/3), "", "UNICENLogo");		
			
	
			
			GUI.Label(new Rect((float)(screenW*0.6),(float)(screenH-screenH*0.2) , 
			          (float)(screenW*0.3), (float)(screenH*0.2)), "", "VsLogo");
			
			
			if (mostrarMensaje){
				GUI.Box (new 
				Rect (screenW/2 - screenW/6, 2*screenH/3 - 100, screenW/3, 170), "Error");
						
						GUILayout.BeginArea (new Rect (screenW/2 - screenW/6 + 10, 2*screenH/3 - 80, screenW/2, 160));
							userScrollPosition = GUILayout.BeginScrollView (userScrollPosition, GUILayout.Width (screenW/2-20), GUILayout.Height (160));
							GUILayout.BeginVertical ();
						
									GUILayout.Label (errorMessage);
								
							GUILayout.EndVertical ();
							GUILayout.EndScrollView ();
						GUILayout.EndArea ();
					
					if (GUI.Button(new Rect(screenW/2 - 50, 2*screenH/3+90, 100, 24), "Aceptar") || (Event.current.type == EventType.keyDown && Event.current.character == '\n')){
						errorMessage = "";
						mostrarMensaje=false;
					}
			
			}
			else{
			//ACA DIBUJA TODO
			
		// Game project list
					GUI.Box (new Rect ((float)(screenW*0.05), 20, (float)(screenW*0.2), (float)(screenH*0.6)), "Proyectos Disponibles");
					GUILayout.BeginArea (new Rect ((float)(screenW *0.07), 40, (float)(screenW*0.18), (float)(screenH*0.5)));
					if (projectsFull.Count > 0) {
	
						projectScrollPosition = GUILayout.BeginScrollView (projectScrollPosition, GUILayout.Width ((float)(screenW*0.18)), GUILayout.Height ((float)(screenH*0.50)));
						
						projectSelection = GUILayout.SelectionGrid (projectSelection, projectFullStrings, 1, "RoomListButton");
						if (projectSelection >= 0) {
							SFSObject aux = new SFSObject();
							aux.PutUtfString("sala",(string)projectNameStrings[projectSelection]);						
							ExtensionRequest req = new ExtensionRequest("obtenerUsuariosPorProyecto",aux,
																		smartFox.LastJoinedRoom,false);
							smartFox.Send(req);
							projectSelection=-1;
							
						}
						GUILayout.EndScrollView ();
						
					} else {
						GUILayout.Label ("No existen salas disponibles");
					}
					GUILayout.EndArea ();
			
			// User list
					GUI.Box (new Rect ((float)(screenW*0.30), 20, (float)(screenW*0.2), (float)(screenH*0.6)), "Usuarios");
					int cantUsers = 10;
					if (usersFullStrings != null)
							cantUsers = usersFullStrings.Length;
					userScrollPosition = GUI.BeginScrollView(new Rect ((float)(screenW *0.30+5), 50, (float)(screenW*0.20-5), (float)(screenH*0.40))
			                                          			,userScrollPosition,
			                                          			new Rect (0, 0, (float)(screenW*0.25/2), (float)(cantUsers*20))
			                                          		  ); 
										
			GUILayout.BeginArea (new Rect (0, 0, (float)(screenW*0.10-5), (float)(cantUsers*20)));
					if (usersFull.Count > 0) {	
								GUILayout.SelectionGrid (userSelection, usersNameStrings, 1, "RoomListButton");
					} else {
						GUILayout.Label ("No existen salas disponibles");
					}
					GUILayout.EndArea ();
			GUILayout.BeginArea (new Rect ((float)(screenW*0.10-5), 0, (float)(screenW*0.10), (float)(cantUsers*20)));
						if (usersFull.Count > 0) {	
							GUILayout.SelectionGrid (userSelection, usersFullStrings, 1, "RoomListButton");
						} else {
							GUILayout.Label ("No existen salas disponibles");
						}
					GUILayout.EndArea ();
					GUI.EndScrollView ();
			//add Project
			
			
			inicio_x = (float)(screenW*0.6);
			inicio_y = 40;
			float offset = 20;
			
			GUI.Box (new Rect (inicio_x, inicio_y, (float)(screenW*0.3), (float)(screenH*0.35)), "Cargar Proyecto");
			
			GUI.Label(new Rect(inicio_x+offset,inicio_y+offset,(float)(screenW*0.3-40),30),"Nombre");
			nombreProyecto = GUI.TextField(new Rect(inicio_x+offset,inicio_y+offset+20+10,(float)(screenW*0.3-40),30),nombreProyecto,25);
				
			GUI.Label(new Rect(inicio_x+offset,inicio_y+offset+60,(float)(screenW*0.3-40),30),"Sala");
			salaProyecto = GUI.TextField(new Rect(inicio_x+offset,(float)(inicio_y+offset+80+10),(float)(screenW*0.3-40),30),salaProyecto,25);
			
			GUI.Label(new Rect(inicio_x+offset,inicio_y+offset+120,(float)(screenW*0.3-40),30),"Path");
			pathProyecto = GUI.TextField(new Rect(inicio_x+offset,(float)(inicio_y+offset+140+10),(float)(screenW*0.3-40),30),pathProyecto,25);
	

			
			if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.2) , inicio_y+205, 85, 28), "Cargar")) {
				if(!(salaProyecto.Equals("") && nombreProyecto.Equals(""))){
						SFSObject aux = new SFSObject();
						aux.PutUtfString("nombre",nombreProyecto);
						aux.PutUtfString("sala",salaProyecto);
						aux.PutUtfString ("path", pathProyecto);
						ExtensionRequest req = new ExtensionRequest("crearProyecto",aux,
																		smartFox.LastJoinedRoom,false);
						smartFox.Send(req);
						nombreProyecto="";
						salaProyecto="";
				}else{
						mostrarMensaje=true;	
						errorMessage="Error, falta cargar datos";
				}
					
				
			}
			if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.2-100) , inicio_y+205, 85, 28), "Eliminar")){
					if(!(salaProyecto.Equals("") && nombreProyecto.Equals(""))){
						SFSObject aux = new SFSObject();
						aux.PutUtfString("sala",salaProyecto);
						ExtensionRequest req = new ExtensionRequest("eliminarProyecto",aux,
																		smartFox.LastJoinedRoom,false);
						smartFox.Send(req);
						nombreProyecto="";
						salaProyecto="";
				}else{
						mostrarMensaje=true;	
						errorMessage="Error, falta cargar datos";
				}
			}
			
					
			
			//add User
			inicio_x = (float)(screenW*0.6);
			inicio_y = 300;
			GUI.Box (new Rect (inicio_x, inicio_y, (float)(screenW*0.3), (float)(screenH*0.32)), "Cargar Usuario");
			
			GUI.Label(new Rect(inicio_x+offset,inicio_y+offset,(float)(screenW*0.3-40),30),"Nombre");
			userName = GUI.TextField(new Rect(inicio_x+offset,inicio_y+offset+20+10,(float)(screenW*0.3-40),30),userName,25);
				
			GUI.Label(new Rect(inicio_x+offset,inicio_y+offset+60,(float)(screenW*0.3-40),30),"Contraseña");
			userPass = GUI.PasswordField(new Rect(inicio_x+offset,(float)(inicio_y+offset+80+10),(float)(screenW*0.3-40),30),userPass,'*');
			
			if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.2) , inicio_y+145, 85, 28), "Cargar")) {
						if(!(userName.Equals("") && userPass.Equals(""))){
							SFSObject aux = new SFSObject();
							aux.PutUtfString("nick",userName);
							Byte[] originalBytes;
							Byte[] encodedBytes;
							MD5 md5;

				//Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
				md5 = new MD5CryptoServiceProvider();
				originalBytes = ASCIIEncoding.Default.GetBytes(userPass);
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
							aux.PutUtfString("password",contraseniaHash);
							ExtensionRequest req = new ExtensionRequest("crearUsuario",aux,
																		smartFox.LastJoinedRoom,false);
							smartFox.Send(req);
							userName="";
							userPass="";
				}
				else{
						errorMessage="Error, falta cargar datos";
						mostrarMensaje=true;	
				}
				
					
			}
			
			if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.2-100) , inicio_y+145, 85, 28), "Eliminar")) {
						if(!(userName.Equals("") && userPass.Equals(""))){
							SFSObject aux = new SFSObject();
							aux.PutUtfString("nick",userName);
							ExtensionRequest req = new ExtensionRequest("eliminarUsuario",aux,
																		smartFox.LastJoinedRoom,false);
							smartFox.Send(req);
							userName="";
							userPass="";
				}
				else{
						errorMessage="Error, falta cargar datos";
						mostrarMensaje=true;	
				}
				
					
			}
			// project-user-rol
			inicio_x = (float)(screenW*0.17);
			inicio_y = (float)(screenH-screenH*0.35);
			offset=60;
			GUI.Box (new Rect (inicio_x, inicio_y, (float)(screenW*0.3), (float)(screenH*0.25)), "Vincular Usuarios a Proyecto");
			//user vincular
			cantUsers=10;
			if (!(allUsers.Count == 0)){
				cantUsers = allUsers.Count;
			}
			userScrollPositionVincular = GUI.BeginScrollView(new Rect (inicio_x+10, inicio_y+30, (float)(screenW*0.10-10), (float)(screenH*0.20-20)),
				userScrollPositionVincular,new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantUsers*20))); 
				GUILayout.BeginArea (new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantUsers*20)));
					if (allUsers.Count > 0) {	
						userSelectionVincular=GUILayout.SelectionGrid (userSelectionVincular, allUsersFullStrings, 1, "RoomListButton");
					}else {
						GUILayout.Label ("No existen usuarios");
					}
				GUILayout.EndArea ();
			GUI.EndScrollView ();
			//proyecto vincular
			int cantProj = 10;
			if (projectFullStrings != null)
				cantProj = projectsFull.Count;
			projectScrollPositionVincular = GUI.BeginScrollView(new Rect (inicio_x+10+(float)(screenW*0.10-10), inicio_y+30, (float)(screenW*0.10-10), (float)(screenH*0.20-20)),
				projectScrollPositionVincular,new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantProj*20))); 
				GUILayout.BeginArea (new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantProj*20)));
					if (projectsFull.Count > 0) {	
						projectSelectionVincular=GUILayout.SelectionGrid (projectSelectionVincular, projectNameStrings, 1, "RoomListButton");
					}else {
						GUILayout.Label ("Ningun proyecto disponible");
					}
				GUILayout.EndArea ();
			GUI.EndScrollView ();
			//roles vincular
			int cantRoles = 2;
			string[] rolNameStrings = new string[2];
			rolNameStrings[0]="ScrumMaster";
			rolNameStrings[1]="ScrumMember";
			float offsetVincular = inicio_x+10+(float)(screenW*0.10-10)*2;
			rolScrollPositionVincular = GUI.BeginScrollView(new Rect (offsetVincular, inicio_y+30, (float)(screenW*0.10-10), (float)(screenH*0.20-20)),
				rolScrollPositionVincular,new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantProj*20))); 
				GUILayout.BeginArea (new Rect (0, 0, (float)(screenW*0.10-30), (float)(cantRoles*20)));
						
					rolSelectionVincular=GUILayout.SelectionGrid (rolSelectionVincular, rolNameStrings, 1, "RoomListButton");
					
				GUILayout.EndArea ();
			GUI.EndScrollView ();
	
			if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.25) ,inicio_y+(float)(screenH*0.25), 75, 28), "Vincular")) {
						
				if ((userSelectionVincular!=-1) && (projectSelectionVincular!=-1) && (rolSelectionVincular!=-1)){
					SFSObject aux = new SFSObject();
							String nickV=allUsers[userSelectionVincular];
							String salaV=projects[projectSelectionVincular];
							String rolV=rolNameStrings[rolSelectionVincular];
							aux.PutUtfString("nick",nickV);
							aux.PutUtfString("sala",salaV);
							aux.PutUtfString("rol",rolV);
							ExtensionRequest req = new ExtensionRequest("asignarRol",aux,
																		smartFox.LastJoinedRoom,false);
							smartFox.Send(req);
							userSelectionVincular=-1;
							projectSelectionVincular=-1;
							rolSelectionVincular=-1;
				}
				else{
					mostrarMensaje=true;	
					errorMessage="Error, falta seleccionar campo";
				}
				
			}
			//eliminar vincular
				if (GUI.Button (new Rect ((float)(inicio_x+screenW*0.25-100) ,inicio_y+(float)(screenH*0.25), 75, 28), "Eliminar")) {
						
				if ((userSelectionVincular!=-1) && (projectSelectionVincular!=-1) && (rolSelectionVincular!=-1)){
					SFSObject aux = new SFSObject();
							String nickV=allUsers[userSelectionVincular];
							String salaV=projects[projectSelectionVincular];
							String rolV=rolNameStrings[rolSelectionVincular];
							aux.PutUtfString("nick",nickV);
							aux.PutUtfString("sala",salaV);
							aux.PutUtfString("rol",rolV);
							ExtensionRequest req = new ExtensionRequest("eliminarRol",aux,
																		smartFox.LastJoinedRoom,false);
							smartFox.Send(req);
							userSelectionVincular=-1;
							projectSelectionVincular=-1;
							rolSelectionVincular=-1;
				}
				else{
					mostrarMensaje=true;	
					errorMessage="Error, falta seleccionar campo";
				}
				
			}
			
			if (GUI.Button (new Rect ((float)(screenW-90) ,20, 75, 28), "Salir")) {
				smartFox.Send(new JoinRoomRequest("The Lobby", null, smartFox.LastJoinedRoom.Id));
				Application.LoadLevel("The Lobby");
				LobbyGUI.isLoggedIn = true;
			}
		
		}//cierra llave primer else
			
		}
			
		
		
	}
		