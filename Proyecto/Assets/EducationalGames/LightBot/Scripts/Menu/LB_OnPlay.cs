using UnityEngine;
using System.Collections;

public class LB_OnPlay : MonoBehaviour {

	void OnMouseDown(){
		LB_LevelManager.Init();
		LB_LevelGenerator.Init();
		Application.LoadLevel("LB_Level");
	}
}
