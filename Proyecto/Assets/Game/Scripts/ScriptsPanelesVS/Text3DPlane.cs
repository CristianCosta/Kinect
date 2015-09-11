using UnityEngine;
using System.Collections;

public class Text3DPlane  {

	private string texto;
	GameObject textoGameObject;
	protected GameObject plano;
	
	private int tamFont;
	private float posx,posy,posz;
	private float localScalex,localScaley,localScalez;
	
	private Vector3 posicion,escala;
	
	
	public Text3DPlane(string texto3d,GameObject padre,Vector3 posicion,Vector3 escala,int fontsize, string name)
	{
		 
		 plano=GameObject.CreatePrimitive(PrimitiveType.Plane);
		 plano.name = "post"+name;
		 plano.transform.parent = padre.transform;
		 plano.transform.localRotation = new Quaternion(0,0,0,0);
		 plano.transform.localPosition = posicion;
		 plano.transform.localScale = escala;
		 plano.layer = 9;
		 //Rigidbody r = (Rigidbody) cubo.AddComponent<Rigidbody>();
		 //r.freezeRotation=true;
         //r.useGravity=false;		
		 //cubo.AddComponent<DragRigidbody>();
		
		 textoGameObject = new GameObject("Texto");
		 TextMesh textoMesh = (TextMesh)textoGameObject.AddComponent<TextMesh>();
		 textoGameObject.transform.parent = plano.transform;
//		 MeshRenderer meshRenderer = (MeshRenderer)textoGameObject.AddComponent<MeshRenderer> ();
		 textoGameObject.transform.localPosition = new Vector3(0f, 0.1f, 0f);
		 textoGameObject.transform.localScale = new Vector3(1,1,1);
		 textoGameObject.transform.localRotation = plano.transform.localRotation;
		 textoGameObject.transform.Rotate(new Vector3(90,180,0));
		 
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = fontsize;
		 textoMesh.text = texto3d;
		 textoMesh.GetComponent<Renderer>().material.color = Color.black;
		 
		 
		
	}
	
	
	public void setMaterial(Material mat){
		textoGameObject.GetComponent<Renderer>().material=mat;
	}
	
	public void setFont(Font font){
	((TextMesh)textoGameObject.GetComponent("TextMesh")).font=font;
	}
	
	public void setTexto(string tex){
		((TextMesh)textoGameObject.GetComponent("TextMesh")).text=tex;
	}

	public void setPosX(float posx){
		plano.transform.localPosition = new Vector3(posx,plano.transform.position.y,plano.transform.position.z);
	}
	
	public void setPosY(float posy){
		plano.transform.localPosition = new Vector3(plano.transform.position.x,posy,plano.transform.position.z);
	}
	
	public void setPosZ(float posz){
		plano.transform.localPosition = new Vector3(plano.transform.position.x,plano.transform.position.y,posz);
	}

	public void setlocalScalex(float scalex){
		plano.transform.localScale = new Vector3(scalex,plano.transform.localScale.y,plano.transform.localScale.z);
	}
	
	public void setlocalScaley(float scaley){
		plano.transform.localScale = new Vector3(plano.transform.localScale.x,scaley,plano.transform.localScale.z);
	}
	
	public void setlocalScalez(float scalez){
		plano.transform.localScale = new Vector3(plano.transform.localScale.x,plano.transform.localScale.y,scalez);
	}
	
	
	/*public void setPosX(float posx){
		plano.transform.position =new Vector3(posx, cubo.transform.position.y, cubo.transform.position.z);
	}

	public void setPosY(float posy){
		cubo.transform.position=new Vector3(cubo.transform.position.x, posy, cubo.transform.position.z);
	}
	
	public void setPoZ(float posz){
		cubo.transform.position=new Vector3(cubo.transform.position.x,cubo.transform.position.y,posz);
	}
	
	public void setlocalScalex(float scalex){
		cubo.transform.localScale=new Vector3(scalex,cubo.transform.localScale.y,cubo.transform.localScale.z);
	}
	
	public void setlocalScaley(float scaley){
		cubo.transform.localScale=new Vector3(cubo.transform.localScale.x,scaley,cubo.transform.localScale.z);
	}
	
	public void setLocalScalez(float scalez){
		cubo.transform.localScale=new Vector3(cubo.transform.localScale.x,cubo.transform.localScale.y,scalez);
	}*/
	
	public int getTamFont(int tam){
		return ((TextMesh)textoGameObject.GetComponent("TextMesh")).fontSize;
	}
	
	public string getTexto(){
		return ((TextMesh)textoGameObject.GetComponent("TextMesh")).text;
	}
	
	
	
	
	public float getPosx(){
		return (float)plano.transform.position.x;
	}
	
	public float getPosy(){
		return (float)plano.transform.position.y;
	}
	public float getPosz(){
		return (float)plano.transform.position.z;
	}
	
	public float getLocalScaleX(){
		return (float)plano.transform.localScale.x;
	}
	
	public float getLocalScaleY(){
		return (float)plano.transform.localScale.y;
	}
	
	public float getLocalScaleZ(){
		return plano.transform.localScale.z;
	}
	
	
	public void setParent(GameObject p){
		this.plano.transform.parent=p.transform;
	}
			
	}


