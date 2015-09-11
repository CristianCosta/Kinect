using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;
using AssemblyCSharp;

public class MultiPlayer : MonoBehaviour {
	
	//----------------------------------------------------------
	// Setup variables
	//----------------------------------------------------------
	

	private NextStories backlog;
	private UserVS user;
	public GUISkin windowskin;
	public UserVS getUser()
	{
		return user;
	}

	public bool datosCargados=false;
	

	private static MultiPlayer instance;
	public static MultiPlayer Instance {
		get {
			return instance;
		}
	}
	

	void Awake(){
		instance = this;
	}
	
	public LogLevel logLevel = LogLevel.DEBUG;
	private SmartFox smartFox;
	private static ArrayList listaSprints;
	private static ArrayList listaUsers;
	private static ArrayList listaDailyMeetings; //agregamos esto
	crearPlanningPoker poker;
	SFSObject param;
	
	void Start() {
		
		backlog = (NextStories)GameObject.Find("ButtonNextUS/Center").GetComponent("NextStories");
		poker = (crearPlanningPoker)(GameObject.Find("panelPlanningPoker")).GetComponent("crearPlanningPoker");	
		listaSprints = new ArrayList();
		listaUsers = new ArrayList();
		listaDailyMeetings=new ArrayList();//agregamos esto
		
		/*UserVS u1 = new UserVS("marcelo","scrummaster");
		UserVS u2 = new UserVS("nico","scrummember");
		UserVS u3 = new UserVS("maria","scrummember");
		UserVS u4 = new UserVS("juan","scrummember");
		listaUsers.Add(u1);
		listaUsers.Add(u2);
		listaUsers.Add(u3);
		listaUsers.Add(u4);*/

		if (!SmartFoxConnection.IsInitialized) {
            Debug.Log("Smartfox is not initialized");
			Application.LoadLevel("The Lobby");
			return;
		}
		smartFox = SmartFoxConnection.Connection;
					
		// Register callback delegates
		//smartFox.AddEventListener(SFSEvent.OBJECT_MESSAGE, OnObjectMessage);
		//smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);		
		//smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
		//smartFox.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
		//smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
		//smartFox.AddLogListener(logLevel, OnDebugMessage);
	//	smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);		
	//	smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
	//	smartFox.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
	//	smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
	
		smartFox.AddEventListener (SFSEvent.EXTENSION_RESPONSE, OnExtensionResponce);
		
		param = new SFSObject();
        param.PutLong("id_proyecto",user.getProyecto());
				
		//smartFox.Send(new ExtensionRequest("cargarDatos", param));
		
		smartFox.Send(new ExtensionRequest("cargarDatos",param));
		Debug.Log("cargarDatos");
		//smartFox.Send (new ExtensionRequest("crearus",new SFSObject()));

		//Debug.Log ("mando datos");
		
	}
	
	void FixedUpdate() {
		if (smartFox != null) {
			smartFox.ProcessEvents();			
		}
	}
	
	public SmartFox getSmartFox (){
		return smartFox;
	}
	
	void OnApplicationQuit() {
		Debug.Log("Quitting");
		if (smartFox != null && smartFox.IsConnected)
		{
		smartFox.RemoveAllEventListeners();
		smartFox.Disconnect();
		}
	}
	
	private void OnUserLeaveRoom(BaseEvent evt) {
		
	}
	
	
	public ArrayList getListaSprints(){
		return listaSprints;
	}
	
	public ArrayList getListaUsuarios(){
		return listaUsers;
	}
	
