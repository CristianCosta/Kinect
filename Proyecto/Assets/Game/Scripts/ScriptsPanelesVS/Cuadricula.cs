using UnityEngine;
using System.Collections;

public class Cuadricula
{

	protected ArrayList elementos;
	protected int maxElementos;
	protected int index;
	protected int sprintActual=0;
	protected GameObject gameobject;
	protected int fontSize = 14;

	public int getCount()
	{
		return elementos.Count;
	}

	public int getSprintActual()
	{
		return sprintActual;
	}

	public void setSprintActual(int i)
	{
		sprintActual = i;
	}

	public Cuadricula (GameObject gameobject)
	{
		elementos = new ArrayList ();
		maxElementos = 4;
		index = 0;
		this.gameobject = gameobject;
	}
	
	public void destruir(){
	 
		foreach(Text3DCubo item in elementos)
		{ 
			item.destruir();
		}	
	}
	
	
	public void setIndex (int i)
	{
		index= i;
	}
	
	public int getIndex ()
	{
		return index;
	}
	
	public void setMaxElementos (int cant)
	{
		this.maxElementos = cant;
	}
	
	public int getMaxElementos ()
	{
		return this.maxElementos;
	}
	
	public virtual void addElemento (Text3DCubo e)
	{		
		this.elementos.Add(e);
		
		string quad = "quad";
		int cant = (elementos.Count-1) % maxElementos + 1;
		string quadn = quad+cant.ToString();
		
		if ((elementos.Count - 1 >= index * maxElementos) && (elementos.Count - 1 < index * maxElementos + 4)) 
						e.dibujarObjetoEnQuad (FindChild (quadn), fontSize);
				
	}
	
	
	public virtual void changePage(int i){
		if (((i == -1) && (index > 0)) || ((i == 1) && (index < (Mathf.CeilToInt((float)elementos.Count/(float)maxElementos)-1)))){
			for (int j = index*maxElementos; j<index*maxElementos+4;j++){
				if (j < elementos.Count)
					((Text3DCubo)elementos[j]).noDibujar();
			}
			index+=i;
			for(int j = index*maxElementos; j<index*maxElementos+4;j++){
				if (j < elementos.Count)
					((Text3DCubo)elementos[j]).dibujarObjetoEnQuad(FindChild("quad"+((j%maxElementos)+1)),fontSize);
			}
		}
	}
	
	
	public void removeElemento (Text3DCubo e){
		this.elementos.Remove(e);
	}
		
	/*private GameObject FindChild (string pName)
	{   
		Transform pTransform = gameobject.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}	*/
	
	
	
	
	public GameObject FindChild (string pName)
	{
		Transform pTransform = gameobject.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}
	
	public void borrarTareas(){
		if(elementos!=null){
			for (int j = index*maxElementos; j<index*maxElementos+4;j++){
				if (j < elementos.Count)
					((Text3DCubo)elementos[j]).noDibujar();
			}
			foreach(Text3DCubo t in elementos){
				t.destruir();
			}
			elementos.Clear();
		}
	}
	
	public void borrarPlano()
	{
		foreach (Text3DCubo t in elementos) {
			t.noDibujar ();
			t.destruir();		
		}
		elementos.Clear ();

	}
	
}
