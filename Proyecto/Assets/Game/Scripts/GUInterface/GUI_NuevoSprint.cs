using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;

public class GUI_NuevoSprint : MonoBehaviour {
	
	
	// Elementos del GUI
	private bool sprintWindow = false;
	float maxWidth = 350;
	float maxHeight = 400;
	Rect windowRect;
	string idSprint = "";
	string descripcion = "";
	string fec_inicio = "";
	string fec_fin = "";
	Vector2 scrollPosition = Vector2.zero;
	Dictionary<int,UserStory> stories = new Dictionary<int, UserStory>();
	Dictionary<int,bool> selected = new Dictionary<int, bool>();
	
	// Use this for initialization
	void Start () {
		sprintWindow = false;
	}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if (sprintWindow){
			Color c = new Color(150,150,150,1f);
			GUI.color = c;
			windowRect = new Rect(15,15,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doNewSprintWindow,"Nuevo Sprint");
		}
		GUI.skin = oldSkin;
	}
	
	void OnMouseUpAsButton(){
		if (GUIUtility.hotControl == 0) {
			cargarStories (stories, selected);
			sprintWindow = true;
		}
	}
	
	void cargarStories(Dictionary<int,UserStory> stories,Dictionary<int,bool> selected){	
		stories.Clear();
		selected.Clear();
		ArrayList lista=(ArrayList) MultiPlayer.Instance.getListaSprints();
		Sprint sprint0=(Sprint) lista[0];
		ArrayList listastories=sprint0.getListaStories();
		for(int i=0;i<listastories.Count;i++){
			UserStory user=(UserStory) listastories[i];
		 	stories.Add((int)user.getId_UserStory(),user);
		    selected.Add((int)user.getId_UserStory(),false);	
		}
	}
	
	
	public ArrayList getStoriesSelected(Dictionary<int,bool> selected,Dictionary<int,UserStory> stories){
         ArrayList resultado=new ArrayList();		
		 foreach( KeyValuePair<int, bool> parClave in selected )
        {
            if(parClave.Value)
				resultado.Add(stories[parClave.Key]);
				
		}
        return resultado;
		
	}
	
	
	
	
	void doNewSprintWindow(int windowID){
		
		idSprint = GUIComponents.labelTextField(new Rect(20,30,maxWidth-40,20),idSprint,"Titulo",220);
		descripcion = GUIComponents.labelTextArea(new Rect(20,60,maxWidth-40,80),descripcion,"Descripcion");
		fec_inicio = GUIComponents.labelTextField(new Rect(20,150,maxWidth-40,20),fec_inicio,"Fecha de inicio",180);
		fec_fin = GUIComponents.labelTextField(new Rect(20,180,maxWidth-40,20),fec_fin,"Fecha de finalizacion",180);
		scrollPosition = GUIComponents.itemList(new Rect(20,215,maxWidth-40,120),scrollPosition,"User Stories Asociadas",stories,selected);
		if (GUI.Button(new Rect(maxWidth/3-50,maxHeight-30,100,20),"Crear Sprint")){
			Sprint s = new Sprint();
			s.setTitulo(idSprint);
			s.setDescripcion(descripcion);
			char[] delimiterChars = { '/' };
			String[] fecha1 = fec_inicio.Split(delimiterChars);
			if(fecha1 != null)
				s.setFechaInicio(new DateTime(Convert.ToInt32(fecha1[2]),Convert.ToInt32(fecha1[1]),Convert.ToInt32(fecha1[0])));
			String[] fecha2 = fec_fin.Split(delimiterChars);
			if(fecha2 != null)
				s.setFechaFin(new DateTime(Convert.ToInt32(fecha2[2]),Convert.ToInt32(fecha2[1]),Convert.ToInt32(fecha2[0])));
			s.setEstado("Planned");
			ArrayList storiesSeleccionadas=getStoriesSelected(selected,stories);
			UserVS user = MultiPlayer.Instance.getUser();
			s.setId_Proyecto(user.getProyecto());
			s.setListaStories(storiesSeleccionadas);
			SFSObject obj = s.toSFSObject();
			long max = MultiPlayer.Instance.getListaSprints().Count;
			obj.PutLong("Id_Sprint",max);
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("crearSprint",obj));
			sprintWindow = false;
		}			
			//MultiPlayer.Instance
		if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-30,100,20),"Cancelar"))
			sprintWindow = false;
	}
	
}
