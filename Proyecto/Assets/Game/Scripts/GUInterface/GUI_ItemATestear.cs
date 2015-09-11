using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System.Security.Permissions;
using AssemblyCSharp;

public class GUI_ItemATestear : MonoBehaviour 
{
			private bool usWindow = false;
			private float maxWidth = 350;
			private float maxHeight = 350;
			private Rect windowRect;	
			private Vector2 gameScrollPositionTareas = new Vector2();
			private Vector2 gameScrollPositionSprints = new Vector2 ();
			private Vector2 gameScrollPositionuss = new Vector2 ();
			private Vector2 gameScrollPositionAcceptanceCriterias = new Vector2();
			private Vector2 gameScrollPositionTests= new Vector2();
			private int iTabSelected = 0;
			private List<Task>  tareas;
			private List<UserStory>  uss;
			private List<AcceptanceCriteria>  acceptanceCriteria;
			private List<Test>  tests;
			private List<Sprint>  sprints;
			private CrearPlanoTest crear;


			// use this for initialization
			public void Start () {
				crear = (CrearPlanoTest)(GameObject.Find("panelTesting")).GetComponent("CrearPlanoTest");
				if (!crear.Inicializado ()) {
						crear.inicializar ();
						crear.inicializado = true;
				}
				//usWindow = false;
			}

			public void OnGUI(){
				GUISkin oldSkin = GUI.skin;
				GUI.skin = Skin.Instance.skin;
				if (usWindow){
					Color c = new Color(150,150,150,1f);
					GUI.color = c;
					windowRect = new Rect(500,15,maxWidth,maxHeight);
					windowRect = GUI.Window(0,windowRect,doNewusWindow,"Seleccion de Item");
					setupTaskList();
					setupUsList();
					setupSprintsList();
					setupTestList();
					setupAcceptanceCriteriaList();		
					
				}
				GUI.skin = oldSkin;
			}

			public void Mostrar(){
				usWindow = true;
			}

			public void doNewusWindow(int windowID){

				List<ItemModelo> lista = new List<ItemModelo> ();
				GUILayout.BeginVertical ();
				{
						GUILayout.BeginHorizontal ();
						{
								string[] text = new string[] {"Tareas", "US", "Sprints", "Criterios", "Tests"};

								iTabSelected = (GUI.Toolbar(new Rect(15,30,320,20),iTabSelected, text));
								
								/*if (GUI.Toggle (new Rect (15, 30, 50, 20), iTabSelected == 0, "Tareas", EditorStyles.toolbarButton))
										iTabSelected = 0;
								if (GUI.Toggle (new Rect (65, 30, 90, 20), iTabSelected == 1, "User Stories", EditorStyles.toolbarButton))
										iTabSelected = 1;
								if (GUI.Toggle (new Rect (155, 30, 60, 20), iTabSelected == 2, "Sprints", EditorStyles.toolbarButton))
										iTabSelected = 2;
								if (GUI.Toggle (new Rect (215, 30, 70, 20), iTabSelected == 3, "Criterios", EditorStyles.toolbarButton))
										iTabSelected = 3;
								if (GUI.Toggle (new Rect (285, 30, 50, 20), iTabSelected == 4, "Tests", EditorStyles.toolbarButton))
										iTabSelected = 4;
								*/
						}
						GUILayout.EndHorizontal ();
						//Draw different UI according to iTabSelected
						DoGUI (iTabSelected);    
				}
				GUILayout.EndVertical ();
				if (GUI.Button (new Rect (maxWidth / 2 - 150, maxHeight - 30, 125, 20), "Aceptar")) {
						foreach (Task t in tareas)
								if (t.getToggle ().Equals (true)) {
										lista.Add (t);
										t.setToggle (false);
								}
						foreach (UserStory u in uss)
								if (u.getToggle ().Equals (true)) {
										lista.Add (u);
										u.setToggle (false);
								}
						foreach (AcceptanceCriteria ac in acceptanceCriteria)
								if (ac.getToggle ().Equals (true)) {
										lista.Add (ac);
										ac.setToggle (false);
								}
						foreach (Sprint s in sprints)
							if (s.getToggle ().Equals (true)) {
									lista.Add (s);
									s.setToggle (false);
							}
						foreach (Test t in tests)
							if (t.getToggle ().Equals (true)) {
									lista.Add (t);
									t.setToggle (false);
							}
					
						foreach (ItemModelo i in lista) {
								i.setHistoriales ();
						}
						crear.setItems (lista);
					
						usWindow = false;
						Destroy (this);
					
				}  
				if (GUI.Button (new Rect (maxWidth / 2 + 25, maxHeight - 30, 125, 20), "Cancelar")) {
						usWindow = false;
						Destroy(this);
				}

		}

