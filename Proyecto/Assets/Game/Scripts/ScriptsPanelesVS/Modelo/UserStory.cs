using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Sfs2X.Protocol.Serialization;
using Sfs2X.Entities.Data;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;
using AssemblyCSharp;
using Sfs2X.Requests;
using System;

public class UserStory : ItemModelo{
	
	private long id_UserStory;
	private string descripcion;
	private int prioridad;
	private string titulo;
	private long id_Sprint;
	private ArrayList listaTareas;
	private ArrayList listaEstimacion;
	private ArrayList listaCriterios;
	private int estadoEstimacion; // 0 1
	private float valorEstimacion;
	private long id_proyecto;
	private bool cerrada;
	private DateTime fecha_ultimo_cambio;

	public bool Cerrada()
	{
		return cerrada;
	}
	public void cerrar()
	{
		cerrada = true;
		MultiPlayer.Instance.getSmartFox ().Send (new ExtensionRequest ("modificarEstadoUserStory", this.toSFSObject ()));
		Debug.Log ("modificarEstadoUserStory");
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.inicializar (MultiPlayer.Instance.getListaSprints ());
	}
	public void setCerrada(bool c)
	{
		cerrada = c;
	}

	public override int getNumeroAtributos ()
	{
		if (!descripcion.Equals (""))
						return 4;
		return 3;
	}
	public override string getInfo ()
	{
		string info;
		info = "Id user Story: " + id_UserStory + "\n"+ "\n";
		info = "Id Sprint: " + id_Sprint + "\n"+ "\n";
		info = info + "Titulo: " + titulo + "\n"+ "\n";
		if(!descripcion.Equals(""))
			info = info + "Descripcion: " + descripcion + "\n"+ "\n";
		return info;
	}

	public void set_fecha_ultimo_cambio(DateTime s)
	{
		this.fecha_ultimo_cambio = s;
		Sprint sprint = getSprint (id_Sprint);
		MultiPlayer.Instance.getSmartFox ().Send (new ExtensionRequest ("modificarFechaUltimoCambioStory", this.toSFSObject ()));
		sprint.set_fecha_ultimo_cambio (s);
	}

	public bool done()
	{
		foreach (Task t in this.getListaTareas()) {
			if(!t.getEstado ().Equals ("DONE"))
				return false;
		}
		return true;
	}


	public override bool completo_y_correcto (DateTime tiempo_ultimo_cambio)
	{
	
		return (this.done () && base.completo_y_correcto (tiempo_ultimo_cambio));
	}

	private Sprint getSprint(long id)
	{
		foreach (Sprint s in MultiPlayer.Instance.getListaSprints())
						if (s.getId_Sprint().Equals (id))
								return s;
		return null;
	}

	public override string getName ()
	{
		return id_UserStory + " " + titulo;
	}


	public void setProyecto(long id){
		this.id_proyecto = id;
	}
	
	public long getProyecto(){
		return this.id_proyecto;
	}	
	
	public ArrayList getListaEstimacion() {
		return listaEstimacion;
	}

	public void setListaEstimacion(ArrayList listaestimacion) {
		this.listaEstimacion = listaestimacion;
	}

	public int getEstadoEstimacion() {
		return estadoEstimacion;
	}

	public void setEstadoEstimacion(int estadoEstimacion) {
		this.estadoEstimacion = estadoEstimacion;
	}

	public void addCriteria(AcceptanceCriteria ac)
	{
		listaCriterios.Add (ac);
	}

	public UserStory (){
		listaTareas = new ArrayList();
		listaEstimacion = new ArrayList();
		listaCriterios = new ArrayList ();
	}
	
	public string getTitulo(){
		return titulo;
	}
	
	public void setTitulo(string tit){
	   titulo=tit;
	}
	
	public void setId_Sprint(long id_sp) {
		id_Sprint = id_sp;
	}
	
	public long getId_UserStory() {
		return id_UserStory;
	}

	public void setId_UserStory(long idUserStory) {
		id_UserStory = idUserStory;
	}

	public string getDescripcion() {
		return descripcion;
	}

	public void setDescripcion(string descripcion) {
		this.descripcion = descripcion;
	}

	public int getPrioridad() {
		return prioridad;
	}

	public void setPrioridad(int prioridad) {
		this.prioridad = prioridad;
	}

	public ArrayList getListaTareas() {
		return listaTareas;
	}

	public ArrayList getListaAcceptanceCriteria()
	{
		return listaCriterios;
	}

	public void setListaAcceptanceCriteria(ArrayList criterios)
	{
		listaCriterios = criterios;
	}

	public void setListaTareas(ArrayList listaTareas) {
		this.listaTareas = listaTareas;
	}
	



