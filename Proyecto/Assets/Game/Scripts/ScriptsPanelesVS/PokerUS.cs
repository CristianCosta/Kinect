using UnityEngine;
using System.Collections;

public class PokerUS : Text3DCuboUS {
	
	GameObject botonHabilitar;
	
	public PokerUS(UserStory u):base (u)
	{	
	}
	
	public override void dibujarObjetoEnQuad(GameObject padre,int fontsize){
		base.dibujarObjetoEnQuad(padre,fontsize);
		textoGameObject.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
		UserVS aux = (UserVS)GameObject.Find("Multi").GetComponent("UserVS");
		//No se usa!!  //string userName = aux.getMyUserName();
		if (user.getEstadoEstimacion() == 1){
			plano.AddComponent<Gui_estimacion>();
			Gui_estimacion script = (Gui_estimacion)plano.GetComponent("Gui_estimacion");
			script.setStory(user);
			plano.GetComponent<Renderer>().material = (Material)Resources.Load("UserStoryS");			
		}
		if (aux.esScrumMaster())
			dibujarBoton(FindChild("habilitar",padre));
		
	}
		
	public void dibujarBoton(GameObject padre){
		 botonHabilitar = GameObject.CreatePrimitive(PrimitiveType.Plane);
		 Debug.Log("quien es el padre: "+padre.name);
		 botonHabilitar.name = "boton";
		 botonHabilitar.transform.parent = padre.transform;
		 botonHabilitar.transform.localRotation = new Quaternion(0,0,0,0);
		 botonHabilitar.transform.localPosition = new Vector3(0,0.1f,0);
		 botonHabilitar.transform.localScale = Vector3.one;
		 botonHabilitar.layer = 12;
		 botonHabilitar.AddComponent<habilitarEstimacion>();
		 habilitarEstimacion script = (habilitarEstimacion)botonHabilitar.GetComponent("habilitarEstimacion");
		 script.setUS(this);
	}
	
	public UserStory getUS(){
		return user;
	}
	
	public void cambiarEstado(int estado){
		user.setEstadoEstimacion(estado);
		MultiPlayer aux = (MultiPlayer)GameObject.Find("Multi").GetComponent("MultiPlayer");
		if (estado == 0){
			user.calcularValorEstimacion();
			aux.cambiarValorEstimacion(user);
		}
		else 
			user.limpiarListaEstimacion();
		aux.cambiarEstadoEstimacion(user);
	}
	
	public int getEstado(){
		return user.getEstadoEstimacion();
	}
	
	public GameObject FindChild (string pName,GameObject padre)
	{
		Transform pTransform = padre.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}
	
	public override void destruir(){
		base.destruir();
		GameObject.Destroy(botonHabilitar);
	}
	
	public float getEstimacion(){
		return user.getValorEstimacion();
	}
		
}
