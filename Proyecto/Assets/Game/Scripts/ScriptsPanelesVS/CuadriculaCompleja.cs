using UnityEngine;
using System.Collections;

public class CuadriculaCompleja
{
	//private ArrayList cuadriculas;
	private GameObject gameObject;
	private Cuadricula toDoQuad;
	private Cuadricula doingQuad;
	private Cuadricula doneQuad;
	private Cuadricula onTestQuad;
	private Text3DCuboUS story;
	
	public CuadriculaCompleja (GameObject g)
	{
		//cuadriculas = new ArrayList ();
		gameObject = g;
		toDoQuad = new Cuadricula (FindChild ("toDo"));	
		doingQuad = new Cuadricula (FindChild ("doing"));
		doneQuad = new Cuadricula (FindChild ("done"));
		onTestQuad = new Cuadricula (FindChild ("onTest"));
		setBotones (toDoQuad);
		setBotones (doingQuad);
		setBotones (onTestQuad);
		setBotones (doneQuad);
	}
	
	public void setUserStory(Text3DCuboUS t)
	{
		story = t;
		story.dibujarObjetoEnQuad(FindChild("story"),12);
	}
	
	private void setBotones (Cuadricula c)
	{
		avanzarQuadTaskboard accion = (avanzarQuadTaskboard)c.FindChild ("backButton").GetComponent ("avanzarQuadTaskboard");
		accion.setCuadricula (c);
		accion = (avanzarQuadTaskboard)c.FindChild ("forwardButton").GetComponent ("avanzarQuadTaskboard");
		accion.setCuadricula (c);
	}
	
	public void destruir ()
	{
		toDoQuad.destruir ();
		doingQuad.destruir ();
		onTestQuad.destruir ();
		doneQuad.destruir ();
		story.destruir();
	}
	
	public void addElementoToDo (Text3DCubo t)
	{
		toDoQuad.addElemento (t);
	}
	
	public void addElementoDoing (Text3DCubo t)
	{
		doingQuad.addElemento (t);
	}
	
	public void addElementoDone (Text3DCubo t)
	{
		doneQuad.addElemento (t);
	}

	public void addElementoOnTest (Text3DCubo t)
	{
		onTestQuad.addElemento (t);
	}
	
	public GameObject FindChild (string pName)
	{
		Transform pTransform = gameObject.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}
	
	public void borrarTareas(){
		toDoQuad.borrarTareas();
		doingQuad.borrarTareas();
		doneQuad.borrarTareas();
		onTestQuad.borrarTareas ();
	}
}


