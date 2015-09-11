using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Sfs2X.Protocol.Serialization;
using Sfs2X.Entities.Data;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;
public class UserVS : MonoBehaviour {
	
	private string nick;
	private string rol;
	private long id_proyecto;
	private string pathProyecto;
	private MultiPlayer link;
	private SmartFox smartFox;
	
	
	void Start(){
	
		if (!SmartFoxConnection.IsInitialized) {
			Application.LoadLevel("Connection");
			return;
		}
		smartFox = SmartFoxConnection.Connection;
		smartFox.AddEventListener (SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);

        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> add event listener");
		SFSObject param = new SFSObject();
		RoomVariable rv = smartFox.LastJoinedRoom.GetVariable("idGrupo");
		long grupo;
		grupo = (long)rv.GetIntValue();
        param.PutUtfString("nick",getMyUserName());
		param.PutLong("grupo",grupo);
		
		smartFox.Send(new ExtensionRequest("cargarDatosJugador",param));
		SFSObject param2 = new SFSObject();
		Debug.Log ( " proyecto");
		param2.PutLong ("Id_Proyecto", this.getProyecto ());
		Debug.Log (this.getProyecto () + " proyecto ");
		smartFox.Send(new ExtensionRequest("obtenerPathProyecto",param2));
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> smartfox send");

		link = (MultiPlayer)GameObject.Find("Multi").GetComponent("MultiPlayer");

        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> UserVS start termino");
	}
	
	public string getMyUserName(){
		int userId  = smartFox.MySelf.Id;
		User userInstance = smartFox.UserManager.GetUserById(userId);
		Debug.Log("NOMBREEEEE: " + userInstance.Name);
		return userInstance.Name;
	}
	
	public bool esScrumMaster(){
       return (this.rol.Equals("scrummaster"));
    }
	
	public void OnExtensionResponse (BaseEvent evt)
	{
				
		string iden = (string)evt.Params ["cmd"];
		SFSObject dataObject = (SFSObject)evt.Params ["params"];
		
		switch (iden) {
		    case "cargarDatosJugador":
			    fromSFSObject(dataObject);
			    link.setUser(this);
			    link.enabled = true;
			    break;
			case "cargarPath":
			Debug.Log("response path!!!!");
				pathProyecto=dataObject.GetUtfString("path");
				break;
		}	
	}
	
	public void setPathProyecto(string s)
	{
		pathProyecto = s;
	}
	public UserVS(string n,string r){
		nick=n;
		rol=r;
	}

	public string getNick(){
	return nick;
	}
	
	public string getRol(){
		return rol;
	}

	public long getProyecto(){
		return id_proyecto;
	}
	
	public SFSObject toSFSObject ()
	{
		SFSObject userObject = new SFSObject ();
	    userObject.PutUtfString("nick",this.getNick());
		userObject.PutLong("id_proyecto",this.getProyecto());
	    userObject.PutUtfString("rol",this.getRol());
		return userObject;
	
}
	
	public string getPathProyecto(){
		return pathProyecto;
	}
	public void fromSFSObject(SFSObject datos){
		ISFSObject objeto = datos.GetSFSObject("usuario");
		if(objeto != null){
			Debug.Log("PERTENEZCO A ESTA SALA");
			this.nick=objeto.GetUtfString("nick");
			Debug.Log(this.nick);
			this.id_proyecto=objeto.GetLong("id_proyecto");
			Debug.Log(this.id_proyecto);
			this.rol=objeto.GetUtfString("rol");
			Debug.Log(this.rol);
		} else{
			Debug.Log("ME ECHARON A PATADAS PORQUE NO SOY SCRUM MASTER");
			DoorLogManager.getInstance().insertarDoorLog(LobbyGUI.user, "VScampusp1", "Campus");
			doorManager.doorBack="VScampusp1";
			NetworkManager.Instance.changeToState("Campus");
		}
	}
		
		
		
}