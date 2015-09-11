using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System;

public class DoorLogManager{

	private static SFSExtensionManager extensionManager;
	private static DoorLogManager doorLogManager;
	
	private DoorLogManager (){		
	}	
		
	public static DoorLogManager getInstance(){
		if (doorLogManager == null){
			doorLogManager= new DoorLogManager();	
		}
		extensionManager= SFSExtensionManager.getInstance();
		return doorLogManager;
	}	
	
	//Insercion de doorlog
	
	public void insertarDoorLog(string usuario,string puerta, string siguienteescena){
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario.ToString());
		sfsObject.PutUtfString("escena",siguienteescena.ToString());
		sfsObject.PutUtfString("puerta",puerta.ToString());		
		extensionManager.sendRequest(new ExtensionRequest("insertarDoorLog", sfsObject));
		Debug.Log("InsertarAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
	}		
}
