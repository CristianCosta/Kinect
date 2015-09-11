using UnityEngine;
using System.Collections;

public class LetterSelected: MonoBehaviour{

	public GUIStyle style_SelectedLetter;
	public int x, y,letterID; 
	//private int clicks = 1;
	private char actualLetter = ' ';
	private bool mustRenderize = false; //para indicar si hay que renderizarlo o no (en caso de que deba estar oculto)
	public Controller_Camera cameraCont;

	public void setActualLetter(char letter){
		actualLetter = letter;
	}
	
	public char getActualLetter (){
		return actualLetter;
	}
	
	public void renderize (bool renderizeValue){ //true > renderize object, false > not renderize
		mustRenderize = renderizeValue;
	}
	
	void OnGUI () {
		if (mustRenderize){
			GUI.Box(new Rect(cameraCont.getOffsetInGameLS_X()+cameraCont.getInGameLS_Space(letterID)+x,cameraCont.getOffsetInGameLS_Y()+y,96*cameraCont.getInGameLS_Scaler(),96*cameraCont.getInGameLS_Scaler()),actualLetter.ToString(),style_SelectedLetter);			
		}
	}
}

