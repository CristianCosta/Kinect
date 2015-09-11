using UnityEngine;
using System.Collections;

public class PlayOnClick : MonoBehaviour {
	
	private OTSound onClick; 
	
	// Use this for initialization
	void Start () {
	
		this.onClick = new OTSound("menuSelect"); 
		this.onClick.Idle(); 
		
	}
	
	// Update is called once per frame
	void Update () {
	
		
		
	}
	
	void OnMouseDown(){
		
		this.onClick.Play(); 

	}
}
