using UnityEngine;
using System.Collections;

public class LB_OnInstructions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		Application.LoadLevel("LB_Instructions");
	}
}