			private void DoGUI(int iTabSelected)
			{

				

				if (iTabSelected == 0)
				{
					GUI.Label(new Rect(maxWidth/2-25,60,50,20),"Tareas");
					gameScrollPositionTareas = GUI.BeginScrollView(new Rect (0, 100, maxWidth -50 , maxHeight - 150), gameScrollPositionTareas, new Rect (0, 0, maxWidth -100 , tareas.Count*20));
					int heightActual = 0;
					
					int i=0;
					foreach(Task s in tareas){
						s.setToggle(GUI.Toggle(new Rect(40, heightActual, 50, 20), s.getToggle(),""));
				        GUI.Label (new Rect (100, heightActual, 200, 20), s.getId_Task() + " " +s.getTitulo());
						heightActual += 20;
						i++;
					}
					GUI.EndScrollView ();
				}
		
				if (iTabSelected == 1)
				{
					
					GUI.Label(new Rect(maxWidth/2-45,60,90,20),"User Stories");
					gameScrollPositionSprints = GUI.BeginScrollView(new Rect (0, 100, maxWidth -50 , maxHeight - 150), gameScrollPositionSprints, new Rect (0, 0, maxWidth -100 , uss.Count*20));
					int heightActual = 0;
					foreach(UserStory s in uss){
						s.setToggle(GUI.Toggle(new Rect(40, heightActual, 50, 20),s.getToggle(),""));
				        GUI.Label (new Rect (100, heightActual, 200, 20), s.getId_UserStory() + " " + s.getTitulo());
						heightActual += 20;
					}
					GUI.EndScrollView ();
					
				}

				if (iTabSelected == 2)
				{
					GUI.Label(new Rect(maxWidth/2-30,60,60,20),"Sprints");
					gameScrollPositionSprints = GUI.BeginScrollView(new Rect (25, 100, maxWidth -50 , maxHeight - 150), gameScrollPositionSprints, new Rect (0, 0, maxWidth -100 , sprints.Count*20));
					int heightActual = 0;
					foreach(Sprint s in sprints){
						s.setToggle (GUI.Toggle(new Rect(40, heightActual, 50, 20), s.getToggle(),""));
						GUI.Label (new Rect (100, heightActual, 200, 20), s.getId_Sprint() + " " + s.getTitulo());
						heightActual += 20;
					}
					GUI.EndScrollView ();

				}
				if (iTabSelected == 3)
				{
					GUI.Label(new Rect(maxWidth/2-35,60,70,20),"Criterios");

					gameScrollPositionAcceptanceCriterias = GUI.BeginScrollView(new Rect (0, 100, maxWidth -50 , maxHeight - 150), gameScrollPositionAcceptanceCriterias, new Rect (0, 0, maxWidth -100 , acceptanceCriteria.Count*20));
					int heightActual = 0;
					foreach(AcceptanceCriteria s in acceptanceCriteria){
						s.setToggle(GUI.Toggle(new Rect(40, heightActual, 50, 20), s.getToggle(),""));
						GUI.Label (new Rect (100, heightActual, 200, 20), s.getId_Criteria() + " " + s.getTitulo());
						heightActual += 20;
					}
					GUI.EndScrollView ();

				}
				if (iTabSelected == 4)
				{
					GUI.Label(new Rect(maxWidth/2-25,60,50,20),"Tests");
					gameScrollPositionTests = GUI.BeginScrollView(new Rect (0, 100, maxWidth -50 , maxHeight - 150), gameScrollPositionTests, new Rect (0, 0, maxWidth -100 , tests.Count*20));
					int heightActual = 0;
					foreach(Test s in tests){
						bool valor = false;
						s.setToggle(GUI.Toggle(new Rect(40, heightActual, 50, 20), s.getToggle(),""));
						GUI.Label (new Rect (100, heightActual, 200, 20), s.getIdTest() + " " + s.getClase() + "." + s.getMetodo());
						heightActual += 20;
					}
					GUI.EndScrollView ();
				}
		}
	
		private	void setupTaskList ()
		{

			ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
			tareas = new List<Task>();
			foreach (Sprint s in listaSprints)
				foreach (UserStory us in s.getListaStories())
					foreach (Task t in us.getListaTareas())
						tareas.Add (t);
		}

		private	void setupUsList ()
		{
			ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
			uss = new List<UserStory>();
			foreach (Sprint s in listaSprints)
				foreach (UserStory us in s.getListaStories())
					uss.Add (us);
		}


		private	void setupAcceptanceCriteriaList ()
		{
		ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
	
			acceptanceCriteria = new List<AcceptanceCriteria>();
			foreach (Sprint s in listaSprints)
				foreach (UserStory us in s.getListaStories())
					foreach (AcceptanceCriteria ac in us.getListaAcceptanceCriteria())
						acceptanceCriteria.Add (ac);
		}
		private	void setupTestList ()
		{
			ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
				tests = new List<Test>();
			foreach (Sprint s in listaSprints)
					foreach (UserStory us in s.getListaStories())
						foreach (AcceptanceCriteria ac in us.getListaAcceptanceCriteria())
							foreach (Test t in ac.getAssociatedTests())
								tests.Add (t);
		}


		private	void setupSprintsList ()
		{
			ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
				sprints = new List<Sprint>();
				foreach (Sprint s in listaSprints)
					sprints.Add (s);
		}


}


