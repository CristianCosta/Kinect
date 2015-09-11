using UnityEngine;
using System.Collections;

public class Button_NewWord: MonoBehaviour {

	//Variables del botón
	public Texture2D icon;
	public GUIStyle btn_newWordStyle;
	public ArrayList sprites;
	public int x,y;
	
	public Game game;
	
	//Controladores
	public Controller_NewWord newWordCont;
	public Controller_SelectedWord selWordCont;
	public Controller_SolutionsVisualization solVisualCont;
	public Controller_Scores scoresCont;
	
	public Controller_Camera cameraCont;
	
	private bool enable = true, firstClick = true;
	
	
	void OnGUI () {
		if (GUI.Button(new Rect(cameraCont.getOffsetInGame_X()+x,cameraCont.getOffsetInGame_Y()+y,125,63),icon,btn_newWordStyle)){
			if (firstClick || scoresCont.getActualScore() >= game.getPointsForNewWord()){
					enable = true;
					firstClick = false;
				}
				else
					enable = false;
			
			if (enable){
				if (game.isMatchFinished()) //si el match terminó -> inicia uno nuevo
					game.startNewMatch();
				else{
					scoresCont.resetWordsData(); //match no terminó pero van a cambiar de palabras -> se reinician los datos.			
				}
				
				//elegir palabra de diccionario y luego cargarla
				string selectedWord = game.getNewWord().ToUpper();
				//Debug.Log (" palabra "+selectedWord);
				
				//Le dice al controlador de las palabras nuevas  que agregue la nueva palabra
				newWordCont.chargeNewWord(selectedWord);
				
				//Le dice al controlador de las palabras seleccionadas que elimine la palabra que se estaba armando (si es que había una)
				selWordCont.removeAllLetters();
				
				//Indica la cantidad máxima de palabras de cada cantidad de letras
				solVisualCont.setWordLeftCount (game.getCountOfSolutionsOf(6),game.getCountOfSolutionsOf(5),game.getCountOfSolutionsOf(4),game.getCountOfSolutionsOf(3));
			}					
		}
	}
}