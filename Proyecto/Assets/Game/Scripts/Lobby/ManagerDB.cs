using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;

public class ManagerDB 
{	

	private static SmartFox smartFox;
	private dbCallback getCallback;
	private dbCallback insertCallback;
	private static ManagerDB DBManager= null;
	
	private ManagerDB (){
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
		}
	}	
	
	
	public static ManagerDB getInstance(){
		if (DBManager == null){
			DBManager= new ManagerDB();
			Debug.Log ("Manager DB NULL");
		}
		Debug.Log(DBManager.ToString());
		smartFox.RemoveEventListener(SFSEvent.EXTENSION_RESPONSE, DBManager.OnExtensionResponse);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, DBManager.OnExtensionResponse);
		return DBManager;
	}
	
	public delegate void dbCallback(string data);
	
	public void consultarUsuarios(){
		
		Debug.Log("Entro al consultar---------------!!!");
		SFSObject sfsObject = new SFSObject();
		//sfsObject.PutUtfString("consulta",consulta);
		smartFox.Send(new ExtensionRequest("consultarUsuario",sfsObject));    
	
		//Debug.Log("Se envio la request");
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
	
	private string getResultado(SFSObject data){
			
			ISFSArray resultado = data.GetSFSArray("result");
			SFSObject item= (SFSObject) resultado.GetSFSObject(0);
			if (item != null) {
				string[] keys = item.GetKeys();
				return  item.GetUtfString(keys[0]);		
			}
		
			return null;
	}
	
	public void getToken(string usuario,string redSocial, dbCallback getCallback){
		Debug.Log("Entro al getToken---------------!!!");
		this.getCallback= getCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario);
		sfsObject.PutUtfString("redSocial",redSocial);
		smartFox.Send(new ExtensionRequest("consultarToken",sfsObject));
	}
	
	public void eliminarToken(string usuario,string redSocial){
  		Debug.Log("Entro al eliminarToken---------------!!!");
  		SFSObject sfsObject = new SFSObject();
  		sfsObject.PutUtfString("usuario",usuario);
  		sfsObject.PutUtfString("redSocial",redSocial);

  		smartFox.Send(new ExtensionRequest("eliminarToken",sfsObject)); 
 	}
	
	public void insertarToken(string usuario,string redSocial, string token, dbCallback insertCallback){
		Debug.Log("Entro al insertarToken---------------!!!");
		this.insertCallback= insertCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario);
		sfsObject.PutUtfString("redSocial",redSocial);
		sfsObject.PutUtfString("token",token);
		smartFox.Send(new ExtensionRequest("insertarToken", sfsObject)); 
	}
	
	
	public void OnExtensionResponse(BaseEvent evt){
		//Debug.Log ("Entro recibir respuesta");
		string key = (string)evt.Params ["cmd"];
		SFSObject dataObject = (SFSObject)evt.Params ["params"];
		
		switch (key) {			
			case "getToken": 
				this.getCallback(getResultado(dataObject));
			break;
			
			case "insertarTokenRespuesta": 
			    this.insertCallback(getResultado(dataObject));
			break;
			
			case "eliminarTokenRespuesta": 
    			Debug.Log ("Elimino");
			break;
		}
	}
}

