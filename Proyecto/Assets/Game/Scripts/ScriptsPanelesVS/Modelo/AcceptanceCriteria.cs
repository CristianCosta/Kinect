using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Sfs2X.Protocol.Serialization;
using Sfs2X.Entities.Data;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;
using System;
using AssemblyCSharp;
using Sfs2X.Requests;

public class AcceptanceCriteria : ItemModelo{

	
	private long id_Criteria;
	private long id_Story;
	private String descripcion;
	private String titulo;
	private long prioridad;
	private List<Test> associatedTests;

	public override int getNumeroAtributos ()
	{
		if (!descripcion.Equals (""))
			return 5;
		return 4; 
	}
	public override string getInfo ()
	{
		string info;
		info = "Id Acceptance Criteria: " + id_Criteria + "\n"+ "\n";
		info = info + "Id user Story: " + id_Story + "\n"+ "\n";
		if(!descripcion.Equals(""))
			info = info + "Descripcion: " + descripcion + "\n"+ "\n";
		info = info + "Titulo: " + titulo + "\n"+ "\n";
		info = info + "Prioridad: " + prioridad + "\n"+ "\n";
		return info;
	}



	public AcceptanceCriteria()
	{
		associatedTests = new List<Test> ();
	}

	public override string getName ()
	{
		return id_Criteria + " " + titulo;
	}

	public void addTest(Test t)
	{
		associatedTests.Add (t);
	}




	public void setPrioridad(long p)
	{
		prioridad = p;
	}

	public long getPrioridad()
	{
		return prioridad;
	}

	public void setAssociatedTests(List<Test> tests)
	{
		associatedTests = tests;
	}

	public void setTitulo(String t)
	{
		titulo = t;
	}

	public String getTitulo()
	{
		return titulo;
		}

	public List<Test> getAssociatedTests()
	{
		return associatedTests;
	}

	public void setId_Criteria(long id_Criteria){
		this.id_Criteria = id_Criteria;
	}

	public void setId_Story (long id_Story){
		this.id_Story = id_Story;
	}

	public void setDescripcion (String Descripcion){
		this.descripcion = Descripcion;
	}

	public long getId_Criteria(){
		return id_Criteria;
	}

	public long getId_Story(){
		return id_Story;
	}

	public String getDescripcion(){
		return descripcion;
	}

	public SFSArray getListaTestToSFSArray(){
		SFSArray listaTestsArray=new SFSArray();
		foreach(Test t in this.getAssociatedTests())
			listaTestsArray.AddSFSObject(t.toSFSObject());
		return listaTestsArray;
		
	}



	public override SFSObject toSFSObject() {
		SFSObject acceptanceCriteriaObject = new SFSObject();
		acceptanceCriteriaObject.PutLong("Id_Criteria", this.getId_Criteria());
		acceptanceCriteriaObject.PutLong("Id_Story", this.getId_Story());
		acceptanceCriteriaObject.PutLong("Prioridad", this.getPrioridad());
		acceptanceCriteriaObject.PutSFSArray ("listaTests", this.getListaTestToSFSArray ());
		acceptanceCriteriaObject.PutUtfString("Titulo", this.getTitulo());
		acceptanceCriteriaObject.PutUtfString("Descripcion", this.getDescripcion());
		return acceptanceCriteriaObject;
	}

	public override void fromSFSObject(SFSObject item){
		associatedTests = new List<Test> ();
		this.id_Criteria = item.GetLong("Id_Criteria");
		this.id_Story	 = item.GetLong("Id_Story");
		this.descripcion = item.GetUtfString ("Descripcion");
		this.titulo = item.GetUtfString ("Titulo");
		this.prioridad = item.GetLong("Prioridad");
		ISFSArray tests=item.GetSFSArray("listaTests");
		if(tests != null)
			foreach(SFSObject test in tests)
		{
			Test t=new Test();
			t.fromSFSObject(test);
			associatedTests.Add(t);
		}	
	}

	public override List<Test> getTests()
	{
		return associatedTests;
	}

	public override List<EjecucionTest> ejecutarTests()
	{
		List<Test> tests = this.getTests ();
		TestUnityTestTools.Instance.ejecutarTests (tests);
		foreach (Test t in tests) {
			t.getInfoTest ().setEntorno ("Criterio de Aceptacion");
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("agregarEjecucionTest", t.getInfoTest().toSFSObject()));	
			t.getHistorial().Add(t.getInfoTest());
		}
		
		List<EjecucionTest> resultado = this.getResultadosEjecucion (tests);
		return resultado;
		
	}

}