using UnityEngine;
using System.Collections;
using System.Xml;

public class CurrentConfig {

	static string language = "spanish-words"; //lenguage por defecto
	static XmlDocument configFile;
		
	public static void setCurrentLanguage(string newLang){
		Debug.Log("New lang: "+newLang);
		language = 	newLang;
	}
	
	public static string getCurrentLanguage (){
		return language;
	}
	
	public static void setConfigFile (XmlDocument doc){
		configFile = doc;
	}
	
	public static XmlDocument getConfigFile(){
		return configFile;	
	}
	
}
