using UnityEngine;
using System.Collections;

public class NextStories : MonoBehaviour {

	private int cont= 0;
	private int contStories= 0;
	private ArrayList stories = new ArrayList();
	private crearPlanoUS planoUs; 
	// Use this for initialization
	void Start () {
		 planoUs = (crearPlanoUS)(GameObject.Find("panelBacklog")).GetComponent("crearPlanoUS");
	}
	
	public void cargarVector (){
	
		stories.Clear();
		cont = 0;
		contStories = 0;
		ArrayList listaSprints = MultiPlayer.Instance.getListaSprints();
		foreach (Sprint s in listaSprints){
			foreach (UserStory u in s.getListaStories()){
				
				u.setId_Sprint(s.getId_Sprint());
				stories.Add(u);
				cont++;
			}
		}
		completarStories();
	}
	
	private void completarStories(){
		
		while ((cont % 5) != 0){
//			Debug.Log("Porcentaje"+ cont % 5);
			UserStory aux = new UserStory();
			aux.setTitulo("");
			aux.setPrioridad(0);
			aux.setId_UserStory(0);
			aux.setDescripcion("");
			aux.setId_Sprint(0);
			stories.Add(aux);
			cont++;
		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp(){
		if (GUIUtility.hotControl == 0) {
						//Debug.Log("Este es el cont"+ cont);
				if (contStories < cont) {
	
						//pasar solo 5
						ArrayList storiesAux = new ArrayList ();
						for (int i= contStories; i < contStories + 5; i++)
								storiesAux.Add (stories [i]);
						contStories = contStories + 5;
						planoUs.inicializar (storiesAux);
				}
		}
	//	Debug.Log("Este es el contStories"+ contStories);
	}
	
	public void BackStories(){
		//Debug.Log("Dentro de BackStories"+contStories);
		contStories = contStories - 5;
		if (contStories < 0 )
			contStories=0;
		//Debug.Log("Dentro de Bacsadsa"+contStories);	
	   //pasar solo 5
		ArrayList storiesAu = new ArrayList();
		for (int i= contStories;i < contStories + 5;i++)
			storiesAu.Add(stories[i]);
		
		//contStories = contStories + 5;
		//Debug.Log("Este es el cont"+ cont);
		planoUs.inicializar(storiesAu);
	}
	
	public void addStory(UserStory aux){
	
		foreach (UserStory u in stories){
			if (u.getDescripcion().Equals("")){
				u.setDescripcion(aux.getDescripcion());
				u.setId_Sprint(0);
				u.setId_UserStory(aux.getId_UserStory());
				u.setPrioridad(aux.getPrioridad());
				u.setTitulo(aux.getTitulo());
				return;
			}
		}
		stories.Add(aux);
		cont++;
		completarStories();
		
		
	}
	
	public void addTarea(Task t){
		foreach (UserStory u in stories){
			if (u.getId_UserStory().Equals(t.getId_Story())){
				u.addTask(t);
				break;
			}
				
		}
		
	}

	public void addCriterio(AcceptanceCriteria ac){
		foreach (UserStory u in stories){
			if (u.getId_UserStory().Equals(ac.getId_Story())){
				u.addCriteria(ac);
				break;
			}
			
		}
		
	}

	public void cargarInicial(){
		OnMouseUp();
	}
}
