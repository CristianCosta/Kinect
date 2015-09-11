using UnityEngine;
using System.Collections;

public class crearPlanoTask : MonoBehaviour
{

	// use this for initialization
	
	
	GameObject posts;
	private CuadriculaCompleja userStories1, userStories2;
	private CuadriculaSprint sprintBar;
	public Material materialTexto;
	public Font fontTexto;
	public int comienzosprint = 0;
	public int comienzoUserStory = 0;


	public int getSprintActual()
	{
		return sprintBar.getSprintActual ();
	}

	public void setSprint (int i)
	{
		this.comienzosprint = i;
		this.comienzoUserStory = 0;
		ArrayList Sprints = new ArrayList ();
		foreach (Sprint s in MultiPlayer.Instance.getListaSprints())
			if (!s.Cerrado ())
				Sprints.Add (s);
		inicializar(Sprints);
	}
	
	public void cambiarUserStory(int i)
	{
		ArrayList Sprints = MultiPlayer.Instance.getListaSprints();
		Sprint s = (Sprint)Sprints [comienzosprint];
		ArrayList listaStories = new ArrayList ();
		foreach (UserStory us in s.getListaStories())
			if (!us.Cerrada ())
				listaStories.Add (us);
		if (!(((comienzoUserStory == 0) && (i == -1)) || ((comienzoUserStory+1 >= listaStories.Count-1) && (i == 1)))){
			this.comienzoUserStory = this.comienzoUserStory + 2*(i);
		}
		inicializar(Sprints);
		
	}
	
	public void inicializar (ArrayList Sprints)
	{
		if (userStories1 != null)
			userStories1.destruir ();
		if (userStories2 != null)
			userStories2.destruir ();
		if (sprintBar != null)
			sprintBar.destruir ();
		 
		sprintBar = new CuadriculaSprint(GameObject.Find ("sprintbar"));
		if (comienzosprint > Sprints.Count)
			comienzosprint = 0;
		sprintBar.setSprintActual (comienzosprint);

		for (int i = 0; i < Sprints.Count;i++){
			if(!((Sprint)Sprints[i]).Cerrado()){
				Text3DCuboSprint cubo = new Text3DCuboSprint(((Sprint)Sprints[i]).getTitulo(),i);
				cubo.setus(((Sprint)Sprints[i]));
				cubo.setMaterial (materialTexto);
				cubo.setFont (fontTexto);
				sprintBar.addElemento(cubo);
			}
		}
		
		
		// si hay Sprints    si hay 1 es el PB
		if (Sprints.Count >= 2) {
			Debug.Log (comienzosprint);
			Sprint s = (Sprint)Sprints [comienzosprint];
			if(!s.Cerrado()){
				s.setHistoriales();
				ArrayList listaStories = new ArrayList ();
				foreach (UserStory us in s.getListaStories())
					if (!us.Cerrada ())
						listaStories.Add (us);

				int i = comienzoUserStory;
			//Reviso que haya user Stories
				if (listaStories.Count > i) {
					userStories1 = new CuadriculaCompleja (GameObject.Find ("filaUserStory1"));//
					Text3DCuboUS story1 = new Text3DCuboUS(((UserStory)listaStories[i]));
					story1.setMaterial (materialTexto);
					story1.setFont (fontTexto);
					userStories1.setUserStory(story1);
					//story1.dibujarObjetoEnQuad(userStories1.FindChild("story"),12);
					recorrerTareas ((UserStory)listaStories [i], userStories1);
					i = i+1;
					if (listaStories.Count > i){
						userStories2 = new CuadriculaCompleja (GameObject.Find ("filaUserStory2"));//
						Text3DCuboUS story2 = new Text3DCuboUS(((UserStory)listaStories[i]));
						story2.setMaterial (materialTexto);
						story2.setFont (fontTexto);
						userStories2.setUserStory(story2);
						//story2.dibujarObjetoEnQuad(userStories2.FindChild("story"),12);
						recorrerTareas ((UserStory)listaStories [i], userStories2);
						i = i + 1;	
					}
					i = i + 1;
				}

			}
		
		}
	}
			
