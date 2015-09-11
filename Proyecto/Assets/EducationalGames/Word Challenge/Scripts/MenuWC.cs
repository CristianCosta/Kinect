using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;


public class MenuWC : MonoBehaviour {
	public String targetSceneName;
	public string door;
	
	public GUIStyle btn_MenuPlay,btn_MenuLanguage,btn_MenuRanking,btn_tutorial,btn_MenuExit,
					btn_spanish,btn_english,btn_italian,loading,errorOnLoad, liberarMouse;
	public int x_newGame,y_newGame,x_lang,y_lang,x_rank,y_rank,x_exit,y_exit,
				x_spanish,y_spanish,y_loadingError,x_tutorial,y_tutorial, x_libMouse,y_libMouse;
	public Controller_Camera cameraCont;
	
	private bool isLoading, hasError;
	
	IEnumerator Start(){
		isLoading = true;
		hasError = false;
	    //Load XML data from a URL
		GameLogManager.getInstance().getHighScores("top5wordchallenge");

		//el link que hay que usar
		string url = "http://"+LobbyGUI.serverIP+":"+LobbyGUI.gamesConfigFilesPort+"/configGameWC.xml";
	    
		//para probar (usar local)
		//string url = "http://taller.isistan.unicen.edu.ar:8086/configGameWC.xml"; 
		
	    WWW www = new WWW(url);
	 
	    //Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
	    yield return www;
	    if (www.error == null)
	    {
		    //Sucessfully loaded the XML
		    Debug.Log("Loaded following XML " + www.text);
		 
		    //Create a new XML document out of the loaded data
		    XmlDocument xmlDoc = new XmlDocument();
		    xmlDoc.LoadXml(www.text);
				
			//setea el archivo ya cargado -> será usado luego por el juego.
			CurrentConfig.setConfigFile(xmlDoc);
			
			isLoading = false;
			hasError = false;
	    }
	    else{
		  isLoading = false;
		  hasError = true;
	      Debug.Log("ERROR: " + www.error);
	    }
	}
	
	void OnGUI(){
		GUI.Box (new Rect(cameraCont.getOffsetMB_X()+x_libMouse,cameraCont.getOffsetMB_Y()+y_libMouse,900,46),"Atencion!!! Para liberar el Mouse presione la tecla ESCAPE", liberarMouse);
		
		if (!isLoading && !hasError){
			if (GUI.Button(new Rect(cameraCont.getOffsetMB_X()+x_newGame,cameraCont.getOffsetMB_Y()+y_newGame,140,46),"",btn_MenuPlay)){
					Application.LoadLevel("LevelWC");
			}
			if (GUI.Button(new Rect(cameraCont.getOffsetMB_X()+x_rank,cameraCont.getOffsetMB_Y()+y_rank,239,48),"",btn_MenuRanking)){
					Application.LoadLevel("RankingWC");
			}
			if (GUI.Button(new Rect(cameraCont.getOffsetMB_X()+x_tutorial,cameraCont.getOffsetMB_Y()+y_tutorial,242,65),"",btn_tutorial)){
					Application.LoadLevel("TutorialWC");
			}
			if (GUI.Button(new Rect(cameraCont.getOffsetMB_X()+x_exit,cameraCont.getOffsetMB_Y()+y_exit,124,50),"",btn_MenuExit)){
				doorManager.doorBack=door;
				NetworkManager.Instance.changeToState(targetSceneName);		
				//Application.LoadLevel(""); //HAY QUE VER COMO SE SALE!!!!
			}
			
		// Texto Idiomas
				
			GUI.Box(new Rect(cameraCont.getOffsetML_X()+x_lang,cameraCont.getOffsetML_Y()+y_lang,222,48),"",btn_MenuLanguage);
			//Botones de idiomas
			if (GUI.Button(new Rect(cameraCont.getOffsetMF_X()+x_spanish,cameraCont.getOffsetMF_Y()+y_spanish,77,50),"",btn_spanish)){
					CurrentConfig.setCurrentLanguage("spanish-words"); //nombre del archivo en los resources
			}
			if (GUI.Button(new Rect(cameraCont.getOffsetMF_X()+x_spanish+125,cameraCont.getOffsetMF_Y()+y_spanish,77,50),"",btn_english)){
					CurrentConfig.setCurrentLanguage("english-words");
			}
			if (GUI.Button(new Rect(cameraCont.getOffsetMF_X()+x_spanish+250,cameraCont.getOffsetMF_Y()+y_spanish,77,50),"",btn_italian)){
					CurrentConfig.setCurrentLanguage("italian-words");
			}
		}
		else if (isLoading){
			GUI.Box(new Rect(cameraCont.getOffsetML_X()+x_lang,cameraCont.getOffsetML_Y()+y_lang,308,46),"",loading);
		}
		else{ //hasError = true -> error al cargar!!
			//Muestra el mensaje de error
			GUI.Box(new Rect(cameraCont.getOffsetML_X()+x_lang,cameraCont.getOffsetML_Y()+y_loadingError,500,300),"Ocurrio un error al cargar \nlas configuraciones.\n No se puede jugar.",errorOnLoad);
			//Botón para salir
			if (GUI.Button(new Rect(cameraCont.getOffsetMB_X()+x_exit,cameraCont.getOffsetMB_Y()+y_exit,124,50),"",btn_MenuExit)){
				doorManager.doorBack=door;
				NetworkManager.Instance.changeToState(targetSceneName);		
				//Application.LoadLevel(""); //HAY QUE VER COMO SE SALE!!!!
			}
		}		
	}
}
