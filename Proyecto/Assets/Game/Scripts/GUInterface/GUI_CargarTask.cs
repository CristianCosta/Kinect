using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;

public class GUI_CargarTask : windowsManager {
	
	
	// conexion SmartFox
	public String serverName = "127.0.0.1";
	public int serverPort = 9933;
	SmartFox sfs;
	
	// Elementos del GUI
	private bool sprintWindow = false;
	private int offset;
	private int inicio;
	private int height;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private Rect windowRect;
	private string id_Task = "";
	private string titulo = "";
	private string descripcion = "";
	private string responsable = "";
	private string t_Estimado = "";
	private string t_Total = "";
	private string estado = "";
	private string prioridad = "";
	private string id_Story = "";
	private static List<GameObject> ventanas = new List<GameObject>();
	private int ventananumero;
	
	Vector2 scrollPosition = Vector2.zero;
	Dictionary<string,string> stories = new Dictionary<string, string>();
	Dictionary<string,bool> selected = new Dictionary<string, bool>();
	
	
	//Ventana Anterior
	GUI_DetalleStory ant;
	
	// Use this for initialization
	void Start () {
		ventananumero++;
	}
	
	public void Mostrar(GUI_DetalleStory anterior){
		sprintWindow=true;
		ant = anterior;
	}

	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if (sprintWindow){
			if (esActiva()){
				windowRect = new Rect(15,15,maxWidth,maxHeight);
				windowRect = GUI.Window(0,windowRect,doNewSprintWindow,"Nueva Tarea");
			}
		}
		GUI.skin = oldSkin;
	}
	
	//destruye una ventana segun el ID que viene en el parametro nv
	public void removeVentana(int nv)
    {
        destruirVentana(nv);
		activarVentana();
    }
	
	//destruye una ventana segun el ID que viene en el parametro nv
	//y cierra la ventana principal de la User Story
	public void cerrarTodo(int nv)
    {
        destruirVentana(nv);
		sprintWindow=false;
		
    }
	 
	
	void doNewSprintWindow(int windowID){
		inicio = 30;
		height = 20;
		offset = 0;
		
		offset= 10; inicio = inicio + height;
		titulo = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-40,height),titulo,"Titulo",220);
		inicio = inicio + height + offset;
		descripcion = GUIComponents.labelTextArea(new Rect(20,inicio+offset ,maxWidth-40,height*4),descripcion,"Descripcion");
		inicio = inicio + height*4 + offset;
		responsable =GUIComponents.labelTextField(new Rect(20,inicio+offset ,maxWidth-40,height),responsable,"Responsable",220);
		inicio = inicio + height + offset;
		t_Estimado = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-200,height),t_Estimado,"Tiempo estimado",60);
		inicio = inicio + height + offset;
		t_Total = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-200,height),t_Total,"Tiempo total",60);
		inicio = inicio + height + offset;
		estado = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-200,height),estado,"Estado",60);
		inicio = inicio + height + offset;
		prioridad = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-200,height),prioridad,"Prioridad",60);
		inicio = inicio + height + offset;	
		
		if(GUI.Button(new Rect (maxWidth/3-100,maxHeight-35,140,20), "Cargar Tarea")){
			if (titulo.Equals("") || descripcion.Equals("") || responsable.Equals("")||t_Estimado.Equals("") || t_Total.Equals("") || estado.Equals("")||prioridad.Equals("")){
				desactivarVentana();
				incVentana();
				setTitulo("ERROR");
				setMensaje("Debe completar todos los campos");
				setLlamador("GUI_CargarTask");
				ejecutar("GUI_Error");
				}
			else{	
				Task t = new Task();
				t.setId_Task(this.getIdTask()+1);
				t.setTitulo(titulo);
				t.setDescripcion(descripcion);
				t.setResponsable(responsable);
				t.setT_Estimado(Convert.ToInt32(t_Estimado));
				t.setT_Total(Convert.ToInt32(t_Total));
				t.setEstado(estado);
				t.setPrioridad(Convert.ToInt32(prioridad));
				
				t.setId_Story(ant.getUserStory().getId_UserStory());
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("crearTask",t.toSFSObject()));
				//NextStories button = (NextStories)GameObject.Find("ButtonNextStories").GetComponent("NextStories");
				//button.addTarea(t);
				desactivarVentana();
				incVentana();
				GameObject g = new GameObject();
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/GUI_CargarTask.cs (135,5)", "GUI_Mensaje");
				g.SendMessage("setTitulo","Nueva Tarea");
				g.SendMessage("setMensaje","La Tarea fue cargada exitosamente.");
				g.SendMessage("Mostrar");
				limpiarFormulario();
			}	
		}
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-35,140,20),"Cancelar")){
			sprintWindow = false;
			ant.Mostrar();
			Destroy(this);
		}
		
	}

	public long getIdTask(){
		long max = 0;
		ArrayList sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in sprints){
			foreach( UserStory u in s.getListaStories()){
				foreach (Task t in u.getListaTareas()){
					if (t.getId_Task() > max)
						max = t.getId_Task(); 
				}
			}
		}
		return max;
	}
	public void setSprintWindowsTrue(){
		sprintWindow = true;
	}
	public void limpiarFormulario(){
		titulo = "";
		descripcion = "";
		responsable = "";
		t_Estimado = "";
		t_Total = "";
		estado = "";
		prioridad= "";
		
	}
	
}
