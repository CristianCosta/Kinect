using UnityEngine;
using System.Collections;
using System.Xml;

public class BB_LoadConfig : MonoBehaviour {

	private bool isLoading, hasError;
	
	IEnumerator Start(){
		isLoading = true;
		hasError = false;
	    //Load XML data from a URL
		
		//el link que hay que usar
		string url = "http://"+LobbyGUI.serverIP+":"+LobbyGUI.gamesConfigFilesPort+"/configGameBB.xml";
		GameLogManager.getInstance().getHighScores("top5bigballs");

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
			LevelManager.ReadConfig(xmlDoc);
			
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
		
		// Oculto los textos de menu o el "cargando"
		GameObject.Find("TextLoading").transform.GetComponent<Renderer>().enabled=isLoading && !hasError;
			
		foreach (Transform child in GameObject.Find("GUITexts").transform){
			child.GetComponent<Renderer>().enabled=!isLoading && !hasError;
		}
		
		foreach (Transform child in GameObject.Find("Error").transform){
			child.GetComponent<Renderer>().enabled=hasError;
		}
		
		//trae los textos del menu al frente o los manda al fondo si esta cargando
		//para que se puedan clickear o no
		if (!isLoading && !hasError)
			GameObject.Find("GUITexts").transform.localPosition= new Vector3(0,0,-120);
		else
			GameObject.Find("GUITexts").transform.localPosition= new Vector3(0,0,500);
	}
}
