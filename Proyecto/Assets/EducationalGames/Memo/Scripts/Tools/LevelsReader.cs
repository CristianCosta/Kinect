using UnityEngine;
using System.Collections;
using System.Xml; 

//GENERA UNA LISTA DE TODOS LOS NIVELES DISPONIBLES
//CARGA A PARTIR DEL config.xml dentro de la carpeta Config
public class LevelsReader : Reader{	
	//Cola de todos los niveles
	private Queue levels;	
	
	public LevelsReader() {
		this.levels = new Queue();		
	}
	
//	public LevelsReader(string dir) : base(dir){
//		this.levels = new Queue();		
//	}
	
	public void readLevels(){	  
		//Lee todos los niveles del archivo y los coloca en la cola
		foreach (XmlNode node in this.config.GetElementsByTagName("level")){			
			Level lv = new Level(); 
			lv.setID(int.Parse(node.Attributes.GetNamedItem("ID").Value));
			lv.setBackground(node.Attributes.GetNamedItem("background").Value);
			lv.setPairs(int.Parse(node.Attributes.GetNamedItem("pairs").Value)); 
			lv.setTime(int.Parse(node.Attributes.GetNamedItem("time").Value)); 		
			this.levels.Enqueue(lv);
		}
	}
	
	public Queue getLevels(){
		//Devuelve una cola de todos los Niveles
		return this.levels; 
	}
	
}
