using UnityEngine;
using System.Collections;
using System.Xml; 
//Clase Madre para todos los Generadores encargados de Leer la informacion almacenada 
//en archivos xml

public class Reader{
            
	public XmlDocument config;      //Archivo XML
	
	public Reader(){
		//Guarda la direccion del archivo y prepara el objeto xml
		this.config = XmlFile.doc; 	
	}
	
//	public Reader(string p){
//		//Guarda la direccion del archivo y prepara el objeto xml
//		//ACA SE DEBERIA CARGAR EL ARCHIVO DESDE EL PATH (p)
//		this.config = XmlFile.doc; 	
//	}
	
}
