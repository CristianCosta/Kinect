using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowRanking : MonoBehaviour {
	
	public List<TextMesh> idJugadores, puntosJugadores;
	ArrayList ranking;
	public string nombreVistaTop5;
	
	void Start(){
		
		ranking = GameLogManager.getInstance().getHighScores(nombreVistaTop5);
	}
	
	void Update(){
		for (int i=0;i<ranking.Count;i++){
			HighScoreData datos = (HighScoreData)ranking[i];
			idJugadores[i].text = datos.getName();
			puntosJugadores[i].text = datos.getScore();
		}
		
		for (int j=ranking.Count;j<5;j++){
			idJugadores[j].text = "----";
			puntosJugadores[j].text = "----";
		}
	}
}