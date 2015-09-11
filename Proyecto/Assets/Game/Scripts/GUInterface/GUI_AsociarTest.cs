using System;
using UnityEngine;
using Sfs2X;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;
using Sfs2X.Requests;

public class GUI_AsociarTest : MonoBehaviour{
			

	// conexion SmartFox
	public String serverName = "127.0.0.1";
	public int serverPort = 9933;
	SmartFox sfs;

	// Elementos del GUI
	private bool usWindow = false;
	private int offset;
	private int inicio;
	private int height;
	private float maxWidth = 350;
	private float maxHeight = 450;
	private int roomSelection = -1;
	private Rect windowRect;
	private int ventananumero;
	private Vector2 gameScrollPositionTests = new Vector2();
	private string[] testsNameStrings;
	private ArrayList listaTests;
	private Task t;
	private Test test;
	//Ventana Anterior
	GUI_DetalleTareaUs ant;

	// use this for initialization
	void Start () {
		setupAvailableTests ();
		ventananumero++;
	}


	public void Mostrar()
	{
		usWindow = true;
	}


	public void setAnterior(GUI_DetalleTareaUs anterior){
		ant = anterior;
	}

	public void setTarea(Task aux)
	{
		this.t = aux;
	}

	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if (usWindow){
			/*if (esActiva())*/{
				windowRect = new Rect(15,15,maxWidth,maxHeight);
				windowRect = GUI.Window(0,windowRect,doNewusWindow,"Asociar Test");
			}
		}
		GUI.skin = oldSkin;
	}
	/*
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
*/
	void doNewusWindow(int windowID)
	{
		Debug.Log ("Entrando " + t.getId_Story());

		setupAvailableTests ();
		if (testsNameStrings.Length != 0) {
						int heightActual = 25;
						GUI.contentColor = Color.yellow;
						GUI.Label (new Rect (20, heightActual, 310, 20), "Tests Disponibles:");

						GUI.contentColor = Color.white;
						heightActual = heightActual + 20;
						GUILayout.BeginArea (new Rect (15, heightActual, 350, 220));
			
						gameScrollPositionTests = GUILayout.BeginScrollView (gameScrollPositionTests, GUILayout.Width (300), GUILayout.Height (200));
						roomSelection = GUILayout.SelectionGrid (roomSelection, testsNameStrings, 1, GUI.skin.GetStyle ("button"));
			
						if (roomSelection >= 0) {
								ArrayList lista = listaTests; 
								test = (Test)lista [roomSelection];
								AsociacionTareaTest a = new AsociacionTareaTest (t.getId_Task (), test.getIdTest ());
								MultiPlayer.Instance.getSmartFox ().Send (new ExtensionRequest ("agregarAsociacionTareaTest", a.toSFSObject ()));

								MultiPlayer.Instance.getSmartFox ().ProcessEvents ();

								GameObject g = new GameObject ();
								g.AddComponent <GUI_Mensaje>();
								g.SendMessage ("setAnterior", this);
								g.SendMessage ("setTitulo", "OK");
								g.SendMessage ("setMensaje", "Asociado exitosamente.");
								g.SendMessage ("Mostrar");
								usWindow = false;
								roomSelection = -1;
						}
						GUILayout.EndScrollView ();
						GUILayout.EndArea ();

						if (GUI.Button (new Rect (maxWidth / 2 - 70, maxHeight - 50, 140, 40), "Cancelar")) {
								usWindow = false;
								ant.Mostrar ();
								Destroy (this);
						}
		} else {
				
						GUI.contentColor = Color.white;
						GUI.Label (new Rect (20, maxHeight / 2 - 40, 310, 20), "No existen Tests disponibles para ser asociados.");
						if (GUI.Button (new Rect (maxWidth / 2 - 70, maxHeight - 50, 140, 40), "Volver")) {
								usWindow = false;
								ant.Mostrar ();
								Destroy (this);

						}
				}
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




	public void setupAvailableTests()
	{
		//Debug.Log(t.getId_Story ());
		List<string> listaNombresTests = new List<string> ();
		listaTests = new ArrayList ();
		UserStory u = getUserStory(t.getId_Story());
		Debug.Log("us " + u.getTitulo() + " criterios " + u.getListaAcceptanceCriteria().Count);

		foreach (AcceptanceCriteria ac in u.getListaAcceptanceCriteria()) {
			Debug.Log("test en " + ac.getTitulo() + " " + ac.getAssociatedTests().Count);
			foreach (Test aux in ac.getAssociatedTests()) {
				Debug.Log("tests x tarea: " + t.getIdTests().Count);
				if(!t.getIdTests().Contains(aux.getIdTest())){
					listaTests.Add (aux);
					listaNombresTests.Add (aux.getIdTest () + " " + aux.getMetodo ());	
				}					
			}
		}
				
		testsNameStrings = listaNombresTests.ToArray ();
	}


	public GUI_AsociarTest (){
			
	}
}

