using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System;

public class GameLogManager{
	
	private static SFSExtensionManager extensionManager;
	private static GameLogManager gameLogManager;
	ArrayList ultimaConsultaHighScores= new ArrayList();
	
	private GameLogManager (){		
	}	
		
	public static GameLogManager getInstance(){
		if (gameLogManager == null){
			gameLogManager= new GameLogManager();	
		}
		extensionManager= SFSExtensionManager.getInstance();
		return gameLogManager;
	}	
	
	//Insercion de gamelog
	
	public void insertarGameLog(string usuario,string juego, int puntaje, int aciertos, int errores, 
								string tiempo, int nivel, bool abandono){
		string leave="0";
		if (abandono) leave="1";		
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("usuario",usuario);
		sfsObject.PutUtfString("juego",juego);
		sfsObject.PutUtfString("puntaje",puntaje.ToString());
		sfsObject.PutUtfString("aciertos",aciertos.ToString());
		sfsObject.PutUtfString("errores",errores.ToString());
		sfsObject.PutUtfString("tiempo",tiempo);
		sfsObject.PutUtfString("nivel",nivel.ToString());
		sfsObject.PutUtfString("abandono",leave);
		extensionManager.sendRequest(new ExtensionRequest("insertarGameLog", sfsObject));
	}		
	
	//Consulta de puntajes altos
	
	public void generaHighScores(SFSObject dataObject){				
		//obtiene arreglo resultado 
		this.ultimaConsultaHighScores= new ArrayList();
		ISFSArray resultado = dataObject.GetSFSArray("result");
		for (int i=0; i < resultado.Size(); i++){
			SFSObject item= (SFSObject) resultado.GetSFSObject(i);
			string user= item.GetUtfString("userid");
			string score= ((double) item.GetDouble("score")).ToString();
			HighScoreData nuevo= new HighScoreData(user, score);
			this.ultimaConsultaHighScores.Insert(i, nuevo);
		}
	}	
	
	public ArrayList getHighScores(string juego){
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("tabla",juego);
		extensionManager.sendRequest(new ExtensionRequest("consultarHighScores", sfsObject), "getHighScores", getHighScoresResponse);
		return this.ultimaConsultaHighScores;
	}
	
	private void getHighScoresResponse(ISFSObject data){
		this.generaHighScores((SFSObject)data);
	}
	

	
}


