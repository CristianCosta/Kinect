using UnityEngine;
using System.Collections;
using System.Xml; 

//GENERA UN ARREGLO CON LAS CARTAS DISPONIBLES
public class CardsReader : Reader{
	//Lista de cartas del config.xml
	private ArrayList cards;	
	
	public CardsReader(){
		this.cards = new ArrayList(); 			
	}
	
//	public CardsReader(string dir): base(dir){
//		this.cards = new ArrayList(); 			
//	}
	
	public void readCards(){                      
		//Lee todas las cartas del archivo y las coloca en el ArrayList			
		foreach (XmlNode node in this.config.GetElementsByTagName("card")){			
			Card c = new Card(); 			
			c.setID(node.Attributes.GetNamedItem("value").Value); 
			c.setPath(node.Attributes.GetNamedItem("image").Value); 			
			this.cards.Add(c);
		}
	}
	
	public ArrayList getCards(){
		//Devuelve un ArrayList con todas las cartas
		return this.cards; 	
	}	
	
}
