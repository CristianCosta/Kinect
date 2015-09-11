using UnityEngine;
using System.Collections;

public class crearPlanningPoker : MonoBehaviour
{
	
	// VERIFICAR EL FUNCIONAMIENTO CORRECTO DE ESTA CLASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	
	private CuadriculaPoker quad1,quad2,quad3,quad4;
	public Material materialTexto;
	public Font fontTexto;
	public int comienzoUserStory = 0;
	
	
	public void cambiarUserStory(int i)
	{
		ArrayList sprints = MultiPlayer.Instance.getListaSprints();
		Sprint s = (Sprint)sprints[0];
		ArrayList listaStories = s.getListaStories();
		if (!(((comienzoUserStory == 0) && (i == -1)) || ((comienzoUserStory+4 >= listaStories.Count-1) && (i == 1)))){
			this.comienzoUserStory = this.comienzoUserStory + 4*(i);
			inicializar();
		}		
	}
	
	
	public void inicializar ()
	{
		if (quad1 != null)
			quad1.destruir();
		if (quad2 != null)
			quad2.destruir();
		if (quad3 != null)
			quad3.destruir();
		if (quad4 != null)
			quad4.destruir();
		quad1 = new CuadriculaPoker (GameObject.Find ("Fila1"),fontTexto,materialTexto);
		quad2 = new CuadriculaPoker (GameObject.Find ("Fila2"),fontTexto,materialTexto);
		quad3 = new CuadriculaPoker (GameObject.Find ("Fila3"),fontTexto,materialTexto);
		quad4 = new CuadriculaPoker (GameObject.Find ("Fila4"),fontTexto,materialTexto);
		
		ArrayList sprints = MultiPlayer.Instance.getListaSprints();
		if (sprints.Count >= 1) {
	    //for (int k = 0; k < sprints.Count; k++){
			Sprint s = (Sprint)sprints[0];
			ArrayList listaStories = s.getListaStories ();
			int i = comienzoUserStory;
			//Reviso que haya User Stories
			for (int j = i; j<i+4; j++){
				if (listaStories.Count > j) {
					PokerUS story = new PokerUS(((UserStory)listaStories[j]));
					story.setMaterial (materialTexto);
					story.setFont (fontTexto);
					int aux = j-i+1;
					if (aux == 1){
						quad1.setUserStory(story);
						cargarEstimaciones (story.getUS(),quad1);
					}
					if (aux == 2){
						quad2.setUserStory(story);
						cargarEstimaciones (story.getUS(),quad2);
					}
					if (aux == 3){
						quad3.setUserStory(story);
						cargarEstimaciones (story.getUS(),quad3);
					}
					if (aux == 4){
						quad4.setUserStory(story);
						cargarEstimaciones (story.getUS(),quad4);
					}
				}
			}
		}
	}
	
	
	public void cargarEstimaciones (UserStory u, CuadriculaPoker c)
	{
		foreach (Estimacion e in u.getListaEstimacion()) {
			Carta card = new Carta(e);
			card.setFont(fontTexto);
			card.setMaterial(materialTexto);
			c.addElemento(card);	
		}
		
	}
	
	
	// Use this for initialization
	void Start () {
		/*Estimacion e = new Estimacion();
		e.setId_UserStory(2);
		e.setUser("Juanito");
		e.setValorEstimacion(20);
		e.setDescripcion("no tenia ni idea de como se hace esto");*/
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
