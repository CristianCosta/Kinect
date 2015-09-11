using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;

public class GUI_CargarUS : windowsManager {

	// conexion SmartFox
	public String serverName = "127.0.0.1";
	public int serverPort = 9933;
	SmartFox sfs;
	
	// Elementos del GUI
	private static bool sprintWindow = false;
	float maxWidth = 350;
	float maxHeight = 350;
	float inicio_x = 15;
	float inicio_y = 15;
	float offset = 10;
	Rect windowRect,windowRect2;
	string titulo = "";
	string idSprint = "";
	string descripcion = "";
	string prioridad = "";
	//string acceptance = "";
	string sprint = "";
	Vector2 scrollPosition = Vector2.zero;
	Dictionary<string,string> stories = new Dictionary<string, string>();
	Dictionary<string,bool> selected = new Dictionary<string, bool>();
	private int numeroVentana;

    public GUISkin skinUS;

	void Start () {
		numeroVentana = getNumVentana();
		incVentana();
	}
    
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;

		if (sprintWindow){
			if (esActiva()){
					windowRect = new Rect(Screen.width - maxWidth - inicio_x,inicio_y+120,maxWidth,maxHeight);
					windowRect = GUI.Window(numeroVentana,windowRect,doNewSprintWindow,"New User Story");
				}
		}

		GUI.skin = oldSkin;
	}
	
	void cargarStories(Dictionary<string,string> stories,Dictionary<string,bool> selected){
		limpiarFormulario();
		ArrayList lista=(ArrayList) MultiPlayer.Instance.getListaSprints();
		Sprint sprint0=(Sprint) lista[0];
		ArrayList listastories=sprint0.getListaStories();
		for(int i=0;i<listastories.Count;i++){
			 UserStory user=(UserStory) listastories[i];
			 stories.Add(user.getId_UserStory().ToString(),user.getDescripcion());
			 selected.Add(user.getId_UserStory().ToString(),false);	
		}
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
		
	//crea el formulario de carga de la User Story
	void doNewSprintWindow(int windowID){
		titulo = GUIComponents.labelTextArea(new Rect(inicio_x+offset,inicio_y+offset,maxWidth-40,40),titulo,"Title");
		descripcion = GUIComponents.labelTextArea(new Rect(inicio_x+offset,inicio_y+offset+50,maxWidth-40,80),descripcion,"Description");
		//acceptance = GUIComponents.labelTextArea(new Rect(inicio_x+offset,inicio_y+offset+140,maxWidth-40,80),acceptance,"Acceptance Criteria");
		prioridad = GUIComponents.labelTextField(new Rect(inicio_x+offset,inicio_y+offset+230,inicio_x+100,20),prioridad,"Priority",60);
		if (GUI.Button(new Rect (inicio_x+offset,maxHeight-30,140,20), "Save User Story")){
			//control de campos en null
			if (titulo.Equals("") || descripcion.Equals("") || prioridad.Equals("")){
				desactivarVentana();
				incVentana();			
				setTitulo("ERROR");
				setMensaje("You must complete all fields");
				setLlamador("GUI_CargarUS");
				ejecutar("GUI_Error");
			}
			else
				{
					UserStory user= new UserStory();
					user.setId_UserStory(getIdStory()+1);
					user.setTitulo(titulo);
					user.setDescripcion(descripcion);
					user.setPrioridad(Convert.ToInt32(prioridad));
					user.setId_Sprint(0);
					UserVS userVS = (UserVS) GameObject.Find("Multi").GetComponent("UserVS");
					user.setProyecto(userVS.getProyecto());
				ExtensionRequest ex= new ExtensionRequest("crearUserStory",user.toSFSObject());
				Debug.Log(ex.ToString());
					MultiPlayer.Instance.getSmartFox().Send(ex);

					//NextStories button = (NextStories)GameObject.Find("ButtonNextStories").GetComponent("NextStories");

					desactivarVentana();
					GameObject g = new GameObject();
					UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/GUI_CargarUS.cs (122,6)", "GUI_Mensaje");
					g.SendMessage("setTitulo","New User Story");
					g.SendMessage("setMensaje","La User Story fue cargada exitosamente.");
					g.SendMessage("Mostrar");
					
				}
			limpiarFormulario();
		}
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-30,140,20),"Cancel")){
			limpiarFormulario();
			sprintWindow = false;
			
		}
	}
	public void setSprintWindowsTrue(){
		sprintWindow = true;
	}
	
	void OnMouseUp(){
		if (GUIUtility.hotControl == 0) {
			sprintWindow = true;
			activarVentana ();
		}
	}
	
	public long getIdStory(){
		long max = 0;
		ArrayList sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in sprints){
			foreach( UserStory u in s.getListaStories()){
				if (u.getId_UserStory() > max)
						max = u.getId_UserStory(); 
			}
		}
		return max;
	}
	
	public void limpiarFormulario(){
		titulo = "";
		descripcion = "";
		prioridad= "";
	}
	
}

