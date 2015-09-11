using UnityEngine;
using System.Collections;

public class avanzarQuadTaskboard : MonoBehaviour {
	
	
	Cuadricula quad;
	public int accion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setAccion(int a){
		if ((a == 1) || (a == -1))
			accion = a;
	}
	
	public void setCuadricula(Cuadricula c){
		quad = c;
	}
	
	void OnMouseUpAsButton(){
		quad.changePage(accion);
	}
	
	
	
	
}
