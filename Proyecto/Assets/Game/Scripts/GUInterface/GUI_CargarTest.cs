using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;


namespace AssemblyCSharp
{
		
	public class GUI_CargarTest : windowsManager
	{
		private bool usWindow = false;
		private Rect windowRect;
		private int offset;
		private int inicio;
		private int height;
		private float maxWidth = 350;
		private float maxHeight = 450;
	
		private string nombre = "";
		private string metodo = "";
		private string descripcion = "";
		private string responsable = "";
		private string clase ="";

		private GUI_DetalleCriterio ant;
		private int ventananumero;


		void Start () {
			ventananumero++;
		}

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
			usWindow=false;
			
		}

		void OnGUI(){
			GUISkin oldSkin = GUI.skin;
			GUI.skin = Skin.Instance.skin;
			if (usWindow){
				if (esActiva()){
					windowRect = new Rect(15,15,maxWidth,maxHeight);
					windowRect = GUI.Window(0,windowRect,doNewusWindow,"Nuevo Test");
				}
			}
			GUI.skin = oldSkin;
		}

	
		public void Mostrar(GUI_DetalleCriterio anterior){
			usWindow=true;
			ant = anterior;
		}

		void doNewusWindow(int windowID){
			inicio = 30;
			height = 20;
			offset = 0;
			
			offset= 10; inicio = inicio + height;
			nombre = GUIComponents.labelTextField(new Rect(20,inicio+offset,maxWidth-40,height),nombre," Nombre",220);
			inicio = inicio + height + offset;
			descripcion = GUIComponents.labelTextArea(new Rect(20,inicio+offset ,maxWidth-40,height*4),descripcion," Descripcion");
			inicio = inicio + height*4 + offset;
			responsable =GUIComponents.labelTextField(new Rect(20,inicio+offset ,maxWidth-40,height),responsable," Responsable",220);
			inicio = inicio + height + offset;
			GUIComponents.labelDetail(new Rect(20,inicio+offset,maxWidth-100,height),"","Estado  NO EJECUTADO");
			inicio = inicio + height + offset;
				
			
			if(GUI.Button(new Rect (maxWidth/3-100,maxHeight-35,140,20), "Cargar Test")){
				if (nombre.Equals("") || descripcion.Equals("") || responsable.Equals("")){
					desactivarVentana();
					incVentana();
					setTitulo("ERROR");
					setMensaje("Debe completar todos los campos");
					setLlamador("GUI_CargarTest");
					ejecutar("GUI_Error");
				}
				else{	
					Test t = new Test();
					t.setIdTest(this.getIdTest()+1);
					String[] partes = nombre.Split('.');
					t.setMetodo(partes[1]);
					t.setClase(partes[0]);
					t.setDescripcion(descripcion);
					t.setEstado("NO EJECUTADO");
					t.setIdCriterio(ant.getAcceptanceCriteria().getId_Criteria());
					
					MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("crearTest",t.toSFSObject()));
					//NextStories button = (NextStories)GameObject.Find("ButtonNextStories").GetComponent("NextStories");
					//button.addTarea(t);
					desactivarVentana();
					incVentana();
					setTitulo("OK");
					GameObject g = new GameObject();
					g.AddComponent<GUI_Mensaje>();
					g.SendMessage("setTitulo","Nuevo Test");
					g.SendMessage("setMensaje","El Test fue cargado exitosamente.");
					g.SendMessage("Mostrar");
					limpiarFormulario();
				}	
			}
			if (GUI.Button(new Rect(2*maxWidth/3-50,maxHeight-35,140,20),"Cancelar")){
				usWindow = false;
				ant.Mostrar();
				Destroy(this);
			}
			
		}

		public long getIdTest(){
			long max = 0;
			ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
			foreach (Sprint s in Sprints){
				foreach(UserStory u in s.getListaStories()){
					foreach (AcceptanceCriteria ac in u.getListaAcceptanceCriteria())
						foreach (Test t in ac.getAssociatedTests()){
						if (t.getIdTest() > max)
							max = t.getIdTest(); 
					}
				}
			}
			return max;
		}


		
		public void setusWindowsTrue(){
			usWindow = true;
		}
		
		public void limpiarFormulario(){
			metodo = "";
			descripcion = "";
			responsable = "";
			clase = "";
		}
	}
}

