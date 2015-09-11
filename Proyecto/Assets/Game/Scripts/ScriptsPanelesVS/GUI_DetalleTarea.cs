using UnityEngine;
using System.Collections;

public class GUI_DetalleTarea : MonoBehaviour {
	
	public bool openWindow;
	private float maxWidth = 350;
	private float maxHeight = 350;
	private Rect windowRect;
	private string puntos="";
	
	private Task t;
	private GUI_DetalleStory ant;
	
	// use this for initialization
	void Start () {
		openWindow = false;
	}


	public void setAnterior(GUI_DetalleStory anterior)
	{
		ant = anterior;
	}
	// Update is called once per frame
	void Update () {
	}
	
	public void setTarea(Task tarea){
		t = tarea;
	}
	
	public void Mostrar(){
		openWindow = true;
	}
		
	
	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(15,15,maxWidth,maxHeight);
			windowRect = GUI.Window(1,windowRect,doTaskDetailWindow,"Detalle de tarea");
		}
		GUI.skin = oldSkin;
	}
		
	void doTaskDetailWindow(int windowID){
		GUIComponents.labelDetail(new Rect(20,30,310,20),"Tarea: ",t.getTitulo());
		GUI.contentColor = Color.yellow;
		GUI.Label(new Rect(20,55,310,20),"DescripciÃ³n:");
		int offset = 18*((int)(t.getDescripcion().Length/50)+1);
		if (offset > 53){
			offset = 53;
		}
		GUI.contentColor = Color.white;
		GUI.Label(new Rect(40,75,290,offset),t.getDescripcion());
		GUIComponents.labelDetail(new Rect(20,80+offset,310,20),"Responsable: ",t.getResponsable());
		GUIComponents.labelDetail(new Rect(20,105+offset,310,20),"Tiempo estimado: ",t.getT_Estimado().ToString());
		GUIComponents.labelDetail(new Rect(20,130+offset,310,20),"Tiempo restante: ",(t.getT_Estimado()-t.getT_Total()).ToString());
		GUIComponents.labelDetail(new Rect(20,155+offset,310,20),"Tiempo total: ",t.getT_Total().ToString());
		GUIComponents.labelDetail(new Rect(20,180+offset,310,20),"Prioridad: ",t.getPrioridad().ToString());
		GUIComponents.labelDetail(new Rect(20,205+offset,310,20),"Estado: ",t.getEstado());

		if (t.getEstado ().Equals ("ON TEST"))
				if (GUI.Button (new Rect (maxWidth / 2 - 90, 235 + offset, 185, 20), "Administrar Tests")) {
						if (Application.isWebPlayer) {
								GameObject g = new GameObject ();
								UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (g, "Assets/Game/Scripts/ScriptsPanelesVS/GUI_DetalleTarea.cs (69,9)", "GUI_Mensaje");
								g.SendMessage ("setMensaje", "Esta funcionalidad no esta disponible en la version Web");
								g.SendMessage ("setAnterior", this);
								g.SendMessage ("Mostrar");
								openWindow = false;
						} else {
								offset += 30;
								GameObject g = new GameObject ();
								UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (g, "Assets/Game/Scripts/ScriptsPanelesVS/GUI_DetalleTarea.cs (77,9)", "GUI_Testing");
								g.SendMessage ("setTask", t);
								g.SendMessage ("setAnterior", this);
								g.SendMessage ("Mostrar");
								openWindow = false;	
						}
				}
		puntos=GUIComponents.labelTextField(new Rect(20,maxHeight-60,120,20),puntos,"Story Points: ",30);
	
		
		//GUIComponents.labelTextField(new Rect(20,225+offset,310,20)," ","Cargar Puntos :",4);
		
		
		if (GUI.Button(new Rect (maxWidth / 2 - 130, maxHeight - 30, 100, 20),"Cargar Puntos")){
		    t.setT_Total(t.getT_Total()+System.Convert.ToInt32(puntos));
			MultiPlayer.Instance.cambiarTTotalTask(t);	
		}
		 
			
			
		if (GUI.Button (new Rect (maxWidth/2+20,maxHeight-30,110,20), "Cerrar")) {
			openWindow = false;
			try {
				this.ant.Mostrar ();
				Destroy (this);
			} catch {
				Debug.Log ("No tiene anterior porque viene del taskboard");
			}
		}

		
	}
	
	
	void OnMouseUpAsButton(){
		if(GUIUtility.hotControl == 0)
		Mostrar();
	}
	
}

