using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using System;


namespace AssemblyCSharp
{
	public class GUI_DetalleCriterio : MonoBehaviour
		{
			private bool openWindow = false;
			private float maxWidth = 350;
			private float maxHeight = 450;
			private float x = 15;
			private float y = 15;
			private Rect windowRect;
			private string longString = "This is a long-ish string";
			private List<string> testNameStrings;
			private Vector2 gameScrollPositionTest = new Vector2();
			private int roomSelection = -1;
			private GUI_DetalleStory ant;
			private AcceptanceCriteria acceptanceCriteria;
			private Test t ;


		public void setAnterior(GUI_DetalleStory g)
		{
			ant = g;		
		}


		void Start () {
			SetupTestList();
		}



		public AcceptanceCriteria getAcceptanceCriteria(){
			return acceptanceCriteria;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		public void Mostrar(){
			openWindow = true;
		}
	

		
		public void setAcceptanceCriteria(AcceptanceCriteria ac){
			acceptanceCriteria = ac;
		}
		
		void OnGUI(){
			GUISkin oldSkin = GUI.skin;
			GUI.skin = Skin.Instance.skin;
			if ((openWindow)){
				windowRect = new Rect(x,y,maxWidth,maxHeight);
				windowRect = GUI.Window(0,windowRect,doAcceptanceCriteriaDetailWindow,"Detalle de Criterio de Aceptacion");
			}
			GUI.skin = oldSkin;
		}
		
		
		void doAcceptanceCriteriaDetailWindow(int windowID){
			SetupTestList();
			int heightActual = 30;
			GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"ID UserStory: ",acceptanceCriteria.getId_Story().ToString());
			GUI.contentColor = Color.yellow;
			heightActual = heightActual + 25;
			GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"ID Acceptance Criteria: ",acceptanceCriteria.getId_Criteria().ToString());
			heightActual = heightActual + 25;
			GUI.contentColor = Color.yellow;
			GUI.Label(new Rect(20,heightActual,310,20),"Descripcion:");
			int offset = 18*((int)(acceptanceCriteria.getDescripcion().Length/50)+1);
			
			GUI.contentColor = Color.white;
			heightActual = heightActual + 25;
			GUI.Label(new Rect(40,heightActual,290,offset+10),acceptanceCriteria.getDescripcion());
			heightActual = heightActual + offset;
			GUI.contentColor = Color.yellow;
			heightActual += 10;
			GUI.Label (new Rect(20,heightActual,310,20),"Tests Asociados:");
			
			
			GUI.contentColor = Color.white;
			heightActual = heightActual + 30;
			gameScrollPositionTest = GUI.BeginScrollView (new Rect(10, heightActual, 350, 200),gameScrollPositionTest, new Rect(0, 0, 250, testNameStrings.Count * 25));
			int height = 0;
			Debug.Log ("Tests " + testNameStrings.Count);
			foreach(string test in testNameStrings)
			{
				long id = Convert.ToInt64(test.Split(' ')[0]);
				t= getTest(id);

				if (GUI.Button(new Rect (15,height ,200, 20),test)){

					GameObject g = new GameObject();
					g.AddComponent<GUI_DetalleTest>();
					g.SendMessage("setAnterior",this);
					g.SendMessage("setTest",t);
					g.SendMessage("Mostrar");
					openWindow = false;	
				}

				if (GUI.Button(new Rect (225, height ,80, 20),"Eliminar")){
					MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("eliminarTest",t.toSFSObject()));
					EliminarTest(t);
					GameObject g = new GameObject ();
					g.AddComponent <GUI_Mensaje>();
					g.SendMessage ("setAnterior", this);
					g.SendMessage ("setTitulo", "OK");
					g.SendMessage ("setMensaje", "Test eliminado exitosamente.");
					g.SendMessage ("Mostrar");
					openWindow = false;
				}


				height += 25;
			}

		
			GUI.EndScrollView ();

			
			GUI.contentColor = Color.white;
			if(GUI.Button(new Rect(maxWidth/2-160,maxHeight-50,150,40),"Agregar"+ System.Environment.NewLine + "Test")){
				GameObject g = new GameObject();
				g.AddComponent<GUI_CargarTest>();
				g.SendMessage("Mostrar",this);
				openWindow=false;
			}

			GUI.contentColor = Color.white;
			if (GUI.Button(new Rect(maxWidth / 2 + 10,maxHeight - 50, 150, 40),"Cerrar")){

				openWindow = false;
				this.ant.Mostrar();
				Destroy(this);
			}
		}

		public Test getTest(long id)
		{
			ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
			foreach (Test t in acceptanceCriteria.getAssociatedTests())
								if (t.getIdTest ().Equals (id)) {
				Debug.Log("encontrado");
										return t;
								}
			return null;
		}
		public UserStory getUserStory(long id)
		{
			ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
			foreach (Sprint s in Sprints)
				foreach (UserStory u in s.getListaStories())
					if (u.getId_UserStory ().Equals (id))
						return u;
			return null;
		}

		public void EliminarTest(Test t)
		{

			UserStory u= getUserStory( acceptanceCriteria.getId_Story ());
			foreach (Task task in u.getListaTareas())
				if (task.getIdTests ().Contains (t.getIdTest ()))
					task.getIdTests ().Remove (t.getIdTest ());
			this.acceptanceCriteria.getAssociatedTests ().Remove (t);		
		}

		public void SetupTestList () {
			testNameStrings = new List<string> ();
			List<Test> lista = acceptanceCriteria.getAssociatedTests();
			foreach(Test t in lista){
				testNameStrings.Add(t.getIdTest() + " " + t.getMetodo());
			}

		}
	}
}

