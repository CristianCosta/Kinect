using UnityEngine;
using System.Collections;

public class EventoSimple{
	
	float time;
	string mensaje;

	public EventoSimple(float t, string m){
		
		time = t;
		mensaje = m;
	}
	
	public float getTime(){
		
		return time;
	}
	
	public string getMensaje(){
		
		return mensaje;
	}
	
	public EventoSimple getEvento(){
		
		return this;
	}
}
