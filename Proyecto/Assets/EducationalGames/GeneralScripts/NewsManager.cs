using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System;

public class NewsManager{
	
	private static SFSExtensionManager extensionManager;
	private static NewsManager newsManager;
	private Hashtable allNews;
	private int cantNews;
//	private bool downloadFinished = false;
	
	public NewsManager (){
		cantNews=0;
	//	downloadFinished = false;
		allNews= new Hashtable();
		extensionManager= SFSExtensionManager.getInstance();
	}	
		
//	
//	private static NewsManager instance;
//	public static NewsManager Instace {
//		get {
//					
//			return instance;
//		}
//	}
//	
//   
//	
//	void Awake(){
//		extensionManager= SFSExtensionManager.getInstance();
//		instance = this;	
//	}
	
	
//	public static NewsManager getInstance(){
//		if (newsManager == null){
//			newsManager= new NewsManager();	
//		}
//		extensionManager= SFSExtensionManager.getInstance();
//		return newsManager;
//	}	
	
	//Consulta cantidad de noticias
	public int getCantNoticias(){
		return this.cantNews;
	}
	
	//Genera el hash segun la consulta previa
	private void generaNews(SFSObject dataObject){				
		//Genera un hash con todas las noticias ordenando con la clave de id de noticia
		this.allNews.Clear();
		ISFSArray resultado = dataObject.GetSFSArray("result");
		this.cantNews= resultado.Size();//guarda la cantidad de retultados de la consulta
		for (int i=0; i < this.cantNews; i++){
			SFSObject item= (SFSObject) resultado.GetSFSObject(i);
			int identificador= item.GetInt("news_id");								//saca la id	
			string titulo= item.GetUtfString("title");								//saca el titulo
			string contenido= item.GetUtfString("content");							//saca el contenido	
			News nueva= new News(identificador, titulo, contenido);					
			this.allNews.Add(identificador, nueva);												//inserta la noticia nueva al hash	
		}
		//Debug.Log("LLEGUE PRIMERO AL GENERANEWS ----------------------------------------------------------");
		//Debug.Log("TERMINE DE BAJAR LAS NOTICIAS! Hay "+allNews.Count +" noticias   ----------------------------------------------------------");
	}	
	
	//consulta las noticias segun la categoria y facultad
	public void getBackNews(string category, string faculty){
		SFSObject sfsObject = new SFSObject();
		sfsObject.PutUtfString("categoria",category);
		sfsObject.PutUtfString("facultad",faculty);
		//downloadFinished = false;
		extensionManager.sendRequest(new ExtensionRequest("consultarNews", sfsObject), "getNews", getNewsResponse);
	}
	
	private void getNewsResponse(ISFSObject data){
		this.generaNews((SFSObject)data);
		//downloadFinished = true;
	}
	
	//Devuelve una noticia segun su id
	public News getOneNews(int identificador){
		if(this.allNews.ContainsKey(identificador)){
			return (News) this.allNews[identificador];
		}
		else return null;
	}
	
	//Devuelve todas las noticias
	public ArrayList getAllNews(){
		return new ArrayList(this.allNews.Values);
	}
	
//	public bool isDownloadFinished(){
//		return this.downloadFinished;
//	}
	
}


