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

public class RoomVariableManager {
	
	private SmartFox smartFox;
	private Room currentActiveRoom;
	private string ip;
	private string currentConectionName;
	private string currentName;
	public List<GUIContent> conectionsList;

		
	public RoomVariableManager(SmartFox smf, Room car ){
		smartFox = smf;
		currentActiveRoom = smartFox.LastJoinedRoom;
		smartFox.AddEventListener(SFSEvent.ROOM_VARIABLES_UPDATE, conectionListChange);
		conectionsList = new List<GUIContent>();
		updateConectionsList();
		currentName = smartFox.MySelf.Name;
		SFSExtensionManager extensionManager=SFSExtensionManager.getInstance();
        extensionManager.sendRequest(new ExtensionRequest("getIP", new SFSObject()), "getIpResponse", getIPResponse);
	}
	
	
	 public void getIPResponse(ISFSObject data){
               
               if (data != null) {
                       SFSObject valorDeIp = (SFSObject)data;
                       
                       ip = valorDeIp.GetUtfString("ip");
                       
       
               }
               else {
                       Debug.Log("Data NULL");
               }        
               
     }
	
	public void addConection(){
		
	
		Debug.Log("addConection");
		List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = new SFSArray();
		if (aux != null)
			array = (SFSArray)aux.GetSFSArrayValue();
		
		SFSArray user = new SFSArray();
		user.AddUtfString(currentName);
		user.AddUtfString(ip);
		SFSArray conection = new SFSArray();
		conection.AddSFSArray(user);
		array.AddSFSArray(conection);		
		
		roomVars.Add( new SFSRoomVariable("RoomVC", array) );
		smartFox.Send ( new SetRoomVariablesRequest(roomVars, smartFox.LastJoinedRoom) );
	
		
	}
	
	public void deleteConection(){
		Debug.Log("deleteConection");
		List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();
		

		for(int i = 0; i< array.Size(); i++)
			if (array.GetSFSArray(i).GetSFSArray(0).GetUtfString(0) == currentName)				{
				array.RemoveElementAt(i);				
				Debug.Log("remove user from conection "+currentName+" : "+ip);
			}
		
		roomVars.Add( new SFSRoomVariable("RoomVC", array) );
		smartFox.Send ( new SetRoomVariablesRequest(roomVars, smartFox.LastJoinedRoom) );
	}
	
	
	public void addSuscriber(string name){
		currentConectionName = name;
		
		Debug.Log("addSuscriber");
		List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();

		SFSArray user;
		SFSArray conection;
		
		for(int i=0; i<array.Size(); i++){
			conection = (SFSArray)array.GetSFSArray(i);
			user = (SFSArray)conection.GetSFSArray(0);
			if (user.GetUtfString(0) == name){	
				
				user = new SFSArray();
				user.AddUtfString(currentName);
				user.AddUtfString(ip);
		
				array.GetSFSArray(i).AddSFSArray(user);
			}
		}
		
		roomVars.Add( new SFSRoomVariable("RoomVC", array) );
		smartFox.Send ( new SetRoomVariablesRequest(roomVars, smartFox.LastJoinedRoom) );
		printRoomVC();
	
		
	}
	
	public void deleteSuscriber(string name){
		if (name == null)
			name = currentConectionName;
		List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();
		
		SFSArray user;
		SFSArray conection;
		
		for(int i=0; i<array.Size(); i++){
			conection = (SFSArray)array.GetSFSArray(i);
			user = (SFSArray)conection.GetSFSArray(0);
			if (user.GetUtfString(0) == name){
				Debug.Log("encontre la coneccion: "+name);
				for(int j=1; j<conection.Size(); j++){				
					user = (SFSArray)conection.GetSFSArray(j);
					if((string)user.GetUtfString(0) == currentName)					
						array.GetSFSArray(i).RemoveElementAt(j);					
					
				}
			}
		}
		currentConectionName = null;
		roomVars.Add( new SFSRoomVariable("RoomVC", array) );
		smartFox.Send ( new SetRoomVariablesRequest(roomVars, smartFox.LastJoinedRoom) );
	
	}
	
	
	
	private void conectionListChange(BaseEvent evt){
		Debug.Log("entro a conectionListChange");
	//	printRoomVC();
		ArrayList changedVars = (ArrayList)evt.Params["changedVars"];
			
		//Room room = (Room)evt.Params["room"];
		
    	if (changedVars.Contains ("RoomVC")) {
			updateConectionsList();
    	} 
	}
	
	private void updateConectionsList(){

		conectionsList = new List<GUIContent>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable rmv = room.GetVariable("RoomVC");
		if (rmv != null){
			SFSArray conections = (SFSArray)rmv.GetSFSArrayValue();
			for(int i=0; i<conections.Size(); i++)	
				conectionsList.Add(new GUIContent((conections.GetSFSArray(i)).GetSFSArray(0).GetUtfString(0)));
		}
	}
	
	public string getConectionIP(string name){
		string userIP = "";
		//List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();

		SFSArray user;
		SFSArray conection;
		for(int i=0; i<array.Size(); i++){
			conection = (SFSArray) array.GetSFSArray(i);
			user = (SFSArray) conection.GetSFSArray(0);
			if (user.GetUtfString(0) == name)			
				userIP = user.GetUtfString(1);
			
		}
		return userIP;
	}
	
	public string getOwnIP(){
		return ip;
	}
	
	private void printRoomVC(){
				Debug.Log("printRoomVC");

		//List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();
		
		SFSArray user;
		SFSArray conection;
		
		for(int i=0; i<array.Size(); i++){
			Debug.Log("array["+i+"]");
			conection = (SFSArray) array.GetSFSArray(i);
			for(int j=0; j<conection.Size (); j++){
				user = (SFSArray)conection.GetSFSArray(0);
				Debug.Log("conection["+j+"]");
				conection = (SFSArray)array.GetSFSArray(i);
				Debug.Log("user[ "+user.GetUtfString(0)+" , "+user.GetUtfString(1)+" ]");
			}

		}
	}
	
	
	public List<string> getSuscribers(string name){
		
		if (name == null)
			name = currentConectionName;
		//List<RoomVariable> roomVars = new List<RoomVariable>();
		Room room = smartFox.LastJoinedRoom;
		RoomVariable aux = room.GetVariable("RoomVC");
		SFSArray array = (SFSArray)aux.GetSFSArrayValue();

		SFSArray user;
		SFSArray conection;
		List<string> result = new List<string>();
		
		for(int i=0; i<array.Size(); i++){
			conection = (SFSArray)array.GetSFSArray(i);
			user = (SFSArray)conection.GetSFSArray(0);
			if (user.GetUtfString(0) == name){	

				for(int j=1; j<conection.Size(); j++){
					result.Add(conection.GetSFSArray(j).GetUtfString(0));
					//result.Add(conection.GetSFSArray(j).GetUtfString(1));
				}

			}
		
		}
		return result;
	}
	
}


