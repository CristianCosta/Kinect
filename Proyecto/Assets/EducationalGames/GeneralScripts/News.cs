using UnityEngine;
using System.Collections;

public class News{

	private int id;
	private string title;
	private string content;
	
	public News(int identificador, string titulo, string contenido){
		this.id= identificador;
		this.title= titulo;
		this.content= contenido;
	}
	
	public News(string titulo){
		this.title= titulo;
	}
	
	public int getId(){
		return this.id;
	}
	
	public string getTitle(){
		return this.title;
	}
	
	public string getContent(){
		return this.content;
	}
}