	public ArrayList getListaDailyMeetings(){
		return listaDailyMeetings;
	}
		
	
	public void OnExtensionResponce (BaseEvent evt)
	{
		//Debug.Log ("llegan datos");
				
		string iden = (string)evt.Params ["cmd"];
		SFSObject dataObject = (SFSObject)evt.Params ["params"];
		
//		Debug.Log ("el mensaje es "+ iden );
		
		switch (iden) {
		case "HistorialTestResponse":
			Debug.Log("llego historial");
			cargarHistorial(dataObject);
			break;
		case "cargarDatos":
			cargarDatos(dataObject);
			break;
		/*case "cargarUsuarios":
			cargarUsuarios(dataObject);
			break;*/ //Abstraido a UserVS
		case "crearSprintResponse":
			agregarSprint(dataObject);
			break;
		case "crearUserStoryResponse":
			agregarUserStory(dataObject);
			break;
		case "crearAcceptanceCriteriaResponse":
			Debug.Log("response to added criteria");
			agregarAcceptanceCriteria(dataObject);
			break;
		case "crearAsociacionTareaTestResponse":
			agregarAsociacion(dataObject);
			break;
		case "crearDailyMeetingResponse":
             agregarDailyMeeting(dataObject);
             break;
		case "crearTaskResponse":
			agregarTask(dataObject);
			break;
		case "crearEstimacionResponse":
			agregarEstimacion(dataObject);
			break;
		case "crearTestResponse":
			Debug.Log ("Test Response");
			agregarTest(dataObject);
			break;
		case "modifcarValorEstimacionUSResponse":
			modificarEstimacionUS(dataObject);
			break;
		case "modifcarEstadoEstimacionUSResponse":
			modificarEstadoEstimacionUS(dataObject);
			break;
		case "modifcarEstadoTaskResponse":
			modificarEstadoTarea(dataObject);
			break;
		case "cargarPath":
			Debug.Log("response path!!!!");
			user.setPathProyecto(dataObject.GetUtfString("path"));
			break;
		}
	}
	
	public void setUser(UserVS refUser){
		this.user = refUser;
	}

	private Test getTest(long id)
	{
		ArrayList sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in sprints)
			foreach( UserStory u in s.getListaStories())
				foreach (AcceptanceCriteria ac in u.getListaAcceptanceCriteria())
					foreach (Test t in ac.getAssociatedTests())
						if(t.getIdTest().Equals(id))
							return t;
		
