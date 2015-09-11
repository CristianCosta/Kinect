using UnityEngine;
using System.Collections;

public class InGameQuitButton_Fractions : MonoBehaviour {
	
	public GameCore_Fractions core;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		core.attendEvent("quitGame");
	}
}
