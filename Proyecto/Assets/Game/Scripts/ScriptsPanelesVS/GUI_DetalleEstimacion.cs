using UnityEngine;
using System.Collections;


public class GUI_DetalleEstimacion : MonoBehaviour {
	
	public bool openWindow;
	private float maxWidth = 350;
	private float maxHeight = 260;
	private Rect windowRect;
	//private string puntos="";
	
	private Estimacion est;
	Texture2D card;
	
	
	// use this for initialization
	void Start () {
		openWindow = false;
		/*est = new Estimacion();
		est.setDescripcion("El desarrollo con esta nueva tecnologia sera dificil para el grupo, sumando " +
			"al nuevo proceso que necesita de personas involucradas y comprometidas estimo de una dificultad alta");
		est.setuser("Pepe");
		est.setValorEstimacion(5);
		card =  (Texture2D)Resources.Load("cards/card-" + est.getValorEstimacion());*/	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void setEstimacion(Estimacion e){
		this.est = e;
		card =  (Texture2D)Resources.Load("cards/card-" + e.getValorEstimacion());
	}
	
	public void Mostrar(){
		openWindow = true;
	}
		
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(15,15,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doTaskDetailWindow,"Detalle de tarea");
		}
		GUI.skin = oldSkin;
	}
		
	void doTaskDetailWindow(int windowID){
		GUIComponents.labelDetail(new Rect(20,30,310,20),"usuario: ",est.getUser());
		GUI.contentColor = Color.yellow;
		GUI.Label(new Rect(20,55,310,20),"Comentario:");
		
		GUI.contentColor = Color.white;
		GUI.Label(new Rect(40,75,290,95),est.getDescripcion());
		
		int ancho = 58;
		int largo = 85;
		if (GUI.Button(new Rect(60,160,ancho,largo),card,"label")){
			
		}
		 
		if (GUI.Button(new Rect(maxWidth/2,maxHeight-40,100,20),"Cerrar")){
			openWindow = false;
		}
	}
	
	void OnMouseUpAsButton(){
		if(GUIUtility.hotControl == 0)
		Mostrar();
	}
	
}

