//------------------------------------------------------------------------------
// <auto-generated>
//     Este cÃ³digo fue generado por una herramienta.
//     VersiÃ³n de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrÃ­an causar un comportamiento incorrecto y se perderÃ¡n si
//     se vuelve a generar el cÃ³digo.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class GUI_CerrarSprint : MonoBehaviour
{
	private bool openWindow = false;
	private float maxWidth = 350;
	private float maxHeight = 200;
	private Rect windowRect;	
	private Vector2 gameScrollPositionTests= new Vector2();
	private Sprint s;
	
	void Start () {
		openWindow = false;
	}
	
	void Update () {
	}
	
	public void setSprint (Sprint s)
	{
		this.s = s;
	}
	
	public void Mostrar(){
		openWindow = true;
	}
	
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(15,15,maxWidth,maxHeight);
			windowRect = GUI.Window(0,windowRect,doTaskDetailWindow,"Cerrar Sprint");
		}	
		GUI.skin = oldSkin;
	}
	
	void doTaskDetailWindow(int windowID){
		int heightActual = 20;
		int lengthScroll = 40;
		if (s.getDescripcion () != null)
			lengthScroll = s.getDescripcion ().Length / 30 * 50;
		gameScrollPositionTests = GUI.BeginScrollView (new Rect (0, heightActual, maxWidth - 5, maxHeight - heightActual - 30), gameScrollPositionTests, new Rect (0, 0, maxWidth - 100, lengthScroll));
	    GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"ID Sprint: ",s.getId_Sprint().ToString());
	    GUI.contentColor = Color.yellow;
	    heightActual = heightActual + 25;
	    GUI.Label(new Rect(20,heightActual,310,20),"Descripcion:");
		int offset = 0;
		if(s.getDescripcion()!=null)
	    offset = 30*((int)(s.getDescripcion().Length/50)+1);
	                        
	    GUI.contentColor = Color.white;
	    heightActual = heightActual + 20;
	    GUI.Label(new Rect(40,heightActual,290,offset),s.getDescripcion());
	    heightActual = heightActual + offset;
	    GUIComponents.labelDetail(new Rect(20,heightActual,310,20),"Titulo: ",s.getTitulo().ToString());
	    GUI.EndScrollView ();
		if (GUI.Button (new Rect (35,maxHeight-40,125,30), "Cerrar Sprint")) {
			if(s.completo_y_correcto(s.get_fecha_ultimo_cambio()))
			{
				s.cerrar();
				GameObject g = new GameObject ();
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (g, "Assets/Game/Scripts/ScriptsPanelesVS/GUI_CerrarSprint.cs (74,5)", "GUI_Mensaje");
				g.SendMessage ("setAnterior", this);
				g.SendMessage ("setTitulo", "OK");
				g.SendMessage ("setMensaje", "Sprint cerrado exitosamente.");
				g.SendMessage ("Mostrar");
				openWindow = false;
			}
			else
			{
				
				bool done = true;
				foreach(UserStory us in s.getListaStories())
					done = done && us.done();
				if(!done)
				{
					GameObject g = new GameObject ();
					UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (g, "Assets/Game/Scripts/ScriptsPanelesVS/GUI_CerrarSprint.cs (90,6)", "GUI_Mensaje");
					g.SendMessage ("setAnterior", this);
					g.SendMessage ("setTitulo", "OK");
					g.SendMessage ("setMensaje", "El estado de todas las tareas debe ser DONE.");
					g.SendMessage ("Mostrar");
					openWindow = false;
				}
				else
				{
					GameObject g = new GameObject ();
					UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (g, "Assets/Game/Scripts/ScriptsPanelesVS/GUI_CerrarSprint.cs (100,6)", "GUI_Mensaje");
					g.SendMessage ("setAnterior", this);
					g.SendMessage ("setTitulo", "OK");
					g.SendMessage ("setMensaje", "Existen tests desactualizados o no ejecutados en estado completo y correcto.");
					g.SendMessage ("Mostrar");
					openWindow = false;
					
				}
				
			}
			
		}
		
		if (GUI.Button (new Rect (190,maxHeight-40,125,30), "Cerrar")) {
			openWindow = false;
		}
	}

}




