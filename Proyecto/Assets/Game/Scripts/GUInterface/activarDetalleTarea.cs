using UnityEngine;
using System.Collections;

public class activarDetalleTarea : MonoBehaviour {

	// Use this for initialization
	public GameObject g;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp(){
		//g.AddComponent("GUI_DetalleTarea");
		Task t1 = new Task();
		t1.setId_Task(1);
		t1.setDescripcion("Esta es una tarea de prueba, que tal?");
		t1.setResponsable("Nicola");
		t1.setEstado("TO DO");
		t1.setT_Estimado(2);
		t1.setT_Total(1);
		t1.setPrioridad(2);
//		t1.dibujar();
		
		Task t2 = new Task();
		t2.setId_Task(2);
		t2.setDescripcion("Esta es una tarea de prueba, que tal?");
		t2.setResponsable("Chupe");
		t2.setEstado("TO DO");
		t2.setT_Estimado(2);
		t2.setT_Total(1);
		t2.setPrioridad(2);
		//t2.dibujar();
		
		Task t3 = new Task();
		t3.setId_Task(3);
		t3.setDescripcion("Esta es una tarea de prueba, que tal?");
		t3.setResponsable("Ari");
		t3.setEstado("TO DO");
		t3.setT_Estimado(2);
		t3.setT_Total(1);
		t3.setPrioridad(2);
		//t3.dibujar();
	}
	
}
