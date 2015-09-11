using UnityEngine;
using System.Collections;

public class Button_New_Letter : MonoBehaviour{

	public GUIStyle btn_NewLetter;
	public int x, y, buttonID; 
	private int clicks = 1;
	private char actualLetter = ' ';
	private bool mustRenderize = false; //para indicar si hay que renderizarlo o no (en caso de que deba estar oculto)
	
	public Controller_Camera cameraCont;

	//Para los datos del botón
	
	public void setActualLetter(char letter){
		actualLetter = letter;
	}
	
	public char getActualLetter (){
		return actualLetter;
	}
	
	public void renderize (bool renderizeValue){ //true > renderize object, false > not renderize
		mustRenderize = renderizeValue;
	}
	
	public bool isRenderized(){
		return mustRenderize;
	}
	
	public int getID(){
		return buttonID;
	}
	
	//Forma 1 de resolver la comunicación con el controlador	
	
	private void notifyClickToController(){
		GameObject controller = GameObject.Find("Controller_NewWord");
		if (controller != null) // -> existe el controlador
			controller.GetComponent<Controller_NewWord>().addPressedButton(this.buttonID);
		else {
			Debug.Log ("El objeto que controla los botones de las letras de la palabra nueva no existe o fue renombrado. Para Solucionarlo, encuentre el nuevo nombre y corrija la línea del Find");
		}
	}
	
	//Forma 2 de resolver la comunicación con el controlador
//	public GameObject controller; //RECORDAR ARRASTRAR EL OBJETO DEL CONTROLADOR EN EL INSPECTOR (dentro de este script, en el campo con el nombre de esta variable)
//	
//	private void notifyClickToController(){
//			controller.GetComponent<Controller_NewWord>().addPressedButton(this.buttonID);
//	}
		
	
	void OnGUI () {
		if (mustRenderize){
			if (GUI.Button(new Rect(cameraCont.getOffsetInGame_X()+cameraCont.getOffsetInGameBSpace(buttonID)+x,cameraCont.getOffsetInGame_Y()+y,65,65),actualLetter.ToString(),btn_NewLetter)){
				//print ("You clicked NEW_LETTER_"+buttonID+" Button! -> "+clicks);
				clicks += 1;
				notifyClickToController();
			}
		}
	}
}

