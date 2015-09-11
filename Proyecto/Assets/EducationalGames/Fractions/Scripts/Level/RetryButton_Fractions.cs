using UnityEngine;
using System.Collections;

public class RetryButton_Fractions : MonoBehaviour {
	
	private GameCore_Fractions core; 
	
	public void setGameCore(GameCore_Fractions c){
		
		this.core = c; 
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		
		//Application.LoadLevel("LevelFractions"); //habría que recargar el nivel usando el gamecore, esto es sólo para probar
	
		core.resetBalls(); 
	
	}
}