	public void addEstimacion(Estimacion e) {
		if (listaEstimacion.Contains(e))
			listaEstimacion.Remove(e);
		listaEstimacion.Add(e);
	}
	
	
	public override SFSObject toSFSObject() {
		SFSObject storyObject = new SFSObject();
		storyObject.PutLong("id_UserStory", this.getId_UserStory());
		storyObject.PutLong ("Id_Sprint", this.id_Sprint);
		storyObject.PutInt("prioridad", this.getPrioridad());
		storyObject.PutUtfString("descripcion", this.getDescripcion());
        storyObject.PutSFSArray("listaTareas", this.getListaTareasToSFSArray());
		storyObject.PutSFSArray("listaEstimacion", this.getListaEstimacionToSFSArray());
		storyObject.PutSFSArray ("listaCriterios", this.getListaCriteriosToSFSArray ());
		storyObject.PutInt("estadoEstimacion",this.getEstadoEstimacion());
		storyObject.PutFloat("valorEstimacion",this.getValorEstimacion());
		storyObject.PutLong("id_proyecto",this.getProyecto());
		storyObject.PutUtfString("Titulo",this.getTitulo());
		storyObject.PutBool("cerrada", this.cerrada);
		storyObject.PutUtfString ("fecha_ultimo_cambio", this.get_fecha_ultimo_cambio ().ToString());
		return storyObject;
	}
	
	
	public override void fromSFSObject(SFSObject item){
		listaTareas = new ArrayList();
		listaEstimacion = new ArrayList();
		listaCriterios = new ArrayList ();
		this.id_UserStory=item.GetLong("id_UserStory");
		this.id_Sprint = item.GetLong ("Id_Sprint");
		this.descripcion=item.GetUtfString("descripcion");
		this.prioridad=item.GetInt("prioridad");
		this.titulo=item.GetUtfString("Titulo");
		this.estadoEstimacion=item.GetInt("estadoEstimacion");
		this.id_proyecto=item.GetLong("id_proyecto");
		string s = item.GetUtfString ("fecha_ultimo_cambio");
		this.cerrada = item.GetBool("cerrada");
		if(!s.Equals(""))
			this.fecha_ultimo_cambio = System.DateTime.Parse(s);
		this.valorEstimacion = item.GetFloat("valorEstimacion");
		ISFSArray tareas=item.GetSFSArray("listaTareas");	


		foreach(SFSObject tarea in tareas)
		{
			Task task=new Task();
			task.fromSFSObject(tarea);

			listaTareas.Add(task);
		}	
		ISFSArray estimaciones=item.GetSFSArray("listaEstimacion");

		foreach(SFSObject estimacion in estimaciones)
		{
			Estimacion est=new Estimacion();
			est.fromSFSObject(estimacion);
			listaEstimacion.Add(est);
		}	

		ISFSArray criterios=item.GetSFSArray("listaCriterios");
		if(criterios !=null)
		foreach(SFSObject criteria in criterios)
		{
			AcceptanceCriteria ac=new AcceptanceCriteria();
			ac.fromSFSObject(criteria);
			listaCriterios.Add(ac);
		}	

	}
	
	public SFSArray getListaTareasToSFSArray(){
		SFSArray listaTareasArray=new SFSArray();
        foreach(Task tarea in this.getListaTareas())
			listaTareasArray.AddSFSObject(tarea.toSFSObject());
	 return listaTareasArray;
	
	}
	
	public SFSArray getListaEstimacionToSFSArray(){
		SFSArray listaEstimacionArray=new SFSArray();
        foreach(Estimacion est in this.getListaEstimacion())
			listaEstimacionArray.AddSFSObject(est.toSFSObject());
	 	return listaEstimacionArray;
	
	}

	public SFSArray getListaCriteriosToSFSArray(){
		SFSArray listaCriteriosArray=new SFSArray();
		foreach(AcceptanceCriteria ac in this.getListaAcceptanceCriteria())
			listaCriteriosArray.AddSFSObject(ac.toSFSObject());
		return listaCriteriosArray;
	
	}
	
	public void addTask (Task tarea){
		this.listaTareas.Add(tarea);
	}
	
	public void setValorEstimacion(float v){
		this.valorEstimacion = v;
	}
	
	public void calcularValorEstimacion(){
		float resultado = 0;
		foreach(Estimacion e in listaEstimacion){
			resultado+= e.getValorEstimacion();
		}
		if (listaEstimacion.Count != 0)
			setValorEstimacion(resultado/listaEstimacion.Count);
		else
			setValorEstimacion(0);
	}
	
	public float getValorEstimacion(){
		return valorEstimacion;
	}
	
	public void limpiarListaEstimacion(){
		MultiPlayer aux = (MultiPlayer)GameObject.Find("Multi").GetComponent("MultiPlayer");
		foreach(Estimacion e in listaEstimacion)
		{
		  aux.eliminarEstimacion(e);
		}
		listaEstimacion.Clear();
	}

	public override List<Test> getTests ()
	{
		List<Test> lista = new List<Test> ();
		foreach (AcceptanceCriteria ac in listaCriterios)
			lista.AddRange (ac.getTests ());
		return lista;
	}

	public DateTime get_fecha_ultimo_cambio()
	{
		return fecha_ultimo_cambio;
	}
	
	public override List<EjecucionTest> ejecutarTests()
	{
		List<Test> tests = this.getTests ();
		TestUnityTestTools.Instance.ejecutarTests (tests);
		foreach (Test t in tests) {
			t.getInfoTest ().setEntorno ("User Story");
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("agregarEjecucionTest", t.getInfoTest().toSFSObject()));	
			t.getHistorial().Add(t.getInfoTest());
		}
		List<EjecucionTest> resultado = this.getResultadosEjecucion (tests);
		return resultado;
	}
	
}
