using UnityEngine;
using System.Collections;

public class OnNextInstruction: MonoBehaviour {
	
	private int pantalla = 1;
	
	// Use this for initialization
	void Start () {
		LoadInstruction(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LoadInstruction(int p){
		GameObject instructions = GameObject.Find("Instructions");	
		GameObject toShow = GameObject.Find("Instructions"+p);	
		if (toShow == null){
			GameObject.Find("TextSiguiente").GetComponent<Renderer>().enabled=false;
			return;
		}
		
		foreach (Transform child in instructions.transform)
			foreach (Transform child2 in child.transform)
				child2.GetComponent<Renderer>().enabled=false;
				
		foreach (Transform text in toShow.transform)
			text.GetComponent<Renderer>().enabled=true;
	}
	
	void OnMouseDown(){
		LoadInstruction (++pantalla);
	}
}
