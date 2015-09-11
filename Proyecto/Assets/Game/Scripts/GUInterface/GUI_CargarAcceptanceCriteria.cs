using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;


public class GUI_CargarAcceptanceCriteria : MonoBehaviour {
				
	// conexion SmartFox
	public String serverName = "127.0.0.1";
	public int serverPort = 9933;
	SmartFox sfs;

	// Elementos del GUI
	private bool acceptanceCriteriaWindow = false;
	private int offset;
	private int inicio;
	private int height;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private Rect windowRect;
	private String titulo = "";
	private String descripcion = "";
	private String prioridad = "";
	private int ventananumero;
	private AcceptanceCriteria acceptanceCriteria;

	public AcceptanceCriteria getAcceptanceCriteria()
	{
		return acceptanceCriteria;
	}
	//Ventana Anterior
	GUI_DetalleStory ant;
	
	// use this for initialization
	void Start () {
		ventananumero++;
	}


	public void Mostrar(){
		acceptanceCriteriaWindow=true;
	
	}

	public void setAnterior(GUI_DetalleStory anterior){
		ant = anterior;
	}
	
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if (acceptanceCriteriaWindow){
			{
				windowRect = new Rect(15,15,maxWidth,maxHeight);
				windowRect = GUI.Window(0,windowRect,doNewAcceptanceCriteriaWindow,"Nuevo Criterio de Aceptacion");
			}
		}
		GUI.skin = oldSkin;
	}
	/*
	//destruye una ventana segun el ID que viene en el parametro nv
	public void removeVentana(int nv)
	{
		destruirVentana(nv);
		activarVentana();
	}
	
	//destruye una ventana segun el ID que viene en el parametro nv
	//y cierra la ventana principal de la user Story
	public void cerrarTodo(int nv)
	{
		destruirVentana(nv);
		acceptanceCriteriaWindow=false;
		
	}
*/
	void doNewAcceptanceCriteriaWindow(int windowID){
		inicio = 30;
		height = 20;
		offset = 0;

		offset= 10; 
		inicio = inicio + height;
		titulo = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-40,height),titulo,"Titulo",220);
		inicio = inicio + height + offset;
		descripcion = GUIComponents.labelTextArea(new Rect(20,inicio+offset,maxWidth-40,height*4),descripcion,"Descripcion");
		inicio = inicio + height*4 + offset;
		prioridad = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-200,height),prioridad,"Prioridad",60);
		inicio = inicio + height + offset;	

		if (GUI.Button (new Rect (maxWidth / 3 - 100, maxHeight - 35, 140, 20), "Cargar Criterio")) {
			if (titulo.Equals ("") || descripcion.Equals ("") || prioridad.Equals ("")) {

				GameObject g= new GameObject();
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/GUI_CargarAcceptanceCriteria.cs (102,5)", "GUI_Mensaje");
				g.SendMessage("setAnterior", this);
				g.SendMessage("setTitulo", "ERROR");
				g.SendMessage("setMensaje", "Debe completar todos los campos");
				g.SendMessage("Mostrar");
				acceptanceCriteriaWindow = false;
			}else{
				AcceptanceCriteria ac = new AcceptanceCriteria();
				ac.setId_Criteria(this.getIdCriteria()+1);
				ac.setId_Story(ant.getUserStory().getId_UserStory());
				ac.setDescripcion(descripcion);
				ac.setTitulo(titulo);
				ac.setPrioridad(Convert.ToInt32(prioridad));
		
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("crearAcceptanceCriteria",ac.toSFSObject()));
				MultiPlayer.Instance.getSmartFox().ProcessEvents();
	
				GameObject g= new GameObject();
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/GUI_CargarAcceptanceCriteria.cs (120,5)", "GUI_Mensaje");
				g.SendMessage("setAnterior", this.ant);
				g.SendMessage("setTitulo", "OK");
				g.SendMessage("setMensaje", "Criterio de Aceptacion Agregado Correctamente");
				g.SendMessage("Mostrar");
				acceptanceCriteriaWindow = false;
				limpiarFormulario();
			}
		}

		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-35,140,20),"Cancelar")){
			acceptanceCriteriaWindow = false;
			ant.Mostrar();
			Destroy(this);
		}
	}

	public long getIdCriteria(){
		long max = 0;
		ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in Sprints){
			foreach( UserStory u in s.getListaStories())
				if(u.getListaAcceptanceCriteria()!=null)
					foreach(AcceptanceCriteria ac in u.getListaAcceptanceCriteria()){
						if (ac.getId_Criteria() > max)
							max = ac.getId_Criteria(); 
			}
		}
		return max;
	}


	public void setusWindowsTrue(){
		acceptanceCriteriaWindow = true;
	}
	public void limpiarFormulario(){
		titulo = "";
		descripcion = "";
		prioridad = "";
	}
}

