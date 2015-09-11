using UnityEngine;
using System.Collections;

//Se encarga de mostrar hasta 5 valores de high scores

public class LB_HighScoresManager : MonoBehaviour {
	
	private ArrayList players; 
	
	public TextMesh name1,score1,name2,score2,name3,score3,name4,score4,name5,score5; 
	
	// Use this for initialization
	void Start () {	
		
		this.players=new ArrayList();
		this.players= GameLogManager.getInstance().getHighScores("top5lightbot");	
	
		this.showText(); 
	}	
	
	public void setPlayers(ArrayList playersScores){
		
		this.players = playersScores; 
		
	}
	
	public void addPlayer(HighScoreData hs){
		
		this.players.Add(hs);
		
	}
	
	private void updateText(int pos, HighScoreData hs){
		
		TextMesh player = null, score = null; 
		
		if(pos == 0){
		
			player = this.name1;
			score = this.score1;
			
		}
		else{
			
			if(pos == 1){
				player = this.name2;
				score = this.score2;
			}
			else{
				if(pos==2){
					player = this.name3;
					score = this.score3;
				}
				else{
					if(pos==3){
						player = this.name4;
						score = this.score4;
					}
					else{
						if(pos==4){
							player = this.name5;
							score = this.score5;
						}
					}
				}
			}
			
		}
		
		if(player != null && score != null){
			player.text = hs.getName();
			score.text = hs.getScore(); 
		}
		
	}
	
	private void showText(){
		
		for(int i = 0; i<this.players.Count && i<5; i++){
			
			HighScoreData hs = (HighScoreData) this.players[i];
			this.updateText(i,hs);
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
		this.showText(); 
		
	}
}
