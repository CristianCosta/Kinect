using UnityEngine;
using System;
using System.Linq;

public class Button_MixLetters : MonoBehaviour {
	
	//Variables del botón
	public Texture2D icon;
	public GUIStyle btn_MixLettersStyle;
	private int clicks = 1;
	
	//Controlador de las letras nuevas
	public Controller_NewWord controllerNWord;
	public Controller_Camera cameraCont;
		
	public char[] randomizeCharacters (string word){ //revisar esta función
		// The random number sequence
		System.Random num = new System.Random();

		// Create new string from the reordered char array and returns it
		return (word.ToCharArray().OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
	}
	
	void OnGUI () {
		if (GUI.Button(new Rect(cameraCont.getOffsetInGameLB_X()+633,cameraCont.getOffsetInGameLB_Y()+cameraCont.getOffsetInGameBSpace(2)+232,110,55),icon,btn_MixLettersStyle)){
			//print ("You clicked Mix Letters Button! -> "+clicks);
			clicks+=1;
			/* Obtiene los caracteres a aleatorizar (cambiar de posiciones), los aleatoriza e indica al controlador
			 * de nuevas palabras (controllerNWord) e indica como reordenar los caracteres (simplemente le pasa la secuencia
			 * ya aleatorizada y el controladore de nuevas palabras (que es quien sabe que box_new_letter (cajita para la letra)
			 * está ocupada y visible y cual no -> sabe como llenarlas de nuevo). 
			 */
			controllerNWord.setRandomizedCharacters(randomizeCharacters(controllerNWord.getCharactersToRandomize()));
		}
	}
}
