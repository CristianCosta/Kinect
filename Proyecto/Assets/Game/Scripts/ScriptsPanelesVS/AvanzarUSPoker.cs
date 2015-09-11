using UnityEngine;
using System.Collections;

public class AvanzarUSPoker : MonoBehaviour {
	
	public int accion = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUpAsButton(){
		if (GUIUtility.hotControl == 0) {
			crearPlanningPoker poker = (crearPlanningPoker)(GameObject.Find ("panelPlanningPoker")).GetComponent ("crearPlanningPoker");
			poker.cambiarUserStory (accion);
		}
	}
}
