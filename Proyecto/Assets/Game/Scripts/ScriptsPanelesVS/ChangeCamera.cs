using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {
	
	public string goToCamara="camBotoneraAscensor";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		GameObject primary = GameObject.Find("Main Camera");
		GameObject secondary = GameObject.Find("camBotoneraAscensor");
		Debug.Log("Intentando cambiar de camara: " + goToCamara);
		if (secondary != null){
			secondary.GetComponent<Camera>().enabled = true;
			primary.GetComponent<Camera>().enabled = false;	
		}
	
	}
	
	void OnMouseUpAsButton(){
		GameObject primary = GameObject.Find("Main Camera");
		GameObject secondary = GameObject.Find("camBotoneraAscensor");
		Debug.Log("Intentando cambiar de camara: " + goToCamara);
		if (secondary != null){
			secondary.GetComponent<Camera>().enabled = false;
			primary.GetComponent<Camera>().enabled = true;	
		}

	}


}
