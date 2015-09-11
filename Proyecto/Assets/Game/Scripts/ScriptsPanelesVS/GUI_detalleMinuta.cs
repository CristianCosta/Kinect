using UnityEngine;


public class GUI_detalleMinuta : MonoBehaviour{
		
	private bool showMinutaWindows = false;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private float x = 15;
	private float y = 15;
	private Rect rectWindow;
	
	void start(){}
	
	void update(){}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if(showMinutaWindows){
			rectWindow = new Rect(x,y,maxWidth,maxHeight);
			rectWindow = GUI.Window(1,rectWindow,doNewMinutaWindow,"Detalle de Minuta");
		}
		GUI.skin = oldSkin;
		
	}
	
	
	void doNewMinutaWindow(int windowID){
		
		
		GUI.Label(new Rect(20,55,310,20),"Fecha de Minuta:");
		GUI.Label(new Rect(20,75,310,20),"Scrum Member:");
		GUI.Label(new Rect(20,95,310,20),"Que hizo Ayer?:");
		GUI.Label(new Rect(20,115,310,20),"Que va a hacer hoy?:");
		GUI.Label(new Rect(20,200,310,20),"Inconvenientes:");
		
		
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-30,140,20),"Cerrar")){
			showMinutaWindows = false;
		}
	}
	
	void OnMouseUpAsButton(){
		showMinutaWindows = true;
	}
}