		return null;
	}

	public void cargarHistorial(SFSObject dataObject)
	{
		List<EjecucionTest> lista = new List<EjecucionTest> ();
		ISFSArray historial=dataObject.GetSFSArray("Historial");
		foreach (SFSObject resultado in historial) {
			EjecucionTest e  = new EjecucionTest(resultado); 
			lista.Add(e);
		}
		long id_test;
		if (lista.Count > 0) {
			id_test = lista [0].getIdTest ();
			Test t = getTest (id_test);
			foreach (EjecucionTest e in lista) {
					e.setNombre (t.getName ());		
			}
			t.setHistorial (lista);
		}
	}


	public void cargarDatos(SFSObject dataObject){
		
		ISFSArray sprints=dataObject.GetSFSArray("sprints");

	  	foreach (SFSObject sprintObject in sprints){
			Sprint sprint = new Sprint();
			sprint.fromSFSObject(sprintObject);	
			listaSprints.Add(sprint);		
		}
	
		foreach(Sprint item in listaSprints)
		{	Debug.Log(item.getId_Sprint());
			foreach (UserStory story in item.getListaStories()){
				Debug.Log (story.getDescripcion());
				   foreach (Task tarea in story.getListaTareas()){
				      Debug.Log (tarea.getDescripcion());
				
					}
			}
		}
	
		backlog.cargarVector();
		backlog.cargarInicial();
		
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.inicializar(listaSprints);
		poker.inicializar();
		
		ISFSArray dailyMeetings = dataObject.GetSFSArray("dailyMeetings");
		foreach (SFSObject dailyMeetingObject in dailyMeetings){
            DailyMeeting dailyMeeting = new DailyMeeting();
            dailyMeeting.fromSFSObject(dailyMeetingObject);    
            listaDailyMeetings.Add(dailyMeeting); 
        }
		
		GUI_CargarDailyPanel dailyPanel = (GUI_CargarDailyPanel)(GameObject.Find ("DailyMeetingPlane")).GetComponent("GUI_CargarDailyPanel");
		
		try{
			
			dailyPanel.inicializar(listaDailyMeetings);
			
		//	GUI_CargarDailyPanel usuario = (GUI_CargarDailyPanel)(GameObject.Find ("DailyMeetingPlane")).GetComponent("GUI_CargarDailyPanel");
		//	usuario.inicializar ();
			
		} catch {
			Debug.Log ("el panel lo rompe");
		}
		datosCargados=true;

	}
	public void agregarAsociacion(SFSObject dataObject)
	{
		long id_tarea = dataObject.GetLong ("Id_Task");
		long id_test = dataObject.GetLong ("Id_Test");

		foreach (Sprint s in listaSprints) {
			foreach (UserStory u in s.getListaStories())
				foreach (Task t in u.getListaTareas())
					if (t.getId_Task ().Equals (id_tarea)){
						t.addIdTest(id_test);
				}
		}
	}
	public void agregarAcceptanceCriteria(SFSObject dataObject)
	{
		AcceptanceCriteria ac = new AcceptanceCriteria ();
		ac.fromSFSObject (dataObject);
		agregarAcceptanceCriteriaUserStory (ac);
	}




	public void agregarAcceptanceCriteriaUserStory (AcceptanceCriteria ac)
	{
		foreach (Sprint s in listaSprints){
			foreach (UserStory u in s.getListaStories()){
				if (u.getId_UserStory().Equals(ac.getId_Story())){
					u.addCriteria(ac);
					Debug.Log("id us for criteria " + u.getId_UserStory());
					break;
				}
			}
		}
	}

	public void agregarTest(SFSObject dataObject)
	{
		Test t = new Test ();
		t.fromSFSObject (dataObject);
		agregarTestToAcceptanceCriteria (t);
	}

	public void agregarTestToAcceptanceCriteria (Test t)
	{
		foreach (Sprint s in listaSprints){
			foreach (UserStory u in s.getListaStories())
				foreach (AcceptanceCriteria ac in u.getListaAcceptanceCriteria()){
					if (ac.getId_Criteria().Equals(t.getIdCriterio())){
						ac.addTest(t);
						break;
				}
			}
		}
	}
	
	
	
	
	
	
	
	public void agregarUserStory(SFSObject dataObject){
		Sprint sprint0 = (Sprint)listaSprints[0];
		UserStory story = new UserStory();
		story.fromSFSObject(dataObject);
		sprint0.addUserStory(story);
		backlog.cargarVector();
		backlog.cargarInicial();
		poker.inicializar();
		
	}
	
	public void agregarTask(SFSObject dataObject){
             
             //buscar UserStory padre
             Task tarea = new Task();
             tarea.fromSFSObject(dataObject);
             agregarTareaUserStory(tarea);
             //backlog.addTarea(tarea);
             //backlog.cargarVector();
     //backlog.cargarInicial();
            // Debug.Log("Llego la tarea");

     }
	
	public void agregarTareaUserStory(Task tarea){
		foreach (Sprint s in listaSprints){
			foreach (UserStory u in s.getListaStories()){
				if (u.getId_UserStory().Equals(tarea.getId_Story())){
					u.addTask(tarea);
					break;
				}
			}
		}
		
		
	}
	
	public void agregarSprint(SFSObject dataObject){
		Sprint sprint=new Sprint();
		Sprint sprint0 = (Sprint)listaSprints[0];
		sprint.fromSFSObjectSinUS(dataObject);
		ISFSArray stories=dataObject.GetSFSArray("listaStories");   
	 	foreach(SFSObject story in stories)
		{
			UserStory userStory=new UserStory();
			userStory.fromSFSObject(story);
			sprint.addUserStory(sprint0.removeUserStory(userStory.getId_UserStory()));		
		}
		listaSprints.Add(sprint);
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.inicializar(listaSprints);
	}
	
	public void agregarEstimacion(SFSObject dataObject){
		Estimacion e = new Estimacion(dataObject);
		Sprint sprint0 = (Sprint)listaSprints[0];
		foreach(UserStory us in sprint0.getListaStories()){
			if (us.getId_UserStory().Equals(e.getId_UserStory()))
				us.addEstimacion(e);
		}
		poker.inicializar();	
	}
	
	public void modificarEstimacionUS(SFSObject dataObject){
		UserStory us = new UserStory();
		us.fromSFSObject(dataObject);
		Sprint sprint0 = (Sprint)listaSprints[0];
		foreach(UserStory u in sprint0.getListaStories()){
			if (u.getId_UserStory().Equals(us.getId_UserStory()))
				u.setValorEstimacion(us.getValorEstimacion());
		}
		poker.inicializar();
		backlog.cargarVector();
		backlog.cargarInicial();
	}
	
	public void modificarEstadoEstimacionUS(SFSObject dataObject){
		UserStory us = new UserStory();
		us.fromSFSObject(dataObject);
		Sprint sprint0 = (Sprint)listaSprints[0];
		foreach(UserStory u in sprint0.getListaStories()){
			if (u.getId_UserStory().Equals(us.getId_UserStory())){
				u.setEstadoEstimacion(us.getEstadoEstimacion());
				if (u.getEstadoEstimacion() == 1)
					u.limpiarListaEstimacion();
			}
		}
		poker.inicializar();
	}
	
	public void cambiarEstadoTarea(Task t){
		smartFox.Send(new ExtensionRequest("modificarEstadoTask",t.toSFSObject()));
	}
	
	public void cambiarValorEstimacion(UserStory s){
		smartFox.Send(new ExtensionRequest("modificarValorEstimacionUS",s.toSFSObject()));
	}
	
	public void cambiarEstadoEstimacion(UserStory s){
		smartFox.Send(new ExtensionRequest("modificarEstadoEstimacionUS",s.toSFSObject()));
	}

	public void cambiarTTotalTask(Task t){
	  smartFox.Send (new ExtensionRequest("modificarTTotalTask",t.toSFSObject()));	
	}
	
	public string getMyUserName(){
		int userId  = smartFox.MySelf.Id;
		User userInstance = smartFox.UserManager.GetUserById(userId);
		return userInstance.Name;
	}
	
	public void guardarEstimacion(Estimacion e)
	{
		smartFox.Send(new ExtensionRequest("crearEstimacion",e.toSFSObject()));
	}
	
	public void eliminarEstimacion(Estimacion e){
		smartFox.Send(new ExtensionRequest("eliminarEstimacion",e.toSFSObject()));
	}
	
	public void cargarUsuarios(SFSObject dataObject){
       ISFSArray usuarios=dataObject.GetSFSArray("usuarios");
       foreach (SFSObject userObject in usuarios){
			UserVS u = new UserVS(userObject.GetUtfString("nick"),userObject.GetUtfString("rol"));
            listaUsers.Add(u);
       }
	   smartFox.Send(new ExtensionRequest("cargarDatos", param));
    }
	
	
	//agregamos esto
	 public void agregarDailyMeeting(SFSObject dataObject){
        DailyMeeting dailyMeeting = new DailyMeeting();
        dailyMeeting.fromSFSObject(dataObject);
        listaDailyMeetings.Add(dailyMeeting);
		GUI_CargarDailyPanel dailyPanel = (GUI_CargarDailyPanel)(GameObject.Find ("DailyMeetingPlane")).GetComponent("GUI_CargarDailyPanel");
		dailyPanel.resetIndex();
		dailyPanel.inicializar(listaDailyMeetings);
    }
	
	public void modificarEstadoTarea(SFSObject dataObject){
		//Debug.Log("volviste tarea!");
		Task tarea = new Task();
		tarea.fromSFSObject(dataObject);
		foreach (Sprint sprint in listaSprints)
			foreach (UserStory us in sprint.getListaStories())
				foreach (Task t in us.getListaTareas()){
					if (t.getId_Task() == tarea.getId_Task())
						t.setEstado(tarea.getEstado());
				}
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.actualizar(listaSprints);
					
	}
	
}


