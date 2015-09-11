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
using Sfs2X.Requests;

namespace AssemblyCSharp
{
	public class Test : ItemModelo
	{		
		private long id_criterio;
		private long id_test;
		private String metodo;
		private String descripcion;
		private String estado;
		private String clase;
		private bool toggle;
		private bool historialCargado = false;
		private List<EjecucionTest> historial = new List<EjecucionTest> ();
		private EjecucionTest info;

		public List<EjecucionTest> getHistorial()
		{
			return historial;		
		}

		public void setHistorial(List<EjecucionTest> h)
		{
			historial = h;
			historial.Sort ((a, b) => a.CompareTo(b));
			historialCargado = true;
		}

		public bool HistorialCargado()
		{
			return historialCargado;		
		}

		public override int getNumeroAtributos ()
		{
			if (!descripcion.Equals (""))
				return 5;
			return 4;
		}
		public override string getInfo ()
		{
			string info;
			info = "Id Test: " + id_test + "\n"+ "\n";
			info = info + "Id Criterio: " + id_criterio + "\n"+ "\n";
			info = info + "Titulo: " + clase + "." + metodo + "\n"+ "\n";
			if(!descripcion.Equals(""))
				info = info + "Descripcion: " + descripcion + "\n"+ "\n";
			info = info + "Estado: " + estado + "\n"+ "\n";
			return info;
		}

		public override string getName ()
		{
			return id_test + " " + clase +  "." + metodo;
		}

		public bool getToggle()
		{
			return toggle;		
		}

		public void setToggle(bool t)
		{
			toggle = t;			
		}

		public long getIdTest()
		{
			return id_test;
		}

		public void setIdTest(long idtest)
		{
			id_test = idtest;
		}

		public void setDescripcion(String desc)
		{
			descripcion = desc;
		}
	
		public string getDescripcion()
		{
			return descripcion;		
		}


		public void setEstado(String est)
		{
			estado = est;
		}

		public string getEstado()
		{
			return estado;
		}

		public String getMetodo()
		{
			return metodo;
		}

		public void setMetodo(String t)
		{
			metodo = t;
		}

		public String getClase()
		{
			return clase;
		}

		public void setClase (String c)
		{
			clase = c;
		}

		public void setIdCriterio(long id_crit)
		{
			id_criterio = id_crit;
		}

		public long getIdCriterio()
		{
			return id_criterio;
		}

		public EjecucionTest getInfoTest(){
			return info;		
		}

		public void setInfoTest(EjecucionTest info){
			this.info = info;
		}

		public Test ()
		{
		}

		public override SFSObject toSFSObject() {

			SFSObject testObject = new SFSObject();
			testObject.PutLong("Id_Criteria", this.getIdCriterio());
			testObject.PutLong("Id_Test", this.getIdTest());
			testObject.PutUtfString("Metodo", this.getMetodo());
			testObject.PutUtfString("Clase", this.getClase());
			testObject.PutUtfString("Descripcion", this.getDescripcion());
			testObject.PutUtfString("Estado", this.getEstado());
			return testObject;
		}

		public override void fromSFSObject(SFSObject item){
			this.id_criterio = item.GetLong ("Id_Criteria");
			this.id_test= item.GetLong("Id_Test");
			this.metodo= item.GetUtfString("Metodo");
			this.clase = item.GetUtfString("Clase");
			this.estado= item.GetUtfString("Estado");
			this.descripcion = item.GetUtfString("Descripcion");
		}

		public override List<Test> getTests ()
		{
			List<Test> lista = new List<Test> ();
			lista.Add (this);
			return lista;
		}





		public override List<EjecucionTest> ejecutarTests()
		{
			List<Test> tests = this.getTests ();
			TestUnityTestTools.Instance.ejecutarTests (tests);
			foreach (Test t in tests) {
				t.getInfoTest ().setEntorno ("Test");
				Debug.Log (t.getInfoTest().getEntorno());
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("agregarEjecucionTest", t.getInfoTest().toSFSObject()));	
			}
			
			List<EjecucionTest> resultado = this.getResultadosEjecucion (tests);
			return resultado;
		}

	}

}
