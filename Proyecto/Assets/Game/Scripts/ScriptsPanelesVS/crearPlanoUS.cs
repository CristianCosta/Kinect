using UnityEngine;
using System.Collections;

public class crearPlanoUS : MonoBehaviour {

	// Use this for initialization
	
	
	GameObject posts;
	
	
	public Material materialTexto;
	public Font fontTexto;
	private static int tope = 20;
	
	
	
	public void inicializar(ArrayList userStory){
		//Text3DCubo text =(Text3DCubo)GetComponent("BL1.1");
		//text.setTexto("");
		try{
			//GameObject.Find("BL2.4").GetComponentInChildren().transform.parent = GameObject.Find("BL2.3").transform;
			for(int i = 1; i <= 5; i++)
			{
				Destroy (GameObject.Find("postBL1."+i));
				Destroy (GameObject.Find("postBL2."+i));
				Destroy (GameObject.Find("postBL3."+i));
				Destroy (GameObject.Find("postBL4."+i));
				Destroy (GameObject.Find("postBL5."+i));
			}
		
		}
		catch{
			Debug.Log("No esta");
		}
			/*  for(int i = 1; i <= 5; i++)
		{
				Destroy(GameObject.Find("BL1."+i).GetComponent("post"));
		}*/
		
		materialTexto.color = Color.black;
		
		for(int i = 1; i <= 5; i++)
			{
				UserStory u = (UserStory)userStory[i-1];
				//ID 
				 Text3DPlane cuboUs1 = new Text3DPlaneUS(u.getId_UserStory().ToString(),GameObject.Find("BL1."+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"BL1."+i,u);
				 cuboUs1.setMaterial(materialTexto);
				 cuboUs1.setFont(fontTexto);
				
				//Descripcion
				
				string aux = u.getTitulo();
				int cont = 0;
				string aux2="";
				while (cont < aux.Length && cont < 80){
					if ((aux.Length - cont) > tope){
						aux2 = aux2 + aux.Substring(cont,tope)+"\n";
					}else{
						aux2 = aux2 + aux.Substring(cont,aux.Length - cont)+"\n";					
					}
					cont = cont +tope;
					
				}
				
				 Text3DPlane cuboUs2 = new Text3DPlaneUS(aux2,GameObject.Find("BL2."+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"BL2."+i,u);
				 cuboUs2.setMaterial(materialTexto);
				 cuboUs2.setFont(fontTexto);
				 //Estimacion
				 Text3DPlane cuboUs5 = new Text3DPlaneUS(u.getValorEstimacion().ToString(),GameObject.Find("BL5."+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"BL5."+i,u);
				 cuboUs5.setMaterial(materialTexto);
				 cuboUs5.setFont(fontTexto);
			
				//Prioridad
				 Text3DPlane cuboUs3 = new Text3DPlaneUS(u.getPrioridad().ToString(),GameObject.Find("BL3."+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"BL3."+i,u);
				 cuboUs3.setMaterial(materialTexto);
				 cuboUs3.setFont(fontTexto);
				//Sprint
				 Text3DPlane cuboUs4 = new Text3DPlaneUS("0",GameObject.Find("BL4."+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"BL4."+i,u);
				 cuboUs4.setMaterial(materialTexto);
				 cuboUs4.setFont(fontTexto);					
			
		}
	 		
	}
	
	void Start () {
		
		 
		
		/*
		 cubo=GameObject.CreatePrimitive(PrimitiveType.Cube);
		 cubo.transform.position=new Vector3(70, 27, 115);
		cubo.transform.localScale=new Vector3(10,10,0);
		Rigidbody r = (Rigidbody) cubo.AddComponent<Rigidbody>();
		r.freezeRotation=true;
		
		cubo.AddComponent<DragRigidbody>();
		 textoGameObject = new GameObject("Texto3D");
		 
		 
		 TextMesh texto = (TextMesh)textoGameObject.AddComponent<TextMesh> ();
		 MeshRenderer meshRenderer = (MeshRenderer)textoGameObject.AddComponent<MeshRenderer> ();
		  
	    	meshRenderer.material = this.materialTexto;
		 texto.transform.parent=cubo.transform;
		
		
		textoGameObject.transform.localPosition=new Vector3(0f, 0f, 0f);
		textoGameObject.transform.localScale=new Vector3(0.1f,0.1f,0.1f);
		texto.anchor = TextAnchor.MiddleCenter;
		
		texto.font = this.fontTexto;
		texto.fontSize = 40;
		
		 texto.text="probando";
		
		// texto.text="Probando";
		/* 
		Font myFont=(Resources.Load("Arial") as Font);
		 texto.font=myFont;
		 texto.fontSize=25;
		 texto.transform.localScale=new Vector3(10,10,0);
		 texto.transform.position=cubo.transform.position;
		 texto.transform.parent=cubo.transform;
	     texto.gameObject.active=true;	
	
		 
         */
 
		
		
		 		
		
		 
 
			
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
