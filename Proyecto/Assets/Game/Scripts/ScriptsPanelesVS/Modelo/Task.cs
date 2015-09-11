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

public class Task : ItemModelo
{
	
	private long id_Task;
	private string titulo;
	private string descripcion;
	private string responsable;
	private int t_Estimado;
	private int t_Total;
	private string estado;
	private int prioridad;
	private long id_Story;
	private ArrayList listaBurnDown;
	private ArrayList idTests;

	public override string getName ()
	{
		return id_Task + " " + titulo;
	}

	public void set_fecha_ultimo_cambio(System.DateTime s)
	{
		UserStory us = getUserStory (id_Story);
		us.set_fecha_ultimo_cambio (s);

	}

	private UserStory getUserStory(long id)
	{
		foreach (Sprint s in MultiPlayer.Instance.getListaSprints())
			foreach (UserStory us in s.getListaStories())
				if (us.getId_UserStory ().Equals (id))
					return us;
		return null;
	}

	public override int getNumeroAtributos()
	{
		if (!descripcion.Equals (""))
			return 7;
		return 6;
	}

	public override string getInfo ()
	{
		string info;
		info = "Id Task: " + id_Task + "\n"+ "\n";
		info = info + "Id user Story: " + id_Story + "\n"+ "\n";
		info = info + "Titulo: " + titulo + "\n"+ "\n";
		if(!descripcion.Equals(""))
			info = info + "Descripcion: " + descripcion + "\n"+ "\n";
		info = info + "Responsable: " + responsable + "\n"+ "\n";
		info = info + "Tiempo Estimado: " + t_Estimado + "\n"+ "\n";
		info = info + "Tiempo Restante: " + (t_Total - t_Estimado).ToString() + "\n"+ "\n";
		info = info + "Prioridad: " + prioridad + "\n"+ "\n";
		return info;
	}



	public Task ()
	{
		listaBurnDown = new ArrayList ();
		idTests =new ArrayList ();
	}

	public ArrayList getIdTests()
	{
		return idTests;
	}
	
	public string getTitulo ()
	{
		return titulo;
	}
	
	public void setTitulo (string tit)
	{
		titulo = tit;
	}
	
	public long getId_Task ()
	{
		return id_Task;
	}
	
	public void setId_Story (long idStory)
	{
		id_Story = idStory;
	}
	
	public long getId_Story ()
	{
		return id_Story;
	}

	public void setId_Task (long idTask)
	{
		id_Task = idTask;
	}

	public string getDescripcion ()
	{
		return descripcion;
	}

	public void setDescripcion (string descripcion)
	{
		this.descripcion = descripcion;
	}

	public string getResponsable ()
	{
		return responsable;
	}

	public void setResponsable (string responsable)
	{
		this.responsable = responsable;
	}
	
	public int getT_Estimado ()
	{
		return t_Estimado;
	}

	public void setT_Estimado (int tEstimado)
	{
		t_Estimado = tEstimado;
	}

	public int getT_Total ()
	{
		return t_Total;
	}

	public void setT_Total (int tTotal)
	{
		t_Total = tTotal;
	}

	public string getEstado ()
	{
		return estado;
	}

	public void setEstado (string estado)
	{
		this.estado = estado;
	}

	public int getPrioridad ()
	{
		return prioridad;
	}

	public void setPrioridad (int prioridad)
	{
		this.prioridad = prioridad;
	}

	public ArrayList getListaBurnDown ()
	{
		return listaBurnDown;
	}

	public void setListaBurnDown (ArrayList listaBurnDown)
	{
		this.listaBurnDown = listaBurnDown;
	}

	public void addIdTest(long t)
	{
		idTests.Add (t);
	
	}

	public override SFSObject toSFSObject ()
	{
		
		SFSObject taskObject = new SFSObject ();
		taskObject.PutLong ("id_Task", this.getId_Task ());
		taskObject.PutUtfString ("descripcion", this.getDescripcion ());
		taskObject.PutUtfString ("responsable", this.getResponsable ());
		taskObject.PutInt ("t_Estimado", this.getT_Estimado ());
		taskObject.PutInt ("t_Total", this.getT_Total ());
		taskObject.PutUtfString ("estado", this.getEstado ());
		taskObject.PutInt ("prioridad", this.getPrioridad ());
		taskObject.PutLong ("id_Story", this.getId_Story ());
		taskObject.PutUtfString ("titulo", this.getTitulo ());
		SFSArray t = new SFSArray ();

		foreach (long l in idTests) {
			SFSObject tsfs=new SFSObject();
			tsfs.PutLong("Id_Test", l);
			t.AddSFSObject(tsfs);
		}
		taskObject.PutSFSArray ("testsAsociados", t);
		//taskObject.pua
		//private ArrayList listaBurnDown;
		return taskObject;
		
		
				
	}

	public Test getTest(long id_test){
		foreach (Sprint s in MultiPlayer.Instance.getListaSprints()) 
			foreach (UserStory u in s.getListaStories())
				foreach (AcceptanceCriteria a in u.getListaAcceptanceCriteria())
					foreach (Test t in a.getAssociatedTests())
					if(t.getIdTest().Equals(id_test)){
						return t;
					}
		return null;
	}

	public override void fromSFSObject (SFSObject item)
	{	
		this.id_Task = item.GetLong ("id_Task");
		this.id_Story=item.GetLong("id_Story");
		this.descripcion = item.GetUtfString ("descripcion");
		this.prioridad = item.GetInt ("prioridad");
		this.responsable = item.GetUtfString ("responsable");
		this.estado = item.GetUtfString ("estado");
		this.t_Total = item.GetInt ("t_Total");
		this.t_Estimado = item.GetInt ("t_Estimado");
		this.titulo = item.GetUtfString ("titulo");
		ISFSArray t = item.GetSFSArray ("testsAsociados");
	
		foreach (SFSObject s in t) {
			this.idTests.Add (s.GetLong ("Id_Test"));
		}
		// falta el burndownchart
	}

	public override List<Test> getTests ()
	{
		List<Test> lista = new List<Test> ();
		foreach (long id in idTests)
			lista.Add (getTest (id));
		return lista;
	}


	public override List<EjecucionTest> ejecutarTests()
	{
		List<Test> tests = this.getTests ();
		TestUnityTestTools.Instance.ejecutarTests (tests);
		foreach (Test t in tests) {
			t.getInfoTest ().setEntorno ("Tarea");
			MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("agregarEjecucionTest", t.getInfoTest().toSFSObject()));
			t.getHistorial().Add(t.getInfoTest());
		}
		
		List<EjecucionTest> resultado = this.getResultadosEjecucion (tests);
		return resultado;

		
	}
	
}
