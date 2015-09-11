using UnityEngine;
using System.Collections;

public class GUI_Ok : windowsManager {

	private static float maxWidth = 300;
	private static float maxHeight = 200;
    private int nventana;
	private Rect windowRect;
	
	// Use this for initialization
	void Start () {
		nventana = getNumVentana();
        incVentana();
        windowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, maxWidth, maxHeight);
	}
	
	//se dibuja
	void OnGUI () {
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		windowRect = GUI.Window(nventana, windowRect, WindowFunction, getTitulo()); 
		GUI.skin = oldSkin;
	}
	

	void WindowFunction (int windowID) {
		GUI.color = Color.white;
		GUI.Label (new Rect (50, 50, 200, 50), getMensaje());
		if (GUI.Button(new Rect (20, 150,125,20), getBoton())){	
			destruirVentana(nventana);
			activarVentana();
			//ejecutarOk(getLlamador());
		}
		if (GUI.Button(new Rect (155, 150,125,20), "Finalizar")){
			destruirVentana(nventana);
		}
		
	}
}
