using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using AssemblyCSharp;


public class GUI_DetalleStory : MonoBehaviour {

	private bool openWindow = false;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private float x = 15;
	private float y = 15;
	private Rect windowRect;
	private string longString = "This is a long-ish string";
	private List<string> taskNameStrings;
	private List<string> criteriaNameStrings;
	private Vector2 gameScrollPositionTask = new Vector2();
	private Vector2 gameScrollPositionCriteria = new Vector2();
	private int roomSelection = -1;
	private Task t ;
	
	private AcceptanceCriteria ac;
	

	
	private UserStory u;
	
	// Use this for initialization
	void Start () {
	}
	
	public UserStory getUserStory(){
		return u;
	}


	// Update is called once per frame
	void Update () {
	
	}
	
	public void Mostrar(){
		openWindow = true;
	}
	
	public void setUserStory(UserStory us){
		u = us;
	}
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(x,y,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doUserSDetailWindow,"Detalle de UserStory");
		}
		GUI.skin = oldSkin;
	}
	
	
	void doUserSDetailWindow(int windowID){
		SetupTaskList();
		SetupCriteriaList ();
		int heightActual = 30;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"ID UserStory: ",u.getId_UserStory().ToString());
		GUI.contentColor = Color.yellow;
		heightActual = heightActual + 25;
		GUI.Label(new Rect(20,heightActual,310,20),"Descripcion:");
		int offset = 18*((int)(u.getDescripcion().Length/50)+1);

		GUI.contentColor = Color.white;
		heightActual = heightActual + 20;
		GUI.Label(new Rect(40,heightActual,290,offset),u.getDescripcion());
		heightActual = heightActual + offset;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Prioridad: ",u.getPrioridad().ToString());
		
		GUI.contentColor = Color.yellow;	
		heightActual = heightActual + 25;
		GUI.Label (new Rect(20,heightActual,310,20),"Tareas:");
	
		
		
	//Cargar las tareas en area con barra deslizable
		GUI.contentColor = Color.white;
		heightActual = heightActual + 30;
		//GUILayout.BeginArea (new Rect(10, heightActual, 350, 500));
		gameScrollPositionTask = GUI.BeginScrollView (new Rect(5, heightActual, 335, 75),gameScrollPositionTask, new Rect(0,0,270, 25 * taskNameStrings.Count));
	
		int height = 0;
		foreach(string nameTask in taskNameStrings)
		{
			long id = Convert.ToInt64(nameTask.Split(' ')[0]);
			t= getTask(id);
			if (GUI.Button(new Rect (10,height ,220, 20),nameTask)){
				GameObject g = new GameObject();
				g.AddComponent<GUI_DetalleTareaUs>();
				g.SendMessage("setAnterior",this);
				g.SendMessage("setTarea",t);
				g.SendMessage("Mostrar");
				openWindow = false;
			}
			
			if (GUI.Button(new Rect (240, height ,70, 20),"Eliminar")){
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("eliminarTask",t.toSFSObject()));
				EliminarTask(t);
				crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
				planoTask.inicializar(MultiPlayer.Instance.getListaSprints());
				GameObject g = new GameObject ();
				g.AddComponent <GUI_Mensaje>();
				g.SendMessage ("setAnterior", this);
				g.SendMessage ("setTitulo", "OK");
				g.SendMessage ("setMensaje", "Tarea eliminada exitosamente.");
				g.SendMessage ("Mostrar");
				openWindow = false;
			}
			
			
			height += 25;
		}
			
		GUI.EndScrollView ();
		//GUILayout.EndArea ();

		GUI.contentColor = Color.yellow;
		heightActual = heightActual + 90;
		GUI.Label (new Rect(20,heightActual,310,20),"Criterios de Aceptacion:");

	//Cargar los criterios en area con barra deslizable 

		GUI.contentColor = Color.white;
		heightActual = heightActual + 30;

		gameScrollPositionCriteria = GUI.BeginScrollView (new Rect(10, heightActual, 320, 75),gameScrollPositionCriteria, new Rect(0,0,270, 25 * criteriaNameStrings.Count));
		height = 0;
		foreach(string nameCriteria in criteriaNameStrings)
		{
			long id = Convert.ToInt64(nameCriteria.Split(' ')[0]);
			ac= getAcceptanceCriteria(id);
			if (GUI.Button(new Rect (10,height ,220, 20),nameCriteria)){
				GameObject g = new GameObject();
				g.AddComponent<GUI_DetalleCriterio>();
				g.SendMessage("setAnterior",this);
				g.SendMessage("setAcceptanceCriteria",ac);
				g.SendMessage("Mostrar");
				openWindow = false;
			}
			
			if (GUI.Button(new Rect (240, height ,70, 20),"Eliminar")){
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("eliminarAcceptanceCriteria",ac.toSFSObject()));
				EliminarAcceptanceCriteria(ac);
				GameObject g = new GameObject ();
				g.AddComponent <GUI_Mensaje>();
				g.SendMessage ("setAnterior", this);
				g.SendMessage ("setTitulo", "OK");
				g.SendMessage ("setMensaje", "Criterio de aceptacion eliminado exitosamente.");
				g.SendMessage ("Mostrar");
				openWindow = false;
			}
			
			
			height += 25;
		}

		GUI.EndScrollView ();

		GUI.contentColor = Color.white;
		if(GUI.Button(new Rect(maxWidth/3-100,maxHeight-50,100,40),"Agregar"+ System.Environment.NewLine + "Tarea")){
				GameObject g = new GameObject();
				g.AddComponent<GUI_CargarTask>();
				g.SendMessage("Mostrar",this);
				//GUI_CargarTask gct = g.GetComponent("GUI_CargarTask");
				
				openWindow=false;
		}

		if(GUI.Button(new Rect(maxWidth/2-50,maxHeight-50,100,40),"Agregar" + System.Environment.NewLine + "Criterio")){
			GameObject g = new GameObject();
			g.AddComponent<GUI_CargarAcceptanceCriteria>();
			g.SendMessage("setAnterior",this);
			g.SendMessage("Mostrar");
			openWindow = false;

		}
		
		
		
		GUI.contentColor = Color.white;
		if (GUI.Button(new Rect(maxWidth*2/3,maxHeight-50,100,40),"Cerrar")){
				openWindow = false;
				Destroy(this);
			}
		}




	public void EliminarTask(Task t)
	{
		this.u.getListaTareas ().Remove (t);
	}

	public UserStory getUserStory(long id)
	{
		ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in Sprints)
			foreach (UserStory u in s.getListaStories())
				if (u.getId_UserStory ().Equals (t.getId_Story ()))
					return u;
		return null;
	}

	public void EliminarAsociacionesDeTest(Test t, AcceptanceCriteria ac)
	{
		UserStory u= getUserStory( ac.getId_Story ());
		foreach (Task task in u.getListaTareas())
			if (task.getIdTests ().Contains (t.getIdTest ()))
				task.getIdTests ().Remove (t.getIdTest ());
	}

	public void EliminarAcceptanceCriteria(AcceptanceCriteria a)
	{
		foreach (Test t in a.getAssociatedTests())
			EliminarAsociacionesDeTest (t, a);

		this.u.getListaAcceptanceCriteria ().Remove (a);
	}

	public Task getTask(long id)
	{
		ArrayList lista = u.getListaTareas();
		foreach(Task s in lista){
			if(s.getId_Task().Equals(id))
				return s;		
		}
		return null;
	}


	public AcceptanceCriteria getAcceptanceCriteria(long id)
	{
		ArrayList lista = u.getListaAcceptanceCriteria();
		foreach(AcceptanceCriteria s in lista){
			if(s.getId_Criteria().Equals(id))
				return s;		
		}
		return null;
	}

	public void SetupCriteriaList()
	{
		criteriaNameStrings = new List<string> ();
		ArrayList lista = u.getListaAcceptanceCriteria ();
		if(lista != null)
		foreach (AcceptanceCriteria ac in lista)
				criteriaNameStrings.Add (ac.getId_Criteria () + " " + ac.getTitulo ());

	}
		
		public void SetupTaskList () {
			taskNameStrings = new List<string> ();
			ArrayList lista = u.getListaTareas();
			foreach(Task s in lista){
				taskNameStrings.Add(s.getId_Task()+"  "+s.getTitulo());
			}
		}

}