	public void recorrerTareas (UserStory u, CuadriculaCompleja c)
	{
		foreach (Task t in u.getListaTareas()) {
			Text3DCubo cubotarea = new Text3DCuboTask (t);
			cubotarea.setMaterial (materialTexto);
			cubotarea.setFont (fontTexto);
			switch (t.getEstado ()) { 
			case "TO DO":
				c.addElementoToDo (cubotarea);
				break;				        
			case "DOING":
				c.addElementoDoing (cubotarea);
				break;
			case "ON TEST":
				c.addElementoOnTest(cubotarea);
				break;
			case "DONE":
				c.addElementoDone (cubotarea);
				break;				
			}
			  
			
		}
		
	}
	
	
		public void actualizar(ArrayList Sprints)
		{
			if (userStories1 != null)
				userStories1.borrarTareas();
			if (userStories2 != null)
				userStories2.borrarTareas();
			// si hay Sprints    si hay 1 es el PB
			if (Sprints.Count >= 2) {
				Sprint s=(Sprint)Sprints [comienzosprint];
				int count = -1;
				foreach(Sprint sp in Sprints){
					if(!sp.Cerrado())
						count++;
					if(count==comienzosprint)
						s = sp;		
				}
			
				ArrayList listaStories = s.getListaStories ();
				int i = comienzoUserStory;
				//Reviso que haya user Stories
				Debug.Log ("listaStories.Count = " + listaStories.Count);
				while ((i < (comienzoUserStory+2)) && (listaStories.Count > 0)) {
					recorrerTareas ((UserStory)listaStories [i], userStories1);
					i = i + 1;
					if (listaStories.Count > 1) {
						recorrerTareas ((UserStory)listaStories [i], userStories2);
						i = i + 1;	
					}
					i = i + 1;
				}
			
			}
		}
		
		
	/*
			
		
	foreach(Sprint s in Sprints)
		{
		  foreach(UserStory u in s.getListaStories())
			{
				foreach(Task t in u.getListaTareas()){
				 Text3DCubo cubotarea = new Text3DCuboTask(t);
				 cubotarea.setMaterial(materialTexto);
				 cubotarea.setFont(fontTexto);
				 userStories1.addElementoToDo(cubotarea);
					return;
					switch (t.getEstado())
					{ 
						case "TO DO":
						 
						
						break;
						        
						case "DOING":
						break;
						case "DONE":
						break;
					default:
						break;
				    }
			
				}
		}
	 		
	}
	}
	
	*/
	
	void Start ()
	{
		
		 
		
		/*
		 cubo=GameObject.CreatePrimitive(PrimitiveType.Cube);
		 cubo.transform.position=new Vector3(70, 27, 115);
		cubo.transform.localScale=new Vector3(10,10,0);
		Rigidbody r = (Rigidbody) cubo.AddComponent<Rigidbody>();
		r.freezeRotation=true;
		
		cubo.AddComponent<DragRigidbody>();
		 textoGameObject = new GameObject("Texto3D");
		 
		 
		 TextMesh texto = (TextMesh)textoGameObject.AddComponent<TextMesh> ();
		 MeshRenderer meshRenderer = (MeshRenderer)textoGameObject.AddComponent<MeshRenderer> ();
		  
	    	meshRenderer.material = this.materialTexto;
		 texto.transform.parent=cubo.transform;
		
		
		textoGameObject.transform.localPosition=new Vector3(0f, 0f, 0f);
		textoGameObject.transform.localScale=new Vector3(0.1f,0.1f,0.1f);
		texto.anchor = TextAnchor.MiddleCenter;
		
		texto.font = this.fontTexto;
		texto.fontSize = 40;
		
		 texto.text="probando";
		
		// texto.text="Probando";
		/* 
		Font myFont=(Resources.Load("Arial") as Font);
		 texto.font=myFont;
		 texto.fontSize=25;
		 texto.transform.localScale=new Vector3(10,10,0);
		 texto.transform.position=cubo.transform.position;
		 texto.transform.parent=cubo.transform;
	     texto.gameObject.active=true;	
	
		 
         */
 
		
		
		 		
		
		 
 
			
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
