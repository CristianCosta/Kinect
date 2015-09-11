using UnityEngine;
using System.Collections;

public class buttonTextures : MonoBehaviour {
	
	public Material activo,sobre;
	
	void Start(){
		GetComponent<Renderer>().material = activo;
	}
	
	void OnMouseOver(){
		GetComponent<Renderer>().material = sobre;
	}
	
	void OnMouseExit(){
		GetComponent<Renderer>().material = activo;
	}
}
