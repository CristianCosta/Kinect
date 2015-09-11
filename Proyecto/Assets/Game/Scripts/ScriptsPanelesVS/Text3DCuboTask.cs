using UnityEngine;
using System.Collections;

public class Text3DCuboTask : Text3DCubo {
	
	private Task tarea;
	
	
	public Text3DCuboTask(Task t):base (t.getTitulo()+"\n"+t.getResponsable()){
	 tarea=t;
		
	
	
	}
	
	public override void dibujarObjetoEnQuad (GameObject padre,int fontsize){
		base.dibujarObjetoEnQuad(padre,fontsize);
		//REDUNDANTE :S //this.tarea=tarea;
		if (this.tarea.getPrioridad() == 1)
			plano.GetComponent<Renderer>().material = (Material)Resources.Load("task-high");
		else
			if (this.tarea.getPrioridad() == 2)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("task-medium");
			else
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("task-low");
		plano.AddComponent<taskBehaviour>();
		taskBehaviour script = (taskBehaviour)plano.GetComponent("taskBehaviour");
		script.setTarea(this);
		plano.AddComponent<GUI_DetalleTarea>();
		GUI_DetalleTarea detalle = (GUI_DetalleTarea)plano.GetComponent("GUI_DetalleTarea");
		detalle.setTarea(this.tarea);
	}
	
	public void changeEstado(){
		if (tarea.getEstado().Equals("TO DO")){
			tarea.setEstado("DOING");
			tarea.set_fecha_ultimo_cambio(System.DateTime.Now);
		}
		else {
			if(tarea.getEstado().Equals("DOING")){
				tarea.setEstado("ON TEST");
				tarea.set_fecha_ultimo_cambio(System.DateTime.Now);
			}
			else
			if (tarea.getEstado().Equals("ON TEST")){
				if(superoTests(tarea)){
					tarea.setEstado("DONE");
					tarea.set_fecha_ultimo_cambio(System.DateTime.Now);
				}
				else
				{
					GameObject g = new GameObject();
					UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/ScriptsPanelesVS/Text3DCuboTask.cs (53,6)", "GUI_Mensaje");
					g.SendMessage("setTitulo","ERROR");
					g.SendMessage("setMensaje","No se completaron con exito todos los tests.");
					g.SendMessage("Mostrar",this);
				}
			}
		}

		MultiPlayer.Instance.cambiarEstadoTarea(tarea);
	}
	
	// EVENTO CREAR DETALLE TAREA 2ble click  guardar tiempo anterior con el actual.
	
	
	public bool superoTests(Task tarea){
		foreach (long idTest in tarea.getIdTests()) 
			if (!tarea.getTest (idTest).getEstado ().Equals ("EXITO"))
					return false;
		return true;
	}
}
