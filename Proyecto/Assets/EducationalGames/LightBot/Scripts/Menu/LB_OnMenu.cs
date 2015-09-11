using UnityEngine;
using System.Collections;

public class LB_OnMenu : MonoBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("LB_Menu");
		GameLogManager.getInstance().getHighScores("top5lightbot");

	}
}
