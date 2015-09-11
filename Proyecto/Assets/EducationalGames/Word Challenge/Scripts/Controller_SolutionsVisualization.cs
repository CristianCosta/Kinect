using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller_SolutionsVisualization : MonoBehaviour {
	
	private int sixLetterWordsLeftCount,fiveLetterWordsLeftCount,fourLetterWordsLeftCount,threeLetterWordsLeftCount;
	public int x_6,y_6,x_5,y_5,x_4,y_4,x_3,y_3; //coordenadas de las cajitas
	public int x_txt_sol,y_txt_sol,x_txt_pal,y_txt_pal,x_wordsLeft,y_wordsLeft,x_txt_6,y_txt_6,x_txt_5,y_txt_5,
				x_txt_4,y_txt_4,x_txt_3,y_txt_3;
	
	public GUIStyle style_WordsLeft,style_text,style_numbers,style_solutions,style_solutionsTitle,style_floatingPanels;
	
	private string discoveredSolutions,newWord;
	
	private int charactersInLine;
	
	public Controller_Camera cameraCont;
	
	private bool panelEndOFMatchActivated, panelLoserActivated;
		
	// Use this for initialization
	void Start () {
		setWordLeftCount(0,0,0,0);
		
		panelEndOFMatchActivated = false;
		panelLoserActivated = false;
		
	}
	
	public void setWordLeftCount (int sixLetterLeft, int fiveLetterLeft, int fourLetterLeft, int threeLetterLeft){
		discoveredSolutions = "";
		newWord ="";
		charactersInLine = 0;
		
		sixLetterWordsLeftCount = sixLetterLeft;
		fiveLetterWordsLeftCount = fiveLetterLeft; 
		fourLetterWordsLeftCount = fourLetterLeft;
		threeLetterWordsLeftCount = threeLetterLeft;		
	}
	
	public void addCorrectResult (string c_word){
		//agrega la palabra descubierta nueva (una solución) a la "tabla" de palabras descubiertas y decrementa la variable correspondiente
		if (c_word.Length == 6)
			sixLetterWordsLeftCount--;
		else if (c_word.Length == 5)
				fiveLetterWordsLeftCount--;
			else if (c_word.Length == 4)
					fourLetterWordsLeftCount--;
				else if (c_word.Length == 3)
						threeLetterWordsLeftCount--;
		newWord = c_word;
		
		//ACA hay que agregar la palabra al cuadro de palabras.
	}
	
	public void setWasEndOfMatch(string result){
		if (result.Equals("win"))
			panelEndOFMatchActivated = true; //debería mostrar el panel
		else //result=lose ->perdió!
			panelLoserActivated = true;
	}
	
	public void setStartOfMatch(){
		panelEndOFMatchActivated = false;
		panelLoserActivated = false;
	}
	
	
	void OnGUI () {
		//Crea el texto de palabras restantes
		GUI.Box(new Rect(cameraCont.getOffsetInGame_X()+x_wordsLeft,cameraCont.getOffsetInGameWTable_Y()+y_wordsLeft,250,100),"Palabras\n    Restantes",style_WordsLeft);
		
		//Crea los textos de las palabras restantes
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_txt_6,cameraCont.getOffsetInGameWTable_Y()+y_txt_6,200,100),"De 6\nletras",style_text);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(1)+x_txt_5,cameraCont.getOffsetInGameWTable_Y()+y_txt_5,200,100),"De 5\nletras",style_text);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(2)+x_txt_4,cameraCont.getOffsetInGameWTable_Y()+y_txt_4,200,100),"De 4\nletras",style_text);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(3)+x_txt_3,cameraCont.getOffsetInGameWTable_Y()+y_txt_3,200,100),"De 3\nletras",style_text);
		
				
		//Crear las cajitas donde se mostrarán las letras restantes
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_6,cameraCont.getOffsetInGameWTable_Y()+y_6,99,99),sixLetterWordsLeftCount.ToString(),style_numbers);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(1)+x_5,cameraCont.getOffsetInGameWTable_Y()+y_5,99,99),fiveLetterWordsLeftCount.ToString(),style_numbers);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(2)+x_4,cameraCont.getOffsetInGameWTable_Y()+y_4,99,99),fourLetterWordsLeftCount.ToString(),style_numbers);
		GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(3)+x_3,cameraCont.getOffsetInGameWTable_Y()+y_3,99,99),threeLetterWordsLeftCount.ToString(),style_numbers);
		
		//Crea la caja con la lista de soluciones si no frenaron el juego por alguna causa
		if (!panelEndOFMatchActivated && !panelLoserActivated){
			//Muestra las soluciones
			GUI.Box(new Rect(cameraCont.getOffsetInGame_X()+x_txt_pal,cameraCont.getOffsetInGameWTable_Y()+y_txt_pal,300,100),"Palabras descubiertas: ",style_solutionsTitle);
			
			if (newWord.Length != 0){
			//agrego la nueva palabra
				if (charactersInLine + newWord.Length + 2 > 60){ //+1 por la coma y el espacio
					discoveredSolutions+= "\n";
					charactersInLine = 0;
				}
				discoveredSolutions+= newWord + ", "; //agrega la nueva palabra
				charactersInLine+= newWord.Length + 2;
				
				newWord=""; //espera a la próxima palabra
			}
		
			GUI.Box(new Rect(cameraCont.getOffsetInGame_X()+x_txt_sol,cameraCont.getOffsetInGameWTable_Y()+y_txt_sol,650,400),discoveredSolutions,style_solutions);
		}
		else if (panelEndOFMatchActivated){
			GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_txt_pal,cameraCont.getOffsetInGameWTable_Y()+y_txt_pal-50,650,250),"GANASTE!!!!",style_floatingPanels);
			GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_txt_pal,cameraCont.getOffsetInGameWTable_Y()+y_txt_pal+50,650,250),"Clickea en Nueva Palabra para seguir jugando \n (Y seguir sumando puntos!!)",style_solutionsTitle);
		}
		else{ //activa el mensaje de que perdió!
			GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_txt_pal,cameraCont.getOffsetInGameWTable_Y()+y_txt_pal-50,650,250),"Perdiste!!",style_floatingPanels);
			GUI.Box(new Rect(cameraCont.getOffsetInGameWTable_X(0)+x_txt_pal,cameraCont.getOffsetInGameWTable_Y()+y_txt_pal+50,650,250),"(Se te acabo el tiempo) \nVolve al menu para jugar de nuevo",style_solutionsTitle);
		}
	}
	
	
}
