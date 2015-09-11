using UnityEngine;
using System.Collections;

public class Controller_Scores : MonoBehaviour {
	
	public int x_txtScore,y_txtScore,x_correctWords,y_correctWords;
	
	private int actualScore,correctWordsCount,incorrectWordsCount, totalScore;
	
	public GUIStyle style_scoreText,style_scoreCount,style_correctWordsText,style_correctWordsCount;
	
	public Controller_Camera cameraCont;
	
	// Use this for initialization
	void Start () {
		totalScore = 0;
		this.resetWordsData();
	}
	
	public void resetWordsData(){ //la usa el juego cuando cambia de nivel
		correctWordsCount = 0;
		incorrectWordsCount = 0;
		actualScore = 0;
	}
	
	public void addPointsToScore(int points){
//		Debug.Log ("adding points: "+points);
		totalScore += points;
		actualScore += points;
	}
	
	public void addCorrect(){
//		Debug.Log ("adding correct");
		correctWordsCount++;
	}
	
	public void addMistake(){
//		Debug.Log ("adding incorrect");
		incorrectWordsCount++;
	}
	
	public int getCorrectCount(){
		return correctWordsCount;
	}
	
	public int getMistakesCount(){
		return incorrectWordsCount;
	}
	
	public int getActualScore(){
		return actualScore;
	}
	
	public int getTotalScore(){
		return totalScore;
	}
	
	void OnGUI(){
		//Crea el texto de puntaje
		GUI.Box(new Rect(cameraCont.getOffsetInGameSC_X()+x_txtScore,cameraCont.getOffsetInGameSC_Y()+y_txtScore,150,100),"Puntaje\n  Actual",style_scoreText);
		
		//Muestra el puntaje actual
		GUI.Box(new Rect(cameraCont.getOffsetInGameSC_X()+x_txtScore+125,cameraCont.getOffsetInGameSC_Y()+y_txtScore,150,100),totalScore.ToString(),style_scoreCount);
		
		//Muestra el texto delas palabras correctas e incorrectas
		GUI.Box(new Rect(cameraCont.getOffsetInGameSCW_X()+x_correctWords,cameraCont.getOffsetInGameSC_Y()+y_correctWords,300,50),"Palabras correctas",style_correctWordsText);
		GUI.Box(new Rect(cameraCont.getOffsetInGameSCW_X()+x_correctWords,cameraCont.getOffsetInGameSC_Y()+y_correctWords+45,300,50),"Palabras incorrectas",style_correctWordsText);
				
		//Muestra las cantidades de palabras correctas e incorrectas
		GUI.Box(new Rect(cameraCont.getOffsetInGameSCW_X()+x_correctWords+275,cameraCont.getOffsetInGameSC_Y()+y_correctWords,100,50),correctWordsCount.ToString(),style_correctWordsCount);
		GUI.Box(new Rect(cameraCont.getOffsetInGameSCW_X()+x_correctWords+275,cameraCont.getOffsetInGameSC_Y()+y_correctWords+45,100,50),incorrectWordsCount.ToString(),style_correctWordsCount);
		
		
	}
		
}
