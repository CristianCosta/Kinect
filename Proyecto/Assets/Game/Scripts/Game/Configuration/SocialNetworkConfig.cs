using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;
using System.Collections.Generic;

public class SocialNetworkConfig : MonoBehaviour{

	private static Dictionary<string, string> Configurations = null;
	private static bool cargo=false;
	IEnumerator Start(){
		
		Configurations = new Dictionary<string,string>();
		WWW www;
		//string url = "http://localhost:80/SocialNetworkConfig.xml";
		//WWW www = new WWW(Application.dataPath + "SocialNetworkConfig.xml");//ConfigurationConstants.URL_XML_SNCONFIG);
		//if ( Application.isEditor ){
				www = new WWW("http://localhost/SocialNetworkConfig.xml");
		//	} else {
		//		www = new WWW(Application.dataPath + "SocialNetworkConfig.xml");
		//	}
	    yield return www;
		Debug.Log("Termine de bajar");
		
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
			
			foreach( KeyValuePair<string, string> kvp in Configurations )
			    {
						Debug.Log( kvp.Key + ">>" + kvp.Value); 
			}	
			cargo = true;
		}
	    else{
	      Debug.Log("ERROR al cargar el archivo de configuracion SocialNetworkConfig.xml: " + www.error);
			cargo = true;
	    }
		
		Debug.Log("Termine de cargar");
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
		while(!cargo){}
		return Configurations[Key];
	}
	
}
