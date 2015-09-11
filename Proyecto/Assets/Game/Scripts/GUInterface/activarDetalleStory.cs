using UnityEngine;
using System.Collections;

public class activarDetalleStory : MonoBehaviour {

	// Use this for initialization
	public GameObject g;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp(){
		Task t1 = new Task();
		t1.setId_Task(1);
		t1.setDescripcion("Esta es una tarea de prueba, que tal?");
		t1.setResponsable("Nicola");
		t1.setEstado("TO DO");
		t1.setT_Estimado(2);
		t1.setT_Total(1);
		t1.setPrioridad(2);
		
		Task t2 = new Task();
		t2.setId_Task(2);
		t2.setDescripcion("Esta es una tarea de prueba, que tal?");
		t2.setResponsable("Chupe");
		t2.setEstado("TO DO");
		t2.setT_Estimado(2);
		t2.setT_Total(1);
		t2.setPrioridad(2);
		
		Task t3 = new Task();
		t3.setId_Task(3);
		t3.setDescripcion("Esta es una tarea de prueba, que tal?");
		t3.setResponsable("Ari");
		t3.setEstado("TO DO");
		t3.setT_Estimado(2);
		t3.setT_Total(1);
		t3.setPrioridad(2);
		
		Task t4 = new Task();
		t4.setId_Task(4);
		t4.setDescripcion("Esta es una tarea de prueba, que tal?");
		t4.setResponsable("Ari");
		t4.setEstado("TO DO");
		t4.setT_Estimado(2);
		t4.setT_Total(1);
		t4.setPrioridad(2);
		
		Task t5 = new Task();
		t5.setId_Task(5);
		t5.setDescripcion("Esta es una tarea de prueba, que tal?");
		t5.setResponsable("Ari");
		t5.setEstado("TO DO");
		t5.setT_Estimado(2);
		t5.setT_Total(1);
		t5.setPrioridad(2);
		
		Task t6 = new Task();
		t6.setId_Task(6);
		t6.setDescripcion("Esta es una tarea de prueba, que tal?");
		t6.setResponsable("Ari");
		t6.setEstado("TO DO");
		t6.setT_Estimado(2);
		t6.setT_Total(1);
		t6.setPrioridad(2);
		
		//g.AddComponent("GUI_DetalleTarea");
		UserStory u = new UserStory();
		ArrayList lista = new ArrayList();
		lista.Add(t1);
		lista.Add(t2);
		lista.Add(t3);
		lista.Add(t4);
		lista.Add(t5);
		lista.Add(t6);
		lista.Add(t1);
		lista.Add(t2);
		lista.Add(t3);
		lista.Add(t4);
		lista.Add(t5);
		lista.Add(t6);
		u.setId_UserStory(123);
		u.setListaTareas(lista);
		u.setDescripcion("Esta es una User Story de prueba, que tal? hhhhhhhhhhhhhhhhhhhhhhhoooooooooooooooooooooooooooooolaaaaaaaaaaaaaaaaa");
		u.setPrioridad(2);
//		u.dibujar();
	}
}
