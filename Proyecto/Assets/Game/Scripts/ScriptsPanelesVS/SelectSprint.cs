using UnityEngine;
using System.Collections;

public class SelectSprint : MonoBehaviour {
	
	private Sprint s;
	private int index;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setSprint(Sprint sp)
	{
		s = sp;
	}
	
	public void setIndex(int i){
		index = i;
	}
	
	void OnMouseUpAsButton(){
		if (GUIUtility.hotControl == 0) {
			crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find ("panelTaskBoard")).GetComponent ("crearPlanoTask");
			if (index != planoTask.getSprintActual ())
					planoTask.setSprint (index);
			else {
					GUI_CerrarSprint detalle = (GUI_CerrarSprint)GetComponent ("GUI_CerrarSprint");
					detalle.setSprint (s);
					detalle.Mostrar ();
			}
		}
	}
}
