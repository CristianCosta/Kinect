using UnityEngine;
using System.Collections;

public class avanzarUSTaskboard : MonoBehaviour {
	
	public int accion = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUpAsButton(){
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.cambiarUserStory(accion);
	}
	
}
