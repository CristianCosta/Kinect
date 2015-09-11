using UnityEngine;
using System.Collections;

public class Text3DCubo  {


	
	protected string titulo;
	protected GameObject textoGameObject;
	protected GameObject plano;
	protected Material material ;
	protected Font font;
	
	protected int tamFont;
	private float posx,posy,posz;
	private float localScalex,localScaley,localScalez;
	
	private Vector3 posicion,escala;
	
	public GameObject getPlano()
	{
		return plano;
	}
	public Text3DCubo(string t){
		
	  	titulo=t;
	}
	
	public virtual void dibujarObjetoEnQuad(GameObject padre,int fontsize)
	{
		 plano=GameObject.CreatePrimitive(PrimitiveType.Plane);
		 plano.name = "post";
		 plano.transform.parent = padre.transform;
		 plano.transform.localRotation = new Quaternion(0,0,0,0);
		 plano.transform.localPosition = new Vector3(0,0.1f,0);
		 plano.transform.localScale = Vector3.one;
		 plano.layer = 12;
		 textoGameObject = new GameObject("Texto");
		 TextMesh textoMesh = (TextMesh)textoGameObject.AddComponent<TextMesh>();
		 textoGameObject.transform.parent = plano.transform;
		 textoGameObject.AddComponent<MeshRenderer> ();
		 textoGameObject.transform.localPosition = new Vector3(0f, 0.02f, 0f);
		 textoGameObject.transform.localScale = new Vector3(1,1,1);
		 textoGameObject.transform.localRotation = plano.transform.localRotation;
		 textoGameObject.transform.Rotate(new Vector3(90,180,0));
		 textoGameObject.GetComponent<Renderer>().material=this.material;
		
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = fontsize;
		 textoMesh.fontStyle = FontStyle.Bold;
		 textoMesh.text = textoArmado(titulo);
		 textoMesh.font = this.font;
	}
	
	
	
	public void setMaterial(Material mat){
		this.material=mat;
	//	textoGameObject.renderer.material=mat;
	}
	
	public void setFont(Font font){
		this.font=font;
//	((TextMesh)textoGameObject.GetComponent("TextMesh")).font=font;
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
	
	public string getTitulo(){
		return titulo;
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
	
	
	public string textoArmado(string texto)
       {
               
               ArrayList lineas=new ArrayList();
               GUIStyle style = new GUIStyle ();
               
               //No se usa!!!  //int tamano = (int)style.CalcSize (new GUIContent (texto)).x;
               
               string[] arregloEnter = texto.Split ('\n');
               
               int tamanoAcumulado = 0;
               
               string resultado = "";
               int numeroLinea = 0;
               
               int maxLargoLinea=80;
               
               
               for (int h=0; h<arregloEnter.Length; h++) {
                       string[] arreglo = arregloEnter [h].ToString ().Split (' ');
                       if (arreglo.Length == 0)
                               Debug.Log ("es 0");
                       else {
                               tamanoAcumulado = 0;
                               resultado = "";
                               numeroLinea = 0;
                               for (int i = 0; i < arreglo.Length; i++) {
                                       tamanoAcumulado += (int)style.CalcSize (new GUIContent (arreglo [i])).x;
                                       if (tamanoAcumulado > maxLargoLinea) {
                                               lineas.Add (resultado);
                                               numeroLinea = i - 1;
                                               resultado = "";
                                               resultado = arreglo [i] + " ";
                                               tamanoAcumulado = (int)style.CalcSize (new GUIContent (arreglo [i])).x;
                                       } else {
                                               resultado += arreglo [i] + " ";
                                               tamanoAcumulado += 5;
                                       }
                               }
                               if (numeroLinea < arreglo.Length) {
                                       lineas.Add (resultado);
                               }
                       }
               }
               
               string r="";
               foreach(string pal in lineas)
                       r=r+pal+"\n";
               
               return r;       
       }
	
	
	
	public void setParent(GameObject p){
		this.plano.transform.parent=p.transform;
	}
		
	public void noDibujar(){
		Object.Destroy(plano);
	}
	
	public virtual void destruir(){
		GameObject.Destroy(plano);
	}
}