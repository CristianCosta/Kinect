using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;

public class TutorialManagerDB : MonoBehaviour { 
	
	private static SFSExtensionManager extensionManager;
	private dbCallback getCallback;
	private static TutorialManagerDB tutorialmanagerdb;
	
	private TutorialManagerDB (){
		
	}	
	
	public delegate void dbCallback(string data);
	
	
	public static TutorialManagerDB getInstance(){
		if (tutorialmanagerdb == null){
			tutorialmanagerdb = new TutorialManagerDB();	
		}
		extensionManager= SFSExtensionManager.getInstance();
		return tutorialmanagerdb;
	}
	
	public void mostrarConsulta(SFSObject dataObject){
		
				
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
	
	
	public void getTutorial(string nick, dbCallback getCallback){
		this.getCallback= getCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		extensionManager.sendRequest(new ExtensionRequest("consultarTutorial", sfsObject), "getTutorial", getTutorialResponse);
	}

	public void insertarTutorial(string nick, string tutorial){
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		sfsObject.PutUtfString("tutorial",tutorial);
		extensionManager.sendRequest(new ExtensionRequest("insertarTutorial", sfsObject));
	}
	
	
	private void getTutorialResponse(ISFSObject data){
		this.getCallback(this.dataToString((SFSObject)data));
	}
	
		public void getMensaje(string nick, dbCallback getCallback){
		this.getCallback= getCallback;
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		extensionManager.sendRequest(new ExtensionRequest("consultarMensaje", sfsObject), "getMensaje", getMensajeResponse);
	}

	public void insertarMensaje(string nick, string mensaje){
		Debug.Log("Entro al insertarMensaje---------------!!!");
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("nick",nick);
		sfsObject.PutUtfString("mensaje",mensaje);
		extensionManager.sendRequest(new ExtensionRequest("insertarMensaje", sfsObject));
	}
	
	
	private void getMensajeResponse(ISFSObject data){
		this.getCallback(this.dataToString((SFSObject)data));
	}
}

