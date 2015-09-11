using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventoAleatorio {
	
	List <String> eventos;
	

	public EventoAleatorio(){
		
		eventos = new List<String>();
	}
	
	public void addEvento(String e){
		
		eventos.Add(e);		
	}
	
	public String getEvento(){
		int pos = UnityEngine.Random.Range(0, eventos.Count);//aleatorios entre 0 y 2
		return eventos[pos];
	}
                 
}
