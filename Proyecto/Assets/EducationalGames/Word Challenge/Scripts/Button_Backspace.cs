using UnityEngine;
using System.Collections;

public class Button_Backspace : MonoBehaviour {

	public Texture2D icon;
	public GUIStyle btn_BackspaceStyle;
	private int clicks = 1;
	
	//Controlador de letras seleccionadas
	public GameObject controllerSWord; /*Debe conocerlo para informarle que debe borrar una letra (la última agregada)
										Se decidió tenerlo como un objeto interno para simplificar la comunicación y
										evitar las búsquedas y/o el uso de eventos	*/
	public Controller_Camera cameraCont;

	
	void OnGUI () {
		if (GUI.Button(new Rect(cameraCont.getOffsetInGameLB_X()+662,cameraCont.getOffsetInGameLB_Y()+105,80,52),icon,btn_BackspaceStyle)){
				//print ("You clicked Backspace Button! -> "+clicks);
				controllerSWord.GetComponent<Controller_SelectedWord>().deleteLastLetter();
				clicks+=1;
		}
	}
}