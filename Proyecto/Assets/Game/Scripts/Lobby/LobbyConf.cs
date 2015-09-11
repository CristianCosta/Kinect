using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;
using System;
using System.Collections.Generic;

public  class LobbyConf : MonoBehaviour{


	//private static SocialNetworkConfig socialNetworkConfig = null;
	private static Dictionary<string, string> Configurations;

	private static bool cargado= false;
	//private static bool initiated = false;
	

	/*void Start(){
	}*/
	IEnumerator Start(){

			Debug.Log(">>>>>>>>>>>>>>Start LobbyConf ");
			
			Configurations = new Dictionary<string,string>();
			WWW www;
			if ( Application.isEditor ){
				Debug.Log ("You are playing in the EDITOR");
				www = new WWW("http://localhost/LobbyConfig.xml");
			} else {
				Debug.Log ("You are playing in the WEBPLAYER in " + Application.dataPath);
				www = new WWW(Application.dataPath + "/LobbyConfig.xml");
			}
			www = new WWW("http://localhost/LobbyConfig.xml");
		    yield return www;
			
		    if (www.error == null)
		    {
			    XmlDocument xmlDoc = new XmlDocument();
			    xmlDoc.LoadXml(www.text);
				
				XmlNodeList ConfigList = xmlDoc.GetElementsByTagName("Config");
				
				foreach (XmlNode ConfigItems in ConfigList) 
		   			{
						XmlNodeList ConfigContent = ConfigItems.ChildNodes;
						
						foreach (XmlNode ConfigurationItems in ConfigContent) 
			   			{	
							if (!Configurations.ContainsKey(ConfigurationItems.Name)){
								Configurations.Add(ConfigurationItems.Name, getValue(ConfigContent, ConfigurationItems.Name) );
							}
						}
					}
				cargado = true;
				foreach( KeyValuePair<string, string> kvp in Configurations )
				    {
							Debug.Log( kvp.Key + ">=>" + kvp.Value); 
				}	
			
				GameObject component = GameObject.Find("lobby");
				if (component != null) {
					LobbyGUI lobbyGUI = component.GetComponent<LobbyGUI>();
					lobbyGUI.enabled = true;
				}
			}
		    else{
		      Debug.Log("ERROR al cargar el archivo de configuracion LobbyConfig.xml: " + www.error);
		    }
	}
	
	private static string getValue(XmlNodeList profileContent,  string key){
			foreach (XmlNode profileItems in profileContent) 
	   			{
					if(profileItems.Name == key){
						return profileItems.InnerText;
					}
				}
			return "";
	}
	
	
	
	public static string getConfig(string Key){
		
		Debug.Log(">>>>>>>>>>>>>>GET " + Key);
		if(Configurations.ContainsKey(Key))
			return Configurations[Key];
		return "";
	}
	

}
