using UnityEngine;
using System.Collections;

public class GUI_Error : windowsManager {
	
	private static float maxWidth = 300;
	private static float maxHeight = 200;
    private int nventana;
	private Rect windowRect;
	
	
	// Use this for initialization
	void Start () {
		//para que se vean las ventanas
		//necesito que el id de cada una sea unico
		nventana = getNumVentana();
        windowRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, maxWidth, maxHeight);
	}
	
	//se dibuja
	void OnGUI () {	
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
        windowRect = GUI.Window(nventana, windowRect, WindowFunction, getTitulo());
		GUI.BringWindowToFront(nventana);	
		GUI.skin = oldSkin;
	}
	
	
	//contiene todo lo que se le puede agregar a la ventana
	void WindowFunction (int windowID) {
		GUI.color = Color.white;
		GUI.Label (new Rect (50, 50, 200, 50), getMensaje());
		GUI.color = Color.white;
		//-----------------------posX,posY,ancho,alto
		if (GUI.Button (new Rect (100, 150, 100, 30), "OK")) 
        {
     		destruirVentana(getNumVentana());
			decVentana();
			//ejecutarOk(getLlamador());
			activarVentana();
		}
        GUI.DragWindow();
	}
}
