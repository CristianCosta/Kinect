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

public class Sprint: ItemModelo {
	
	private long id_sprint;
	private string titulo;
	private string descripcion;
	private System.DateTime fechaInicio;
	private System.DateTime fechaFin;
	private string estado;
	private long id_proyecto;
	private ArrayList listaStories;
	private DateTime fecha_ultimo_cambio;
	private bool cerrado;
	
	public bool Cerrado()
	{
		return cerrado;
	}
	public void cerrar()
	{
		cerrado = true;
		MultiPlayer.Instance.getSmartFox ().Send (new ExtensionRequest ("modificarEstadoSprint", this.toSFSObject ()));
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find ("panelTaskBoard")).GetComponent ("crearPlanoTask");
		if(planoTask.getSprintActual ()!=0)
			planoTask.setSprint (planoTask.getSprintActual () - 1);
		else
			planoTask.setSprint (planoTask.getSprintActual () + 1);
		planoTask.inicializar (MultiPlayer.Instance.getListaSprints ());
	}
	public void setCerrado(bool c)
	{
		cerrado = c;
	}


	public override string getName ()
	{
		return id_sprint + " " + titulo;
	}

	public void set_fecha_ultimo_cambio(System.DateTime s)
	{
		this.fecha_ultimo_cambio = s;
		Debug.Log ("proy " + this.getId_Proyecto ());
		MultiPlayer.Instance.getSmartFox ().Send (new ExtensionRequest ("modificarFechaUltimoCambioSprint", this.toSFSObject ()));
	}

	public override bool completo_y_correcto (System.DateTime tiempo_ultimo_cambio)
	{
		bool us_completas_y_correctas = true;
		foreach (UserStory u in this.getListaStories())
			us_completas_y_correctas = us_completas_y_correctas && u.completo_y_correcto (tiempo_ultimo_cambio);


		return us_completas_y_correctas && base.completo_y_correcto (tiempo_ultimo_cambio);
	}
	public override int getNumeroAtributos ()
	{
		if(descripcion != null)
			if (!descripcion.Equals (""))
				return 7;
		return 6;
	}


	public override string getInfo ()
	{
		string info;
		info = "Id Sprint: " + id_sprint + "\n"+ "\n";
		info = info + "Titulo: " + titulo + "\n"+ "\n";
		if(descripcion != null && !descripcion.Equals(""))
			info = info + "Descripcion: " + descripcion + "\n"+ "\n";
		info = info + "Fecha de Inicio: " + fechaInicio.ToString () + "\n"+ "\n";
		info = info + "Fecha de Finalizacion: " + fechaFin.ToString () + "\n"+ "\n";
		info = info + "Estado: " + estado + "\n"+ "\n";
		info = info + "Id Proyecto: " + id_sprint + "\n"+ "\n";
		return info;
	}

	public Sprint (){
		listaStories = new ArrayList();
	}
	
	
	public string getTitulo(){
		return titulo;
	}
	
	public void setTitulo(string tit){
	   titulo=tit;
	}

	public long getId_Sprint() {
		return id_sprint;
	}

	public void setId_Sprint(long id) {
		id_sprint = id;
	}
	
	public long getId_Proyecto() {
		return id_proyecto;
	}

	public void setId_Proyecto(long idProyecto) {
		id_proyecto = idProyecto;
	}

	public string getDescripcion() {
		return descripcion;
	}

	public void setDescripcion(string descripcion) {
		if (descripcion == null)
			this.descripcion = "";
		else
			this.descripcion = descripcion;
	}

	public System.DateTime getFechaInicio() {
		return fechaInicio;
	}

	public void setFechaInicio(System.DateTime fechaInicio) {
		this.fechaInicio = fechaInicio;
	}

	public System.DateTime getFechaFin() {
		return fechaFin;
	}

	public void setFechaFin(System.DateTime fechaFin) {
		this.fechaFin = fechaFin;
	}

	public string getEstado() {
		return estado;
	}

	public void setEstado(string estado) {
		this.estado = estado;
	}

	public ArrayList getListaStories() {
		return listaStories;
	}

	public void setListaStories(ArrayList listaStories) {
		this.listaStories = listaStories;
	}
	
	public override SFSObject toSFSObject() {
		SFSObject sprintObject = new SFSObject();
		sprintObject.PutLong("Id_Sprint", this.getId_Sprint());
		sprintObject.PutLong("id_Proyecto", this.getId_Proyecto());
		sprintObject.PutUtfString("estado", this.getEstado());
		if(this.descripcion!=null)
			sprintObject.PutUtfString("descripcion", this.getDescripcion());
		else
			sprintObject.PutUtfString("descripcion", "");


		//usObject.PutLong ("fechaInicio", this.getFechaInicio ().toToFileTime ());
		sprintObject.PutUtfString ("fechaInicio", this.getFechaInicio().Year + "-"+this.getFechaInicio().Month+"-"+this.getFechaInicio().Day);
		//usObject.PutLong("fechaFin", this.getFechaFin().ToFileTime());
		sprintObject.PutUtfString ("fechaFin", this.getFechaFin().Year + "-"+this.getFechaFin().Month+"-"+this.getFechaFin().Day);
		sprintObject.PutUtfString("titulo",this.getTitulo());
		sprintObject.PutSFSArray("listaStories",this.getListaStoriesToSFSArray() );
		sprintObject.PutUtfString ("fecha_ultimo_cambio", this.get_fecha_ultimo_cambio ().ToString());
		sprintObject.PutBool ("cerrado", this.cerrado);
		return sprintObject;

	}
	
	
	public override void fromSFSObject(SFSObject item){
		this.id_sprint=item.GetLong("Id_Sprint");
		this.id_proyecto=item.GetLong("id_Proyecto");
		this.estado=item.GetUtfString("estado");
		this.fechaInicio=System.DateTime.Parse(item.GetUtfString("fechaInicio"));//new System.DateTime(us.GetLong ("fechaInicio"));
		this.fechaFin=System.DateTime.Parse(item.GetUtfString("fechaFin"));//new System.DateTime(us.GetLong ("fechaFin"));
		this.titulo=item.GetUtfString("titulo");
		this.cerrado = item.GetBool("cerrado");
		string s = item.GetUtfString ("fecha_ultimo_cambio");
		if(!s.Equals(""))
			this.fecha_ultimo_cambio = System.DateTime.Parse(s);
		ISFSArray stories=item.GetSFSArray("listaStories");
		foreach(SFSObject story in stories)
		{
			UserStory UserStory=new UserStory();
			UserStory.fromSFSObject(story);
			
			listaStories.Add(UserStory);
		}
		Debug.Log (this.fecha_ultimo_cambio.ToString ());
		
		
	  
		
	}
	
	public void fromSFSObjectSinUS(SFSObject us){
		this.id_sprint=us.GetLong("Id_Sprint");
		this.id_proyecto=us.GetLong("id_Proyecto");
		this.estado=us.GetUtfString("estado");
		this.fechaInicio=System.DateTime.Parse(us.GetUtfString("fechaInicio"));//new System.DateTime(us.GetLong ("fechaInicio"));
		this.fechaFin=System.DateTime.Parse(us.GetUtfString("fechaFin"));//new System.DateTime(us.GetLong ("fechaFin"));
		this.titulo=us.GetUtfString("titulo");
		this.cerrado = us.GetBool("cerrado");
		string s = us.GetUtfString ("fecha_ultimo_cambio");
		if(!s.Equals(""))
			this.fecha_ultimo_cambio = System.DateTime.Parse(s);
		Debug.Log (this.fecha_ultimo_cambio.ToString ());
	}
	
	
	public void addUserStory(UserStory u) {
		listaStories.Add(u);
	}
	
	public SFSArray getListaStoriesToSFSArray(){
	 SFSArray listaStoriesArray=new SFSArray();
      foreach(UserStory story in this.getListaStories())
			listaStoriesArray.AddSFSObject(story.toSFSObject());
	 return listaStoriesArray;	
	}
	
	
	public UserStory buscarUserStory(long id_Story){
		foreach (UserStory UserStory in this.getListaStories())
		{
		  if (UserStory.getId_UserStory().Equals(id_Story))
		   return UserStory;
		}
	 Debug.Log ("No se encontro ninguna UserStory asociada con ese idStory");	
	 return null;	
	
	}

	public override List<Test> getTests ()
	{
		List<Test> lista = new List<Test> ();
		foreach (UserStory us in listaStories)
			lista.AddRange (us.getTests ());
		return lista;
	}
	
	public override List<EjecucionTest> ejecutarTests ()
	{
		List<Test> tests = this.getTests ();
		TestUnityTestTools.Instance.ejecutarTests (tests);
		foreach (Test t in tests) {
			t.getInfoTest ().setEntorno ("Sprint");
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("agregarEjecucionTest", t.getInfoTest().toSFSObject()));	
			t.getHistorial().Add(t.getInfoTest());
		}
		
		List<EjecucionTest> resultado = this.getResultadosEjecucion (tests);
		return resultado;
	}
	
	public UserStory removeUserStory(long id_Story){
		//encontrarla
		UserStory u = this.buscarUserStory(id_Story);
		listaStories.Remove(u);
		//removerla
		return u;
	}

	public DateTime get_fecha_ultimo_cambio()
	{
		return fecha_ultimo_cambio;
	}




}
