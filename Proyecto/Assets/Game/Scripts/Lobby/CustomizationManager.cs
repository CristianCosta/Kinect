using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;

public class CustomizationManager : MonoBehaviour { 
	
	private static SFSExtensionManager extensionManager;
	private dbCallback getCallback;
	private static CustomizationManager customizationManager;
	
	private CustomizationManager (){
		
	}	
	
	public delegate void dbCallback(string data);
	
	
	public static CustomizationManager getInstance(){
		if (customizationManager == null){
			customizationManager= new CustomizationManager();	
		}
		extensionManager= SFSExtensionManager.getInstance();
		return customizationManager;
	}
	
	public void mostrarConsulta(SFSObject dataObject){
		
		Debug.Log("Entro al mostrar---------------!!!");
				
		//obtiene arreglo resultado 
		ISFSArray resultado = dataObject.GetSFSArray("result");
		
		for (int i=0; i < resultado.Size(); i++){
			//obtiene objeto y lo muestra
			SFSObject item= (SFSObject) resultado.GetSFSObject(i);
			string[] keys = item.GetKeys();
			for(int j=0; j< keys.Length; j++){
				string s = item.GetUtfString(keys[j]); 
				Debug.Log(s);
				
			}			
		}
	}
	
	private string dataToString(SFSObject data){		
		ISFSArray resultado = data.GetSFSArray("result");
		SFSObject item= (SFSObject) resultado.GetSFSObject(0);
		if (item != null) {
			string[] keys = item.GetKeys();
			return  item.GetUtfString(keys[0]);		
		}
		return null;
	}
	
	
	public void getAvatar(string nick, dbCallback getCallback){
		Debug.Log("Entro al getAvatar---------------!!!");
		this.getCallback= getCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		extensionManager.sendRequest(new ExtensionRequest("consultarAvatar", sfsObject), "getAvatar", getAvatarResponse);
	}

	public void insertarAvatar(string nick, string avatar){
		Debug.Log("Entro al insertarAvatar---------------!!!");
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		Debug.Log("dato insertado = "+ avatar);
		sfsObject.PutUtfString("avatar",avatar);
		extensionManager.sendRequest(new ExtensionRequest("insertarAvatar", sfsObject));
	}
	
	
	private void getAvatarResponse(ISFSObject data){
		Debug.Log("RTA= " + this.dataToString((SFSObject)data));
		this.getCallback(this.dataToString((SFSObject)data));
	}
}

