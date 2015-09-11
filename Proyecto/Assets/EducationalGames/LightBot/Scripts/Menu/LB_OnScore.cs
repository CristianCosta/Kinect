using UnityEngine;
using System.Collections;

public class LB_OnScore : MonoBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("LB_HighScores");
	}
}
