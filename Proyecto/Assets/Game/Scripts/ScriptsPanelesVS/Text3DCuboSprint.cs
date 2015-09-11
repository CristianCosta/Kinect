using UnityEngine;
using System.Collections;

public class Text3DCuboSprint : Text3DCubo {
	private Sprint s;


	public void setus(Sprint s)
	{
		this.s = s;
	}
	private int n;

	public int getId()
	{
		return n;
	}

	private bool selected=false;
	public void setSelected ()
	{
		selected = true;
	}
	public Text3DCuboSprint(string t, int i): base(t){
		
		n = i;
		
	}
	
	public override void dibujarObjetoEnQuad(GameObject padre,int fontsize)
	{
		 plano=GameObject.CreatePrimitive(PrimitiveType.Plane);
		 plano.name = "button";
		 plano.transform.parent = padre.transform;
		 plano.transform.localRotation = new Quaternion(0,0,0,0);
		 plano.transform.localPosition = new Vector3(0,0.1f,0);
		 plano.transform.localScale = Vector3.one;
		 plano.layer = 12;
		if (! selected)
						plano.GetComponent<Renderer>().material.color = Color.grey;
				else 
						plano.GetComponent<Renderer>().material.color = Color.white;
		 textoGameObject = new GameObject("Texto");
		 TextMesh textoMesh = (TextMesh)textoGameObject.AddComponent<TextMesh>();
		 
		// textoGameObject.AddComponent<MeshRenderer> ();
		 textoGameObject.transform.parent = plano.transform;
		 textoGameObject.transform.localPosition = new Vector3(0f, 0.05f, 0f);
		 textoGameObject.transform.localScale = new Vector3(0.3f,1,1);
		 textoGameObject.transform.localRotation = plano.transform.localRotation;
		 textoGameObject.transform.Rotate(new Vector3(90,180,0));
		 textoGameObject.GetComponent<Renderer>().material=this.material;
		
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = fontsize;
		 textoMesh.text = titulo;
		 textoMesh.font = this.font;
		
		plano.AddComponent<SelectSprint>();
		SelectSprint script = (SelectSprint)plano.GetComponent("SelectSprint");
		script.setIndex(n);
		script.setSprint (s);

		plano.AddComponent<GUI_CerrarSprint>();
		GUI_CerrarSprint detalle = (GUI_CerrarSprint)plano.GetComponent("GUI_CerrarSprint");
		detalle.setSprint(this.s);
	}

}
