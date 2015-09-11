using UnityEngine;
using System.Collections;

public class Gui_estimacion : MonoBehaviour {
	
	private bool openWindow = false;
	private float maxWidth = 500;
	private float maxHeight = 470;
	private Rect windowRect;
	private float inicio_x = 15;
	private float inicio_y = 15;
	private string descripcion = "";
	private float card = 0.5f;
	private long inf = 1000;
	
	private Texture2D card1;
	private Texture2D card1s;
	private Texture2D card05;
	private Texture2D card05s;
	private Texture2D card0;
	private Texture2D card0s;
	private Texture2D card2;
	private Texture2D card2s;
	private Texture2D card3;
	private Texture2D card3s;
	private Texture2D card5;
	private Texture2D card5s;
	private Texture2D card8;
	private Texture2D card8s;
	private Texture2D card13;
	private Texture2D card13s;
	private Texture2D card20;
	private Texture2D card20s;
	private Texture2D card40;
	private Texture2D card40s;
	private Texture2D card100;
	private Texture2D card100s;
	private Texture2D cardBack;
	private Texture2D cardBacks;	
	private Texture2D cardI;
	private Texture2D cardIs;	
	
	private Texture2D card_1;
	private Texture2D card_05;
	private Texture2D card_0;
	private Texture2D card_2;
	private Texture2D card_3;
	private Texture2D card_5;
	private Texture2D card_8;	
	private Texture2D card_13;	
	private Texture2D card_20;	
	private Texture2D card_40;
	private Texture2D card_100;
	private Texture2D card_Back;
	private Texture2D card_I;
		
	
	private UserStory u;

	
	// use this for initialization
	void Start () {		
		//Cargo las cartas
		card05  = (Texture2D)Resources.Load("cards/card-05");
	 	card05s = (Texture2D)Resources.Load("cards/card-05s");
		card1  = (Texture2D)Resources.Load("cards/card-1");
	 	card1s = (Texture2D)Resources.Load("cards/card-1s");
	 	card0= (Texture2D)Resources.Load("cards/card-0");
	 	card0s= (Texture2D)Resources.Load("cards/card-0s");
		card2  = (Texture2D)Resources.Load("cards/card-2");
	 	card2s = (Texture2D)Resources.Load("cards/card-2s");
		card3= (Texture2D)Resources.Load("cards/card-3");
		card3s = (Texture2D)Resources.Load("cards/card-3s");
	 	card5 = (Texture2D)Resources.Load("cards/card-5");
		card5s = (Texture2D)Resources.Load("cards/card-5s");
		card8 = (Texture2D)Resources.Load("cards/card-8");
	 	card8s = (Texture2D)Resources.Load("cards/card-8s");
		card13 = (Texture2D)Resources.Load("cards/card-13");
	 	card13s = (Texture2D)Resources.Load("cards/card-13s");
	 	card20 = (Texture2D)Resources.Load("cards/card-20");
		card20s = (Texture2D)Resources.Load("cards/card-20s");
	 	card40 = (Texture2D)Resources.Load("cards/card-40");
	 	card40s = (Texture2D)Resources.Load("cards/card-40s");
	 	card100 = (Texture2D)Resources.Load("cards/card-100");
	 	card100s = (Texture2D)Resources.Load("cards/card-100s");
	 	cardBack = (Texture2D)Resources.Load("cards/card-back");
	 	cardBacks = (Texture2D)Resources.Load("cards/card-backs");	
	 	cardI = (Texture2D)Resources.Load("cards/card-i");
	 	cardIs  = (Texture2D)Resources.Load("cards/card-is");
		cargarCartas();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void cargarCartas(){
		card_05 = card05;
		card_1 = card1;
		card_0 = card0;
		card_2 = card2;
		card_3= card3;
		card_5 = card5;
		card_8 = card8;
		card_13 = card13;
		card_20 = card20;
		card_40 = card40;
		card_100 = card100;
		card_Back = cardBack;
		card_I = cardI;
		
	}
	
	public void Mostrar (){
		openWindow = true;	
	}
	
	void OnMouseUp(){
		Mostrar();
	}
	
	public void setStory(UserStory story){
		u = story;
	}
		
	void OnGUI(){
		if ((openWindow)){
			windowRect = new Rect(inicio_x,inicio_y,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doTaskDetailWindow,"user Story Estimation");
		}
	}
		
	void doTaskDetailWindow(int windowID){
		GUI.contentColor = Color.yellow;
		GUI.Label(new Rect(20,55,400,20),"Description:");
		int offset = 18*((int)(u.getDescripcion().Length/50)+1);
		if (offset > 53){
			offset = 53;
		}
		GUI.contentColor = Color.white;
		GUI.Label(new Rect(40,75,400,offset),u.getDescripcion());
		descripcion = GUIComponents.labelTextArea(new Rect(40,maxHeight-150,maxWidth-120,70),descripcion,"Comments");
		
		if (GUI.Button(new Rect(maxWidth/2,maxHeight-35,100,20),"Cancel")){
			descripcion = "";
			cargarCartas();
			card = 0.5f;
			openWindow = false;	
		}
		if (GUI.Button(new Rect(maxWidth/2+120,maxHeight-35,100,20),"Save")){
			MultiPlayer aux = (MultiPlayer)GameObject.Find("Multi").GetComponent("MultiPlayer");
			string userName = aux.getMyUserName();
			Estimacion est = new Estimacion();
			est.setUser(userName);
			est.setDescripcion(descripcion);
			est.setValorEstimacion(card);
			est.setId_UserStory(u.getId_UserStory());
			///u.addEstimacion(est);
			aux.guardarEstimacion(est);
			descripcion = "";
			cargarCartas();
			card = 0.5f;
			openWindow = false;
			//crearPlanningPoker poker = (crearPlanningPoker)(GameObject.Find("panelPlanningPoker")).GetComponent("crearPlanningPoker");
			//poker.inicializar();
		}
		
		int ancho = 58;
		int largo = 85;
		int separacion = 20;
		int  inicioArriba = 130;
		
		if (GUI.Button(new Rect(20,inicioArriba,ancho,largo),card_0,"label")){
			cargarCartas();
			card_0=card0s;
			card=0;
			
		}     
		
		if (GUI.Button(new Rect(20 + ancho * 1 + separacion * 1,inicioArriba,ancho,largo),card_05,"label")){
			cargarCartas();
			card_05=card05s;
			card=0.5f;
		}
		
		if (GUI.Button(new Rect(20 + ancho * 2 + separacion * 2,inicioArriba,ancho,largo),card_1,"label")){
			cargarCartas();
			card_1=card1s;
			card=1;
		}
		if (GUI.Button(new Rect(20 + ancho * 3 + separacion * 3,inicioArriba,ancho,largo),card_2,"label")){
			cargarCartas();
			card_2=card2s;
			card=2;
		}
		if (GUI.Button(new Rect(20 + ancho * 4 + separacion * 4,inicioArriba,ancho,largo),card_3,"label")){
			cargarCartas();
			card_3=card3s;
			card=3;
		}
		if (GUI.Button(new Rect(20 + ancho * 5 + separacion * 5,inicioArriba,ancho,largo),card_5,"label")){
			cargarCartas();
			card_5=card5s;
			card=5;
		}
		
		if (GUI.Button(new Rect(20,inicioArriba + 100,ancho,largo),card_8,"label")){
			cargarCartas();
			card_8=card8s;
			card=8;
		}
		if (GUI.Button(new Rect(20 + ancho * 1 + separacion * 1,inicioArriba + 100,ancho,largo),card_13,"label")){
			cargarCartas();
			card_13=card13s;
			card=13;
		}
		if (GUI.Button(new Rect(20 + ancho * 2 + separacion * 2,inicioArriba + 100,ancho,largo),card_20,"label")){
			cargarCartas();
			card_20=card20s;
			card=20;
		}
		if (GUI.Button(new Rect(20 + ancho * 3 + separacion * 3,inicioArriba + 100,ancho,largo),card_40,"label")){
			cargarCartas();
			card_40=card40s;
			card=40;
		}
		if (GUI.Button(new Rect(20 + ancho * 4 + separacion * 4,inicioArriba + 100,ancho,largo),card_100,"label")){
			cargarCartas();
			card_100=card100s;
			card=100;
		}
		if (GUI.Button(new Rect(20 + ancho * 5 + separacion * 5,inicioArriba + 100,ancho,largo),card_I,"label")){
			cargarCartas();
			card_I=cardIs;
			card=inf;
		}
	}
	
	
	
}