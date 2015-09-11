using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;

public class TokenManager 
{	
	private static SFSExtensionManager extensionManager;
	//private dbCallback getCallback;
	//private dbCallback insertCallback;
	private static TokenManager tokenManager;
	
	private TokenManager (){
		
	}	
	
	//public delegate void dbCallback(SFSObject data);
	
	
	public static TokenManager getInstance(){
		if (tokenManager == null){
			tokenManager= new TokenManager();	
		}
		extensionManager= SFSExtensionManager.getInstance();
		return tokenManager;
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
	
	public void getToken(string usuario,string redSocial, SFSExtensionManager.ResponseCallback getCallback){
		Debug.Log("Entro al getToken---------------!!!");
		//this.getCallback= getCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario);
		sfsObject.PutUtfString("redSocial",redSocial);
		extensionManager.sendRequest("consultarToken", sfsObject, "getToken", getCallback);
	}
	
	public void eliminarToken(string usuario,string redSocial){
  		Debug.Log("Entro al eliminarToken---------------!!!");
  		SFSObject sfsObject = new SFSObject();
  		sfsObject.PutUtfString("usuario",usuario);
  		sfsObject.PutUtfString("redSocial",redSocial);
		extensionManager.sendRequest("eliminarToken", sfsObject);
 	}
	
	public void insertarToken(string usuario,string redSocial, string token, SFSExtensionManager.ResponseCallback insertCallback){
		Debug.Log("Entro al insertarToken---------------!!!");
		//this.insertCallback= insertCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario);
		sfsObject.PutUtfString("redSocial",redSocial);
		sfsObject.PutUtfString("token",token);
		extensionManager.sendRequest("insertarToken", sfsObject, "insertarTokenRespuesta" , insertCallback);
	}
	
	
	/*private void getTokenResponse(ISFSObject data){
		this.getCallback((SFSObject)data);
	}
	
	private void inserTokenResponse(ISFSObject data){
		this.insertCallback((SFSObject)data);
	}
	
	private void deleteTokenResponse(ISFSObject data){
		Debug.Log("Callback Elimino");
	}
	
	private void insertTokenResponse(ISFSObject data){
		Debug.Log("Callback Inserto");
	}*/
}

