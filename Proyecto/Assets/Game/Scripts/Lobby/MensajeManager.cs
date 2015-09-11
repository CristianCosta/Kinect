using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MensajeManager : MonoBehaviour {
	public float rango1;
	public float rango2;
	
	public GUIStyle g = new GUIStyle();
	
	public static bool tutoEnabled = true;
	
	private static MensajeManager instanceT;
	
	private EventoAleatorio eAleat;
	
	private float tiempoActual;
	
	public static MensajeManager Instace {
		get {
			return instanceT;
		}
	}
	public static bool dibuja= false;
	public static bool dibujaTecla = false;
	
	private bool dtL = false;
	private bool dtC = false;
	
	private String texto;
	private int max = 50;
	public void mensajeRequestCallback(string data){
		Debug.Log("Mensaje Token: " + data);
		if (data.Equals("no")){
			tutoEnabled = false;
			JugabilidadWindow.toggleTxt = false;
		}else
			JugabilidadWindow.toggleTxt = true;
	}
	
	void initMensjaesAleatorios(){
		eAleat = new EventoAleatorio();
		//eAleat.addEvento("Para poder cambiar de cámara presione la tecla C.");
        eAleat.addEvento("Change the camera by pressing C");
		//eAleat.addEvento("Recuerda que puedes realizar gestos para que \n vean otros usuarios.");
		eAleat.addEvento("Remember that the avatar can play different modes");
		
		//eAleat.addEvento("Puedes deshabilitar los mensajes interactivos \n desde Jugabilidad en Configuración.");
		eAleat.addEvento("You can hide interactive messages from Configuration Options");
		
		//eAleat.addEvento("Si querés que tus conocidos sepan lo que hacés \n en el mundo podés asociarte a las redes sociales.");
		eAleat.addEvento("You can share your play with your friends through Social Networks");
		
		//eAleat.addEvento("¿Querés probar tu rapidez en los cálculos \n matemáticos? No te olvides jugar al Fractions.");
		eAleat.addEvento("Play the game Fractions and check your math skills");
		
		//eAleat.addEvento("Se encuentran disponibles las videoconferencias\n en el mundo virtual.");
		eAleat.addEvento("You can hold videoconferences with your friends");
			
		//eAleat.addEvento("¡Saluda a los demás usuarios desde los gestos!.");
		eAleat.addEvento("Say Hello from the Mode menu");
		
		//eAleat.addEvento("Prueba tu ingenio con Lightbot.");
		eAleat.addEvento("Check your brain by playing the game Lightbot");
		
		//eAleat.addEvento("Diviértete jugando al WordChallenge.");
		eAleat.addEvento("Have fun playing WordChallenge");
		
		//eAleat.addEvento("Necesitás un apuntador presiona la tecla L.");
		eAleat.addEvento("You can point elements pressing the key L");
		
		//eAleat.addEvento("Prueba tu memoria jugando al Memo.");
		eAleat.addEvento("Check your memory by playing the game Memo");
		
		//eAleat.addEvento("Prueba tu rapidez en Big Balls.");
		eAleat.addEvento("Check your visual skills by playing the game Big Balls");
		
		tiempoActual = Time.time;
	}
	
	void Start () {
		
		initMensjaesAleatorios();
		TutorialManagerDB.getInstance().getMensaje(LobbyGUI.user,mensajeRequestCallback);
	}
	
	// Update is called once per frame
	int detectarTeclas()
	{
		if (Input.GetKeyDown(KeyCode.L))
			return 1;
		if (Input.GetKeyDown(KeyCode.C))
			return(2);
		
		return(-1);
	}
	
	void Update () {
		if (tutoEnabled)
		{
			int tec = detectarTeclas();
			if (tec != -1)
			{
				tiempoActual = Time.time;
				dibuja = false;
				dibujaTecla = true;
				switch (tec){
					case 1:{
							if (!dtL){
								texto = "Para deshabilitar el laser presione la tecla L.";
								dtL = true;
							}
							else{
								texto = "Para habilitar el laser presione la tecla L.";
								dtL = false;
							}
							}break;
					case 2: texto = "Para cambiar la camara presione la tecla C.";break;
				
				}
			}
			
			if (!dibujaTecla)
			{
				if ((Time.time - tiempoActual > rango1) &&(!dibuja)){
					dibuja=!dibuja;
					texto = eAleat.getEvento();
					tiempoActual = Time.time;
				}
				if ((Time.time - tiempoActual > rango2) &&(dibuja)){
					dibuja=!dibuja;
					texto = eAleat.getEvento();
					tiempoActual = Time.time;
				}
			}
			else
			{
				if ((Time.time - tiempoActual > 8)){
					dibujaTecla=false;
					tiempoActual = Time.time;
				}
			}
		}		
		
	}
	
	void OnGUI()
	{
		if ((dibuja) || (dibujaTecla)){
			int w,h,a = 0;
			if (texto == null) texto = "";
			int len = texto.Length; 
			
			if (len <max)
			{
				w = (int)((Screen.width/2)-(len*12)/2);
				a = (int)(len*12);
				h=25;
			}
			else{
				w = (int)((Screen.width/2)-(max*12)/2);
				a = (int)(max*12);
				h = 50;
			}
			
			GUI.TextField(new Rect(w,0,a,h),texto,g);
		}
		
	}
	
	
}
