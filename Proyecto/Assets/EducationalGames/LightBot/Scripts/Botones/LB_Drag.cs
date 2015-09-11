using UnityEngine;
using System.Collections;

//simple demo of drag and drop with "ghost" copy in unityscript
//written by Will Thomas, nov. 2011, 
//no rights reserved, do whatever the hell you want with it

public class LB_Drag : MonoBehaviour {
	
	//reference to the ghost button object
	public GameObject ghostButtonObj;
	
	//position of our drag source button in screen space
	private Vector3 screenPos;
	
	//offset from where they begin the drag to the center of the button/object
	private Vector3 offset;
	
	//holds current position during drag
	private Vector3 currentPos;
	
	//some cached variables
	//my GUITexture
	private GUITexture texture;
	//the ghost button's GUITexture component
	private GUITexture ghostButtonTex;
	
	//Boton que esta siendo arrastrado
	private GUILayer layer;
	
	
	// Use this for initialization
	void Start () {
		//cache some things..
	    //our (the drag source's) GUITexture
	    texture=GetComponent<GUITexture>();
	    //the GameObject and GUITexture of the GhostButton object
	    //ghostButtonObj=GameObject.Find("GhostButton");
	    ghostButtonTex=ghostButtonObj.GetComponent<GUITexture>();
	    //and calculate the screen position of our guitexture
	    screenPos=Camera.main.ViewportToScreenPoint(transform.position);
		
		layer=Camera.main.GetComponent<GUILayer>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown() 
	{
	    //enable the ghost's texture
	    ghostButtonTex.enabled=true;
	
	    //calc the offset of the drag point to the object
	    //screen position (precalc'd above) minus mouse pos
	    offset = Input.mousePosition - screenPos;
	}
	
	void OnMouseDrag() 
	{ 
	    //adjust mouse pos by offset...
	    currentPos = Input.mousePosition-offset;
	    //and move the ghost object to that position translated into viewport space
	    ghostButtonObj.transform.position=Camera.main.ScreenToViewportPoint(currentPos);
	}
	
	void OnMouseUp()
	{
		/* 
		 * Si encuentra un cuadro vacio dropea el boton y lo clona.
		 * Si ya habia un boton dropeado ahi, no lo va a detectar porque
		 * los ghost buttons tienen que tener el tag "Ignore Raycast"
		 */
		
		GUIElement hit=layer.HitTest(Input.mousePosition);	
		if (hit && hit.name.StartsWith("Vacio")){
			//Elimina todos los hijos del cuadro vacio, un cuadro vacio
			//deberia tener 1 hijo como maximo (1 accion asignada)
			foreach (Transform child in hit.transform)
				Destroy (child.gameObject);
			
			GameObject clone = Instantiate (ghostButtonObj) as GameObject;
			clone.transform.parent=hit.transform;
			clone.transform.localPosition = new Vector3 (0,0,1);		
		}
		
		
	    //move the ghost back to the original position
	    ghostButtonObj.transform.position=transform.position;
	    //deactivate the ghost so it won't draw until we drag again
	    ghostButtonTex.enabled=false;
	}
}
