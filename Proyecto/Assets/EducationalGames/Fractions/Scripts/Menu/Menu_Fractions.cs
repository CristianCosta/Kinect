using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;

public class Menu_Fractions : MonoBehaviour {

	public GameMenuButton playButton, rankingButton, tutorialButton;
	public ExitMenuButton quitButton;
	
	public TextMesh loadingText; 
	
	
	private bool isLoading, hasError;
	
	IEnumerator Start(){
		isLoading = true;
		hasError = false;
		loadingText.text = "Cargando configuraciones ...";
		GameLogManager.getInstance().getHighScores("top5fractions");

	    //Load XML data from a URL
		
	    //el link que hay que usar
		string url = "http://"+LobbyGUI.serverIP+":"+LobbyGUI.gamesConfigFilesPort+"/configGameFractions.xml";
		
		//para probar (usar local)
		//string url = "http://taller.isistan.unicen.edu.ar:8086/configGameFractions.xml"; 
		
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
			loadingText.text = ""; //borra el texto de Carga
	    }
	    else{
		  loadingText.text = "No se pudieron cargar las configuraciones. No es posible jugar.";
		  hasError = true;
		  isLoading = false;
	      Debug.Log("ERROR: " + www.error);
	    }
	}
	
	void Update(){
		if (!isLoading && !hasError){
			playButton.setEnable(true);
			rankingButton.setEnable(true);
			quitButton.setEnable(true);
			tutorialButton.setEnable(true);
		}
		else{
			playButton.setEnable(false);
			rankingButton.setEnable(false);
			
			if (isLoading){
				quitButton.setEnable(false);
				tutorialButton.setEnable (false);
			}
			else{
				quitButton.setEnable(true);
				tutorialButton.setEnable(true);
			}
		}
	}
	
}

