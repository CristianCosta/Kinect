using UnityEngine;
using System.Collections;
using System.Xml; 

public class MenuMemo : MonoBehaviour {
	
	private GameObject all;
	
	public GameMenuButton rankingButton,playButton,quitButton,tutorialButton;
	
	public TextMesh loadingText;
	
	private bool isLoading, hasError;
	
	private bool playedOnce; 
	
//	void Awake(){
//		
//		this.playedOnce = false; 
//		
//	}
	
	IEnumerator Start(){
		isLoading = true;
		hasError = false;
		loadingText.text = "Cargando configuraciones ...";
		GameLogManager.getInstance().getHighScores("top5memo");
	    //Load XML data from a URL
	    //el link que hay que usar
		string url = "http://"+LobbyGUI.serverIP+":"+LobbyGUI.gamesConfigFilesPort+"/configGameMemo.xml";
		
		//para probar (usar local)
		//string url = "http://taller.isistan.unicen.edu.ar:8086/configGameMemo.xml"; 
		
		
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
			
			//setea el archivo ya cargado -> serÃ¡ usado luego por el juego.
			XmlFile.doc = xmlDoc; 
			
			isLoading = false;
			hasError = false;
			loadingText.text = ""; //borra el texto de Carga

//			playedOnce = false; 
			
	    }
	    else{
		  loadingText.text = "No se pudieron cargar las configuraciones.\n No es posible jugar.";
		  hasError = true;
		  isLoading = false;
	      Debug.Log("ERROR: " + www.error);
	    }
	}
	
	private void enableObjects(bool opt, string tag){
		
		GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
			
		foreach(GameObject oneObj in objs){
		
			//oneObj.renderer.enabled = opt;
			
			if(!opt){
				oneObj.GetComponent<Renderer>().enabled = false; 
				oneObj.transform.Translate(new Vector3(800,800,0)); 
				
			}
			else{
				oneObj.GetComponent<Renderer>().enabled = true; 
				oneObj.transform.Translate(new Vector3(-800,-800,0)); 
			}
		
		}
		
	}
	
//	 Update is called once per frame
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
	
	
//	//FUNCION VIEJA:
//	void Update () {
//	
//		if(isLoading){
//			
//			if(!this.playedOnce){
//				this.enableObjects(false, "MenuElement");	//Desactivar elementos del GUI
//				this.enableObjects(false, "ExitElement");	//Desactivar elementos del GUI
//				this.playedOnce = true; 
//			}
//			
//		}
//		else{
//			
//			if(!this.playedOnce){
//				//this.enableObjects(false, "LoadingElement");
//				this.enableObjects(true, "MenuElement");
//				this.enableObjects(true, "ExitElement");
//				this.playedOnce = true; 
//			}
//		}
//	}
}
