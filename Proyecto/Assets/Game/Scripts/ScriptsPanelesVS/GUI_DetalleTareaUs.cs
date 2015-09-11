using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;
using System;
using Sfs2X.Requests;

public class GUI_DetalleTareaUs : MonoBehaviour {
	
	private bool openWindow = false;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private Rect windowRect;
	private float inicio_x = 15;
	private float inicio_y = 15;
	private int roomSelection = -1;
	private List<string> testsNameStrings;
	private Task t;
	private long idtest;
	private GUI_DetalleStory ant;
	private Vector2 gameScrollPositionTests = new Vector2();
	// use this for initialization
	void Start () {
		SetupTestList ();
		//openWindow = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Mostrar (){
		openWindow = true;	
	}
	
	public void setTarea(Task tarea){
		t = tarea;
		//openWindow = true;
	}
	
	public void setAnterior(GUI_DetalleStory anterior){
		ant = anterior;
	}
		
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(inicio_x,inicio_y,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doTaskDetailWindow,"Detalle de tarea");
		}
		GUI.skin = oldSkin;
	}



		
	void doTaskDetailWindow(int windowID){
		SetupTestList ();
		GUIComponents.labelDetail(new Rect(20,30,310,20),"Id Tarea: ",t.getId_Task().ToString());
		GUI.contentColor = Color.yellow;
		int heightActual = 55;
		GUI.Label(new Rect(20,55,310,20),"Descripcion:");
		int offset = 18*((int)(t.getDescripcion().Length/50)+1);
		if (offset > 53){
			offset = 53;
		}
		heightActual = heightActual + offset + 25;
		GUI.contentColor = Color.white;
		GUI.Label(new Rect(40,75,290,offset),t.getDescripcion());
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Responsable: ",t.getResponsable());
		heightActual = heightActual + 25;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Tiempo estimado: ",t.getT_Estimado().ToString());
		heightActual = heightActual + 25;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Tiempo total: ",t.getT_Total().ToString());
		heightActual = heightActual + 25;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Prioridad: ",t.getPrioridad().ToString());
		heightActual = heightActual + 25;
		GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Estado: ",t.getEstado());
		heightActual = heightActual + 25;
		GUI.Label (new Rect(20,heightActual,310,20),"Tests Asociados:");
		
		//Cargar los criterios en area con barra deslizable 
		
		heightActual = heightActual + 30;
		GUILayout.BeginArea (new Rect (15, heightActual, 350, 220));
		
		gameScrollPositionTests = GUILayout.BeginScrollView (gameScrollPositionTests, GUILayout.Width (300), GUILayout.Height (100));

		int height = 0;
		foreach(string nameTest in testsNameStrings)
		{
			idtest = Convert.ToInt64(nameTest.Split(' ')[0]);
			Test test=this.getTest(idtest);
			if (GUI.Button(new Rect (10,height ,200, 20),nameTest)){

				GameObject g = new GameObject();
				g.AddComponent<GUI_DetalleTest>();
				g.SendMessage("setAnterior",this);
				g.SendMessage("setTest",test);
				g.SendMessage("Mostrar");
				openWindow = false;
			}
			
			if (GUI.Button(new Rect (220, height ,80, 20),"Eliminar")){
				AsociacionTareaTest a = new AsociacionTareaTest (t.getId_Task (), test.getIdTest ());
				MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("eliminarAsociacion",a.toSFSObject()));
				EliminarAsociacion(idtest);
				GameObject g = new GameObject ();
				g.AddComponent <GUI_Mensaje>();
				g.SendMessage ("setAnterior", this);
				g.SendMessage ("setTitulo", "OK");
				g.SendMessage ("setMensaje", "Asociacion eliminada exitosamente.");
				g.SendMessage ("Mostrar");
				openWindow = false;
			}
			
			
			height += 25;
		}

		GUILayout.EndScrollView ();
		GUILayout.EndArea ();

		if (GUI.Button(new Rect(maxWidth/2-120,maxHeight-35,100,20),"Asociar Test")){
			GameObject g = new GameObject();
			g.AddComponent<GUI_AsociarTest>();
			g.SendMessage ("setTarea", t);
			g.SendMessage("setAnterior", this);
			g.SendMessage("Mostrar");
			openWindow = false;
		}

		if (GUI.Button(new Rect(maxWidth/2+20,maxHeight-35,100,20),"Cerrar")){
			openWindow = false;
			try {
				this.ant.Mostrar();
			}
			catch{
				Debug.Log("No tiene anterior porque viene del taskboard");
			}
			Destroy(this);
		}


	}

	public Test getTest(long id)
	{
		ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in Sprints)
			foreach( UserStory u in s.getListaStories())
				foreach (AcceptanceCriteria ac in u.getListaAcceptanceCriteria())
					foreach (Test t in ac.getAssociatedTests())
						if(t.getIdTest().Equals(id))
							 return t;

		return null;
	}

	public void EliminarAsociacion(long id_test)
	{
		this.t.getIdTests ().Remove (id_test);
	}



	public void SetupTestList()
	{
		testsNameStrings = new List<string> ();
		ArrayList lista = t.getIdTests ();
		if (lista != null)
		foreach (long id in lista) {
				Test aux = getTest(id);
				if(aux!=null)
				testsNameStrings.Add (aux.getIdTest () + " " + aux.getMetodo ());
		}

	}
}