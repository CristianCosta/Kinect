using UnityEngine;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;



public class XML_Parser {
	XmlDocument configFile;

	public XML_Parser(){
		configFile = new XmlDocument();
	}
	
	public XML_Parser (XmlDocument doc){
		configFile = doc;
	}
	
	public void loadFile (string filepath){
		configFile.Load(filepath);
	}
	
	public int getUniqueIntValueByTag(string tag){
		/* Obtiene del archivo un valor entero único dependiendo del tag.
		 * Ej: starttime -> es el tiempo inicial del reloj
		 * 	   pointsNewLetters -> la cantidad mínima de puntos para desbloquear el botón "Nueva Palabra"
		 * */
		XmlNodeList list = configFile.GetElementsByTagName(tag);
		//Debug.Log (list.Item(0).InnerXml); //lee el valor
		int points = 0;
		if (list != null && list.Count > 0)
			points = int.Parse(list.Item(0).InnerXml);
		
		return points;
	}
	
	public Dictionary<int,List<int>> getDataOfWords(){
		
		Dictionary<int,List<int>> data = new Dictionary<int, List<int>>();
		List<int> values;
		int element;
		
		foreach (XmlNode node in configFile.GetElementsByTagName("match")){
			element = int.Parse(node.Attributes.GetNamedItem("amount").Value);
			values = new List<int>();
			values.Add(int.Parse(node.Attributes.GetNamedItem("points").Value));
			values.Add(int.Parse(node.Attributes.GetNamedItem("bonusTime").Value));
			
			data.Add(element,values);
		}
		return data;
	}
	
	public string getPathOfDictionary (string language){
		XmlNodeList lang = configFile.GetElementsByTagName("language");
		bool found = false;
		string path ="";
//		Debug.Log("language = "+language);
		for (int i=0;i<lang.Count && !found;i++){
//			Debug.Log("lang[i]... = "+lang[i].Attributes.GetNamedItem("name").Value);
			if (lang[i].Attributes.GetNamedItem("name").Value.Equals(language)){
				found = true;
				path = lang[i].Attributes.GetNamedItem("dictionary").Value;
			}
		}
		return path;
	}
}

