using UnityEngine;
using System.Collections;

public class Carta {
	//protected string titulo;
	
	private Estimacion es;
	private GameObject plano;
	protected Material material ;
	protected Font font;
	protected GameObject textoUser;
	protected static int fontsize = 55;
	
	public Carta(Estimacion e){
		es = e;
	}
	
	public void dibujar(GameObject padre,int hide)
	{
		 plano=GameObject.CreatePrimitive(PrimitiveType.Plane);
		 plano.name = "carta";
		 plano.transform.parent = padre.transform;
		 plano.transform.localRotation = new Quaternion(0,0,0,0);
		 plano.transform.localPosition = new Vector3(0,0.03f,0);
		 plano.transform.localScale = Vector3.one;
		 plano.layer = 12;
		 if (hide == 1)
		 	plano.GetComponent<Renderer>().material = (Material)Resources.Load("hide-card");
		 else{
			plano.AddComponent<GUI_DetalleEstimacion>();
			GUI_DetalleEstimacion detalle = (GUI_DetalleEstimacion)plano.GetComponent("GUI_DetalleEstimacion");
			detalle.setEstimacion(this.es);
			double valor =es.getValorEstimacion();
			if (valor == 0)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("0-card");
			if (valor == 1)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("1-card");
			if (valor == 2)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("2-card");
			if (valor == 3)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("3-card");
			if (valor == 5)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("5-card");
			if (valor == 8)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("8-card");
			if (valor == 13)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("13-card");
			if (valor == 20)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("20-card");
			if (valor == 40)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("40-card");
			if (valor == 100)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("100-card");
			if (valor == 1000)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("i-card");
			if (valor == 0.5f)
				plano.GetComponent<Renderer>().material = (Material)Resources.Load("05-card");
		}
		 textoUser = new GameObject("texto");
		 TextMesh textoMesh = (TextMesh)textoUser.AddComponent<TextMesh>();
		 textoUser.transform.parent = FindChild(padre,"member").transform;
		 MeshRenderer meshRenderer = (MeshRenderer)textoUser.AddComponent<MeshRenderer> ();
		 textoUser.transform.localPosition = new Vector3(0f, 0.02f, 0f);
		 textoUser.transform.localScale = new Vector3(0.35f,1,1);
		 textoUser.transform.localRotation = FindChild(padre,"member").transform.localRotation;
		 textoUser.transform.Rotate(new Vector3(90,180,0));
		 textoUser.GetComponent<Renderer>().material=this.material;
		
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = fontsize;
		 textoMesh.fontStyle = FontStyle.Bold;
		 textoMesh.text = es.getUser();
		 textoMesh.font = this.font;
		 textoMesh.GetComponent<Renderer>().material.color = Color.black;
	}
	
	public void setParent(GameObject p){
		this.plano.transform.parent=p.transform;
	}
		
	public void noDibujar(){
		Object.Destroy(plano);
	}
	
	public void destruir(){
		GameObject.Destroy(plano);
		GameObject.Destroy(textoUser);
	}
	
	public void setMaterial(Material mat){
		this.material=mat;
	}
	
	public void setFont(Font font){
		this.font=font;
	}
	
	public GameObject FindChild (GameObject padre,string pName)
	{
		Transform pTransform = padre.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}
	
}
