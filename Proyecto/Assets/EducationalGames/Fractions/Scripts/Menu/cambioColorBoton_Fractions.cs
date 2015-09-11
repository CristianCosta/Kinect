using UnityEngine;
using System.Collections;

public class cambioColorBoton_Fractions : MonoBehaviour {
	
	public TextMesh butText; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver(){
		
		butText.GetComponent<Renderer>().material.color = Color.green; 
		
	}
	
	void OnMouseExit(){
	
		butText.GetComponent<Renderer>().material.color = Color.white; 
	
	}
}
