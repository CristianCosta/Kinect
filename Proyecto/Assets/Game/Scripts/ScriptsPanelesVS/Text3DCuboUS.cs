using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;
using UnityEngine;

public class Text3DCuboUS : Text3DCubo {

	protected UserStory user;
	
	public Text3DCuboUS(UserStory u):base (u.getTitulo())
	{
		
		this.user=u;
	
	}
	
	public override void dibujarObjetoEnQuad(GameObject padre,int fontsize){
		 plano=GameObject.CreatePrimitive(PrimitiveType.Plane);
		 plano.name = "post";
		 plano.transform.parent = padre.transform;
		 plano.transform.localRotation = new Quaternion(0,0,0,0);
		 plano.transform.localPosition = new Vector3(0,0.1f,0);
		 plano.transform.localScale = Vector3.one;
		 plano.layer = 12;
		 textoGameObject = new GameObject("Texto");
		 TextMesh textoMesh = (TextMesh)textoGameObject.AddComponent<TextMesh>();
		 
		 textoGameObject.AddComponent<MeshRenderer> ();
		 textoGameObject.transform.parent = plano.transform;
		 textoGameObject.transform.localPosition = new Vector3(0f, 0.05f, 0f);
		 textoGameObject.transform.localScale = new Vector3(1,1,1);
		 textoGameObject.transform.localRotation = plano.transform.localRotation;
		 textoGameObject.transform.Rotate(new Vector3(90,180,0));
		 textoGameObject.GetComponent<Renderer>().material=this.material;
		
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = fontsize;
		 textoMesh.text = textoArmado(titulo);
		 textoMesh.font = this.font;
		textoMesh.GetComponent<Renderer>().material.color = Color.black;
		plano.GetComponent<Renderer>().material = (Material)Resources.Load("UserStory");

		plano.AddComponent<usBehaviour>();
		usBehaviour script = (usBehaviour)plano.GetComponent("usBehaviour");
		script.setCuboUS(this);
		plano.AddComponent<GUI_CerrarUS>();
		GUI_CerrarUS detalle = (GUI_CerrarUS)plano.GetComponent("GUI_CerrarUS");
		detalle.setUS(this.user);
	}
	
}
